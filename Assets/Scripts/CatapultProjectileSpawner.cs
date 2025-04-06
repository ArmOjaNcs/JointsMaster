using UnityEngine;

public class CatapultProjectileSpawner : MonoBehaviour
{
    [SerializeField] private CatapultProjectile _prefab;
    [SerializeField] private int _capacity;

    private ObjectPool<CatapultProjectile> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<CatapultProjectile>(_prefab, _capacity, transform);
    }

    public CatapultProjectile GetCatapultProjectile()
    {
        CatapultProjectile projectile = _pool.GetElement();
        Initialize(projectile);
        return projectile;
    }

    private void Initialize(CatapultProjectile catapultProjectile)
    {
        catapultProjectile.LifeTimeFinished += OnLifeTimeFinished;
        catapultProjectile.gameObject.SetActive(true);
    }

    private void OnLifeTimeFinished(CatapultProjectile catapultProjectile)
    {
        catapultProjectile.LifeTimeFinished -= OnLifeTimeFinished;
        catapultProjectile.gameObject.SetActive(false);
    }
}