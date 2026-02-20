using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour {
    private InputSystemActions actions;
    private float mouseX;
    private Vector3 offset;
    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform defaultTransform;
    [SerializeField] [Range(0.01f, 1.0f)] private float smoothFactor = 0.1f;

    private void Awake() {
        actions = new InputSystemActions();
        actions.Ship.MouseMovement.performed += ctx => mouseX = ctx.ReadValue<float>();
        offset = transform.position - target.position;
    }

    private void OnEnable() {
        actions.Enable();
    }

    private void OnDisable() {
        actions.Disable();
    }

    private void LateUpdate() {
        if (Mouse.current.rightButton.isPressed) {
            Rotate(mouseX, rotationSpeed);
        }
        else {
            // Smoothly return to default position
            transform.position = Vector3.Lerp(
                transform.position,
                defaultTransform.position,
                returnSpeed * Time.deltaTime
            );
            offset = transform.position - target.position;
        }
        transform.LookAt(target);
    }

    private void Rotate(float input, float speed) {
        Quaternion rotation = Quaternion.AngleAxis(
            input * speed * Time.deltaTime,
            Vector3.up
        );
        offset = rotation * offset;
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothFactor
        );
    }
}