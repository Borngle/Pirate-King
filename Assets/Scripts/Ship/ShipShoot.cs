using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipShoot : MonoBehaviour {
    public Transform[] leftBallistas;
    public Transform[] leftArrows;
    public Transform[] rightBallistas;
    public Transform[] rightArrows;
    public Camera mainCamera;
    private bool facingLeft;
    public bool usingBallistas {get; set;}
    public float ballistaCooldown {get; set;}
    public float arrowCooldown {get; set;}
    public float ballistaTimer {get; set;}
    public float arrowTimer {get; set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        ballistaCooldown = 2f;
        arrowCooldown = 0.5f;
        ballistaTimer = 0f;
        arrowTimer = 0f;
        usingBallistas = true;
    }

    // Update is called once per frame
    void Update() {
        facingLeft = transform.forward.x < 0;
        if(ballistaTimer > 0f) {
            ballistaTimer -= Time.deltaTime;
        }
        if(arrowTimer > 0f) {
            arrowTimer -= Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space)) {
            Shoot();
        }
    }

    void Shoot() {
        if(usingBallistas) {
           if(ballistaTimer > 0f) {
                return;
            } 
            else
            {
                ballistaTimer = ballistaCooldown;
                // Shoot
            }
        }
        else {
            if(arrowTimer > 0f) {
                return;
            }
            else {
                arrowTimer = arrowCooldown;
                // Shoot
            }
        }
    }
}

