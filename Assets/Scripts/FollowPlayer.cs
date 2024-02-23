using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject playerCar;

    [SerializeField] float xVal = 0f;
    [SerializeField] float zVal = 0f;
    [SerializeField] float followOffset = 0f;

    void LateUpdate()
    {
       transform.position = new Vector3(xVal, playerCar.transform.position.y - followOffset, zVal);
    }
}
