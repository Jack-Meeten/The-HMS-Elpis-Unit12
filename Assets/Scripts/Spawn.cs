using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SpawnDropped()
    {
        Vector3 playerPos = new Vector3(player.position.x + 3, player.position.y, player.position.z);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
