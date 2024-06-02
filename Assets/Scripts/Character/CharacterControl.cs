using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterControl : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb2D;
    private Vector3 movement =  Vector3.zero;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float characterSpeed; 
    public float CharacterSpeed {get { return characterSpeed;} set { characterSpeed = value; } }
    private int jumpCounter = 0;

    private bool isToLeft = false;

    private bool isToRight = false;

    private bool isJumping = false;
    public bool isCharacterAbove  =false;
    public bool isCharacterSlidDown = false;
    public bool readyToAttack = false;

    public bool readyToFireballAttack = false;

    private bool readyToStrikeAttack = false;
    public bool ReadyToStrikeAttack { get => readyToStrikeAttack; set { readyToStrikeAttack = value;}}

    private bool strikeAttackReady = false;

    private bool startHurtAnimation = false;
    public bool StartHurtAnimation { get => startHurtAnimation; set => startHurtAnimation = value;}
    
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

    private bool hitEnemyFireball = false;
    public bool HitEnemyFireball { get => hitEnemyFireball; set => hitEnemyFireball = value; }    

    private bool hitAbsorbingObject = false;
    public bool HitAbsorbingObject {get => hitAbsorbingObject; set => hitAbsorbingObject = value;}

    private bool lifeDwindling = false;
    public bool LifeDwindling {get => lifeDwindling; set => lifeDwindling = value;}

    private bool stageReady = false;
    public bool StageReady { get => stageReady; set => stageReady = value; }

    private bool isAerialWind = false;
    public bool IsAerialWind { get => isAerialWind; set => isAerialWind = value;}

    
    //private  int spawnPointSiblingIndex = default;

   
    private void Awake() 
    {

        rb2D = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;

        
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
        if(other.collider.gameObject.layer == 3 || other.collider.gameObject.layer == 7)
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
        if(other.collider.CompareTag("enemyFireball"))
        {
            characterHealthDecrease = true;
            hitEnemyFireball = true;
        }

        if(other.collider.tag == "Zemin")
        {
            isAerialWind = false;
        }
    }


    private void OnCollisionStay2D(Collision2D other) 
    {
        
        if(other.collider.gameObject.layer == 3 || other.collider.gameObject.layer == 7)
        {
            jumpAnimationResume = false;
        }
        if(other.collider.CompareTag("obstacle"))
        {
            characterHealthDecrease  =true;
            hitObstacle = false;
            isCharacterAbove = false;
           
            lifeDwindling = true;
            
            startHurtAnimation  =true;
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
            lifeDwindling = false;
            characterHealthDecrease  =false;
            hitObstacle = false;
            startHurtAnimation = false;
        } 
        if(other.collider.CompareTag("Enemy"))
        {
            characterHealthDecrease  =false;
            hitEnemy = false;
        }    
        if(other.collider.CompareTag("enemyFireball"))
        {
            characterHealthDecrease = false;
            hitEnemyFireball = false;
            startHurtAnimation = true;
        }
         if(other.collider.tag == "Zemin")
        {
            isAerialWind = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("CheckPoint"))
        {
            //spawnPointSiblingIndex =  other.transform.GetSiblingIndex();
            
        } 
        if(other.CompareTag("Cranboline"))
        {
            jumpCounter = 0;
            isCharacterAbove = false;
            jumpAnimationResume = false;
        }

        if(other.CompareTag("Finish"))
        {
            gameManager.StageTransitionReady = true;
            gameManager.Finish = true;
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

        if(Input.GetKey(KeyCode.Q))
        {
            strikeAttackReady = true;
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            strikeAttackReady = false;
        }

        if(Input.GetMouseButtonDown(0) )
        {
           
            if(UIAnimation.Instance.SwordAttackBegin == true)
            {
                if(!strikeAttackReady)
                {
                    readyToAttack = true;
                    isCharacterAbove = false;
                }
                else
                {
                    readyToStrikeAttack = true;
                }
            }
            else
            {
                isCharacterAbove = false;
                readyToFireballAttack = true;
                jumpAnimationResume = false;
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
