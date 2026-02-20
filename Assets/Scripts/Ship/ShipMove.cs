using UnityEngine;

public class ShipMove : MonoBehaviour {       
    public float speed = 12f;            
    public float turnSpeed = 45f;       
    private string movementAxisName;     
    private string turnAxisName;         
    private Rigidbody Rigidbody;         
    private float movementInputValue;    
    private float turnInputValue;        

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable () {
        Rigidbody.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void OnDisable () {
        Rigidbody.isKinematic = true;
    }

    private void Start() {
        movementAxisName = "Vertical";
        turnAxisName = "Horizontal";
    }
    

    private void Update() {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
    }

    private void FixedUpdate() {
        Move();
        Turn();
    }

    private void Move() {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }


    private void Turn() {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
    }
}
