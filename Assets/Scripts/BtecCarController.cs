using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtecCarController : MonoBehaviour
{
    public float speed;
    public int lane;
    public bool isAlive = true;
    public float laneDist = 3;
    public UIManager ui;

    void Start()
    {
        isAlive = true;
        lane = 1;
    }

    void Update()
    {
        if (isAlive)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.A) && lane > 0)
            {
                this.transform.position = new Vector3(this.transform.position.x - laneDist, this.transform.position.y, this.transform.position.z);
                lane--;
            }
            if (Input.GetKeyDown(KeyCode.D) && lane < 2)
            {
                this.transform.position = new Vector3(this.transform.position.x + laneDist, this.transform.position.y, this.transform.position.z);
                lane++;
            }
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
    }
}
