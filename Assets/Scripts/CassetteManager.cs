using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteManager : MonoBehaviour
{

    public float gotoY;
    public float bobSpeed;
    public float rotSpeed;
    public LeanTweenType curve;
    public GameObject cassPrefab;
    public GameObject[] cassettes;
    public int cassAmt = 5;
    public float cassSpacing = 3f;

    void Awake()
    {
        cassettes = new GameObject[cassAmt];
        for (int i = 0; i < cassAmt; i++)
        {
            cassettes[i] = Instantiate(cassPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - (cassSpacing * i)), this.transform.rotation, this.transform);
            LeanTween.moveLocalY(cassettes[i], gotoY, bobSpeed).setLoopPingPong().setEase(curve);
            LeanTween.rotateAround(cassettes[i], Vector3.up, 360, rotSpeed).setLoopClamp();
        }

    }

}
