// Tristan Caetano, Samuel Rouillard, Elijah Karpf
// Descend Project
// CIS 464 Project 1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that deals with spell interactibility
public class EnemySpell : MonoBehaviour
{

// Explosion effect after the spell hits something
public GameObject impactEffect;
GameObject mainEnemy;
public Collider2D thisCollider;
public Collider2D enemyCollider;

// Speed of the spell
public float speed = 20f;

// How much mana the spell uses
public int manaUsage;

// How much damage the spell uses
public int damage;

// Determines how long the spell lasts for and linger
public bool collateral;
public bool linger;
bool lingerTimer = true;

// Used to make sure if the spell hits a wall it doesnt go through it
float initSpeed;

// Destroys GO after this amount of time
public float spellAliveDur;

// Spell's rigidbody
public Rigidbody2D rb;
public bool bounce;

// Linger enemy info
PlayerAttributes lingerPlayer;

    // Making sure the spell flies in the right direction, and then destroys it if it hasnt hit anything in 10 seconds
    void Start(){

        while(mainEnemy == null && bounce){
            mainEnemy = GameObject.Find("The_Sister");
            enemyCollider = mainEnemy.GetComponent<Collider2D>();
        }

        if(bounce){Physics2D.IgnoreCollision(thisCollider, enemyCollider);}

        rb.velocity = transform.right * speed;
        initSpeed = rb.velocity.magnitude;
        Destroy(gameObject, spellAliveDur);
    }

    void Update(){

        if(rb.velocity.magnitude < initSpeed){
            // When the player is hit, the explosion effect plays on the impact location
            GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(impact, 5f);

            if(!collateral){
                // Destroys the spell
                Destroy(gameObject);
            }
        }

        if(lingerTimer){
            // Ignoring the players hitbox
            if(lingerPlayer.tag != "Enemies"){

                    // If an enemy is found, they take damage
                    if(lingerPlayer != null){
                        lingerPlayer.TakeDamage(damage);
                        // FindObjectOfType<AudioManager>().Play("Spell Hit");
                    }

                    // When the player is hit, the explosion effect plays on the impact location
                    GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(impact, 5f);
                
            } else{
                return;
            }
        }
    }

    // If the spell hits something
    void OnTriggerEnter2D(Collider2D hitInfo){

        if(!linger){
            // Getting the enemy info
            PlayerAttributes player = hitInfo.GetComponent<PlayerAttributes>();

            // Ignoring the players hitbox
            if(player.tag != "Enemies"){

                    // If an enemy is found, they take damage
                    if(player != null){
                        player.TakeDamage(damage);
                        FindObjectOfType<AudioManager>().Play("Spell Hit");
                    }

                    // When the player is hit, the explosion effect plays on the impact location
                    GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(impact, 5f);

                    if(!collateral){
                        // Destroys the spell
                        Destroy(gameObject, .1f);
                    }
                
            } else{
                return;
            }
            // TODO: implement linger for boss
        } else {
            // Getting the enemy info
            lingerPlayer = hitInfo.GetComponent<PlayerAttributes>();
            lingerTimer = true;
        }
    }

    // If the spell hits something
    void OnTriggerExit2D(Collider2D hitInfo){lingerPlayer = null; lingerTimer = false;}
}
