using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SIDE { Left,Mid,Right }

public class PlayerMovement : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    public bool SwipeLeft;
    public bool SwipeRight;
    public float XValue;
    private CharacterController m_char;
    private Animator m_animator;
    private float x;
    public float speedDodge;

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
    }

    void Update(){
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        if (SwipeLeft)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_Side = SIDE.Left;
                m_animator.Play("DodgeLeft");
            }
            else if (m_Side == SIDE.Right)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
                m_animator.Play("DodgeLeft");
            }
        }
        else if (SwipeRight)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
                m_animator.Play("DodgeRight");
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0;
                m_Side= SIDE.Mid;
                m_animator.Play("DodgeRight");
            }
        }
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * speedDodge);
        m_char.Move((x-transform.position.x) * Vector3.right);
    }
}


