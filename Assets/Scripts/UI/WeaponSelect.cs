using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour {
    public GameObject container;
    public Image ballistaImage;
    public TextMeshProUGUI ballistaText;
    public Image arrowImage;
    public TextMeshProUGUI arrowText;
    public float ballistaCooldown = 2f;
    public float arrowCooldown = 0.5f;
    private float ballistaTimer = 0f;
    private float arrowTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        container.SetActive(false);
        // Ballistas toggled by default
        ballistaImage.color = new Color(0.6f, 0.6f, 0.6f);
        ballistaText.color = Color.white;
        arrowImage.color = Color.white;
        arrowText.color = Color.black;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.LeftShift)) {
            container.SetActive(true);
        }
        else {
            container.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            ballistaImage.color = new Color(0.6f, 0.6f, 0.6f);
            ballistaText.color = Color.white;
            arrowImage.color = Color.white;
            arrowText.color = Color.black;
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            ballistaImage.color = Color.white;
            ballistaText.color = Color.black;
            arrowImage.color = new Color(0.6f, 0.6f, 0.6f);
            arrowText.color = Color.white;
        }
    }
}
