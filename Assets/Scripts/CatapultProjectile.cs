using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class CatapultProjectile : MonoBehaviour
{
    [SerializeField] private float _lifeDuration;

    private WaitForSeconds _lifeTime;
    private Coroutine _coroutine;
    private Rigidbody _rigidbody;

    public event Action<CatapultProjectile> LifeTimeFinished;

    public SphereCollider SphereCollider { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SphereCollider = GetComponent<SphereCollider>();
        _lifeTime = new WaitForSeconds(_lifeDuration);
    }

    private void OnEnable() => _coroutine = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _coroutine == null && isActiveAndEnabled)
            _coroutine = StartCoroutine(LifeTimeRoutine());
    }

    public void SetStartPosition(Vector3 position) => transform.position = position;

    public void Sleep() => _rigidbody.Sleep();

    private IEnumerator LifeTimeRoutine()
    {
        yield return _lifeTime;

        LifeTimeFinished?.Invoke(this);
    }
}