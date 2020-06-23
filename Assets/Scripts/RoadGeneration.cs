using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    private BtecCarController carController;
    [SerializeField] GameObject[] roadTypes;
    GameObject latestRoad;
    Transform latestRoadStart;
    Transform latestRoadMiddle;
    Transform latestRoadEnd;
    const float distBetweenRoads = 108;
    [SerializeField] GameObject[] ObstaclePrefab;
    [SerializeField] float[] obsticlePos = new float[] { -3.4f, 0.12f, 6.5f };
    [SerializeField] float obsticleHeight = 0.55f;
    GameObject newRoad;
    [SerializeField] int howManyRoadsYaWannaLoad = 12;
    Vector3 newRoadHere;
    [Range(-.5f, -0.3f)] [SerializeField] float obsticleOffset = 2;

    void Awake()
    {
        //yes, i know this is a dumb, unoptimised way to do this. I'll fix it later. Chill
        latestRoad = GameObject.FindGameObjectWithTag("Road");
        latestRoadStart = GameObject.Find("RoadStraightStart").transform;
        latestRoadMiddle = GameObject.Find("RoadStraightMiddle").transform;
        latestRoadEnd = GameObject.Find("RoadStraightEnd").transform;
        carController = (BtecCarController)FindObjectOfType(typeof(BtecCarController));
    }

    void Update()
    {
        if (carController.CarPos.z + (distBetweenRoads * howManyRoadsYaWannaLoad) > (latestRoad.transform.localPosition.z))
        {
            newRoadHere = new Vector3(latestRoad.transform.position.x, latestRoad.transform.position.y, latestRoad.transform.position.z + distBetweenRoads);
            newRoad = Instantiate(roadTypes[0], newRoadHere, latestRoad.transform.rotation);
            latestRoad = newRoad;
            latestRoadEnd = latestRoad.transform.Find("RoadStraightEnd");
            latestRoadMiddle = latestRoad.transform.Find("RoadStraightMiddle");
            latestRoadStart = latestRoad.transform.Find("RoadStraightStart");
            CreateObstacles(latestRoadStart, latestRoadMiddle, latestRoadEnd);
        }
    }

    void CreateObstacles(Transform start, Transform middle, Transform end)
    {
        for (int i = 0; i < 3; i++)
        {
            int randyAndy = Random.Range(0, 3);
            GameObject obstacle = Instantiate(ObstaclePrefab[randyAndy], new Vector3(obsticlePos[randyAndy], obsticleHeight, 0), ObstaclePrefab[randyAndy].transform.rotation);
            obstacle.transform.parent = end;
            obstacle.transform.localPosition = new Vector3(obstacle.transform.localPosition.x, obstacle.transform.localPosition.y, (i * 0.333f) + obsticleOffset);

        }
        // GameObject cass = Instantiate(cassManager, new Vector3(0, 1.5f, 0), cassManager.transform.rotation);
        // cass.transform.parent = end;
        // cass.transform.position = new Vector3(start.position.x + (4.8f * i - 4.8f), cass.transform.position.y, end.position.z);
        // cass.transform.parent = latestRoad.transform;
    }
}
