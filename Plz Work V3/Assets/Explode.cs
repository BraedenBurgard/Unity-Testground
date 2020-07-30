using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] private GameObject createThis;
    [SerializeField] private Transform Spawnpoint;
    [SerializeField] private float lifeSpan;


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

    
    void OnTriggerStay()
    {
        GameObject explosion = (GameObject)Instantiate(createThis, 
                                            Spawnpoint.position, 
                                            createThis.transform.localRotation);
        Destroy(gameObject);
    }
}
