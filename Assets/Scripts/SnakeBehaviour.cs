using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Power
{
    None,
    Electric,
    Fire,
    Water
}

public class SnakeBehaviour : MonoBehaviour
{
    private Vector3 direction = -Vector3.up;
    private List<Transform> tail = new List<Transform>();
    [SerializeField] private bool ate = false;
    private Animator animator;
    public GameObject tailPrefab;
    public Canvas PauseUI;
    public Slider PowerLeft;
    // Power up stuff
    public ParticleSystem[] Powerups;
    [SerializeField]
    private Power power;
    
    public int Score;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PauseUI.gameObject.SetActive(false);
        for (int i = 0; i < Powerups.Length; i++)
        {
            Powerups[i].Stop();
            Powerups[i].gameObject.SetActive(false);
        }
        power = Power.None;
        PowerLeft.maxValue = 10;
        PowerLeft.value = 0;
        InvokeRepeating("Move", 0.1f, 0.1f);
    }
    private void Update()
    {
        if(GameManager.Instance.Mode == true && Score == 50)
        {
            SceneManager.LoadScene("WinScene");
        }
        if(PowerLeft.value > 0)
            PowerLeft.value -= Time.deltaTime;
    }
    public void OnMove(InputValue value)
    {
        Vector2 tempDirection = value.Get<Vector2>();

        if(tempDirection.x > 0) // move right
        {
            //transform.Rotate(new Vector3(-90, 0, 90));
            transform.rotation = Quaternion.Euler(-90, 0, 90);
            //direction = Vector3.right;
        }
        else if(tempDirection.x < 0) // move left
        {
            //transform.Rotate(new Vector3(-90, 0, -90));
            transform.rotation = Quaternion.Euler(-90, 0, -90);
            //direction = -Vector3.right;
        }
        else if(tempDirection.y > 0) // move away from the camera
        {
            //transform.Rotate(new Vector3(-90, 0, 0));
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            //direction = Vector3.up;
        }
        else if(tempDirection.y < 0) // move towards the camera
        {
            //transform.Rotate(new Vector3(-90, 0, 180));
            transform.rotation = Quaternion.Euler(-90, 0, 180);
            //direction = -Vector3.up;
        }
        direction = -Vector3.up;
    }

    public void OnPause(InputValue input)
    {
        if(input.isPressed)
        {
            PauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
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
            Score++;
            ate = false;
            animator.SetBool("Eating", false);
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
            animator.SetBool("Eating", true);
            ate = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            //print("GAMEOVER");
            SceneManager.LoadScene("LoseScene");
        }
        // Colliding with power ups
        if (other.gameObject.CompareTag("Electric") && power == Power.None)
        {
            StartCoroutine(ElectricPower());
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Fire") && power == Power.None)
        {
            StartCoroutine(FirePower());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Water") && power == Power.None)
        {
            StartCoroutine(WaterPower());
            Destroy(other.gameObject);
        }

        // Colliding with spikey
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.GetComponent<SpikeBehaviour>().power == power)
            {
                Destroy(other.gameObject);
            }
            else
            {
                //print("GameOVER");
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

    IEnumerator ElectricPower()
    {
        power = Power.Electric;
        Powerups[0].Play();
        Powerups[0].gameObject.SetActive(true);
        PowerLeft.value = 10;
        yield return new WaitForSeconds(10);
        power = Power.None;
        Powerups[0].Stop();
        Powerups[0].gameObject.SetActive(false);
    }
    IEnumerator FirePower()
    {
        power = Power.Fire;
        Powerups[1].Play();
        Powerups[1].gameObject.SetActive(true);
        PowerLeft.value = 10;
        yield return new WaitForSeconds(10);
        power = Power.None;
        Powerups[1].Stop();
        Powerups[1].gameObject.SetActive(false);
    }

    IEnumerator WaterPower()
    {
        power = Power.Water;
        Powerups[2].Play();
        Powerups[2].gameObject.SetActive(true);
        PowerLeft.value = 10;
        yield return new WaitForSeconds(10);
        power = Power.None;
        Powerups[2].Stop();
        Powerups[2].gameObject.SetActive(false);
    }
}
