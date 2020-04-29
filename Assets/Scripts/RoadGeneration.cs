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
    public GameObject ObstaclePrefab;
    Vector3 obsticlePosVec3 = new Vector3(-6.25f, -1.725f, 2.8f);
    float[] obsticlePos = new float[] { -6.25f, -1.725f, 2.8f };
    public float obsticleHeight = 0.55f;
    public BtecCarController carController;
    public GameObject cassManager;

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
        if (carTrans.localPosition.z > latestRoadEnd.localPosition.x)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            GameObject newRoad = Instantiate(roadTypes[Random.Range(0, 1)]);
            //change to random.range(0, 3) for curvy bois

            newRoad.transform.parent = latestRoadEnd;
            //, spawnPos, rot.rotation


            newRoad.transform.localPosition = new Vector3(0, 0, distBetweenRoads);
            newRoad.transform.localEulerAngles = new Vector3(0, 0, 0);
            newRoad.transform.parent = null;
            latestRoad = newRoad;
            changeLatest = true;
            carTrans.parent = latestRoad.transform;
            // Destroy(latestRoad, 10f);
        }
        if (changeLatest)
        {
            latestRoadEnd = latestRoad.transform.Find("RoadStraightEnd");
            latestRoadMiddle = latestRoad.transform.Find("RoadStraightMiddle");
            latestRoadStart = latestRoad.transform;
            CreateObstacles(latestRoadStart, latestRoadMiddle, latestRoadEnd);
            changeLatest = false;
        }
    }

    void CreateObstacles(Transform start, Transform middle, Transform end)
    {
        int randyAndy = Random.Range(0, 3);
        int randyAndy2 = Random.Range(0, 3);

        //the solution below isnt great. Could use a list and remove a lane that gets chosen so the last random would b. 
        while (randyAndy2 == randyAndy)
        {
            int randyAndy3 = Random.Range(0, 3);
            if (randyAndy3 != randyAndy)
            {
                randyAndy2 = randyAndy3;
            }

        }

        for (int i = 0; i < 3; i++)
        {
            if (randyAndy == i || randyAndy2 == i)
            {
                GameObject obstacle = Instantiate(ObstaclePrefab, new Vector3(obsticlePos[i], obsticleHeight, 0), ObstaclePrefab.transform.rotation);
                obstacle.transform.parent = end;
                obstacle.transform.localPosition = new Vector3(obstacle.transform.localPosition.x, obstacle.transform.localPosition.y, 0);
            }

            if (randyAndy != i && randyAndy2 != i)
            {

                GameObject cass = Instantiate(cassManager, new Vector3(0, 1.5f, 0), cassManager.transform.rotation);
                print(cass.transform.position);
                cass.transform.parent = end;
                cass.transform.localPosition = new Vector3(2.1f * i - 2.1f, cass.transform.localPosition.y, 0);
                cass.transform.parent = latestRoad.transform;
                print(cass.transform.position);
            }
        }
    }

}
