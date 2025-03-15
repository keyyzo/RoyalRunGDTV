using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]

    [SerializeField] CameraController cameraController;
    [SerializeField] ScoreManager scoreManager;

    [Space(10)]

    [Header("Chunk Variables")]

    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkpointChunkPrefab;

    [Tooltip("The amount of chunks we start with")]
    [SerializeField] int startingChunksAmount = 12; 

    [SerializeField] Transform chunkParent;

    [Tooltip("Do not change chunk length value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f;

    [SerializeField] int checkpointChunkSpawnInterval = 8;

    [Space(10)]

    [Header("Level Movement Variables")]

    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float minMoveSpeed = 2.0f;
    [SerializeField] float maxMoveSpeed = 20.0f;

    [Space(10)]

    [Header("Gravity Variables")]

    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;



    List<GameObject> chunks = new List<GameObject>();
    int chunksSpawned = 0;

    private void Start()
    {
        SpawnStartingChunks();

    }

    private void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    { 
        float newMoveSpeed = moveSpeed + speedAmount;

        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        

        if (newMoveSpeed != moveSpeed)
        { 
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);

            cameraController.ChangeCameraFOV(speedAmount);
        }


        
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float newSpawnZPosition = GenerateSpawnZPosition();



        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newSpawnZPosition);

        GameObject chunkToSpawn = ChooseChunkToSpawn();

        GameObject newChunkGO = Instantiate(chunkToSpawn, newPosition, Quaternion.identity, chunkParent);

        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);

        chunksSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;

        if (chunksSpawned % checkpointChunkSpawnInterval == 0 && chunksSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }

        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
    }

    float GenerateSpawnZPosition()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        { 
            spawnPositionZ = transform.position.z;
        }

        else
        {
            
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
