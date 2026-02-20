using UnityEngine;

public class ShipMove : MonoBehaviour {       
    public float speed = 12f;            
    public float turnSpeed = 45f;       
    private Rigidbody Rigidbody;         
    private Vector2 moveInput;    
    private InputSystemActions actions;       

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
        actions = new InputSystemActions();
    }

    private void OnEnable () {
        Rigidbody.isKinematic = false;
        actions.Ship.Enable();
    }

    private void OnDisable () {
        Rigidbody.isKinematic = true;
        actions.Ship.Disable();
    }

    private void Update() {
        moveInput = actions.Ship.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        Move();
        Turn();
    }

    private void Move() {
        float forward = Mathf.Max(0, moveInput.y);
        Vector3 movement = transform.forward * forward * speed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }


    private void Turn() {
        float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
    }
}
