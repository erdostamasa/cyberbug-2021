using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    [Header("Setup")]
    [SerializeField] Transform orientation;
    [SerializeField] TextMeshProUGUI debugText;
    [SerializeField] float gravityStrength = 30f;
    Rigidbody body;
    bool debugTextPresent;
    
    [Header("Movement")]
    [SerializeField] float acceleration = 80f;
    [SerializeField] float maxSpeed = 8f;
    [SerializeField] float airSpeedMultiplier = 0.5f;
    [SerializeField] float counterMovement = 6f;
    [SerializeField] float maxSlopeAngle = 45f;
    float slopeAngle;
    float verticalInput;
    float horizontalInput;
    const float stopSlidingThreshold = 0f;

    [Header("Jumping")]
    //[SerializeField] int walkableLayer;
    [SerializeField] float jumpVelocity = 10f;
    [SerializeField] float timeToJumpAfterFalling = 0.1f;
    public bool wantsToJump;
    public bool wasOnGround;
    public bool canJump;
    public bool onGround;

    [Header("Ground snapping")]
    [Tooltip("Physics steps after leaving ground until snapping is allowed")] [SerializeField]
    int stepsUntilSnapToGround = 3;
    [Tooltip("Downward raycast distance. Determines if we should snap to the ground")] [SerializeField]
    float groundSnappingDistance = 1.1f;
    int stepsSinceLastGrounded;
    bool shouldSnap;
    
    void Start(){
        if (debugText != null) debugTextPresent = true;
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        ReadInput();
    }

    void FixedUpdate(){
        UpdateState();
        AdjustDirectionOnSlope();
        HandleMovement();
        SnapToGround();
        
        HandleJumping();
        // Debug display
        if (debugTextPresent){
            var horizontalVelocity = new Vector2(body.velocity.x, body.velocity.z);
            debugText.text = "Angle: " + slopeAngle.ToString("N1")
                                       + "\nNormal: " + surfaceNormal
                                       + "\nonGround: " + onGround
                                       + "\nVelocity: " + horizontalVelocity.magnitude.ToString("N5")
                                       + "\nShouldSnap: " + shouldSnap;
        }
        
        // Reset values at the end of every physics update
        wasOnGround = onGround;
        onGround = false;
        slopeAngle = 90f; // Reset contact angle    
    }

    void DisableJump(){
        canJump = false;
    }
    
    void UpdateState(){
        stepsSinceLastGrounded += 1;
        onGround = slopeAngle <= maxSlopeAngle;
        if (onGround) canJump = true;
        if (onGround){
            stepsSinceLastGrounded = 0;
        }
        else{
            surfaceNormal = transform.up;
        }
    }

    Vector3 snappingForward;
    Vector3 snappingRight;

    void SnapToGround(){
        // Don't even try to snap if player wants to jump
        if (wantsToJump) return;

        // Don't snap if we moved away quickly
        if (stepsSinceLastGrounded > stepsUntilSnapToGround) return;

        // Don't snap if we dont have anything under us
        RaycastHit hit;
        if (!Physics.Raycast(body.position, -1 * transform.up, out hit, groundSnappingDistance)) return;

        // Don't snap if ground inclination is too high
        if (Vector3.Angle(transform.up, hit.normal) > maxSlopeAngle) return;

        // At this point we unintentionally lost contact with
        // the ground and should snap to it.
        
        //surfaceNormal = hit.normal;
        float speed = body.velocity.magnitude;
        float dot = Vector3.Dot(body.velocity, hit.normal);
        if (dot > 0) body.velocity = (body.velocity - hit.normal * dot).normalized * speed;
        onGround = true;
    }

    void ReadInput(){
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        wantsToJump = Input.GetButton("Jump"); // Prevent forgetting true value physics frame
    }

    /*void OnCollisionEnter(Collision other){
        EvaluateCollision(other);
    }*/

    void OnCollisionStay(Collision other){
        EvaluateCollision(other);
    }

    // Find surface with the smallest angle
    // TODO: calculate average normal is multiple surfaces present
    Vector3 surfaceNormal;
    void EvaluateCollision(Collision other){
        foreach (var contact in other.contacts){
            var normal = contact.normal;
            var angle = Vector3.Angle(Vector3.up, normal);
            if (angle < slopeAngle){
                slopeAngle = angle;
                surfaceNormal = normal;
            }
        }
    }
    
    // Always move parallel to the ground
    Vector3 adjustedForward;
    Vector3 adjustedRight;
    void AdjustDirectionOnSlope(){
        // Only adjust angles when on ground
        if (onGround){
            adjustedForward = Vector3.ProjectOnPlane(orientation.forward, surfaceNormal).normalized;
            adjustedRight = Vector3.ProjectOnPlane(orientation.right, surfaceNormal).normalized;
        }
        else{
            adjustedForward = orientation.forward;
            adjustedRight = orientation.right;
        }
    }
    
    void HandleJumping(){
        if (!onGround && wasOnGround){
            Invoke(nameof(DisableJump), timeToJumpAfterFalling);
        }
        
        if (wantsToJump && canJump){
            wantsToJump = false;
            canJump = false;
            body.velocity = new Vector3(body.velocity.x, jumpVelocity, body.velocity.z);
        }
    }

    void HandleMovement(){
        float _moveSpeed;
        float _maxSpeed;
        float _counterMovement;

        if (onGround){
            _moveSpeed = acceleration;
            _maxSpeed = maxSpeed;
            _counterMovement = counterMovement;
        }
        else{
            _moveSpeed = acceleration * airSpeedMultiplier;
            _maxSpeed = maxSpeed * airSpeedMultiplier;
            _counterMovement = counterMovement * airSpeedMultiplier;
        }

        // Prevent faster than normal diagonal movement
        var horizontalVelocity = new Vector2(body.velocity.x, body.velocity.z);
        if (horizontalVelocity.magnitude >= maxSpeed){
            var limitedHorizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            body.velocity = new Vector3(limitedHorizontalVelocity.x, body.velocity.y, limitedHorizontalVelocity.y);
        }

        // Calculate velocity relative to look direction
        var relativeVel = orientation.InverseTransformDirection(body.velocity);
        
        // Stop sliding if there is no movement input
        if (Mathf.Abs(relativeVel.x) > stopSlidingThreshold && Mathf.Abs(horizontalInput) < 0.05f){
            body.AddForce(adjustedRight * (_moveSpeed * Time.fixedDeltaTime * -relativeVel.x * _counterMovement));
        }
        if (Mathf.Abs(relativeVel.z) > stopSlidingThreshold && Mathf.Abs(verticalInput) < 0.05f){
            body.AddForce(adjustedForward * (_moveSpeed * Time.fixedDeltaTime * -relativeVel.z * _counterMovement));
        }

        // Limit maximum speed
        if (horizontalInput > 0 && relativeVel.x > _maxSpeed) horizontalInput = 0;
        if (horizontalInput < 0 && relativeVel.x < -_maxSpeed) horizontalInput = 0;
        if (verticalInput > 0 && relativeVel.z > _maxSpeed) verticalInput = 0;
        if (verticalInput < 0 && relativeVel.z < -_maxSpeed) verticalInput = 0;

        // Calculate & apply gravity
        Vector3 gravityDirection;
        if (onGround){
            // project down to surface normal
            var projeciton = Vector3.ProjectOnPlane(-1 * transform.up, surfaceNormal);

            // get cross product of projection & down
            var perpendicularVector = Vector3.Cross(projeciton, -1 * transform.up);

            // rotate down slope degrees around cross product
            gravityDirection = (Quaternion.AngleAxis(slopeAngle, perpendicularVector) * transform.up * -1).normalized * gravityStrength;
        }
        else{
            gravityDirection = transform.up * (-1 * gravityStrength);
        }
        body.AddForce(gravityDirection);
        
        // Apply movement
        body.AddForce(adjustedForward * (verticalInput * acceleration));
        body.AddForce(adjustedRight * (horizontalInput * acceleration));

        // Display gravity direction
        //Debug.DrawLine(body.position, body.position + gravityDirection, Color.red);
        // Display slope-adjusted vectors in scene view 
        //Debug.DrawLine(transform.position, transform.position + adjustedForward * 2, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + adjustedRight * 2, Color.red);
    }
}