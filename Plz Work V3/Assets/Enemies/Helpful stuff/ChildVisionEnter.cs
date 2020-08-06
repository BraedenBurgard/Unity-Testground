using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildVisionEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<EnemyState>().PlayerDetected();
        }
    }
}
