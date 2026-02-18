﻿using Ditzelgames;
using UnityEngine;

public class Float : MonoBehaviour {
    public float airDrag = 1;
    public float waterDrag = 10;
    public bool affectDirection = true;
    public bool attachToSurface = false;
    public Transform[] floatPoints;
    protected Rigidbody Rigidbody;
    protected Waves waves;
    protected float waterLine;
    protected Vector3[] waterLinePoints;
    protected Vector3 smoothVectorRotation;
    protected Vector3 targetUp;
    protected Vector3 centerOffset;
    public Vector3 center {get{return transform.position + centerOffset;}}

    void Awake() {
        waves = FindAnyObjectByType<Waves>();
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;
        waterLinePoints = new Vector3[floatPoints.Length];
        for (int i = 0; i < floatPoints.Length; i++) {
            waterLinePoints[i] = floatPoints[i].position;
        }
        centerOffset = PhysicsHelper.GetCenter(waterLinePoints) - transform.position;
    }

    void FixedUpdate() {
        var newWaterLine = 0f;
        var pointUnderWater = false;
        for (int i = 0; i < floatPoints.Length; i++) {
            waterLinePoints[i] = floatPoints[i].position;
            waterLinePoints[i].y = waves.GetHeight(floatPoints[i].position);
            newWaterLine += waterLinePoints[i].y / floatPoints.Length;
            if (waterLinePoints[i].y > floatPoints[i].position.y) {
                pointUnderWater = true;
            }
        }
        var waterLineDelta = newWaterLine - waterLine;
        waterLine = newWaterLine;
        targetUp = PhysicsHelper.GetNormal(waterLinePoints);
        var gravity = Physics.gravity;
        Rigidbody.linearDamping = airDrag;
        if (waterLine > center.y) {
            Rigidbody.linearDamping = waterDrag;
            if (attachToSurface) {
                Rigidbody.position = new Vector3(Rigidbody.position.x, waterLine - centerOffset.y, Rigidbody.position.z);
            }
            else {
                gravity = affectDirection ? targetUp * -Physics.gravity.y : -Physics.gravity;
                transform.Translate(Vector3.up * waterLineDelta * 0.9f);
            }
        }
        Rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(waterLine - center.y), 0, 1));
        if (pointUnderWater) {
            targetUp = Vector3.SmoothDamp(transform.up, targetUp, ref smoothVectorRotation, 0.2f);
            Rigidbody.rotation = Quaternion.FromToRotation(transform.up, targetUp) * Rigidbody.rotation;
        }
    }
}