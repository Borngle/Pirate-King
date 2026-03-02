using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour {
    public GameObject container;
    public Image ballistaImage;
    public TextMeshProUGUI ballistaText;
    public Image arrowImage;
    public TextMeshProUGUI arrowText;
    public ShipShoot shipShoot;

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
            shipShoot.usingBallistas = true;
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            ballistaImage.color = Color.white;
            ballistaText.color = Color.black;
            arrowImage.color = new Color(0.6f, 0.6f, 0.6f);
            arrowText.color = Color.white;
            shipShoot.usingBallistas = false;
        }
        Cooldown();
    }

    void Cooldown() {
        float ballistaPercent = 1f - (shipShoot.ballistaTimer / shipShoot.ballistaCooldown);
        float arrowPercent = 1f - (shipShoot.arrowTimer / shipShoot.arrowCooldown);
        ballistaImage.fillAmount = Mathf.Clamp01(ballistaPercent);
        arrowImage.fillAmount = Mathf.Clamp01(arrowPercent);
    }
}
