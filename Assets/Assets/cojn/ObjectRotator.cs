using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around its Y-axis
        Quaternion currentRotation = transform.rotation;

        // Calculate the new rotation based on the rotation speed
        Quaternion newRotation = Quaternion.Euler(
            currentRotation.eulerAngles.x,
            currentRotation.eulerAngles.y + (rotationSpeed * Time.deltaTime),
            currentRotation.eulerAngles.z
        );

        // Apply the new rotation to the object
        transform.rotation = newRotation;
    }
}
