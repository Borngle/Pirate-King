using System;
using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class ShipShoot : MonoBehaviour {
    [Serializable]
    public struct Weapon {
        public Transform[] positions;
        public float cooldown;
        public float timer;
        public GameObject gameObject;
    }

    public Camera mainCamera;
    private bool facingLeft;
    public bool usingBallistas {get; set;}
    private Trajectory trajectory;

    public Weapon leftBallistas;
    public Weapon rightBallistas;
    public Weapon leftArrows;
    public Weapon rightArrows;
    public float shootForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        leftBallistas.cooldown = 3f;
        rightBallistas.cooldown = 3f;
        leftArrows.cooldown = 1.5f;
        rightArrows.cooldown = 1.5f;
        usingBallistas = true;
        trajectory = GetComponent<Trajectory>();
    }

    // Update is called once per frame
    void Update() {
        facingLeft = transform.forward.x < 0;
        ref Weapon active = ref GetActiveWeapon();
        if(active.timer > 0f) {
            active.timer -= Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space)) {
            Shoot();
        }
        if(Input.GetKey(KeyCode.LeftShift)) {
            for(int i = 0; i < active.positions.Length; i++) {
                trajectory.Predict(active.positions[i], shootForce);
            }
        }
    }

    ref Weapon GetActiveWeapon() {
        if (facingLeft) {
            return ref usingBallistas ? ref leftBallistas : ref leftArrows;
        }
        else {
            return ref usingBallistas ? ref rightBallistas : ref rightArrows;
        }
    }

    void Shoot() {
        ref Weapon active = ref GetActiveWeapon();
        if(active.timer > 0f) {
            return;
        }
        active.timer = active.cooldown;
    }
}

