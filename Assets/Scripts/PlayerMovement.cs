using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Declaring variables:
    public float speed = 0; //Use this variable to store value for speed.
    private Rigidbody body; //Use this object to access the properties of the GameObject's Rigidbody component

    private float xInput, zInput; //Use these variables to store user input

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        xInput = 0.0f;
        zInput = 0.0f;
   }

    void OnMove (InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        xInput = movementVector.x;
        zInput = movementVector.y;
    }
    void FixedUpdate () {
        Vector3 movement = new Vector3(xInput, 0, zInput);
        body.AddForce(movement*speed);
   }
}
