using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Chunk Variables")]

    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8.0f;


    GameObject[] chunks = new GameObject[12];

    private void Start()
    {
        SpawnChunks();

    }

    private void Update()
    {
        MoveChunks();
    }

    void SpawnChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            float newSpawnZPosition = GenerateSpawnZPosition(i);

            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newSpawnZPosition);

            GameObject newChunk =  Instantiate(chunkPrefab, newPosition, Quaternion.identity, chunkParent);

            chunks[i] = newChunk;
        }
    }

    float GenerateSpawnZPosition(int i)
    {
        float spawnPositionZ;

        if (i == 0)
        { 
            spawnPositionZ = transform.position.z;
        }

        else
        {
            spawnPositionZ = transform.position.z + (i * chunkLength);
        }

        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i].transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
        }
    }
}
