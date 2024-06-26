using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCar : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

public void SetInitialLane(float lanePosition, bool isOppositeDirection)
{
    float startingY = transform.position.y + 100;
    float zPosition = -0.1f;

    transform.position.Set(lanePosition, startingY, zPosition);

    if (isOppositeDirection)
    {
        speed = Mathf.Abs(speed);
        transform.rotation = Quaternion.Euler(180, 0, 0);
    }
    else
    {
        speed = Mathf.Abs(speed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

void FixedUpdate()
{
    
    Debug.Log($"NPC car {gameObject.name} is supposed to be moving at speed: {speed}");
    transform.Translate(0f, speed * Time.deltaTime, 0f);
}
}
