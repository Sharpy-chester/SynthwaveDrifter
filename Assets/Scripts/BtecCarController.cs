using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtecCarController : MonoBehaviour
{
    public float speed;
    float startSpeed;
    public float laneChangeSpeed;
    public int lane;
    public bool isAlive = true;
    public float laneDist = 3;
    public UIManager ui;
    public bool left = false;
    public bool right = false;
    public float gotohere = 0;
    float cooldown = 0;
    public float cooldownMax;
    float timeAlive = 0;
    public int cassettes;
    public GameObject cassPart;
    public LeanTweenType curve;


    void Awake()
    {
        isAlive = true;
        lane = 1;
        cooldown = cooldownMax;
        cassettes = 0;
        startSpeed = speed;
    }

    void Update()
    {
        //need to change movement so that you go to the left as soon as A is pressed and same with right and D, even if you're midway through going to a different lane

        if (isAlive)
        {
            cooldownMax = laneChangeSpeed * (speed / startSpeed) + 0.001f;
            //could cap speed
            timeAlive += Time.deltaTime / 10000;
            speed = speed + timeAlive;
            cooldown += Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.A) && lane > 0 && cooldown >= cooldownMax)
            {
                this.transform.position = new Vector3(gotohere, this.transform.position.y, this.transform.position.z);
                gotohere = this.transform.position.x - laneDist;
                left = true;
                lane--;
                cooldown = 0;
            }
            if (Input.GetKey(KeyCode.D) && lane < 2 && cooldown >= cooldownMax)
            {
                this.transform.position = new Vector3(gotohere, this.transform.position.y, this.transform.position.z);
                gotohere = this.transform.position.x + laneDist;
                right = true;
                lane++;
                cooldown = 0;
            }
        }
        if (left)
        {
            // this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, gotohere, laneChangeSpeed), this.transform.position.y, this.transform.position.z);
            LeanTween.moveX(this.gameObject, gotohere, laneChangeSpeed * (speed / startSpeed)).setEase(curve);
            left = false;
        }
        if (right)
        {
            // this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, gotohere, laneChangeSpeed), this.transform.position.y, this.transform.position.z);
            LeanTween.moveX(this.gameObject, gotohere, laneChangeSpeed * (speed / startSpeed)).setEase(curve);
            right = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Obstacle(Clone)")
        {
            speed = 0;
            isAlive = false;
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
}
