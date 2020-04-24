using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public Transform carTrans;
    public GameObject[] roadTypes;
    GameObject[] roadSections;
    public GameObject latestRoad;
    public Transform latestRoadStart;
    Transform latestRoadMiddle;
    public Transform latestRoadEnd;
    public float distBetweenRoads = 21;
    bool changeLatest = false;
    public Vector3 carpos;
    public Vector3 endpos;
    public Transform rot;

    void Awake()
    {
        //yes, i know this is a dumb, unoptimised way to do this. I'll fix it later. Chill
        latestRoad = GameObject.FindGameObjectWithTag("Road");
        latestRoadStart = GameObject.Find("RoadStraightStart").transform;
        latestRoadMiddle = GameObject.Find("RoadStraightMiddle").transform;
        latestRoadEnd = GameObject.Find("RoadStraightEnd").transform;
        carTrans.parent = latestRoad.transform;
    }

    void Update()
    {


        carpos = carTrans.localPosition;
        endpos = latestRoadEnd.localPosition;

        //if the position of the car relative to its self (how)
        //could set car as child of latest road piece
        //if name is left, go left?
        // if (carTrans.localPosition.z > latestRoadEnd.localPosition.z)
        if (carTrans.localPosition.z > latestRoadEnd.localPosition.x)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            GameObject newRoad = Instantiate(roadTypes[Random.Range(0, 2)]);
            //, spawnPos, rot.rotation
            newRoad.transform.parent = latestRoadEnd;
            newRoad.transform.localPosition = new Vector3(0, 0, distBetweenRoads);
            newRoad.transform.localEulerAngles = new Vector3(0, 0, 0);
            newRoad.transform.parent = null;
            latestRoad = newRoad;
            changeLatest = true;
            carTrans.parent = latestRoad.transform;
            print("working");

            //
        }

        // GameObject newRoad = Instantiate(roadTypes[Random.Range(0, 3)], latestRoadEnd.position + new Vector3(0, 0, distBetweenRoads), latestRoadEnd.rotation);
        // latestRoad = newRoad;
        // carTrans.parent = latestRoad.transform;
        // Destroy(latestRoad, 10f);

        if (changeLatest)
        {
            latestRoadEnd = latestRoad.transform.Find("RoadStraightEnd");
            latestRoadMiddle = latestRoad.transform.Find("RoadStraightMiddle");
            latestRoadStart = latestRoad.transform;
            changeLatest = false;
        }
    }

}
