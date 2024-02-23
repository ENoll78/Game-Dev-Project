using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float currentSpeed;
    
    [SerializeField] int lane = 1; //car starts in the middle lane
    [SerializeField] int gear = 0; //car starts in first gear

    [SerializeField] float[] laneArray; //length 3 = 0 left, 1 mid, 2 right
    [SerializeField] float[] gearArray; //length 4 - 0 1st, 1 2nd, 2 3rd, 3 4th

    [SerializeField] bool passedGoal = false; //serialize for debugging

    private void Start() 
    {
        currentSpeed = gearArray[gear];
    }

    void Update()
    {
        transform.Translate(0f, currentSpeed * Time.deltaTime, 0f);

        if(!passedGoal)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                SpeedUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                SlowDown();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Goal")
        {
            currentSpeed = 0;
            passedGoal = true;
            Debug.Log("You did it!");
        }
    }

    void SpeedUp ()
    {
        if(gear < gearArray.Length - 1)
        {
            gear++;
            currentSpeed = gearArray[gear];
        }
    }
    void SlowDown()
    {
        if (gear > 0)
        {
            gear--;
            currentSpeed = gearArray[gear];
        }
    }
    void MoveLeft()
    {
        if (lane > 0)
        {
            lane--;
            transform.position = new Vector3 (laneArray[lane], transform.position.y, 0f);
        }
    }
    void MoveRight()
    {
        if (lane < laneArray.Length - 1)
        {
            lane++;
            transform.position = new Vector3 (laneArray[lane], transform.position.y, 0f);
        }
    }
}
