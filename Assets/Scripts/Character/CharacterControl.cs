using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterControl : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb2D;
    public Rigidbody2D Rb2D { get { return rb2D; }  set { rb2D = value; } }
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
    public bool   CharacterHealthDecrease { get => characterHealthDecrease; set => characterHealthDecrease = value; }

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

    private bool slidDownMove = false;

    private bool leftSlidDown=  false;
    private bool rightSlidDown= false;

    private bool leftForce = false;
    private bool rightForce = false;

    [SerializeField] private BoxCollider2D boxCollider2D;
    private PolygonCollider2D polygonCollider2D;

   
    private void Awake() 
    {

        rb2D = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        
        boxCollider2D = GetComponent<BoxCollider2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        
    }

    void Update()
    {
        MovementInputController();

        if(readyToAttack)
        {
            boxCollider2D.enabled = true;
            polygonCollider2D.enabled = false;
        }
        else
        {
            boxCollider2D.enabled = false;
            polygonCollider2D.enabled = true;
        }

        
    }

    private void FixedUpdate()
    {
        CharacterMovement();
        JumpingMovement();
        
        SlidDownMoveMethod();
    }

    private void SlidDownMoveMethod()
    {
        if (slidDownMove)
        {
            if (leftSlidDown)
            {
                rb2D.AddForce(Vector2.left * 10);
            }
            if (rightSlidDown)
            {
                rb2D.AddForce(Vector2.right * 10);

            }
        }
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
       
        if(other.collider.CompareTag("enemyFireball"))
        {
            characterHealthDecrease = true;
            hitEnemyFireball = true;
        }

        if(other.collider.tag == "Zemin")
        {
            isAerialWind = false;
        }

        if(other.collider.CompareTag("Enemy"))
        {
            if(readyToStrikeAttack || readyToAttack)
            {
                other.gameObject.GetComponent<EnemyController>().EnemyHealthReduction();
                other.gameObject.GetComponent<EnemyController>().EnemyDirectionControl();

                other.gameObject.GetComponent<EnemyController>().EnemyHurtAnimation = true;

                HurtAnimation(other);

                if (other.gameObject.GetComponent<EnemyController>().Healt == 0)
                {
                    other.gameObject.GetComponent<EnemyController>().Deadly = true;
                    other.gameObject.GetComponent<EnemyController>().EnemyName = other.gameObject.name;
                }
            }


        }
       
    }


    private void HurtAnimation(Collision2D other)
    {
        if(other.collider.gameObject.CompareTag("Enemy"))
        {
            if(other.collider.gameObject.GetComponent<EnemyController>().EnemyHurtAnimation)
            {
                if(transform.position.x >other.collider.gameObject.transform.position.x)
                {
                    print("Enemy sola kuvvet uygulandi");
                    other.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 600);
                }
                else if(transform.position.x < other.collider.gameObject.transform.position.x)
                {
                    print("Enemy sağa kuvvet uygulandi");
                    other.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 600);
                    
                }
            }
            
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
            leftForce = false;
            rightForce = false;
            other.gameObject.GetComponent<EnemyController>().EnemyHurtAnimation = false;
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
        
        if(other.CompareTag("Cranboline"))
        {
            jumpCounter = 0;
            isCharacterAbove = false;
            jumpAnimationResume = false;
        }

        if(other.CompareTag("Finish"))
        {
            if(gameManager.NumberCollectedCoins >= 4)
            {
                gameManager.StageTransitionReady = true;
                gameManager.Finish = true;
                if(SceneManager.GetActiveScene().name == Scene_Manager.Instance.Scenes[Scene_Manager.Instance.Scenes.Length-1].name)
                {
                    SaveManager.SetcheckLastLevel();
                }
            }
            
            
        }

        if(other.CompareTag("downControl"))
        {
            isCharacterDead = true;
        }
        if(isCharacterDead)
        {
            gameManager.CharcDead = true;
        }
    }

   
    
    void JumpingMovement()
    {
        if(isJumping && !gameManager.StageTransitionReady && !UIManager.Instance.StandbyScreenWorked!)
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
            leftSlidDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            leftSlidDown = false;
            isToLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            fireballLocalScale = false;
            isToRight = true;
            rightSlidDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isToRight = false;
            rightSlidDown = false;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            isCharacterSlidDown  =true;
            slidDownMove = true;
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            isCharacterSlidDown = false;
            slidDownMove = false;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            strikeAttackReady = true;
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            strikeAttackReady = false;
        }

        if(Input.GetMouseButtonDown(0)  && !UIManager.Instance.StandbyScreenWorked)
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
            else if(!UIAnimation.Instance.SwordAttackBegin && !gameManager.AgainFireball)
            {
                isCharacterAbove = false;
                readyToFireballAttack = true;
                jumpAnimationResume = false;
                
                gameManager.Fireball.GetComponent<FireballController>().birKereYonAlindi = false;
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
       
        if(!gameManager.StageTransitionReady && !UIManager.Instance.StandbyScreenWorked)
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
}
