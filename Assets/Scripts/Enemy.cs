using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int dead = 0;
    public  bool isEnemyDead = false;
    private void Awake() 
    {
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
