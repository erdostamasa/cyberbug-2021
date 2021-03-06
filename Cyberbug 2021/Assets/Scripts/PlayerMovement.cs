using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    // Setup
    [SerializeField] Transform orientation;
    Rigidbody body;
    public TextMeshProUGUI debugText;
    bool debugTextPresent = false;

    // Movement
    [SerializeField] float moveSpeed = 80f;
    [SerializeField] float maxSpeed = 8f;
    [SerializeField] float airSpeedMultiplier = 0.5f;
    [SerializeField] float counterMovement = 6f;
    [SerializeField] float maxSlopeAngle = 35f;
    float slopeAngle;
    float verticalInput;
    float horizontalInput;
    float threshold = 0.01f;

    // Jumping
    [SerializeField] int walkableLayer;
    [SerializeField] float jumpVelocity = 10f;
    [SerializeField] float timeToJumpAfterFalling = 0.1f;
    public bool wantsToJump = false;
    public bool wasOnGround = false;
    public bool canJump;
    
    
    // Stick to ground
    [Tooltip("Physics steps after leaving ground until snapping is allowed")] [SerializeField]
    int stepsUntilSnapToGround = 5;

    [SerializeField] float gravityStrength = 30f;
    bool onGround = false;

    [Tooltip("Downward raycast distance. Determines if we should snap to the ground")] [SerializeField]
    float groundSnappingDistance = 1.1f;

    int stepsSinceLastGrounded = 0;
    bool canChangeGravityDirection = true;
    Vector3 gravity;

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
        HandleJumping();
        HandleMovement();

        if (onGround) canJump = true;
        if (!onGround && wasOnGround){
            Invoke(nameof(DisableJump), timeToJumpAfterFalling);
        }

        if (debugTextPresent){
            var horizontalVelocity = new Vector2(body.velocity.x, body.velocity.z);
            debugText.text = "Angle: " + slopeAngle.ToString("N1")
                                       + "\nNormal: " + surfaceNormal
                                       + "\nonGround: " + onGround
                                       //+ "\nonWall: " + onWall
                                       //+ "\ninAir: " + onAir
                                       + "\nVelocity: " + horizontalVelocity.magnitude.ToString("N2")
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

    //////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField] bool shouldSnap;

    void UpdateState(){
        stepsSinceLastGrounded += 1;
        onGround = slopeAngle <= maxSlopeAngle;

        shouldSnap = ShouldWeSnapToGround();

        if (onGround || shouldSnap)stepsSinceLastGrounded = 0;
        //TODO: normalize steep walls
        else
            surfaceNormal = transform.up;
    }

    Vector3 snappingForward;
    Vector3 snappingRight;

    bool ShouldWeSnapToGround(){
        // Don't even try to snap if jump is in progress
        if (wantsToJump) return false;

        // Don't snap if we moved away quickly
        if (stepsSinceLastGrounded > stepsUntilSnapToGround) return false;

        // Don't snap if we dont have anything under us
        RaycastHit hit;

        if (!Physics.Raycast(body.position, -1 * transform.up, out hit, groundSnappingDistance))//Debug.DrawLine(body.position, body.position + Vector3.down * groundSnappingDistance, Color.red);
            return false;

        //Debug.DrawLine(body.position, body.position + Vector3.down * groundSnappingDistance, Color.green);

        // Don't snap if there is no ground under us
        if (Vector3.Angle(transform.up, hit.normal) > maxSlopeAngle) return false;

        // At this point we lost contact with the ground

        onGround = true;
        surfaceNormal = hit.normal;

        var speed = body.velocity.magnitude;
        var dot = Vector3.Dot(body.velocity, hit.normal);
        if (dot > 0) body.velocity = (body.velocity - hit.normal * dot).normalized * speed;

        return true;
    }

    void ReadInput(){
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        wantsToJump = Input.GetButton("Jump"); // Prevent forgetting true value physics frame
    }

    void OnCollisionEnter(Collision other){
        EvaluateCollision(other);
    }

    void OnCollisionStay(Collision other){
        EvaluateCollision(other);
    }

    // Find surface with the smallest angle
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

    // Jump if holdin jump && on ground
    // TODO: allow jump after falling from ledge for ~0.5sec
    void HandleJumping(){
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
            _moveSpeed = moveSpeed;
            _maxSpeed = maxSpeed;
            _counterMovement = counterMovement;
        }
        else{
            _moveSpeed = moveSpeed * airSpeedMultiplier;
            _maxSpeed = maxSpeed * airSpeedMultiplier;
            _counterMovement = counterMovement * airSpeedMultiplier;
        }

        if (!onGround && ShouldWeSnapToGround()){
            adjustedForward = snappingForward;
            adjustedRight = snappingRight;
        }

        // Calculate velocity relative to look direction
        var relativeVel = orientation.InverseTransformDirection(body.velocity);

        // Prevent faster diagonal movement
        var horizontalVelocity = new Vector2(body.velocity.x, body.velocity.z);
        if (horizontalVelocity.magnitude >= maxSpeed){
            var limitedHorizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            body.velocity = new Vector3(limitedHorizontalVelocity.x, body.velocity.y, limitedHorizontalVelocity.y);
        }

        // Stop sliding
        if (Mathf.Abs(relativeVel.x) > threshold && Mathf.Abs(horizontalInput) < 0.05f) body.AddForce(adjustedRight * (_moveSpeed * Time.fixedDeltaTime * -relativeVel.x * _counterMovement));

        if (Mathf.Abs(relativeVel.z) > threshold && Mathf.Abs(verticalInput) < 0.05f) body.AddForce(adjustedForward * (_moveSpeed * Time.fixedDeltaTime * -relativeVel.z * _counterMovement));

        // Limit max speed
        if (horizontalInput > 0 && relativeVel.x > _maxSpeed) horizontalInput = 0;
        if (horizontalInput < 0 && relativeVel.x < -_maxSpeed) horizontalInput = 0;
        if (verticalInput > 0 && relativeVel.z > _maxSpeed) verticalInput = 0;
        if (verticalInput < 0 && relativeVel.z < -_maxSpeed) verticalInput = 0;

        // Calculate & apply gravity
        if (canChangeGravityDirection){
            if (onGround && slopeAngle <= maxSlopeAngle){
                // project down to surface normal
                var projeciton = Vector3.ProjectOnPlane(-1 * transform.up, surfaceNormal);

                // get cross product of projection & down
                var perpendicularVector = Vector3.Cross(projeciton, -1 * transform.up);

                // rotate down slope degrees around cross product
                gravity = (Quaternion.AngleAxis(slopeAngle, perpendicularVector) * transform.up * -1).normalized * gravityStrength;
            }
            else{
                gravity = transform.up * (-1 * gravityStrength);
            }
        }

        body.AddForce(gravity);
        Debug.DrawLine(body.position, body.position + gravity, Color.red);

        // Apply movement
        body.AddForce(adjustedForward * (verticalInput * moveSpeed));
        body.AddForce(adjustedRight * (horizontalInput * moveSpeed));

        // Display slope-adjusted vectors in scene view 
        //Debug.DrawLine(transform.position, transform.position + adjustedForward * 2, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + adjustedRight * 2, Color.red);
    }
}