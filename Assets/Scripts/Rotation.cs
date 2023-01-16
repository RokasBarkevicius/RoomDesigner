using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 60;
    float minRotation = 90;
    float maxRotation = 180;
    Vector3 currentRotation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
            LimitRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, -rotationSpeed, 0) * Time.deltaTime);
            LimitRotation();
        }
    }

    void LimitRotation()
    {
        currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
