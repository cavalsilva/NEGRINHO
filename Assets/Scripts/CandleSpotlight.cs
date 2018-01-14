using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSpotlight : MonoBehaviour {

    Candle candle;
    Light light;
    
    float spotAngleMax;
    float intensityMax;

    private void Start()
    {
        light = GetComponentInParent<Light>();
        candle = FindObjectOfType<Candle>();

        spotAngleMax = light.spotAngle;
        intensityMax = light.intensity;
    }

    private void Update()
    {
        light.spotAngle = Mathf.Lerp(0, spotAngleMax, candle.lerpT);
        light.intensity = Mathf.Lerp(0, intensityMax, candle.lerpT);

        float angle = (light.spotAngle / 2) * Mathf.Deg2Rad;
        candle.radius = Mathf.Tan(angle) * candle.transform.position.y;
    }

}
