using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private Animator enemyAnimator;

    [SerializeField] private Image exclamation;

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
    public bool EnemyDead { get { return enemyDead; } set {enemyDead = value;}}

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

    private bool changeEnemyCharacter = false;
    public bool ChangeEnemyCharacter { get { return changeEnemyCharacter;} set { changeEnemyCharacter = value; }}

    private bool deadly = false;
    public bool Deadly { get { return deadly;} set { deadly = value; }}

    private bool attackReady = false;
    public bool AttackReady { get { return attackReady; } set { attackReady = value; }}
   
    private bool upForce = false;
    private bool downForce = false;

    private bool enemyHurtAnimation = false;
    public bool EnemyHurtAnimation { get { return enemyHurtAnimation;} set { enemyHurtAnimation = value;}}

    private string enemyName = default(string);
    public string EnemyName { get { return enemyName;} set { enemyName = value; }}

    private float minDistanceTravel = 0f;
    private float maxDistanceTravel = 0f;

    [SerializeField] private float enemyJumpPower;
    private int healt = 3; 
    public int Healt { get { return healt;} set { healt = value; }}
    
    private void Awake() 
    {
        rgb2D = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        exclamation = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }
    
//enemyAnimationController.DeadAnimationStart = true;
    void Update()
    {

        print(transform.name +"   "+healt);

        if(deadly  || healt == 0)
        {
            if(enemyName == transform.name)
            {
                enemyAnimator.enabled = false;
                enemyDead = true;
                transform.GetComponent<EnemyAnimationController>().DeadAnimationStart = true;
                enemyAttackAnimation = false;
                exclamation.enabled = false;
                rgb2D.velocity  =Vector2.zero;
            }
            
        }
       
        if(enemyAttackAnimation)
        {
            enemyAnimationController.Attack = true;
            enemyAnimator.enabled = false;
            enemyAttackAnimation  =false;
        }
        
    }

    
    private void LookMove()
    {
        if(!enemyDead && !enemyHurtAnimation)
        {
            int layerMask = 1 <<10;
            layerMask = ~1 <<8;
            

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 10, Color.red);
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 10f,layerMask);

            if (hit2D.collider != null)
            {
                if(hit2D.collider.CompareTag("Player"))
                {
                    attackReady = true;
                    targetTranform = hit2D.collider.transform;
                    carpti = true;
                }
                else
                {
                    attackReady = false; // yeni eklendi
                }
                
            }
            else
            {
                carpti = false;
                
            }

            if(carpti)
            {
                exclamation.fillAmount += .3f;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 10, Color.blue);


                transform.position = Vector2.Lerp(transform.position, targetTranform.position, 1f  * Time.deltaTime * enemyCharcterSpeed * 4);
            }
            else
            {
                exclamation.fillAmount -= .3f;

            }
        }

        
        
        
        
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
        if(other.collider.CompareTag("fireball") )
        {
            ChangeDirectionEnemy(other);
           
            EnemyHealthReduction();
        }


        if(other.collider.CompareTag("enemyFireball"))
        {
            
            ChangeDirectionEnemy(other);
            
            EnemyHealthReduction();
        }


        if(other.collider.CompareTag("Enemy"))
        {
            enemyReadyJump = true;

            EnemyDirectionControl();

            EnemyJumpMove(45);

        }

        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
        {
            zeminHit = true;
        }


        if(other.collider.CompareTag("CoinCounter"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    
    public void EnemyDirectionControl()
    {
        if (!leftPosition)
        {
            rightPosition = true;
            leftPosition = true;
        }
        else if (rightPosition)
        {
            rightPosition = false;
            leftPosition = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {

            if(!deadly && !enemyHurtAnimation)
            {
                swordStrike = true;
                if(attackReady )
                {
                    enemyAttackAnimation = true;
                    enemyName = gameObject.transform.name;
                }

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
            else
            {
                swordStrike = false;
                if(attackReady)
                {
                    enemyAttackAnimation = false;
                    enemyName = "";
                }
            }

           
        }

        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
        {
            float resultX = transform.position.x - other.transform.position.x;
            float ressultY = transform.position.y - other.transform.position.y;


            if(transform.position.y > 1.5f && ressultY > 0)
            {
                
                if(resultX > other.transform.position.x)
                {
                    UpForce();
                }
                

                if (resultX < other.transform.position.x )
                {
                    UpForce();
                }
                
            }
            else if(transform.position.y < 1.5f && ressultY < 0)
            {
                DownForce();
            }
            if (transform.position.y  == other.transform.position.y)
            {
                DownForce();
            }
            

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
                EnemyDirectionControl();
                
                zeminHit = false;
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

        if(other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))  
        {
            zeminHit = false;
        }
        
        if(other.collider.CompareTag("CoinCounter"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }


    private void UpForce()
    {
        rgb2D.AddForce(Vector2.up * 30);
    }

    private void DownForce()
    {
        rgb2D.AddForce(Vector2.down * 30);
    }



    private void ChangeDirectionEnemy(Collision2D other)
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

    

    public void EnemyHealthReduction()
    {
        if(healt > 0)
        {
            healt --;
        }
        else
        {
            deadly = true;
        }
    }

    private void EnemyCharacterMove()
    {   

        if(!enemyDead && !enemyHurtAnimation)
        {
            if(!calculateOnce)
            {
                minDistanceTravel = transform.position.x + -25;
                maxDistanceTravel = transform.position.x + 25;
                calculateOnce = true;
            }

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
        if(!enemyDead && !enemyHurtAnimation)
        {
            if (enemyReadyJump)
            {
                rgb2D.AddForce(Vector2.up * enemyJumpPower);
                enemyReadyJump = false;
            }
        }
    }
}
