using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cassettes;
    public float highScore;
    public UIManager ui;

    void Awake()
    {
        cassettes = PlayerPrefs.GetInt("cass", 0);
        highScore = PlayerPrefs.GetFloat("highScore", 0);
    }


    public void UpdateValues(int cass, float score)
    {
        if (score > highScore)
        {
            highScore = score;
        }

        cassettes += cass;
        PlayerPrefs.SetInt("cass", cassettes);
        PlayerPrefs.SetFloat("highScore", highScore);
    }

}
