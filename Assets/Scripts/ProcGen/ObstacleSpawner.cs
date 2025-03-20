using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnTime = 1f;
    [SerializeField] float minObstacleSpawnTime = 0.25f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObstacle();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
       
       StartCoroutine(SpawnObstacleRoutine());
        
    }

    public void DecreaseObstacleSpawnTime(float amount)
    { 
        obstacleSpawnTime -= amount;

        if (obstacleSpawnTime <= minObstacleSpawnTime)
        {
            obstacleSpawnTime = minObstacleSpawnTime;
        }
    }

    IEnumerator SpawnObstacleRoutine()
    {

        while (true)
        {
            GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Vector3 spawnPos = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

            yield return new WaitForSeconds(obstacleSpawnTime);

            Instantiate(obstacleToSpawn, spawnPos, Random.rotation, obstacleParent);
            
        }


        
    }
}
