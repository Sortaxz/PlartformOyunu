using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingFloorController : MonoBehaviour
{
    private float characterFirstSpeed = 0F;
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
                characterFirstSpeed = other.transform.GetComponent<CharacterControl>().CharacterSpeed;
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 12;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "SlidingFloor")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = characterFirstSpeed;
            }
        }
    }
}
