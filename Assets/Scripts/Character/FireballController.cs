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
        if(other.gameObject.CompareTag("leftControl") || other.gameObject.CompareTag("rightControl"))
        {
            Destroy(gameObject);
        }

        if(transform.tag == "fireball")
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
            if(!other.collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        if(transform.tag == "enemyFireball")
        {
            if(other.collider.CompareTag("Player"))
            {
                gameManager.CreateEnemyFireball = true;
                gameManager.CreateWind = true;
                gameManager.mainCharacter.HitEnemyFireball = false;
                Destroy(gameObject,0.01f);
            } 
            if(other.collider.CompareTag("obstacle"))
            {
                gameManager.CreateEnemyFireball = true;
                gameManager.CreateWind = true;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

            if(other.collider.gameObject.layer == LayerMask.NameToLayer("Zemin"))
            {
                gameManager.CreateEnemyFireball = true;
                gameManager.CreateWind = true;
                Destroy(other.collider.gameObject);
                Destroy(gameObject);
            }

            if( other.collider.tag != "CheckPoint"   && other.collider.tag != "Player" && other.collider.tag == "AbsorbingObject")
            {
                if(other.collider.tag != "Cranboline")
                {
                    gameManager.CreateEnemyFireball = true;
                    gameManager.CreateWind = true;
                    Destroy(other.collider.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    gameManager.CreateEnemyFireball = true;
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
    private void EnemyFireballMovement()
    {
        if(transform.tag =="enemyFireball")
        {
            if(gameManager.FireballWasFired)
            {
                if(gameManager.EnemyFireballLeftGo)
                {
                    fireballRb2D.AddForce(Vector2.left * gameManager.EnemyFireballSpeed);
                    transform.localScale = new Vector3(-1, 1,1);
                }   
                if(gameManager.EnemyFireballRightGo)
                {
                    fireballRb2D.AddForce(Vector2.right * gameManager.EnemyFireballSpeed);
                    transform.localScale = new Vector3(1, 1,1);
                }
                gameManager.FireballWasFired = false;
            }
        }
    }
}
