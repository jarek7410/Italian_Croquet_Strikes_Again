using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float spawnChance = .7f;
    [SerializeField]
    GameObject[] possiblePickups;
    void Start()
    {
        if (spawnChance < Random.Range(0f, 1f)) {
            Destroy(gameObject);
            return;
        }

        var spawnedPickup = possiblePickups[Random.Range(0, possiblePickups.Length)];
        Instantiate(spawnedPickup, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
