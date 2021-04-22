using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public Text ScoreText;

    

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Blocks Consumed: " + FindObjectOfType<SnakeBehaviour>().Score.ToString();
    }
}
