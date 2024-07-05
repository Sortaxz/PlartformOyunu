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
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform fireballPosition;

    private bool dead = false;
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
        if(dead)
        {
            gameObject.SetActive(false);
        }
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
            gameObject.SetActive(false);
        }
        
        
        FireballCollisionControl(other);

        ContinuosEnemyFireballCollisionControl(other);

        EnemyFireballCollisionControl(other);
        

    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(transform.tag == "enemyFireball")
        {
            if(other.collider.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
            
        }    
        
    }
    


    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("CoinCounter"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }    

        if(transform.tag == "enemyFireball")
        {
            if(other.collider.CompareTag("Player"))
            {
                dead = false;
            }
            
        }
    }

    private void FireballCollisionControl(Collision2D other)
    {
        if (transform.tag == "fireball")
        {
            if (other.collider.CompareTag("Enemy"))
            {
                isCollider = true;



                if(other.gameObject.GetComponent<EnemyController>().Healt == 1)
                {
                    other.gameObject.GetComponent<EnemyController>().Deadly  =true;
                    other.gameObject.GetComponent<EnemyController>().EnemyName = other.gameObject.name;
                }

                gameObject.SetActive(false);
            }

            if (other.collider.CompareTag("obstacle"))
            {
                Destroy(other.gameObject);
                gameObject.SetActive(false);
            }
            if (!other.collider.CompareTag("Player"))
            {
                gameObject.SetActive(false);

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

                    dead = true;
                }
                if (other.collider.CompareTag("obstacle"))
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    
                    Destroy(other.gameObject);

                    gameObject.SetActive(false);
                }

                if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    
                    Destroy(other.collider.gameObject);
                    
                    gameObject.SetActive(false);
                }
                if (other.collider.gameObject.layer == 7)
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    
                    gameObject.SetActive(false);
                    
                }
                if (other.collider.tag == "Cranboline" )
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    
                    gameObject.SetActive(false);
                    
                }

                if(other.collider.CompareTag("Enemy"))
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = false;
                    
                    if(other.gameObject.GetComponent<EnemyController>().Healt == 1)
                    {
                        other.gameObject.GetComponent<EnemyController>().Deadly  =true;
                        other.gameObject.GetComponent<EnemyController>().EnemyName = other.gameObject.name;
                    }
                    
                    gameObject.SetActive(false);
                }


                if(other.collider.CompareTag("CoinCounter"))
                {
                    other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
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
                    
                    dead = true;
                }

                if (other.collider.CompareTag("obstacle"))
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    
                    Destroy(other.gameObject);
                    gameObject.SetActive(false);
                    
                }

                if (other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    
                    Destroy(other.collider.gameObject);
                    gameObject.SetActive(false);
                    
                }
                if(other.collider.gameObject.layer == 7)
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    
                    gameObject.SetActive(false);

                }
                
                if (other.collider.tag == "Cranboline" )
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;
                    
                    gameObject.SetActive(false);

                }

                if(other.collider.CompareTag("Enemy"))
                {
                    gameManager.CreateEnemyFireball = false;
                    gameManager.CreateWind = true;

                    if(other.gameObject.GetComponent<EnemyController>().Healt == 1)
                    {
                        other.gameObject.GetComponent<EnemyController>().Deadly  =true;
                        other.gameObject.GetComponent<EnemyController>().EnemyName = other.gameObject.name;
                    }

                    gameObject.SetActive(false);
                }
            }
        }
    }


    private void FireballMovement()
    {
        if(transform.tag =="fireball")
        {
            if(!GameManager.Instance.Finish && !UIManager.Instance.StandbyScreenWorked )
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
