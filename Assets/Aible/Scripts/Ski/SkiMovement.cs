using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiMovement : MonoBehaviour
{
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canAnimate;
    public bool moveRight;
    [HideInInspector] public bool moveLeft;

    public bool canMoveArms = true;

    [Header("MovementSettings")]
    public float maxSpeed = 15f;
    public float startSpeed = 10f;
    public float acceleratingSpeed = 1f;
    public float slowDownSpeed = 10f;
    public float sideMovementForce = 5f;
    public float turnTorque = 80f;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool canUseInput;
    [HideInInspector] public bool isSlowingDown;
    [HideInInspector] public float rightArmAngle;
    [HideInInspector] public float leftArmAngle;

    private float forwardMoveSpeed;
    private float sideMoveSpeed;
    private float speed;

    private bool isMovingToSide;

    private IsGrounded isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = GetComponent<IsGrounded>();

        speed = startSpeed;
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        if (speed < maxSpeed && !isSlowingDown)
        {
            Accelerate();
        }
        else if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        MoveSki();
    }

    private void LateUpdate()
    {     
        /*
        if (!canAnimate)
            return;
        */

        if (canMoveArms)
        {
            SkiAnimation.SetLayerWeight(1, rightArmAngle);
            SkiAnimation.SetLayerWeight(2, leftArmAngle);
        }

        StartCoroutine(HandleAnimation());
    }

    public IEnumerator StartMovement()
    {
        rb.isKinematic = false;

        canMove = true;
        canAnimate = true;

        SkiAnimation.PlayAnimation("Skiing_Straight");

        yield return new WaitForSeconds(0.2f);

        canUseInput = true;
    }

    public void Accelerate()
    {
        if (speed > maxSpeed - 1)
        {
            speed = maxSpeed;
        }

        speed += Time.fixedDeltaTime * acceleratingSpeed;
    }

    public void SlowDown()
    {
        if (speed == 0)
            return;

        isSlowingDown = true;

        if (speed < 0.2f)
        {
            speed = 0;
        }

        speed -= Time.fixedDeltaTime * slowDownSpeed;
    }

    void MoveSki()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        sideMoveSpeed = sideMovementForce * Time.fixedDeltaTime;
        float horizontalMovement = horizontalInput * sideMoveSpeed;
        Vector3 sideMovement = transform.right * horizontalMovement;

        if (!canUseInput || !isGrounded.isGrounded())
            sideMovement = Vector3.zero;

        forwardMoveSpeed = speed * Time.fixedDeltaTime;
        Vector3 movement = transform.forward * forwardMoveSpeed;

        rb.MovePosition(rb.position + movement + sideMovement);
        if(canUseInput && isGrounded.isGrounded())
            rb.AddTorque(horizontalInput * turnTorque * transform.up);
    }

    IEnumerator HandleAnimation()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!isMovingToSide)
            {
                SkiAnimation.SetAnimationBool("Skiing_To_Straight", false);
                SkiAnimation.SetAnimationBool("Idle_To_Skiing", true);
                isMovingToSide = true;
            }
        }
        else if (isMovingToSide)
        {
            yield return new WaitForSeconds(0.01f);

            if(Input.GetAxis("Horizontal") != 0)
            {
                isMovingToSide = false;
            }
            else
            {
                SkiAnimation.SetAnimationBool("Idle_To_Skiing", false);
                SkiAnimation.SetAnimationBool("Skiing_To_Straight", true);
                isMovingToSide = false;
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            SkiAnimation.SetAnimationBool("MoveRight", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            SkiAnimation.SetAnimationBool("MoveRight", true);
        }
    }
}
