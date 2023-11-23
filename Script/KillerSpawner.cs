using UnityEngine;

public class KillerSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] killerPrefabs;
    [SerializeField] Transform bombParentGO;
    private readonly int _maxAliveBomp = 12;
    private static int _aliveBomb = 0;
    private readonly float _leftMostX = -20f, _rightMostX = 20f;
    private readonly float _bottomMostY = -5.7f, _upMostY = 1.3f;
    private float _spawnInterval = 0f, _maxSpawnInterval = 6f;

    private void Update()
    {
        if (_aliveBomb < _maxAliveBomp)
        {
            _spawnInterval += Time.deltaTime;
            if (_spawnInterval >= _maxSpawnInterval)
            {
                _spawnInterval = 0f;
                _maxSpawnInterval = Random.Range(3f, 6f);
                Spawn(Random.Range(1, 2));
            }
        }
    }

    private void Spawn(int count)
    {
        if (_aliveBomb == _maxAliveBomp) return;
        for (int i = 0; i < count; i++)
        {

            if (_aliveBomb == _maxAliveBomp) break;
            GameObject bomb = Instantiate(killerPrefabs[Random.Range(0, killerPrefabs.Length)], bombParentGO);
            bomb.transform.position = InstantiateRandomPoint();
            _aliveBomb++;
        }
    }

    private Vector3 InstantiateRandomPoint()
    {
        return new Vector3(Random.Range(_leftMostX + .5f, _rightMostX - .5f), Random.Range(_bottomMostY + .5f, _upMostY - 1f));
    }

    public static void RemoveBomb()
    {
        _aliveBomb--;
    }
}
