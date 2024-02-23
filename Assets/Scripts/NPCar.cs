using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCar : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float laneVal = 0;
    [SerializeField] float startingDistance = 0;
    
    private void Start() 
    {
        transform.position = new Vector3 (laneVal, startingDistance, 0);
    }

    void Update()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
    }
}
