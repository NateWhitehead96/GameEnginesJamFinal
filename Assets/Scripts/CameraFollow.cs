using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float ZOffset;
    public float XOffset;
    public float YOffset;

    private void Start()
    {
        InvokeRepeating("Follow", 0.1f, 0.1f);
    }
    
    public void Follow()
    {
        transform.position = new Vector3(player.position.x + XOffset, player.position.y + YOffset, player.position.z + ZOffset);
    }
}
