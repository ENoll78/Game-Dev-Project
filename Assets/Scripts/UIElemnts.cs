using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElemnts : MonoBehaviour
{
    public Driver driver;
    float speedLimit = 90.0f;
    public TextMeshProUGUI speedLimitElement;
    public RectTransform needleTransform;
    private Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = driver.currentSpeed;

        if (speedLimitElement != null) {
            speedLimitElement.text = speedLimit.ToString("#");
            if ((currentSpeed * 9F) > speedLimit)
            {
                speedLimitElement.text = "<color=yellow>" + speedLimit.ToString("#") + "</color>";
            }
            if ((currentSpeed * 9F) > (speedLimit + 10F))
            {
                speedLimitElement.text = "<color=red>" + speedLimit.ToString("#") + "</color>";
            }
        }

        if (needleTransform != null) {
            temp = needleTransform.rotation.eulerAngles;
            temp.z = ((currentSpeed*9f) - 90f) * -1f;
            needleTransform.rotation = Quaternion.Euler(temp);
        }
    }
}
