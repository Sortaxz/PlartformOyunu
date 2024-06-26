using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController instance;
    public static EnemyController Instance
    {
        get
        {
            instance =  FindAnyObjectByType<EnemyController>();
            return instance;
        }
        
    }
    [SerializeField] private EnemyAnimationController enemyAnimationController;

    private Rigidbody2D rgb2D ;
    private RaycastHit2D hit2D;
    private Transform  targetTranform;

    private Vector2 colliderHitPoint;

    [SerializeField] private float enemyCharcterSpeed;
    private bool leftPosition = false;
    public bool LeftPosition { get { return leftPosition; } set { leftPosition = value; } }
    private bool rightPosition = false; 
    public bool RightPosition { get { return rightPosition; } set { rightPosition = value; } }

    private bool enemyReadyJump = false;

    private bool enemyWalkAnimationStart = false;
    public bool EnemyWalkAnimationStart { get { return enemyWalkAnimationStart;} set{enemyWalkAnimationStart = value;}} 

    private bool enemyDead = false;
    public bool EnemyDead { get { return enemyDead; }}
    bool carpti = false;
    private bool runAnimation = false;
    public bool RunAnimation { get { return runAnimation; } set { runAnimation = value; }}
    private bool enemyAttackAnimation = false;
    public bool EnemyAttackAnimation { get { return enemyAttackAnimation;} set { enemyAttackAnimation = value; }}
    
    private bool swordStrike = false;
    public bool SwordStrike { get { return swordStrike;} set { swordStrike = value; }}

    private bool characterCollidesWind = false;
    public bool CharacterCollidesWind { get { return characterCollidesWind;} set { characterCollidesWind = value; }}  

    private bool zeminHit = false;

    private bool calculateOnce = false;

    private float minDistanceTravel = 0f;
    private float maxDistanceTravel = 0f;

    [SerializeField] private float enemyJumpPower;
    private int healt = 3; 
    public int Healt { get { return healt;}}
    
    private void Awake() 
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }
    

    void Update()
    {
        if(healt ==0)
        {
            enemyDead = true;
        }


    }

    
    private void LookMove()
    {
        int layerMask = 1 <<10;
        layerMask = ~1 <<8;
        

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 10, Color.red);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 10f,layerMask);

        if (hit2D.collider != null)
        {
            if(hit2D.collider.CompareTag("Player"))
            {
                targetTranform = hit2D.collider.transform;
                carpti = true;
            }
            
        }
        else
        {
            carpti = false;
            
        }

        if(carpti)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 10, Color.blue);
            
            transform.position = Vector2.Lerp(transform.position, targetTranform.position, 1f  * Time.deltaTime * enemyCharcterSpeed * 4);
        }

        
        
        //EnemyJumpMove(enemyJumpPower);
        
        
    }

    private void FixedUpdate() 
    {
        if(!UIManager.Instance.StandbyScreenWorked && !GameManager.Instance.Finish)
        {
            if(!enemyDead)
            {
                LookMove();
                
                EnemyJumpMove(enemyJumpPower);
                
                if(!carpti)
                {
                    EnemyCharacterMove();
                }
                
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if( other.collider.CompareTag("Cranboline"))
        {
            enemyReadyJump = true;
        }    
          
        if(other.collider.gameObject.layer == 7)
        {
            enemyReadyJump = false;
        }
        if(other.collider.CompareTag("fireball") || other.collider.CompareTag("enemyFireball"))
        {
            
            if(other.collider.transform.position.x < transform.position.x)
            {
                leftPosition = false;
                rightPosition = false;
            }
            else if(other.collider.transform.position.x > transform.position.x)
            {
                leftPosition = true;
                rightPosition = true;
                
            }


            EnemyHealthReduction();
        }

        if(other.collider.CompareTag("Player"))
        {

            if(GameManager.Instance.mainCharacter.readyToAttack || GameManager.Instance.mainCharacter.ReadyToStrikeAttack)
            {
                EnemyHealthReduction();

            }
        } 
        if(other.collider.CompareTag("Enemy"))
        {
            enemyReadyJump = true;

            //EnemyJumpMove(45);
        }

        Method(other);
    }
    
    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            enemyAttackAnimation = true;

            SwordStrikeMethod(other);

            if(swordStrike)
            {
                GameManager.Instance.mainCharacter.CharacterHealthDecrease = true;
                GameManager.Instance.mainCharacter.HitEnemy = true;
                GameManager.Instance.mainCharacter.StartHurtAnimation = true;
            }

            if(characterCollidesWind)
            {
                enemyReadyJump = true;
                characterCollidesWind = false;
            }
        }

        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
        {
            float sonuc = transform.position.x - other.transform.position.x;


            if(transform.position.y > 1.5f)
            {
                if(sonuc > 3 || sonuc <-3 )
                {
                    rgb2D.AddForce(Vector2.up * 30);
                }
            }
            else if(transform.position.y < 1.5f)
            {
                rgb2D.AddForce(Vector2.down * 30);
            }

            /*
            if(sonuc > 3 || sonuc <-3 )
            {
                rgb2D.AddForce(Vector2.up * 30);
            }
            */

            zeminHit = true;
            
            

            
        }

        if(other.collider.CompareTag("Wind"))
        {
            enemyReadyJump = true;

            EnemyJumpMove(45);

        }

        if(other.collider.CompareTag("obstacle"))
        {

            if(!zeminHit)
            {
                enemyReadyJump = true;
                
                EnemyJumpMove(45);
            }
            else
            {
                if(!leftPosition)
                {
                    rightPosition = true;
                    leftPosition = true;
                   print("leftPosition : " + leftPosition);
                }
                if(rightPosition)
                {
                    rightPosition = false;
                    leftPosition = false;
                    
                   print("rightPosition : " + rightPosition);
                }
                zeminHit = false;
            }
            
        }
    }

    private void Method(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.position.x > other.transform.position.x)
            {
                leftPosition = false;
                rightPosition = false;
            }
            else if(transform.position.x < other.transform.position.x)
            {
                rightPosition = true;
                leftPosition = true;
            }
        }
    }

    private void SwordStrikeMethod(Collision2D other)
    {
        if (swordStrike)
        {
            if (!leftPosition)
            {
                if (carpti)
                {
                    other.rigidbody.AddForce(Vector2.left * 60);
                }
                else
                {
                    swordStrike = false;
                }
            }
            if (rightPosition)
            {
                if (carpti)
                {
                    other.rigidbody.AddForce(Vector2.right * 60);
                }
                else
                {

                    swordStrike = false;
                }
            }

        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            enemyAttackAnimation = false;
            swordStrike = false;
            
        }     
    }

    private void EnemyHealthReduction()
    {
        if(healt > 0)
        {
            healt --;
        }
    }

    private void EnemyCharacterMove()
    {   
        if(!calculateOnce)
        {
            minDistanceTravel = transform.position.x + -25;
            print( "minDistanceTravel : " + minDistanceTravel);
            maxDistanceTravel = transform.position.x + 25;
            print( "minDistanceTravel : " + maxDistanceTravel);
            calculateOnce = true;
        }

        if(!enemyDead)
        {
            if (!leftPosition)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

                transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * enemyCharcterSpeed;
                if (transform.position.x <= minDistanceTravel)
                {
                    leftPosition = true;
                    rightPosition = true;

                }
            }
            if (rightPosition)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);


                transform.position += new Vector3(10, 0, 0) * Time.deltaTime * enemyCharcterSpeed;
                if (transform.position.x >= maxDistanceTravel)
                {
                    leftPosition = false;
                    rightPosition = false;
                }
            }

            EnemyJumpMove(enemyJumpPower);
        }


    }

    private void EnemyJumpMove(float enemyJumpPower)
    {
        if (enemyReadyJump)
        {
            rgb2D.AddForce(Vector2.up * enemyJumpPower);
            enemyReadyJump = false;
        }
    }
}
