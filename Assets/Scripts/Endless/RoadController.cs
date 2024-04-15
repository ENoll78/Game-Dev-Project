using UnityEngine;

public class RoadController : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);

        if (transform.position.y < -10)
        {
            FindObjectOfType<RoadPoolManager>().ReturnRoad(gameObject);
        }
    }
}
