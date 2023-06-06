using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SIDE { Left, Mid, Right }

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public SIDE m_Side = SIDE.Mid;
    public float XValue;
    private float NewXPos = 0f;
    private float x;
    private float y;
    public float speedDodge;
    public float fwdSpeed = 7f;
    public float maxSpeed = 14f;
    public float speedIncreaseRate = 0.1f;
    private bool isCollided = false;

    // Input variables
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;

    // Animation variables
    private CharacterController m_char;
    private Animator m_animator;
    public float JumpPower = 7f;
    public bool inJump;
    public bool inRoll;
    private float colHeight;
    private float colCenterY;
    internal float slideCounter;

    // Power-up variables
    private PowerUp.PowerUpType currentPowerUp = PowerUp.PowerUpType.None;
    private bool hasPowerUp = false;

    // Set the current power-up and whether the player has the power-up
    public void SetCurrentPowerUp(PowerUp.PowerUpType powerUpType, bool hasPowerUp)
    {
        currentPowerUp = powerUpType;
        this.hasPowerUp = hasPowerUp;
    }

    // Initialize variables
    void Start()
    {
        m_char = GetComponent<CharacterController>();
        colHeight = m_char.height;
        colCenterY = m_char.center.y;
        m_animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Gradually increase the player's speed
        fwdSpeed = Mathf.Min(fwdSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);

        // Checks if the player collided with an obstacle
        if (isCollided)
        {
            m_char.Move(new Vector3(0, -10f, 0) * Time.deltaTime);
            return;
        }

        // Check if the player picked up the magnet power up
        if (hasPowerUp && currentPowerUp == PowerUp.PowerUpType.Magnet)
        {
            MagnetCoins(10f); // Attract coins within a radius of 5 units
        }

        // Check if the player picked up the speed power up
        if (hasPowerUp && currentPowerUp == PowerUp.PowerUpType.Speed)
        {
            // Move the player character forward at an increased pass
            Vector3 speedVector = new Vector3(x - transform.position.x, y * Time.deltaTime, fwdSpeed * Time.deltaTime * 2);
            m_char.Move(speedVector);
        }

        // Check for input from the player
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        // Move the player character left or right based on input
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
                m_Side = SIDE.Mid;
            }
        }

        // Move the player character
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, fwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * speedDodge);
        m_char.Move(moveVector);

        // Handle jumping and rolling
        Jump();
        Roll();
    }

    // Handle jumping
    public void Jump()
    {
        float jumpHeight = 0.0f;

        if (m_char.isGrounded)
        {
            // Perform a regular jump if the player is on the ground and the jump button is pressed
            if (SwipeUp)
            {
                y = JumpPower;
                m_animator.CrossFadeInFixedTime("Jump", 0.1f);
                inJump = true;
                jumpHeight = transform.position.y;
            }
        }
        else if (hasPowerUp && currentPowerUp == PowerUp.PowerUpType.DoubleJump && inJump && SwipeUp)
        {
            // Perform a double jump if the player has the double jump power-up and the jump button is pressed
            if (SwipeUp)
            {
                y = JumpPower + 3;
                m_animator.CrossFadeInFixedTime("Jump", 0.1f);
                inJump = true;
                jumpHeight = transform.position.y;
            }
        }
        else
        {
            // Apply gravity if the player is in the air
            y -= JumpPower * 2 * Time.deltaTime;
        }

        // Check if the player character has reached the maximum jumpheight
        if (jumpHeight != 0.0f && transform.position.y - jumpHeight >= JumpPower)
        {
            inJump = false;
        }
    }

    // Handle rolling
    public void Roll()
    {
        slideCounter -= Time.deltaTime;
        if (slideCounter <= 0f)
        {
            // End the roll animation and reset the player character's collider size and position
            slideCounter = 0f;
            m_char.center = new Vector3(0, colCenterY, 0);
            m_char.height = colHeight;
            inRoll = false;
        }
        if (SwipeDown)
        {
            // Start the roll animation and shrink the player character's collider size
            slideCounter = 0.5f;
            m_char.center = new Vector3(0, colCenterY / 2f, 0);
            m_char.height = colHeight / 2f;
            m_animator.CrossFadeInFixedTime("Roll", 0.1f);
            inRoll = true;
            inJump = false;
        }
    }

    // Handle Magnet Power Up
    public void MagnetCoins(float range)
    {
        // Create a box collider trigger to detect coins within a certain range
        BoxCollider triggerCollider = gameObject.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector3(range * 2f, range * 2f, range * 2f);

        // Find all the coins in the trigger collider
        Collider[] hitColliders = Physics.OverlapBox(transform.position, triggerCollider.size / 2f);
        foreach (Collider col in hitColliders)
        {
            // Check if the collider is a coin
            if (col.gameObject.CompareTag("Coin"))
            {
                // Calculate the direction from the coin to the player
                Vector3 direction = transform.position - col.transform.position;
                direction.y = 0f; // Ignore the y-component

                // Move the coin towards the player
                col.transform.position += direction.normalized * Time.deltaTime * 10f;
            }
        }

        // Remove the trigger collider
        Destroy(triggerCollider);
    }

    // Handle collisions with obstacles
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (hasPowerUp && currentPowerUp == PowerUp.PowerUpType.DestroyObstacle)
            {
                // Destroy the obstacle if the player has the destroy obstacle power-up
                Destroy(other.gameObject);
            }
            else
            {
                // Trip the player character and set isCollided to true if they collide with an obstacle and don't have the destroy obstacle power-up
                isCollided = true;
                CoinManager.Instance.AddCoins(PlayerManager.numberOfCoins);
                m_animator.SetTrigger("Trip");
            }
        }
    }
}