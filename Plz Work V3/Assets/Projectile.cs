using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody defaultProjectile;
    public Transform Spawnpoint;

    private Rigidbody projectile;
    private Vector3 forwardVector;
    private Vector3 rightVector;
    private Vector3 upVector;
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
            forwardVector = Spawnpoint.TransformDirection (Vector3.forward);
            rightVector = Quaternion.AngleAxis(-90, Vector3.up) * forwardVector;
            upVector = Quaternion.AngleAxis(-45, Vector3.forward) * forwardVector;
            Rigidbody clone;
            characterForwardVector = transform.localRotation*Vector3.forward;
            cameraForwardVector = Camera.main.transform.localRotation * Vector3.forward;
            characterForwardVector.y = cameraForwardVector.y;

            clone = (Rigidbody)Instantiate(projectile, 
                                            Spawnpoint.position + characterForwardVector*2 + rightVector*2, 
                                            projectile.rotation);

            //clone.velocity = Spawnpoint.TransformDirection (Vector3.forward*20) + cameraForwardVector*20;
            clone.velocity = characterForwardVector*20;
            
        }
    }
}
