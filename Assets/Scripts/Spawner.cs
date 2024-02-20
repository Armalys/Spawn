using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxSize;

    [SerializeField] private float _repitRate;
    [SerializeField] private float _spawnDelay = 1f;

    [SerializeField] private GameObject[] _spawnPoints = new GameObject[3];

    private ObjectPool<Enemy> _poolOfEnemy;

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), _spawnDelay, _repitRate);
    }

    private void Awake()
    {
        _poolOfEnemy = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: SetUpEnemy,
            actionOnRelease: enemy => enemy.gameObject.SetActive(true),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    private void SetUpEnemy(Enemy enemy)
    {
        Transform randomPoint = ChoseRandomPoint();
        
        enemy.SetPosition(randomPoint.position);
        enemy.SetRotation(randomPoint.rotation);
    }

    private void GetEnemy()
    {
        Enemy enemy = _poolOfEnemy.Get();
        StartCoroutine(enemy.MoveForward());
    }

    private Transform ChoseRandomPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform;
    }
}