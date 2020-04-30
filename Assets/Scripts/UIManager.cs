using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text distance;
    public Text gameOver;
    public Text cassettesTxt;
    public Button startAgain;
    public Transform car;
    public BtecCarController carController;
    float carSpeed;
    float dist;
    float time;


    void Awake()
    {
        carSpeed = carController.speed;
        cassettesTxt.text = "Cassettes: 0";
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

    public void UpdateCassettes(int cass)
    {
        //could have just added one every time this is called, but might want a cassette thats worth more than one. Also could have asked for an int to add as an input but theres little performance difference and this way is more reliable
        cassettesTxt.text = "Cassettes: " + cass;
    }
}
