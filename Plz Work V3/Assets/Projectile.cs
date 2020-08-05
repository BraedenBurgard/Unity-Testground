using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{

    public Rigidbody leftClick;
    public Rigidbody rightClick;
    public float leftClickCooldown;
    public float rightClickCooldown;
    public Transform Spawnpoint;
    public float bombPower;

    private float OGleftCooldown;
    private float OGrightCooldown;
    private Rigidbody projectile;
    private Vector3 cameraForwardVector;
    private Vector3 characterForwardVector;

    // Start is called before the first frame update
    void Start()
    {
        OGleftCooldown = leftClickCooldown;
        OGrightCooldown = rightClickCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        rightClickCooldown -= Time.fixedDeltaTime;
        leftClickCooldown -= Time.fixedDeltaTime;
        if(Input.GetButtonDown("Fire1") && rightClickCooldown <= 0){
            projectile = leftClick;
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
                                            Spawnpoint.position + characterForwardVector*.7f, 
                                            projectile.rotation);
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<CharacterController>());


            //clone.velocity = Spawnpoint.TransformDirection (Vector3.forward*20) + cameraForwardVector*20;
            clone.velocity = characterForwardVector*20;
            
            leftClickCooldown = OGleftCooldown;
        }

        else if(Input.GetButtonDown("Fire2") && rightClickCooldown <= 0){
            projectile = rightClick;
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
                                            Spawnpoint.position + characterForwardVector*.7f, 
                                            projectile.rotation);
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<CharacterController>());
            AdjustBomb(clone);

            //clone.velocity = Spawnpoint.TransformDirection (Vector3.forward*20) + cameraForwardVector*20;
            clone.velocity = characterForwardVector*20;
            
            rightClickCooldown = OGrightCooldown;
        }
    }

    void AdjustBomb(Rigidbody projectile)
    {
        projectile.gameObject.GetComponent<Explode>().bombPower = this.bombPower;
    }
}
