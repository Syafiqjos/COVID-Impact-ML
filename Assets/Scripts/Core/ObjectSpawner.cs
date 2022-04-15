using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToBeSpawned;

    public void Spawn() {
        if (objectToBeSpawned)
        {
            GameObject spawnedObject = Instantiate(objectToBeSpawned, transform.position, Quaternion.identity);
        }
    }
}
