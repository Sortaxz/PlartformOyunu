using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRb2D;
    int dead = 0;
    public  bool isEnemyDead = false;
    BoxCollider2D enemyBoxCollider;
    private void Awake() 
    {
        enemyRb2D = GetComponent<Rigidbody2D>();    
        enemyBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player") || other.collider.CompareTag("fireball")  && dead !=1 )
        {
            dead++;
            isEnemyDead = true;
        }   

        
    }
}
