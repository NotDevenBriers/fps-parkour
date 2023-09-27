using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovementAdvanced pm;
    public PlayerCam cam;

    [Header("Sliding")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool canSlide;
    public bool isSlide;
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;
    public float slideDelay;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();

        startYScale = playerObj.localScale.y;
        slideDelay = 0;
    }

    public void slideDetect()
    {
        // check for slidable
        isSlide = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 2.5f, whatIsGround);
        if (isSlide)
            canSlide = true;
        else
            canSlide = false;
    }

    private void Update()
    {

        // Decrease slideDelay over time
        if (slideDelay > 0)
        {
            slideDelay -= Time.deltaTime;
            slideDelay = Mathf.Clamp(slideDelay, 0, maxSlideTime); // Ensure it doesn't go below 0
        }
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (slideDelay <= 0) && (horizontalInput != 0 || verticalInput != 0))
            StartSlide();

        if (Input.GetKeyUp(slideKey) && pm.sliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
            SlidingMovement();
    }

    private void StartSlide()
    {
        if (pm.grounded || pm.OnSlope() || canSlide)
        {
            pm.sliding = true;
            cam.DoFov(90f);
            slideDelay = 0.75F;

            playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            slideTimer = maxSlideTime;
        }
        /* if (pm.OnSlope())
        {
            pm.sliding = true;

            playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            slideTimer = maxSlideTime;
        } */
        
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Check if the player is on a slope and sliding down
        if (pm.OnSlope() && rb.velocity.y < -0.1f)
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }
        else
        {
            // Apply a lower force or no force when not on a slope
            rb.AddForce(Vector3.zero);
        }

        slideDelay -= Time.deltaTime;
        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
            StopSlide();
    }
    
    private void StopSlide()
    {
        pm.sliding = false;
        cam.DoFov(60f);

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
