using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxSize;

    [SerializeField] private float _repeatRate;
    [SerializeField] private bool _isSpawnActive = true;

    [SerializeField] private Transform[] _targetPoints = new Transform[3];

    private ObjectPool<Enemy> _poolOfEnemy;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Awake()
    {
        _poolOfEnemy = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemy),
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
        enemy.SetPosition(transform.position);
    }

    private void GetEnemy()
    {
        Enemy enemy = _poolOfEnemy.Get();

        Mover mover = enemy.GetComponent<Mover>();
        mover.StartMoving(ChoseRandomTargetPoint());

        Destroyer destroyer = enemy.GetComponent<Destroyer>();
        destroyer.StartDestroying();
    }

    private Vector3 ChoseRandomTargetPoint()
    {
        return _targetPoints[Random.Range(0, _targetPoints.Length)].position;
    }
}