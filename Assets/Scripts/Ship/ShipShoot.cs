using System;
using UnityEngine;

[RequireComponent(typeof(Trajectory))]
public class ShipShoot : MonoBehaviour {
    public Camera mainCamera;
    private bool facingLeft;
    public bool usingBallistas {get; set;}
    private Trajectory trajectory;
    public Weapon ballistas;
    public Weapon arrows;
    public ArcherAnimator archerAnimator;

    [Serializable]
    public class WeaponPositions {
        public Transform[] positions;
        public GameObject projectile;
    }

    [Serializable]
    public class Weapon {
        public WeaponPositions left;
        public WeaponPositions right;
        public float shootForce;
        public float maximumAngle;
        public float angle;
        public float cooldown;
        public float timer;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        ballistas.shootForce = 150;
        arrows.shootForce = 50;
        ballistas.cooldown = 3f;
        arrows.cooldown = 1.5f;
        ballistas.angle = 0;
        ballistas.maximumAngle = 3;
        arrows.angle = 0;
        arrows.maximumAngle = 15;
        usingBallistas = true;
        trajectory = GetComponent<Trajectory>();
    }

    // Update is called once per frame
    void Update() {
        facingLeft = Vector3.Dot(transform.right, mainCamera.transform.forward) < 0;
        if(ballistas.timer > 0f) {
            ballistas.timer -= Time.deltaTime;
        }
        if(arrows.timer > 0f) {
            arrows.timer -= Time.deltaTime;
        }
        Weapon active = GetActiveWeapon();
        if(Input.GetKey(KeyCode.Space)) {
            Shoot();
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            active.angle = Mathf.Clamp(active.angle - 1f, -active.maximumAngle, active.maximumAngle);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            active.angle = Mathf.Clamp(active.angle + 1f, -active.maximumAngle, active.maximumAngle);
        }
        if(Input.GetKey(KeyCode.LeftShift)) {
            if(!usingBallistas) {
                archerAnimator.SetArcherAnimation("Aim", true, facingLeft);
            }
            else {
                archerAnimator.ClearArcherAnimation("Aim");
            }
            Transform[] positions = GetWeaponPositions(active);
            Vector3[] origins = new Vector3[positions.Length];
            Vector3[] directions = new Vector3[positions.Length];
            for(int i = 0; i < positions.Length; i++) {
                origins[i] = positions[i].position;
                if(active.Equals(arrows)) {
                    origins[i].y += 1.1f;
                }
                Vector3 direction = facingLeft ? -transform.right : transform.right;
                float angle = facingLeft ? -active.angle : active.angle;
                direction = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
                if(active.Equals(arrows)) {
                    direction.y += 0.15f;
                    direction.Normalize();
                }
                directions[i] = direction;
            }
            trajectory.Predict(origins, active.shootForce, directions);
        }
        else {
            trajectory.Hide();
            archerAnimator.ClearArcherAnimation("Aim");
        }
    }

    Transform[] GetWeaponPositions(Weapon weapon) {
        if(facingLeft) {
            return weapon.left.positions;
        }
        else {
            return weapon.right.positions;
        }
    }

    Weapon GetActiveWeapon() {
        if(usingBallistas) {
            return ballistas;
        }
        else {
            return arrows;
        }
    }

    void ResetShoot() {
        archerAnimator.SetArcherAnimation("Shoot", false, facingLeft);
    }

    void Shoot() {
        Weapon active = GetActiveWeapon();
        if(usingBallistas) {
           if(ballistas.timer > 0f) {
                return;
            } 
            else {
                ballistas.timer = ballistas.cooldown;
            }
        }
        else {
            if(arrows.timer > 0f) {
                return;
            }
            else {
                arrows.timer = arrows.cooldown;
            }
            archerAnimator.SetArcherAnimation("Shoot", true, facingLeft);
            Invoke("ResetShoot", 0.1f);
        }
        Transform[] positions = GetWeaponPositions(active);
    }
}

