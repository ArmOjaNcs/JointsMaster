using System.Collections;
using UnityEngine;

[RequireComponent (typeof(CatapultProjectileSpawner))]
[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(Rigidbody))]
public class Catapult : MonoBehaviour
{
    private const float Half = 0.5f;
    private const float TimeBeforeCanCharge = 0.5f;

    [SerializeField] private PlayerInput _input;
    [SerializeField] private HingeJoint _hingeJoint;  

    private CatapultProjectileSpawner _projectileSpawner;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private WaitForSeconds _waitBeforeCanCharge;
    private bool _isCharged;
    private bool _isLaunched;

    private void Awake()
    {
        _projectileSpawner = GetComponent<CatapultProjectileSpawner>();
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _waitBeforeCanCharge = new WaitForSeconds(TimeBeforeCanCharge);
    }

    private void OnEnable()
    {
        _input.ProjectileLaunched += LaunchProjectile;
        _input.LeverDroppedDown += DropDownLever;
    }

    private void OnDisable()
    {
        _input.ProjectileLaunched -= LaunchProjectile;
        _input.LeverDroppedDown -= DropDownLever;
    }

    private void Update()
    {
        Charge();
    }

    private void Charge()
    {
        if (_rigidbody.linearVelocity == Vector3.zero && _isCharged == false)
        {
            PutProjectileToBowl();
            _isCharged = true;
            _isLaunched = false;
        }
    }

    private void LaunchProjectile() 
    {
        if (_isCharged)
        {
            _hingeJoint.useMotor = true;
            _isLaunched = true;
        }
    }

    private void DropDownLever()
    {
        if (_isLaunched)
        {
            _hingeJoint.useMotor = false;
            _isLaunched = false;
            StartCoroutine(WaitBeforeCanCharge());
        }
    }

    private void PutProjectileToBowl()
    {
        CatapultProjectile catapultProjectile = _projectileSpawner.GetCatapultProjectile();
        catapultProjectile.SetStartPosition(GetStartPosition(catapultProjectile));
        catapultProjectile.Sleep();
    }

    private Vector3 GetStartPosition(CatapultProjectile catapultProjectile)
    {
        return new Vector3(transform.position.x,
            transform.position.y + _boxCollider.bounds.extents.y + catapultProjectile.SphereCollider.bounds.size.y * Half, transform.position.z);
    }

    private IEnumerator WaitBeforeCanCharge()
    {
        yield return _waitBeforeCanCharge;

        _isCharged = false;
    }
}