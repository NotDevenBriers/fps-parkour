using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
  [Header("WallRunning")]
  public LayerMask whatIsWall;
  public LayerMask whatIsGround;
  public float wallRunForce;
  public float  maxWallRunTime;
  private float wallRunTimer;

  [Header("Input")]
  private float horizontalInput;
  private float verticalInput;

  [Header("Detection")]
  public float wallCheckDistance;
  public float minJumpHeight;
  private RaycastHit leftWallhit;
  private RaycastHit rightWallhit;
  private bool wallLeft;
  private bool wallRight;

  [Header("Reference")]
  public Transform orientation;
  private PlayerMovementAdvanced pm;
  private Rigidbody rb;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    pm = GetComponent<PlayerMovementAdvanced>();
  }

  private void Update()
  {
    CheckForWall();
  }

  private void CheckForWall()
  {
    wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
    wallLeft = Physics.Raycast(transform.position, -orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
  }

  private bool AboveGround()
  {
    return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
  }

  private void StateMachine()
  {
    // Getting Inputs
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    // State 1 - Wallrunning
    if((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
    {
        // start wallrun here
    }
  }

  private void StartWallRun()
  {

  }

  private void WallRunningMovement()
  {

  }

  private void StopWallRun()
  {
    
  }
}
