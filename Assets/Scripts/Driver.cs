using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Driver : MonoBehaviour
{
    [SerializeField]public float currentSpeed;
    [SerializeField] int lane = 0;
    [SerializeField] float[] laneArray; //length 4
    [SerializeField] float initalSpeed;
    [SerializeField] float speedUpRate;
    [SerializeField] float slowDownRate;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float minSpeed;

    [SerializeField] bool passedGoal = false; //serialize for debugging

    private Vector3 LeftTemp;
    private Vector3 RightTemp;

    private void Start() 
    {
        // currentSpeed = gearArray[gear];
        LeftTemp = new Vector3();
        RightTemp = new Vector3();
        currentSpeed = initalSpeed;
    }

    void FixedUpdate()
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
        else
            SceneManager.LoadScene("GameOver");
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
            LeftTemp.Set(laneArray[lane], transform.position.y, 0f);
            transform.position = LeftTemp;
        }
    }
    void MoveRight()
    {
        if (lane < laneArray.Length - 1)
        {
            lane++;
            RightTemp.Set(laneArray[lane], transform.position.y, 0f);
            transform.position = RightTemp;
        }
    }
}
