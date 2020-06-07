using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    [SerializeField] Transform car;

    void LateUpdate()
    {
        this.transform.position = car.transform.position;
    }
}
