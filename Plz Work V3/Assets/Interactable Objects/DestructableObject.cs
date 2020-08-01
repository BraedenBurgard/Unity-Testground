using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{

    public int health;
    // Start is called before the first frame update
    public void Hit(int damage)
    {
        health -= damage;

        //do any destruction effects

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
