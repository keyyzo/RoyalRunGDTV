using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]

    [SerializeField] CameraController cameraController;

    [Space(10)]

    [Header("Chunk Variables")]

    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;

    [Space(10)]

    [Header("Level Movement Variables")]

    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float minMoveSpeed = 2.0f;



    List<GameObject> chunks = new List<GameObject>();

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
        moveSpeed += speedAmount;

        if (moveSpeed < minMoveSpeed)
        { 
            moveSpeed = minMoveSpeed;
        }

        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);

        cameraController.ChangeCameraFOV(speedAmount);
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

        GameObject newChunk = Instantiate(chunkPrefab, newPosition, Quaternion.identity, chunkParent);

        chunks.Add(newChunk);
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
