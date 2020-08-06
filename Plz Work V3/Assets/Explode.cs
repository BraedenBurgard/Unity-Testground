using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] private GameObject createThis;
    [SerializeField] private Transform Spawnpoint;
    [SerializeField] private float lifeSpan;
    public float bombPower;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeSpan < 0)
        {
            GameObject explosion = (GameObject)Instantiate(createThis, 
                                            Spawnpoint.position, 
                                            createThis.transform.localRotation);
            Destroy(gameObject);
        }
        lifeSpan -= Time.fixedDeltaTime;
    }

    
    void OnTriggerStay(Collider collider)
    {
        if(!(collider.gameObject.tag == "Room") && !(collider.gameObject.tag == "Trigger"))
        {
            GameObject explosion = (GameObject)Instantiate(createThis, 
                                                Spawnpoint.position, 
                                             createThis.transform.localRotation);
            explosion.GetComponent<Explosion>().boopStrength = bombPower;
            Destroy(gameObject);
        }
    }
}
