using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody defaultProjectile;
    public Transform Spawnpoint;

    private Rigidbody projectile;
    private Vector3 cameraForwardVector;
    private Vector3 characterForwardVector;

    // Start is called before the first frame update
    void Start()
    {
        projectile = defaultProjectile;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Rigidbody clone;
            characterForwardVector = transform.localRotation*Vector3.forward;
            cameraForwardVector = Camera.main.transform.localRotation * Vector3.forward;
            characterForwardVector.y = cameraForwardVector.y;
           

            //convert character vector to the direction I want to shoot
            //I had to write this on paper to figure out the math :/
            bool xneg = false;
            bool zneg = false;
            if(characterForwardVector.x < 0) {xneg = true;}
            if(characterForwardVector.z < 0) {zneg = true;}
            float remainingMagnitude = 1 - characterForwardVector.y * characterForwardVector.y;
            float ratio = Mathf.Abs(characterForwardVector.x / characterForwardVector.z);
            characterForwardVector.z = Mathf.Sqrt(remainingMagnitude / (ratio*ratio + 1));
            characterForwardVector.x = characterForwardVector.z * ratio;
            if(xneg) {characterForwardVector.x *= -1;}
            if(zneg) {characterForwardVector.z *= -1;}
            
            clone = (Rigidbody)Instantiate(projectile, 
                                            Spawnpoint.position + characterForwardVector/2, 
                                            projectile.rotation);
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<CharacterController>());


            //clone.velocity = Spawnpoint.TransformDirection (Vector3.forward*20) + cameraForwardVector*20;
            clone.velocity = characterForwardVector*20;
            
        }
    }
}
