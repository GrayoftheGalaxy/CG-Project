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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

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

        if(lightOn) { //If light is on, cast ray
            if(Physics.Raycast(ray, out hit, 50f)){
            Debug.Log(hit.transform.gameObject.tag);
            if(hit.transform.gameObject.CompareTag("DoorGreen") && light.color == new Color32(0, 255, 0, 255)) { //If we hit DoorGreen and our color is green
                hit.collider.gameObject.GetComponent<Animation>().Play("DoorOpenGreen"); //Play the door's opening animation
            } else if(hit.transform.gameObject.CompareTag("DoorRed") && light.color == new Color32(255, 0, 0, 255)) { //If we hit DoorRed and our color is red
                hit.collider.gameObject.GetComponent<Animation>().Play("DoorOpenRed"); //Play the door's opening animation
            }
            Debug.DrawRay(transform.position, transform.forward * 50f, Color.green);
        }
        }
    }
}