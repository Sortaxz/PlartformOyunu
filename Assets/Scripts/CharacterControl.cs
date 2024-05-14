using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    Vector3 movement =  Vector3.zero;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float characterSpeed; 
    public float CharacterSpeed {get { return characterSpeed;} set { characterSpeed = value; } }
    private int jumpCounter = 0;
    private bool isToLeft = false;
    public bool IsToLeft
    {
        get { return isToLeft; }
    }
    private bool isToRight = false;
    private bool isJumping = false;
    public bool isCharacterAbove  =false;
    public bool isCharacterSlidDown = false;
    public bool readyToAttack = false;

    public bool readyToFireballAttack = false;
    
    public bool fireballLocalScale = false;

    private bool isCharacterDead  =false;
    public bool IsCharacterDead {get => isCharacterDead; set => isCharacterDead = value; }
    public bool jumpAnimationResume = false;

    private bool characterHealthDecrease = false;
    public bool CharacterHealthDecrease { get => characterHealthDecrease; set => characterHealthDecrease = value; }

    private bool hitObstacle = false;
    public bool HitObstacle { get => hitObstacle; set => hitObstacle = value; }

    private bool hitEnemy = false;
    public bool HitEnemy { get => hitEnemy; set => hitEnemy = value; }
    private  int spawnPointSiblingIndex = default;
   
    private void Awake() 
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        MovementInputController();
    }

    private void FixedUpdate() 
    {
        CharacterMovement();    
        JumpingMovement();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.gameObject.layer == 3)
        {
            jumpCounter = 0;
            isCharacterAbove = false;
            jumpAnimationResume = false;
        }
        
        if(other.collider.CompareTag("obstacle"))
        {
            jumpAnimationResume = false;
            characterHealthDecrease = true;
            hitObstacle =true;
            isCharacterAbove =false;
        }
        if(other.collider.CompareTag("Enemy"))
        {
            characterHealthDecrease = true;
            hitEnemy =true;
        }
    }


    private void OnCollisionStay2D(Collision2D other) 
    {
        
        if(other.collider.gameObject.layer == 3)
        {
            jumpAnimationResume = false;
        }
        if(other.collider.CompareTag("obstacle"))
        {
            characterHealthDecrease  =false;
            hitObstacle = false;
            isCharacterAbove = false;
        }
        if(other.collider.CompareTag("Enemy"))
        {
            characterHealthDecrease  =false;
            hitEnemy = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("obstacle"))
        {
            characterHealthDecrease  =false;
            hitObstacle = false;
        } 
        if(other.collider.CompareTag("Enemy"))
        {
            characterHealthDecrease  =false;
            hitEnemy = false;
        }    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("SpawnPoint"))
        {
            spawnPointSiblingIndex =  other.transform.GetSiblingIndex();
            PlayerPrefs.SetInt("SpawnPoint",spawnPointSiblingIndex);
        } 
        if(other.CompareTag("Cranboline"))
        {
            jumpCounter = 0;
            isCharacterAbove = false;
            jumpAnimationResume = false;
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
                isCharacterAbove = false;
            }
            else
            {
                isCharacterAbove = false;
                readyToFireballAttack = true;
                Spawner.Instance.Fireball.GetComponent<FireballController>().birKereYonAlindi = false;// düzeltilicek field olarak tanımla
            }
        }
        

        if(Input.GetKeyDown(KeyCode.Space) && jumpCounter != 2)
        {
            isJumping = true;
            isCharacterAbove  =true;
            jumpCounter++;
        }    
    }

    void CharacterMovement()
    {
        if(isToLeft)
        {
            movement = new Vector3(-characterSpeed,0,0);
            transform.position += movement * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0,-180 ,0);
        }
        if(isToRight)
        {
            movement =new Vector3(characterSpeed,0,0);
            transform.position += movement * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
