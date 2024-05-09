using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    Vector3 movement=  Vector3.zero;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float characterSpeed; 
    private int jumpCounter = 0;
    private bool isToLeft = false;
    private bool isToRight = false;
    private bool isJumping = false;
    public bool isCharacterAbove  =false;
    public bool isCharacterSlidDown = false;
    public bool readyToAttack = false;

    public bool readyToFireballAttack = false;
    
    public bool fireballLocalScale = false;

    private bool isCharacterDead  =false;
    public bool IsCharacterDead
    {
        get
        {
            return isCharacterDead;
        }
    }

    private  int spawnPointSiblingIndex = default;
   
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider)
        {
            jumpCounter = 0;
            isCharacterAbove = false;
        } 

        if(other.collider.CompareTag("obstacle"))
        {
            isCharacterDead = true;
            GameManager.Instance.isCharacterOnPoint = false;
        }
        
    }
    //oyun başlark en son ki temas etti spanw objesinden doğucak
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("SpawnPoint"))
        {
            spawnPointSiblingIndex =  other.transform.GetSiblingIndex();
            PlayerPrefs.SetInt("SpawnPoint",spawnPointSiblingIndex);
        } 
        
    }
    
    void JumpingMovement()
    {
        if(isJumping)
        {
            rb2D.AddForce(Vector2.up * jumpingPower);
            isJumping =false;
        }
    }

    private void MovementInputController()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isToLeft = true;
            fireballLocalScale = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isToLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            fireballLocalScale = false;
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

        if(Input.GetMouseButtonDown(0) )
        {
            if(UIAnimation.Instance.SwordAttackBegin == true)
            {
                readyToAttack = true;
            }
            else
            {
                readyToFireballAttack = true;
                Spawner.Instance.Fireball.GetComponent<FireballController>().birKereYonAlindi = false;
            }
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
