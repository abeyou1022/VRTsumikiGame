using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public static bool state;
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        state = true;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TextMesh scoreText = gameObject.GetComponent<TextMesh>();
        if (state) scoreText.text = "現在 : " + score;
        else scoreText.text = "Game Over...";
    }
}
