using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] Text scoreTxt;
    [SerializeField] Text gameOver;
    [SerializeField] Button startAgain;
    [SerializeField] BtecCarController carController;
    float dist;
    float time;
    [SerializeField] float score;
    [SerializeField] int cassettes;
    float cassModifier;
    public float CassModifier => cassModifier;


    void Update()
    {
        if (carController.IsAlive)
        {
            time += Time.deltaTime;
            dist = carController.Speed * time;
            score = dist + (cassettes * cassModifier);
            scoreTxt.text = Mathf.Round(score).ToString();
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
        cassettes = cass;
    }
}
