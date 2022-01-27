using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform SpawnPoint;

    void Awake()
    {
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    }
    public void SpawnDropped()
    {
        Vector3 playerPos = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
