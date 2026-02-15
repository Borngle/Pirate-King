using UnityEngine;

public class Floater : MonoBehaviour {
    Water water;
    public Rigidbody rigidBody;
    // Depth at which objects start to experience buoyancy
    public float depthBeforeSubmerged = 1f;
    // Amount of buoyant force applied
    public float displacementAmount = 3f;
    // Number of points applying buoyant force
    public int floaters = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    void Start() {
        water = FindFirstObjectByType<Water>();
    }

    void FixedUpdate() {
        rigidBody.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);
        float waterHeight = water.GetHeight(transform.position);
        if(transform.position.y < waterHeight) {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rigidBody.AddForceAtPosition(new UnityEngine.Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidBody.AddForce(displacementMultiplier * -rigidBody.linearVelocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidBody.AddTorque(displacementMultiplier * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}