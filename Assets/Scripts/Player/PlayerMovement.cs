using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SIDE { Left,Mid,Right }

public class PlayerMovement : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    public float XValue;
    private CharacterController m_char;
    private Animator m_animator;
    private PowerUp powerUp;
    private float x;
    public float speedDodge;
    public float JumpPower = 7f;
    private float y;
    public bool inJump;
    public bool inRoll;
    public float fwdSpeed = 7f;
    private float colHeight;
    private float colCenterY;
    private bool isCollided = false;
    private float maxSpeed = 25f;
    private float speedIncreaseRate = 0.1f;

    public PlayerMovement()
    {
        
    }

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        colHeight = m_char.height;
        colCenterY = m_char.center.y;
        m_animator = GetComponent<Animator>();
        powerUp = GetComponent<PowerUp>();
        transform.position = Vector3.zero;
    }

    void Update(){
        // Gradually increase the player's speed
        fwdSpeed = Mathf.Min(fwdSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        
        // Checks if the player collided with an obstacle
        if (isCollided)
        {
            m_char.Move(new Vector3(0, -10f, 0) * Time.deltaTime);
            return;
        }

        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);



        if (SwipeLeft && !inRoll)
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
        else if (SwipeRight && !inRoll)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0;
                m_Side= SIDE.Mid;
            }
        }
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, fwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * speedDodge);
        m_char.Move(moveVector);
        Jump();
        Roll();
    }
    public void Jump()
    {
        if (m_char.isGrounded)
        {
            if (SwipeUp)
            {
                y = JumpPower;
                m_animator.CrossFadeInFixedTime("Jump", 0.1f);
                inJump = true;
            }
        }
        else
        {
            y -= JumpPower * 2 * Time.deltaTime;
        }
    }
    internal float slideCounter;
    public void Roll()
    {
        slideCounter -= Time.deltaTime;
        if (slideCounter <= 0f)
        {
            slideCounter = 0f;
            m_char.center = new Vector3(0, colCenterY, 0);
            m_char.height = colHeight;
            inRoll = false;
        }
        if (SwipeDown)
        {
            slideCounter = 0.5f;
            m_char.center = new Vector3(0, colCenterY / 2f, 0);
            m_char.height = colHeight/2f;
            m_animator.CrossFadeInFixedTime("Roll", 0.1f);
            inRoll = true;
            inJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            isCollided = true;
            m_animator.SetTrigger("Trip");
        }
        Debug.Log(other);
    }
}


