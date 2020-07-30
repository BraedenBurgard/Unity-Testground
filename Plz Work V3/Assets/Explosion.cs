using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Explosion : MonoBehaviour
{
    public float boopStrength;

    private bool playerBooped = false;
    private int lifeSpan = 7;
    void Update()
    {
        if(lifeSpan == 0) {Destroy(gameObject);}
        lifeSpan--;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && playerBooped == false)
        {
            Vector3 direction = (collider.gameObject.transform.position - transform.position);
            direction.Normalize();

            direction *= boopStrength;
            Debug.Log(direction.x + " " + direction.y + " " + direction.z);

            collider.gameObject.GetComponent<FirstPersonController>().Bounce(direction);
            playerBooped = true;
        }
    }
}
