using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    private PolygonCollider2D passeblePolygonCollider2D;
    [SerializeField] private BoxCollider2D passebleBoxCollide2D;
    [SerializeField] private bool movementDirectionUp;
    public bool MovementDirectionUp { get=> movementDirectionUp; set=> movementDirectionUp = value; }
    private void Awake()
    {
        PassableFloorColliderControl();
    }
    private void Start() 
    {
           
    }
    private void PassableFloorColliderControl()
    {
        if (transform.tag == "PassableFloor")
        {
            passebleBoxCollide2D = GetComponent<BoxCollider2D>();
            passeblePolygonCollider2D = GetComponent<PolygonCollider2D>();
        }
    }

   
   

    #region  Control with Collision and Trigger
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "SlidingFloor")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 12;
            }
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
        if(other.CompareTag("Enemy"))
        {
            if(transform.tag == "PassableFloor")
            {
                passeblePolygonCollider2D.isTrigger =true;
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
        if(other.CompareTag("Enemy"))
        {
            if(transform.tag == "PassableFloor")
            {
                passeblePolygonCollider2D.isTrigger =true;
            }
        } 
    }
    #endregion
}
