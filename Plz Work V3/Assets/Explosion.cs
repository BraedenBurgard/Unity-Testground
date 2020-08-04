using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject createThis;
    [SerializeField] private Transform Spawnpoint;
    public float explosionRadius;
    public float boopStrength;
    public int environmentalDamage;
    private int lifeSpan = 7;
    
    //a list of the colliders hit. Stored as pointers for efficiency
    private GameObject[] collidersHit = new GameObject[50];
    private int numCollisions = 0;
    private GameObject explosionEffect;

    void Start()
    {
        GetComponent<SphereCollider>().radius = explosionRadius;
        explosionEffect = (GameObject)Instantiate(createThis, 
                                            Spawnpoint.position, 
                                            createThis.transform.localRotation);
        foreach(Transform child in explosionEffect.GetComponentsInChildren<Transform>())
        {
        child.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        }
    }
    void Update()
    {
        if(lifeSpan == 0) {Destroy(explosionEffect); Destroy(gameObject);}
        lifeSpan--;
    }

    void OnTriggerStay(Collider collider)
    {
        GameObject collidedObject = collider.gameObject;

        //check to see if the object was already hit. If so, return
        for(int i = 0; i < numCollisions && i < 10; i++)
        {
            if(GameObject.ReferenceEquals(collidedObject, collidersHit[i])) {return;}
        }
        //if it hasn't been hit yet, add its GameObject to the array
        collidersHit[numCollisions] = collidedObject;
        numCollisions++;

        //on player hit
        if (collider.gameObject.tag == "Player")
        {
            Vector3 direction = (collider.gameObject.transform.position - transform.position);
            direction.Normalize();

            direction *= boopStrength;

            collider.gameObject.GetComponent<FirstPersonController>().Bounce(direction);
        }

        //on movableobject hit
        else if(collider.gameObject.tag == "Movable")
        {
            Vector3 direction = (collider.gameObject.transform.position - transform.position);
            direction.Normalize();

            direction *= boopStrength;
            collider.gameObject.GetComponent<MovableObject>().Boop(direction);
        }

        else if(collider.gameObject.tag == "Destructable")
        {
            collider.gameObject.GetComponent<DestructableObject>().Hit(environmentalDamage);
        }

    }
}
