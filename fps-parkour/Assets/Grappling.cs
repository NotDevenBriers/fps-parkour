using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("References")]
    private PlayerMovementAdvanced pm;
    private WallRunning wr;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;
    public Sliding sm;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovementAdvanced>();
        wr = GetComponent<WallRunning>();
    }

    // resetting wall run variables if you successfully grapple
    private void ResetWall()
    {
        wr.maxWallJumpLimit = 3;
        wr.wallRunTimer = wr.maxWallRunTime;
        print(wr.maxWallJumpLimit); 
        print(wr.wallRunTimer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey))
         StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
         if (grappling)
            {
                lr.SetPosition(0, gunTip.position);
                ResetWall();
            }
            
        else
            return;
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        sm.canSlide = false;
        grappling = true;
        pm.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
        }
        else
        {
            // If no valid grapple point is found, cancel the grapple
            CancelGrapple();
            return;
        }

        // Check if the gunTip is not null
        if (gunTip != null)
        {
            lr.enabled = true;
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, grapplePoint);

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            // If gunTip is null, cancel the grapple
            CancelGrapple();
        }
    }

private void CancelGrapple()
{
    sm.canSlide = true;
    grappling = false;
    pm.freeze = false;
    grapplingCdTimer = grapplingCd;
    lr.enabled = false;
}

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}