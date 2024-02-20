using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxSize;

    [SerializeField] private float _repeatRate;
    [SerializeField] private float _spawnDelay = 1f;

    [SerializeField] private GameObject[] _spawnPoints = new GameObject[3];
    [SerializeField] private GameObject[] _targetPoints = new GameObject[3];

    private ObjectPool<Enemy> _poolOfEnemy;

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), _spawnDelay, _repeatRate);
    }

    private void Awake()
    {
        _poolOfEnemy = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: SetUpPosition,
            actionOnRelease: enemy => enemy.gameObject.SetActive(true),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    private void SetUpPosition(Enemy enemy)
    {
        enemy.SetPosition(ChoseRandomPoint(_spawnPoints));
    }

    private void GetEnemy()
    {
        Enemy enemy = _poolOfEnemy.Get();

        enemy.StartMoving(ChoseRandomPoint(_targetPoints));
    }

    private Vector3 ChoseRandomPoint(GameObject[] points)
    {
        return points[Random.Range(0, points.Length)].transform.position;
    }
}