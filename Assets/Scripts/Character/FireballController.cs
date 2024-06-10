using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enemyFireball
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
        EnemyFireballMovement();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("leftControl") || other.gameObject.CompareTag("rightControl"))
        {
            Destroy(gameObject);
        }
        
        
        FireballCollisionControl(other);

        ContinuosEnemyFireballCollisionControl(other);

        EnemyFireballCollisionControl(other);
        

    }

    private void FireballCollisionControl(Collision2D other)
    {
        if (transform.tag == "fireball")
        {
            if (other.collider.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                //Destroy(other.gameObject, 1);
                isCollider = true;
            }

            if (other.collider.CompareTag("obstacle"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            if (!other.collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void ContinuosEnemyFireballCollisionControl(Collision2D other)
    {
        if (gameManager.EnemyFireballl)
        {
            if (transform.tag == "enemyFireball")
            {
                if (other.collider.CompareTag("Player"))
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    gameManager.mainCharacter.HitEnemyFireball = false;

                    Destroy(gameObject, 0.01f);
                }
                if (other.collider.CompareTag("obstacle"))
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }

                if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    Destroy(other.collider.gameObject);
                    Destroy(gameObject);
                }
                if (other.collider.gameObject.layer == 7)
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    Destroy(gameObject);
                }
                if (other.collider.tag == "Cranboline" )
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    Destroy(gameObject);
                }

                
            }
        }
    }

    private void EnemyFireballCollisionControl(Collision2D other)
    {
        if(!gameManager.EnemyFireballl)
        {
            if (transform.tag == "enemyFireball")
            {
                if (other.collider.CompareTag("Player"))
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    gameManager.mainCharacter.HitEnemyFireball = false;

                    Destroy(gameObject, 0.01f);
                }

                if (other.collider.CompareTag("obstacle"))
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }

                if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    Destroy(other.collider.gameObject);
                    Destroy(gameObject);
                }
                if(other.collider.gameObject.layer == 7)
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    Destroy(gameObject);
                }
                
                if (other.collider.tag == "Cranboline" )
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    Destroy(gameObject);
                }
            }
        }
    }


    private void FireballMovement()
    {
        if(transform.tag =="fireball")
        {
            if(!GameManager.Instance.Finish && !UIManager.Instance.StandbyScreenWorked)
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
                    //fireballRb2D.AddForce(yon* fireballSpeed);
                }
                transform.Translate(yon * Time.fixedDeltaTime * fireballSpeed);
            }
            
        }

    }
    private void EnemyFireballMovement()
    {
        if(!GameManager.Instance.Finish && !UIManager.Instance.StandbyScreenWorked)
        {
            if(transform.tag =="enemyFireball")
            {
                if(gameManager.FireballWasFired)
                {
                    if(gameManager.EnemyFireballLeftGo)
                    {
                        transform.Translate(Vector3.left * Time.fixedDeltaTime* gameManager.EnemyFireballSpeed);
                        transform.localScale = new Vector3(-1, 1,1);
                    }   
                    if(gameManager.EnemyFireballRightGo)
                    {
                        transform.Translate(Vector3.right * Time.fixedDeltaTime * gameManager.EnemyFireballSpeed);
                        transform.localScale = new Vector3(1, 1,1);
                    }

                }
            }
        }
        
       
    }
}
