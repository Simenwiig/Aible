using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiMovement : MonoBehaviour
{
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canAnimate;
    [HideInInspector] public bool moveRight;
    [HideInInspector] public bool moveLeft;

    public bool canMoveArms = true;
    public bool canMoveHips = true;

    [Header("MovementSettings")]
    public float maxSpeed = 15f;
    public float startSpeed = 10f;
    public float obsticleSpeed = 7f;
    public float acceleratingSpeed = 1f;
    public float slowDownSpeed = 10f;
    public float sideMovementForce = 5f;
    public float maxTurnTorque = 45f;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool canUseInput;
    [HideInInspector] public bool isSlowingDown;
    [HideInInspector] public float rightArmAngle;
    [HideInInspector] public float leftArmAngle;
    [HideInInspector] public float lLayerWeight;
    [HideInInspector] public float rLayerWeight;

    [HideInInspector] public float speed;
    public float turnTorque;
    private float forwardMoveSpeed;
    private float sideMoveSpeed;

    private bool isMovingToSide;

    private IsGrounded isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = GetComponent<IsGrounded>();

        turnTorque = maxTurnTorque;
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
        if (!canAnimate)
            return;       

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

        yield return new WaitForSeconds(0.1f);

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
            rLayerWeight = 0;
            lLayerWeight = 0;
        }

        speed -= Time.fixedDeltaTime * slowDownSpeed;

        rLayerWeight -= Time.fixedDeltaTime;
        lLayerWeight -= Time.fixedDeltaTime;
        SkiAnimation.SetLayerWeight(1, rLayerWeight);
        SkiAnimation.SetLayerWeight(2, lLayerWeight);
    }

    void MoveSki()
    {
        float horizontalInput = 0;
        if (moveRight)
        {
            horizontalInput = 1;
        }
        else if (moveLeft)
        {
            horizontalInput = -1;
        }
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
        if ((moveRight || moveLeft) && canMoveHips)
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
            yield return new WaitForSeconds(0.02f);

            if(moveRight || moveLeft)
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

        if (moveRight && canMoveHips)
        {
            SkiAnimation.SetAnimationBool("MoveRight", false);
        }
        else if (moveLeft && canMoveHips)
        {
            SkiAnimation.SetAnimationBool("MoveRight", true);
        }
    }
}
