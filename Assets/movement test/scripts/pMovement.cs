using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class pMovement : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    public bool SwipeLeft;
    public bool SwipeRight;
    public float XValue;
    private CharacterController m_char;
    public float moveSpeed = 3;
    public bool isJumping = false;
    public bool coomingDown = false;
    public bool isSliding = false;
    public GameObject playerObject;

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        if (SwipeLeft)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_Side = SIDE.Left;
            }
            else if (m_Side == SIDE.Right)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
            }
        }
        else if (SwipeRight)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
            }
        }
        m_char.Move((NewXPos - transform.position.x) * Vector3.right);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (isSliding == false)
            {
                
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