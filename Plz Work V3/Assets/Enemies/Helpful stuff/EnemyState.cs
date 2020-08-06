using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class should ONLY BE INHERITED

//it will control the AI of the enemy, and the animator

public class EnemyState : MonoBehaviour
{

    public float health;

    public bool playerDetected;


    //store the max value of parameters that can change
    protected float maxHealth;
    protected float maxTick;
    protected Animator animator;
    protected Transform player;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }



    public virtual void UpdateState() {}

    public virtual void PlayerDetected() {}
}
