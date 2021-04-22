using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeySpawner : MonoBehaviour
{
    public GameObject[] Spikeys;

    public Transform Top;
    public Transform Bot;
    public Transform Left;
    public Transform Right;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 10, 10);
    }


    public void Spawn()
    {
        int thingToSpawn = Random.Range(0, Spikeys.Length);
        int x = (int)Random.Range(Left.position.x, Right.position.x);
        int z = (int)Random.Range(Bot.position.z, Top.position.z);
        Instantiate(Spikeys[thingToSpawn], new Vector3(x, 0.5f, z), Quaternion.identity);
    }
}
