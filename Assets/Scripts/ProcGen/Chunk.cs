using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Prefab Variables")]

    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applesPrefab;
    [SerializeField] GameObject coinsPrefab;

    [Space(10)]

    [Header("Spawn Chance Variables")]

    [SerializeField] float appleSpawnChance = 0.33f;
    [SerializeField] float coinSpawnChance = 0.5f;

    [Space(10)]

    [Header("Lane Variables")]

    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f};
    [SerializeField] float[] coinSpawnPositions = { -4f, -2f, 0f, 2f, 4f };
    [SerializeField] float coinSeperationLength = 2f;

    List<int> availableLanes = new List<int> { 0,1,2 };
    List<int> availableCoinPositions = new List<int> { 0, 1, 2, 3, 4 };

    LevelGenerator levelGenerator;
    ScoreManager scoreManager;

    private void Start()
    {
        SpawnFence();
        SpawnApple();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    { 
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    void SpawnFence()
    {

        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {

            if (availableLanes.Count <= 0)
                break;

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);

            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }


    }


    void SpawnApple()
    {
        if (Random.value > appleSpawnChance)
            return;

        if (availableLanes.Count <= 0)
            return;

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);

        Apple newApple = Instantiate(applesPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }


    void SpawnCoins()
    {
        if (Random.value > coinSpawnChance)
            return;

        if (availableLanes.Count <= 0)
            return;

        if(availableCoinPositions.Count <= 0)
            return;

        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            //int selectedPos = SelectCoinPosition();

            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength);

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);

           Coin newCoin = Instantiate(coinsPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
           newCoin.Init(scoreManager);
        }

        
    }

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

    // My own solution to the problem of spawning multiple coins within a lane on the chunk
    // Have stopped using it due to following the course, however keeping the code intact
    // incase of a potential resuse for it
    int SelectCoinPosition()
    {
        int randomPosIndex = Random.Range(0, availableCoinPositions.Count);
        int selectedPos = availableCoinPositions[randomPosIndex];
        availableCoinPositions.RemoveAt(randomPosIndex);
        return selectedPos;
    }

    
}
