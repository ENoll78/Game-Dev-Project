using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @Author Hunter Lane
 * @Description This enables the movement for the car.
 **/
public class Movement : MonoBehaviour
{
    public Rigidbody2D body2D;

    /**
     * These variables are used to control the coeffcients of the car.
     * To modify these values click on the car object and go to inspect.
     * Then change values.
     **/

    //Coeffciients for the accerations.
    public float breakCoeffcient = 0.0f;
    public float speedCoeffcient = 0.0f;
    public float horizontalCoeffcient = 0.0f;

    //The speed the car starts with.
    public float startSpeed = 0.0f;


    // Start is called before the first frame update
    private void Start()
    {
        horizontalCoeffcient = 1 / horizontalCoeffcient;
        //Add the start speed to the vehicle.
        body2D.AddForce(new Vector2(0.0f, 32f));
    }


    public void FixedUpdate()
    {
        //Apply the user input as a force to the car.
        float veticalControl = Input.GetAxis("Vertical");
        float horizontalControl = Input.GetAxis("Horizontal");
        body2D.AddForce(new Vector2(horizontalControl * horizontalCoeffcient, veticalControl * speedCoeffcient));

        //Apply the vertical friciton to slow down the car in the vertical axis.
        Vector2 current_Velocity = body2D.velocity;
        body2D.AddForce(new Vector2(-current_Velocity.x * 0.4f, 0.0f));
    }
}
