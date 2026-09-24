
using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] public float timeToApex = 0.35f;
    [SerializeField] public float timeToFall = .25f;
    [SerializeField] public float jumpHeight;

    [SerializeField] private float maxSpeed = 8;
    [SerializeField] private float maxSpeedOnAir = 4;

    [SerializeField] private float timeToReachMax = .3f;

    [SerializeField] private float timeToStop = .1f;

    [SerializeField] private float turnSpeed = .5f;

    [SerializeField] private float turnSpeedScalar = 2;

    float acceleration => (isGrounded ? maxSpeed : maxSpeedOnAir) / timeToReachMax;
    float deceleration => (isGrounded ? maxSpeed : maxSpeedOnAir) / timeToStop;

    float turnRate => (maxSpeed * turnSpeedScalar) / turnSpeed;
    
    float riseG => 2f * jumpHeight / (timeToApex * timeToApex);
    float fallG => 2f * jumpHeight / (timeToFall * timeToFall);
    float jumpVelocity => 2f * jumpHeight / timeToApex;

    Vector3 velocity = Vector3.zero;

    private float groundPos;
    private bool isGrounded => Mathf.Approximately(transform.position.y,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        ShortJump();
        HeldJump();
        DoubleJump();
       
        transform.position += velocity * Time.deltaTime;
        
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, groundPos, transform.position.z);
        }
    }

    private void HorizontalMovement()
    {
        float input = Input.GetAxisRaw("Horizontal");
        float targetSpeed = maxSpeed * input;
        float targetAccel = ChooseAccel(input);
        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, targetAccel * Time.deltaTime);
    }

    void ShortJump()
    {
        if (isGrounded && Input.GetButton("Jump"))
        {
            jumpHeight = 1;
            velocity.y = jumpVelocity;
        }
         velocity.y -= (velocity.y < 0 ? fallG : riseG) * Time.deltaTime;
         Debug.Log("Short Jump, boing boing boing as they say.");
    }

    void HeldJump()
    {
         if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpHeight = 3;
            velocity.y = jumpVelocity;
        }
        velocity.y -= (velocity.y < 0 ? fallG : riseG) * Time.deltaTime;
        Debug.Log("Normal Jump, how many times have you jumped in real life?");
    }

    void DoubleJump()
    {
        if (!isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpHeight = 6;
            velocity.y = jumpVelocity;
        }
        velocity.y -= (velocity.y < 0 ? fallG : riseG) * Time.deltaTime;
        Debug.Log("The best jump, double jump.");
    }

    float ChooseAccel(float input)
    {
        bool isTurning = !Mathf.Approximately(Mathf.Sign(input), Mathf.Sign(velocity.x));
        Debug.Log($"{input}, {isTurning}");
        if (Mathf.Abs(input) > 0)
        {
            if (!isTurning)
                return acceleration;

            return turnRate;
        }
        else
            return deceleration;
    }
}   