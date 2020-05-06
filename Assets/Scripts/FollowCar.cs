using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform car;

    void LateUpdate()
    {
        this.transform.position = car.transform.position;
    }
}
