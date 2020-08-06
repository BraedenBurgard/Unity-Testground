using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class should ONLY BE INHERITED but I don't know how to make abstract/virtual classes in Unity :P

//it deals with enemy characteristics that do not deal directly with AI/attack

public class EnemyState : MonoBehaviour
{

    public float health;

    [Tooltip("How often the AI will check for updates to its state (in seconds)")]
    public float tick;

    //store the max value of parameters that can change
    private float maxHealth;
    private float maxTick;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxTick = tick;
    }

    void FixedUpdate()
    {
        tick -= Time.deltaTime;
    }

    
}
