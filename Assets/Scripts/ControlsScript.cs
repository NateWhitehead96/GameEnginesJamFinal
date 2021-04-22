using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
{
    public void Endless()
    {
        GameManager.Instance.Mode = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void Survival()
    {
        GameManager.Instance.Mode = true;
        SceneManager.LoadScene("SampleScene");
    }
}
