using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scoreText2;
    public static int score;

    void Update()
    {
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
    }
}
