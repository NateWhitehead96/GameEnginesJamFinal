using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool Mode; // True is with win condition, false will be endless
       
    public void Endless()
    {
        Mode = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void Survival()
    {
        Mode = true;
        SceneManager.LoadScene("SampleScene");
    }
}
