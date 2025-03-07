using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float obstacleSpawnTime = 1f;

    int obstaclesSpawned = 0;

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

    IEnumerator SpawnObstacleRoutine()
    {

        while (obstaclesSpawned < 5)
        {
            yield return new WaitForSeconds(obstacleSpawnTime);

            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            obstaclesSpawned++;
        }


        
    }
}
