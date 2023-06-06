using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player's transform
    public float distanceBehindPlayer = 5.0f; // Distance behind the player
    public float smoothTime = 0.3f; // Smoothing time for movement
    public float moveSpeed = 5.0f; // Movement speed
    public GameObject player;
    private PlayerMovement PM;
    private Animator animator;

    private Vector3 velocity = Vector3.zero;
    private float targetXPos = 0.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PM = player.GetComponent<PlayerMovement>();
        playerTransform = player.transform;
    }

    void LateUpdate()
    {
        // Check for input keys
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetXPos = -PM.XValue;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetXPos = PM.XValue;
        }
        else
        {
            targetXPos = 0.0f;
        }

        // Calculate the target position for the character
        Vector3 targetPosition = playerTransform.position - playerTransform.forward * distanceBehindPlayer + new Vector3(targetXPos, 0, 0);

        // Smoothly move the character towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Check if the player is jumping
        if (PM.inJump)
        {
            // Trigger the jump animation for the character
            StartCoroutine(MotherJump());
        }
    }

    IEnumerator MotherJump()
    {
        animator.CrossFadeInFixedTime("Jump", 0.1f);
        yield return new WaitForSeconds(0.5f);
        PM.inJump = false;
    }
}
