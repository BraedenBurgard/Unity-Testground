using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlimeState : EnemyState
{
    [Tooltip("How far should the enemy stand from the player while in combat?")]
    public float combatRange;
    public float movementSpeed; 

    //this represents the different states of the slime
    // 0 = idle
    // 10 = walking
    // 20 = idle goofing around
    // 21 = noticing player
    // 30 = idle combat
    // 40 = walking combat 
    // 50 = attacking
    // 60 = in hit lag
    // 70 = dying
    private int state = 1;

    //cooldowns on the enemy's actions
    public float attackCooldown;
    private float maxAttackCooldown;
    private float animationWait = 0;
    private Rigidbody rigidbody;

    void Start()
    {
        maxHealth = health;
        rigidbody = GetComponent<Rigidbody>();
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
            case 0:
                break;

            //noticing player. Play for .5 seconds then move to combat
            case 21: 
                animationWait -= Time.deltaTime;
                if(animationWait < 0)
                {
                    animationWait = 0f;
                    state = 30; //combat state
                }
                break;
            
            //idle combat state. Decrease attack cooldown, switch to walking combat state if too far
            case 30:
                transform.LookAt(player);
                transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                //out of range
                if(Vector3.Distance(player.position, transform.position) > combatRange)
                {
                    state = 40; //combat state
                }
                //in range, can't attack yet
                else if(attackCooldown > 0) {attackCooldown -= Time.deltaTime;}
                //in range, attack
                else
                {
                    attackCooldown = maxAttackCooldown;
                    animationWait = 1f;
                    state = 50;
                }

                break;

            //walking combat. Move to enemy, switch to idle combat state if close enough. Decrease attack cooldown
            case 40:
                transform.LookAt(player);
                if(Vector3.Dot(rigidbody.velocity, transform.forward) < movementSpeed * movementSpeed)
                {
                    rigidbody.velocity += transform.forward * movementSpeed * Time.deltaTime * 10;
                }
                if(Vector3.Distance(player.position, transform.position) <= combatRange)
                {
                    state = 30; //idle combat state
                }
                if(attackCooldown > 0) {attackCooldown -= Time.deltaTime;}
                break;

            //attacking. Try to shmack the player, then eventually return to idle combat state
            case 50:
                animationWait -= Time.deltaTime;
                if(animationWait < 0)
                {
                    animationWait = 0f;
                    state = 30;
                }
                break;
        }


        //send current state information to the animator
        animator.SetInteger("state", state);
    }

    //called from ChildVisionEnter when a player is detected
    public override void PlayerDetected() 
    {
        Debug.Log("player detected");
        playerDetected = true;
        if(state == 1) 
        {
            state = 21; 
            animationWait = 1.5f;
        } //if idle, set to combat state
    }


}
