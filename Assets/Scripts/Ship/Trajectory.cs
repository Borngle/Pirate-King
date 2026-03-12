using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour {
    public LineRenderer lineRendererPrefab;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    public int linePoints = 500;
    public float interval = 0.5f;

    public void Predict(Vector3[] origins, float shootForce, Vector3[] directions) {
        while (lineRenderers.Count < origins.Length) {
            LineRenderer lineRenderer = Instantiate(lineRendererPrefab, transform);
            lineRenderers.Add(lineRenderer);
        }
        for(int i = origins.Length; i < lineRenderers.Count; i++) {
            lineRenderers[i].enabled = false;
        }
        for(int i = 0; i < origins.Length; i++) {
            Draw(lineRenderers[i], origins[i], shootForce, directions[i]);
        }
    }

    public void Draw(LineRenderer lineRenderer, Vector3 origin, float shootForce, Vector3 direction) {
        lineRenderer.positionCount = linePoints;
        Vector3 velocity = direction * shootForce;
        for(int i = 0; i < linePoints; i++) {
            float time = i * interval;
            float x = origin.x + velocity.x * time;
            // y = y + velocity * time + 1/2 * gravity * time^2;
            float y = origin.y + velocity.y * time + 0.5f * Physics.gravity.y * time * time;
            float z = origin.z + velocity.z * time;
            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }
        lineRenderer.enabled = true;
    }

    public void Hide() {
        for(int i = 0; i < lineRenderers.Count; i++) {
            lineRenderers[i].enabled = false;
        }
    }
}
