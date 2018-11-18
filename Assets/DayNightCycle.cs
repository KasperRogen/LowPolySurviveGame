using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Complete day-night cycle duration (in seconds).
    public float dayDuration = 10.0f;

    // Read-only property that informs if it is currently night time.
    public bool isNight { get; private set; }

    // Private field with the day color. It is set to the initial light color.
    private Color dayColor;

    // Private field with the hard-coded night color.
    private Color nightColor = Color.white * 0.1f;

    void Start()
    {
        dayColor = GetComponent<Light>().color;
    }

    void Update()
    {
        float lightIntensity = 0.5f +
                      Mathf.Sin(Time.time * 2.0f * Mathf.PI / dayDuration) / 2.0f;
        isNight = (lightIntensity < 0.3);
        GetComponent<Light>().color = Color.Lerp(nightColor, dayColor, lightIntensity);
    }
} // class DayNightCycle
