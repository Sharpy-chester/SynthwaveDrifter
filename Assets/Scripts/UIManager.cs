using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text distance;
    public Text gameOver;
    public Button startAgain;
    public Transform car;
    public BtecCarController carController;
    float carSpeed;
    float dist;
    float time;

    void Awake()
    {
        carSpeed = carController.speed;
    }

    void Update()
    {
        if (carController.isAlive)
        {
            time += Time.deltaTime;
            dist = carSpeed * time;
            distance.text = "Distance: " + Mathf.Round(dist);
        }

    }

    public void GameOver()
    {
        startAgain.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        time = 0;
        SceneManager.LoadScene("SampleScene");

    }
}
