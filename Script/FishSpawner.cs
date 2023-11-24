using UnityEngine;

public class FishSpawner : MonoBehaviour
{


    [SerializeField] GameObject _fishPrefab;
    [SerializeField] GameObject _fishPrefab2;
    [SerializeField] Sprite[] fishSprites;
    [SerializeField] Sprite[] fishSprites2;
    private float _spawnInterval = 0f;
    private float _maxSpawnInterval = 5f;
    private static int _alive;
    private readonly int _maxAliveFish = 40;

    void Awake()
    {
        _alive = 0;
    }

    void Update()
    {
        if (_alive < _maxAliveFish)
        {
            if (_alive < _maxAliveFish * .25f)
            {
                _spawnInterval += 1.5f * Time.deltaTime;
            } else
            {
                _spawnInterval += .75f * Time.deltaTime;
            }
            if (_spawnInterval >= _maxSpawnInterval)
            {
                _maxSpawnInterval = Random.Range(3f, 6f);
                SpawnFish();
                _alive++;
                _spawnInterval = 0;
            }
        }
    }

    private void SpawnFish()
    {
        if (fishSprites.Length != 0 && fishSprites2.Length != 0)
        {
            if (Random.Range(1, 11) < 6)
            {
                GameObject fish = _fishPrefab;
                int randomIndex = Random.Range(0, fishSprites.Length);
                fish.GetComponent<SpriteRenderer>().sprite = fishSprites[randomIndex];
                Instantiate(fish, transform);
            } else
            {
                GameObject fish = _fishPrefab2;
                int randomIndex = Random.Range(0, fishSprites2.Length);
                fish.GetComponent<SpriteRenderer>().sprite = fishSprites2[randomIndex];
                Instantiate(fish, transform);
            }
        }
    }

    public static void RemoveFish()
    {
        _alive--;
    }
}
