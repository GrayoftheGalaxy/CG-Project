using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    private bool lightOn = true;
    private int lightColor = 0;
    private int maxReflections = 5; //Maximum number of times a ray can reflect.

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
                } 
                else if(hit.transform.gameObject.CompareTag("DoorRed") && light.color == new Color32(255, 0, 0, 255)) { //If we hit DoorRed and our color is red
                    hit.collider.gameObject.GetComponent<Animation>().Play("DoorOpenRed"); //Play the door's opening animation
                } 
                else if(hit.transform.gameObject.CompareTag("Mirror")){ //If we hit a mirror
                    //Begin reflecting, calculate reflection angle with Vector3.Reflect
                    // 1. hit.point is the point where we hit the mirror.
                    // 2. Vector3.Reflect calculates the reflection angle.
                    // 3. 1 is the number of reflections, now 1.
                    //Reflect(transform.forward * hit.distance, Vector3.Reflect(hit.transform.forward, hit.normal), 1); 
                    Reflect(hit.point, Vector3.Reflect(ray.direction, hit.normal), 1);
                }
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green); //Debug ray (Player doess not see this.)
            }
        }

    }

    void Reflect(Vector3 startingPosition, Vector3 direction, int numReflects) {
        if(numReflects < maxReflections) {
            Ray reflect = new Ray(startingPosition, direction);
            RaycastHit reflectHit;
            if(Physics.Raycast(reflect, out reflectHit, 100f)){
                if(reflectHit.transform.gameObject.CompareTag("DoorGreen") && light.color == new Color32(0, 255, 0, 255)) { //If we hit DoorGreen and our color is green
                    reflectHit.collider.gameObject.GetComponent<Animation>().Play("DoorOpenGreen"); //Play the door's opening animation
                } 
                else if(reflectHit.transform.gameObject.CompareTag("DoorRed") && light.color == new Color32(255, 0, 0, 255)) { //If we hit DoorRed and our color is red
                    reflectHit.collider.gameObject.GetComponent<Animation>().Play("DoorOpenRed"); //Play the door's opening animation
                } 
                else if(reflectHit.transform.gameObject.CompareTag("Mirror")){ //If we hit a mirror
                    //Reflect again, iterating the number of reflects.
                    Reflect(reflectHit.point, Vector3.Reflect(reflect.direction, reflectHit.normal), numReflects + 1);
                }
                Debug.DrawRay(reflect.origin, direction * reflectHit.distance, Color.green);
            }
        }
    }
}