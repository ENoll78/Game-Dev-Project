using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body2D;
    public float verticalSpeed = 0.1f;
    public float horizontalForce = 0.2f;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void FixedUpdate()
    {
        float HorizontalControl = Input.GetAxis("Horizontal");
        Vector2 force = new Vector2(HorizontalControl * verticalSpeed, horizontalForce);
        body2D.AddForce(force);
    }
}
