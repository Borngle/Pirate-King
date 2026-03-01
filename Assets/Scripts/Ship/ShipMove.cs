using UnityEngine;

public class ShipMove : MonoBehaviour {       
    public float moveAcceleration = 4f;
    public float turnAcceleration = 15f;
    public float maxMoveSpeed = 12f;            
    public float maxTurnSpeed = 45f;  
    private float currentMoveSpeed = 0f;
    private float currentTurnSpeed = 0f;     
    private float wheelSpinAngle = 0f;
    private Rigidbody Rigidbody;         
    private Vector2 moveInput;    
    private InputSystemActions actions;
    public ParticleSystem waterTrail;
    private AudioSource wind;
    public Transform rudder;
    public Transform wheel;

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
        actions = new InputSystemActions();
        wind = GetComponent<AudioSource>();
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
        float targetSpeed = forward * maxMoveSpeed;
        currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetSpeed, moveAcceleration * Time.fixedDeltaTime);
        if (currentMoveSpeed > 0.05f) {
            if(!wind.isPlaying){
                wind.volume = 0f;
                if (wind.clip != null) {
                    wind.time = Random.Range(0f, wind.clip.length);
                    wind.Play();
                }
            }
            wind.volume = Mathf.MoveTowards(wind.volume, 0.8f, 0.3f * Time.deltaTime);
        }
        else {
            if (waterTrail.isPlaying) {
                waterTrail.Stop();
            }
            wind.volume = Mathf.MoveTowards(wind.volume, 0f, 0.5f * Time.deltaTime);
            if(wind.volume <= 0.01f && wind.isPlaying) {
                wind.Stop();
            }
        }
        Vector3 movement = transform.forward * currentMoveSpeed * Time.fixedDeltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }

    private void Turn() {
        float targetTurnSpeed = moveInput.x * maxTurnSpeed;
        currentTurnSpeed = Mathf.MoveTowards(currentTurnSpeed, targetTurnSpeed, turnAcceleration * Time.fixedDeltaTime);
        float turn = currentTurnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
        float rudderTurn = moveInput.x * 30f;
        rudder.localRotation = Quaternion.Euler(0f, rudderTurn, 0f);
        wheelSpinAngle += moveInput.x * 200f * Time.fixedDeltaTime;
        wheel.localRotation = Quaternion.Euler(0f, 0f, -wheelSpinAngle);
    }
}
