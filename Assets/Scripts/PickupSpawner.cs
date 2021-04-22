using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject food;

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
        int x = (int)Random.Range(Left.position.x, Right.position.x);
        int z = (int)Random.Range(Bot.position.z, Top.position.z);

        Instantiate(food, new Vector3(x, 0, z), Quaternion.identity);
    }
}
