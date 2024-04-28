using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float currentSpeed;
    
    [SerializeField] int lane = 0;
    [SerializeField] float[] laneArray; //length 4
    [SerializeField] float initalSpeed;
    [SerializeField] float speedUpRate;
    [SerializeField] float slowDownRate;
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;

    [SerializeField] bool passedGoal = false; //serialize for debugging

    private void Start() 
    {
        // currentSpeed = gearArray[gear];
        currentSpeed = initalSpeed;
    }

    void Update()
    {
        if(!PauseMenu.isPaused) // Should kill inputs while paused??
        {
            transform.Translate(0f, currentSpeed * Time.deltaTime, 0f);

            if(!passedGoal)
            {
                if(Input.GetAxis("Vertical") == 1)
                {
                    SpeedUp();
                }
                else if(Input.GetAxis("Vertical") == -1)
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
        if(currentSpeed + speedUpRate <= maxSpeed)
        {
            currentSpeed += speedUpRate;
        }
        else
        {
            currentSpeed = maxSpeed;
        }
    }
    void SlowDown()
    {
        if(currentSpeed - slowDownRate >= minSpeed)
        {
            currentSpeed -= slowDownRate;
        }
        else
        {
            currentSpeed = minSpeed;
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
