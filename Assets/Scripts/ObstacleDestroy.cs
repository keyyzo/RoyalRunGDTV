using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " is Destroyed");
        Destroy(other.gameObject);
    }
}
