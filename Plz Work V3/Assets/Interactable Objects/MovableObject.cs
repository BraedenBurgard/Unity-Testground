using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float boopPercent;
    public float keepMomentumOnBoop;
    public Rigidbody rigidBody;


    //called when hit by a collider that can boop it
    public void Boop(Vector3 boop)
    {
        rigidBody.velocity = (rigidBody.velocity*keepMomentumOnBoop) + (boop*boopPercent);
    }
}
