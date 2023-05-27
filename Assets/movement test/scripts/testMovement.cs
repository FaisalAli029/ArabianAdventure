using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum SIDE { Left, Mid, Right }

public class testMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to control player movement speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement * speed;
    }


}
