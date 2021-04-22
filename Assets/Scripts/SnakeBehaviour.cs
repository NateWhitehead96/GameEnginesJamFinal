using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class SnakeBehaviour : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    private List<Transform> tail = new List<Transform>();
    [SerializeField] private bool ate = false;

    public GameObject tailPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    public void OnMove(InputValue value)
    {
        Vector2 tempDirection = value.Get<Vector2>();

        if(tempDirection.x > 0) // move right
        {
            direction = Vector3.right;
        }
        else if(tempDirection.x < 0) // move left
        {
            direction = -Vector3.right;
        }
        else if(tempDirection.y > 0) // move away from the camera
        {
            direction = Vector3.forward;
        }
        else if(tempDirection.y < 0) // move towards the camera
        {
            direction = -Vector3.forward;
        }
    }

    public void Move()
    {
        Vector3 pos = transform.position;

        transform.Translate(direction);

        if(ate)
        {
            GameObject newTail = Instantiate(tailPrefab, pos, Quaternion.identity);
            tail.Insert(0, newTail.transform);
            ate = false;
        }
        // if we have tail
        if (tail.Count > 0)
        {
            tail.Last().position = pos;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            ate = true;
            Destroy(other.gameObject);
        }
    }
}
