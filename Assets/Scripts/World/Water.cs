using UnityEngine;
using System;

public class Water : MonoBehaviour {
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

    [Serializable]
    public struct Octave {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }
}