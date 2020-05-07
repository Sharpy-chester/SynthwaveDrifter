using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    void Awake()
    {
        carController.enabled = false;
        roadGenerator.enabled = false;
        ui.gameObject.SetActive(false);
    }

    public void Play()
    {
        animator.SetBool("change", true);
        carController.enabled = true;
        roadGenerator.enabled = true;
        ui.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CustomisationBtnClicked()
    {
        ComeIn(customisation);
        yeet(MainPage);
        pageNum = 1;
    }
    public void HomeBtnClicked()
    {
        ComeIn(MainPage);
        yeet(pages[pageNum]);
        pageNum = 0;
    }
    public void AnimEnded()
    {
        animator.SetBool("change", false);
    }
    void yeet(GameObject yeetThisPls)
    {
        LeanTween.moveX(yeetThisPls, -800, swapTime);
    }
    void ComeIn(GameObject thisNextPls)
    {
        thisNextPls.transform.localPosition = new Vector3(950, 0, 0);
        LeanTween.moveX(thisNextPls, 800, swapTime);
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
    }
}