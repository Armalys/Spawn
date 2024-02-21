using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxSize;

    [SerializeField] private float _repeatRate;
    [SerializeField] private bool _isSpawnActive = true;

    [SerializeField] private Transform[] _spawnPoints = new Transform[3];

    private ObjectPool<Enemy> _poolOfEnemy;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
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
    
    private IEnumerator SpawnEnemies()
    {
        while (_isSpawnActive)
        {
            GetEnemy();
            yield return new WaitForSeconds(_repeatRate);
        }
    }

    private void SetUpPosition(Enemy enemy)
    {
        enemy.SetPosition(ChoseRandomPoint());
    }

    private void GetEnemy()
    {
        Enemy enemy = _poolOfEnemy.Get();

        enemy.StartMoving(ChooseRandomDirection());
    }

    private Vector3 ChoseRandomPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }

    private Vector3 ChooseRandomDirection()
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        return directions[Random.Range(0, directions.Length)];
    }
}