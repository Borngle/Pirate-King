using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 origin;
    private Vector3 direction;
    private float force;
    private float timeElapsed = 0f;
    private Vector3 previousPosition;

    public void Launch(Vector3 direction, float force, Quaternion initialRotation) {
        this.direction = direction;
        this.force = force;
        origin = transform.position;
        previousPosition = transform.position;
        transform.rotation = initialRotation;
    }

    void Update() {
        timeElapsed += Time.deltaTime;
        Vector3 velocity = direction * force;
        float x = origin.x + velocity.x * timeElapsed;
        float y = origin.y + velocity.y * timeElapsed + 0.5f * Physics.gravity.y * timeElapsed * timeElapsed;
        float z = origin.z + velocity.z * timeElapsed;
        Vector3 newPosition = new Vector3(x, y, z);
        Vector3 delta = newPosition - previousPosition;
        if (delta.magnitude > 0f) {
            if (Physics.Raycast(previousPosition, delta.normalized, out RaycastHit hit, delta.magnitude)) { // hit will be used for different behaviour (ship damage)
                Destroy(gameObject);
                return;
            }
        }
        previousPosition = newPosition;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(new Vector3(
            velocity.x,
            velocity.y + Physics.gravity.y * timeElapsed,
            velocity.z
        )) * Quaternion.Euler(-90f, 0f, 180f);
    }
}