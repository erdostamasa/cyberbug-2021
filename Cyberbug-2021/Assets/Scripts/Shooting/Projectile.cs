using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
    [Header("Setup")]
    [Tooltip("Projectile will not collide with this layer(s)")]
    [SerializeField] LayerMask ignoreLayer;
    
    [Header("Settings")]
    [Tooltip("Destroy bullet after this many seconds")]
    [SerializeField] float timeout = 10f;
    [SerializeField] float speed = 10f;
    
    [Header("Impact settings")]
    [SerializeField] float explosionRadius = 2f;
    [SerializeField] float explosionForce = 5f;
    [Tooltip("How strong the bullet should push on the object")]
    [SerializeField] float bulletForce = 10f;
    [Tooltip("How strong the explosion should be relative to distace from impact point")]
    [SerializeField] AnimationCurve forceFalloff;

    [SerializeField] int damage = 1;
    
    Vector3 lastFramePosition;

    void Start(){
        // Self destruct after [timeout]
        Invoke(nameof(Delete), timeout);

        lastFramePosition = transform.position;
    }

    void Delete(){
        Destroy(gameObject);
    }

    void FixedUpdate(){
        // Move forward
        transform.Translate(transform.forward * (speed * Time.fixedDeltaTime), Space.World);

        // Raycast between last and current position
        var distance = (transform.position - lastFramePosition).magnitude;
        var direction = (transform.position - lastFramePosition).normalized;

        // Execute effects at impact location
        if (Physics.Raycast(lastFramePosition, direction, out var hit, distance, ~ignoreLayer)){
            IShootable other = hit.collider.GetComponent<IShootable>();
            if(other != null) other.ReceiveProjectile(damage);
            Destroy(this.gameObject);
            
            //BulletImpact(hit);
            Explosion(hit.point);
        }

        lastFramePosition = transform.position;
    }


    // Apply force parallel to the path of bullet
    void BulletImpact(RaycastHit hit){
        var forceDirection = (hit.point - lastFramePosition).normalized;
        var body = hit.collider.GetComponent<Rigidbody>();

        if (body != null){
            body.AddForceAtPosition(forceDirection * bulletForce, hit.point, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }

    // Apply explosion effects at the point of impact
    void Explosion(Vector3 contactPoint){
        // Find objects in explosion radius
        var hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var collider in hitColliders){
            if (collider.gameObject.layer == 11) continue;
            var body = collider.GetComponent<Rigidbody>();

            // Object can be pushed 
            if (body != null){
                var distance = (collider.transform.position - contactPoint).magnitude;
                var direction = (collider.transform.position - contactPoint).normalized;
                var forceMultiplier = forceFalloff.Evaluate(distance / explosionRadius);

                body.AddForce(direction * (explosionForce * forceMultiplier), ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}