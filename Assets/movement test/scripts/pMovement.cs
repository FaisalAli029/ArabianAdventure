using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float leftRightSpeed = 4;
    public bool isJumping = false;
    public bool coomingDown = false;
    public bool isSliding = false;
    public GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > LevelBoundry.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < LevelBoundry.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
            }

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (isSliding == false)
            {
                
                playerObject.GetComponent<Animator>().Play("Running Slide");
            }

        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (isJumping == false)
            {
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());
            }

        }

        if (isJumping == true)
        {
            if (coomingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 9, Space.World);
            }
            if (coomingDown == true)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -9, Space.World);
            }
        }
    }

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f);
        coomingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        coomingDown = false;
        playerObject.GetComponent<Animator>().Play("Running");
    }
}