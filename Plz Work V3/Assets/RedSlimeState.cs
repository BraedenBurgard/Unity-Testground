using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlimeState : EnemyState
{
    [Tooltip("How far should the enemy stand from the player while in combat?")]
    public float combatRange;
    public float movementSpeed; 

    //this represents the different states of the slime
    // 1 = idle
    // 2 = in combat (not attacking/getting hit)
    // 3 = attacking
    // 4 = in hit stun
    // 5 = dying
    private int state = 1;

    //cooldowns on the enemy's actions
    public float attackCooldown;
    private float maxAttackCooldown;

    void Start()
    {
        maxHealth = health;
        animator = GetComponent<Animator>();
        player = GameObject.Find("Character").transform;
        maxAttackCooldown = attackCooldown;
    }

    void FixedUpdate()
    {
        Debug.Log(state);
        //handle logic of current state
        switch(state)
        {
            //idle state. Do nothing
            case 1:
                break;
            
            //combat state. Move to enemy, wait if close enough
            case 2:
                transform.LookAt(player);
                //out of range
                if(Vector3.Distance(player.position, transform.position) > combatRange)
                {
                    transform.position += transform.forward * movementSpeed * Time.deltaTime;
                }
                //in range, can't attack yet
                else if(attackCooldown > 0) {attackCooldown -= Time.deltaTime;}
                //in range, attack
                else
                {

                }

                break;
        }


        //send current state information to the animator
        animator.SetBool("playerDetected", playerDetected);
    }

    //called from ChildVisionEnter when a player is detected
    public override void PlayerDetected() 
    {
        Debug.Log("player detected");
        playerDetected = true;
        if(state == 1) {state = 2;} //if idle, set to combat state
    }


}
