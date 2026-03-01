using System;
using UnityEngine;

public class ShipShoot : MonoBehaviour {
    public Transform[] leftBallistas;
    public Transform[] leftArrows;
    public Transform[] rightBallistas;
    public Transform[] rightArrows;
    public Camera mainCamera;
    public bool ballistasWaiting;
    public bool arrowsWaiting;
    private bool facingLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        facingLeft = transform.forward.x < 0;
    }
}

