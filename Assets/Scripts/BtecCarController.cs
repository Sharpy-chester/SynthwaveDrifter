using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtecCarController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float laneChangeSpeed;
    public int lane;
    public bool isAlive = true;
    public UIManager ui;
    public bool left = false;
    public bool right = false;
    public float gotoX;
    float timeAlive = 0;
    public int cassettes;
    public GameObject cassPart;
    public LeanTweenType curve;
    float[] laneX = { -4.2f, 0, 4.2f };
    float desiredPos;
    LTDescr lean;
    bool swipeRight;
    bool swipeLeft;
    float swipeStartTime;
    Vector3 startPos;
    public float minSwipeDist = 0.2f;
    bool canRegTouchMovement = true;
    public GameManager manager;
    float distance = 0;
    float time = 0;
    float score = 0;


    void Awake()
    {
        isAlive = true;
        lane = 1;
        cassettes = 0;
    }
    void Update()
    {
        if (isAlive)
        {
            time += Time.deltaTime;
            distance = speed * time;
            score = Mathf.Round(distance) + (cassettes * ui.cassModifier);
            timeAlive += Time.deltaTime / 10000;
            if (speed < maxSpeed)
            {
                // laneChangeSpeed = laneChangeSpeed - timeAlive / 20;
                speed = speed + timeAlive * 2;
            }
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + speed * Time.deltaTime);
            SwipeDetection();
            if ((Input.GetKeyDown(KeyCode.D) || swipeRight) && lane < 2)
            {
                lane++;
                right = true;
                left = false;
                LeanTween.cancel(this.gameObject);
            }
            else if ((Input.GetKeyDown(KeyCode.A) || swipeLeft) && lane > 0)
            {
                lane--;
                left = true;
                right = false;
                LeanTween.cancel(this.gameObject);
            }

            if (right || left)
            {
                desiredPos = laneX[lane];
                // this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, desiredPos, laneChangeSpeed), this.transform.position.y, this.transform.position.z);
                lean = LeanTween.moveLocalX(this.gameObject, desiredPos, laneChangeSpeed);
                right = false;
                left = false;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Obstacle(Clone)")
        {
            speed = 0;
            isAlive = false;
            manager.UpdateValues(cassettes, score);
            ui.GameOver();
        }
        if (col.gameObject.name == "CassetteTape(Clone)")
        {

            Destroy(col.gameObject);
            GameObject part = Instantiate(cassPart, col.transform.position, col.transform.rotation);
            cassettes++;

            ui.UpdateCassettes(cassettes);
        }
    }

    void SwipeDetection()
    {
        swipeLeft = false;
        swipeRight = false;
        if (Input.touches.Length > 0)
        {
            Touch uwu = Input.GetTouch(0);
            if (uwu.phase == TouchPhase.Began)
            {
                startPos = new Vector2(uwu.position.x / Screen.width, uwu.position.y / Screen.width);
                swipeStartTime = Time.time;
            }
            if (uwu.phase == TouchPhase.Moved && canRegTouchMovement)
            {
                Vector2 endPos = new Vector2(uwu.position.x / Screen.width, uwu.position.y / Screen.width);
                Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
                if (swipe.magnitude < minSwipeDist)
                    return;
                if (swipe.x > 0)
                {
                    swipeRight = true;
                }
                else
                {
                    swipeLeft = true;
                }
                canRegTouchMovement = false;
            }
            if (uwu.phase == TouchPhase.Ended)
            {
                canRegTouchMovement = true;
            }
        }
    }
}