using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public BtecCarController carController;
    public RoadGeneration roadGenerator;
    public UIManager ui;
    public Animator animator;
    public GameObject master;
    public float swapTime = 1f;
    public GameObject[] cars;
    public GameObject customisation;
    public GameObject MainPage;
    int carNum = 0;
    int pageNum = 0;
    public GameObject[] pages;
    public Text cassTxt;
    public Text highScore;
    public Text buyTxt;
    int score;
    public GameManager manager;
    public Car[] carSO;


    void Awake()
    {
        carController.enabled = false;
        roadGenerator.enabled = false;
        ui.gameObject.SetActive(false);
        cassTxt.text = "Cassettes: " + manager.cassettes;
        highScore.text = "High Score: " + manager.highScore;
        UpdateCurrentCarUI();
    }
    public void UpdateCurrentCarUI()
    {
        Car car = carSO[carNum];
        if (car.unlocked)
        {
            buyTxt.text = "Unlocked";
            buyTxt.fontSize = 64;
        }
        else
        {
            buyTxt.text = "Buy for " + car.cassNeeded + " cassettes";
            buyTxt.fontSize = 52;
        }
    }

    public void Play()
    {
        if (carSO[carNum].unlocked) //if the selected car is unlocked
        {
            animator.SetBool("change", true);
            carController.enabled = true;
            roadGenerator.enabled = true;
            ui.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }


    public void NextCar()
    {
        //could make this foreach just to be last carnum-1. Want the script to work first. Can do this later for optimisation.
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
        carNum++;
        if (carNum > cars.Length - 1)
            carNum = 0;
        cars[carNum].SetActive(true);
        UpdateCurrentCarUI();
    }
    public void LastCar()
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
        carNum--;
        if (carNum < 0)
            carNum = cars.Length - 1;
        cars[carNum].SetActive(true);
        UpdateCurrentCarUI();
    }
    public void buyCar()
    {
        if (manager.cassettes >= carSO[carNum].cassNeeded) //if u have enough cassettes
        {
            carSO[carNum].unlocked = true;
            manager.cassettes -= carSO[carNum].cassNeeded;
            cassTxt.text = "Cassettes: " + manager.cassettes;
            UpdateCurrentCarUI();
        }
    }
}