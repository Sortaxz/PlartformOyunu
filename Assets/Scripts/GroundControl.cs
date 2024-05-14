using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    [SerializeField] private BoxCollider2D passebleBoxCollide2D;

    private void Awake()
    {
        PassableFloorColliderControl();
    }

    private void PassableFloorColliderControl()
    {
        if (transform.tag == "PassableFloor")
        {
            passebleBoxCollide2D = GetComponent<BoxCollider2D>();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "SlidingFloor")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 12;
            }
        }
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "StonyFloor ")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 4;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "SlidingFloor")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 8;
            }
        }
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "StonyFloor ")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 8;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(transform.tag == "PassableFloor")
            {
                passebleBoxCollide2D.isTrigger = true;
            }
        }    
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
         if(other.CompareTag("Player"))
        {
            if(transform.tag == "PassableFloor")
            {
                passebleBoxCollide2D.isTrigger = false;
            }
        }   
    }
}
