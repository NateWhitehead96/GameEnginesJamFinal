using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] Pickups;

    public Transform Top;
    public Transform Bot;
    public Transform Left;
    public Transform Right;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 3, 4);
    }

    public void Spawn()
    {
        int thingToSpawn = Random.Range(0, 10);

        int x = (int)Random.Range(Left.position.x, Right.position.x);
        int z = (int)Random.Range(Bot.position.z, Top.position.z);

        if(thingToSpawn < 7) // spawn food, high chance to spawn food
        Instantiate(Pickups[0], new Vector3(x, 0.5f, z), Quaternion.identity);

        if(thingToSpawn == 7)
            Instantiate(Pickups[1], new Vector3(x, 0.5f, z), Quaternion.identity);
        if (thingToSpawn == 8)
            Instantiate(Pickups[2], new Vector3(x, 0.5f, z), Quaternion.identity);
        if (thingToSpawn == 9)
            Instantiate(Pickups[3], new Vector3(x, 0.5f, z), Quaternion.identity);
    }
}
