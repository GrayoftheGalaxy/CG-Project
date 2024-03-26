using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    private bool lightOn = true;
    private int lightColor = 0;

    void Start() {
        light = GetComponent<Light>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) { //turn on/off the light
            lightOn = !lightOn;
            light.enabled = !light.enabled;
        }

        if(Input.GetMouseButtonDown(1)) {
            if (lightColor == 0) { //if Green light is on 
                lightColor = 1;
                light.color = new Color32(255, 0, 0, 255); //swap to Red light color
            } 
            else { //if Red light is on
                lightColor = 0;
                light.color = new Color32(0, 255, 0, 255); //swap to Green color
            }
        }
    }
}