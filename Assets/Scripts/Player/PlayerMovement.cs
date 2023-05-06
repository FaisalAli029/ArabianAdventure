using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;

    void Update(){
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}


