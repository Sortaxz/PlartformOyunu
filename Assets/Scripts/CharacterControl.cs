using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    Vector3 movement=  Vector3.zero;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float characterSpeed; 
    int jumpCounter = 0;
    bool isToLeft = false;
    bool isToRight = false;
    bool isJumping = false;
    public bool isCharacterAbove  =false;
    public bool isCharacterSlidDown = false;
    private void Awake() 
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        MovementInputController();
        if(Input.GetKeyDown(KeyCode.Space) && jumpCounter != 2)
        {
            isJumping = true;
            isCharacterAbove  =true;
            jumpCounter++;
        }
    }

    private void FixedUpdate() 
    {
        CharacterMovement();    
        JumpingMovement();
    }
   

    void JumpingMovement()
    {
        if(isJumping)
        {
            rb2D.AddForce(Vector2.up * jumpingPower);
            isJumping =false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider)
        {
            jumpCounter = 0;
            isCharacterAbove = false;
        }    
    }
    private void MovementInputController()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isToLeft = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isToLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            isToRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isToRight = false;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            isCharacterSlidDown  =true;
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            isCharacterSlidDown = false;
        }
        
    }

    
    void CharacterMovement()
    {
        if(isToLeft)
        {
            movement = new Vector3(-characterSpeed,0,0);
            transform.position += movement * Time.fixedDeltaTime;
            transform.localScale = new Vector3(-1,1,1);
        }
        if(isToRight)
        {
            movement =new Vector3(characterSpeed,0,0);
            transform.position += movement * Time.fixedDeltaTime;
            transform.localScale = new Vector3(1,1,1);
        }
    }
}
