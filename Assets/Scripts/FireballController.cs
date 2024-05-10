using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    private Rigidbody2D fireballRb2D;
    private GameManager gameManager;
    Vector3 yon;
    public float fireballSpeed =0f;
    public bool birKereYonAlindi = false;
    private bool isCollider = false;
    public bool IsCollider
    {
        get
        {
            return isCollider;
        }
    }
    private void Awake() 
    {
        fireballRb2D = GetComponent<Rigidbody2D>();    
        gameManager = GameManager.Instance;
        fireballSpeed = gameManager.FireballSpeed;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        FireballMovement();
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject,1);
            isCollider = true;
        }    

        if(other.collider.CompareTag("obstacle"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    

    private void FireballMovement()
    {
        if(!birKereYonAlindi)
        {
            if (gameManager.mainCharacter.fireballLocalScale)
            {
                yon = Vector3.left;
            }
            else
            {
                yon = Vector3.right;
            }

            birKereYonAlindi = true;
            fireballRb2D.AddForce(yon* fireballSpeed);
        }
        
    }
}
