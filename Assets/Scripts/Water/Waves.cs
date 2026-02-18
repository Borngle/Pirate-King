using UnityEngine;
using System;

public class Waves : MonoBehaviour {
    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] displacedVertices;
    public Octave[] octaves;

    void Start() {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        displacedVertices = new Vector3[baseVertices.Length];
    }

    void Update() {
        for (int i = 0; i < baseVertices.Length; i++) {
            Vector3 vertex = baseVertices[i];
            float y = 0f;
            for (int o = 0; o < octaves.Length; o++) {
                if (octaves[o].alternate) {
                    float sampleX = vertex.x * octaves[o].scale.x + Time.time * octaves[o].speed.x;
                    float sampleZ = vertex.z * octaves[o].scale.y + Time.time * octaves[o].speed.y;
                    float perlin = Mathf.PerlinNoise(sampleX, sampleZ) * Mathf.PI * 2f;
                    y += Mathf.Cos(perlin) * octaves[o].height;
                }
            }
            displacedVertices[i] = new Vector3(vertex.x, y, vertex.z);
        }
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }

    public float GetHeight(Vector3 worldPosition) {
        if (displacedVertices == null || displacedVertices.Length == 0) {
            return transform.position.y;
        }
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        float closestDistance = float.MaxValue;
        float height = 0f;
        for (int i = 0; i < displacedVertices.Length; i++) {
            Vector2 vertex = new Vector2(displacedVertices[i].x, displacedVertices[i].z);
            Vector2 point = new Vector2(localPosition.x, localPosition.z);
            float distance = Vector2.SqrMagnitude(vertex - point);
            if (distance < closestDistance) {
                closestDistance = distance;
                height = displacedVertices[i].y;
            }
        }
        return transform.TransformPoint(new Vector3(0, height, 0)).y;
    }

    [Serializable]
    public struct Octave {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }
}