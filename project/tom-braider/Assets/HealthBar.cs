using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Color Healthy;
    public Color Unhealthy;

    private Slider HealthSlider;
    private Image HealthImage;

    // Start is called before the first frame update
    void Start()
    {
        HealthImage = transform.Find("Health").GetComponent<Image>();
        HealthSlider = GetComponent<Slider>();
        SetHealth(1f);
    }

    public void SetHealth(float t) {
        Debug.Log("Setting health " + t);
        HealthSlider.value = t;
        SetColor(t);
    }

    public void SetColor(float t) {
        HealthImage.color = Color.Lerp(Unhealthy, Healthy, t);
    }
}
