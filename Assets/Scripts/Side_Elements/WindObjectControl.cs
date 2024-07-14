using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindObjectControl : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;
    [SerializeField] private AnimationClip animationClip;
    private Rigidbody2D windRb2D;
    private bool toRight;
    public bool ToRight { get { return toRight; } set { toRight = value; } }
    private float windSpeed;
    private bool rightGo= false;
    private bool leftGo= false;
    private void Awake() 
    {
        gameManager = GameManager.Instance;
        windRb2D = GetComponent<Rigidbody2D>();
        windSpeed = gameManager.WindSpeed;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        WindMovement();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(EnemyController.Instance != null)
        {
            if(!gameManager.WindObject)
            {
                if(other.collider.CompareTag("Player"))
                {
                    EnemyController.Instance.CharacterCollidesWind = true;
                }
            }
            if(gameManager.WindObject)
            {
                if(other.collider.CompareTag("Player"))
                {
                    EnemyController.Instance.CharacterCollidesWind = true;
                }
            }
        }

        if(other.collider.CompareTag("CoinCounter"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(!gameManager.WindObject)
        {
            if(other.collider.CompareTag("Player"))
            {
                /*
                gameManager.CreateWind = false;
                gameManager.CreateEnemyFireball = true;
                */
                if(EnemyController.Instance != null)
                {
                    EnemyController.Instance.CharacterCollidesWind = false;
                    
                }

                animator.SetBool("Wind",true);
                

                //gameObject.SetActive(false);

                StartCoroutine(timer());


            }
        }
        
        if(gameManager.WindObject)
        {
            if(other.collider.CompareTag("Player"))
            {
                /*
                gameManager.CreateWind = true;
                gameManager.CreateEnemyFireball = false;
                */

                if(EnemyController.Instance != null)
                {
                    EnemyController.Instance.CharacterCollidesWind = false;
                    
                }

                animator.SetBool("Wind",true);
                
                //gameObject.SetActive(false);

                StartCoroutine(timer());
            }
        }

        if(other.collider.CompareTag("CoinCounter"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }


    }
    
    public  void WindMovement()
    {
        if(!gameManager.Finish && !UIManager.Instance.StandbyScreenWorked && !gameManager.mainCharacter.IsCharacterDead) 
        {
            if(gameManager.mainCharacter != null)
            {
                if(gameManager.WindLeftGo)
                {
                    leftGo = true;
                    rightGo = false;
                }
                else if(gameManager.WindRightGo)
                {
                    rightGo = true;
                    leftGo =false;
                }
                if(leftGo)
                {
                    transform.Translate(Vector2.left * Time.fixedDeltaTime * windSpeed );
                }

                if(rightGo)
                {
                    transform.Translate(Vector2.right *Time.fixedDeltaTime * windSpeed );
                }
            }
        }
       
    }


    private IEnumerator timer()
    {
        yield return new WaitForSeconds(animationClip.length);
        if(!gameManager.WindObject)
        {
            gameManager.CreateWind = false;
            gameManager.CreateEnemyFireball = true;
        }

        if(gameManager.WindObject)
        {
            gameManager.CreateWind = true;
            gameManager.CreateEnemyFireball = false;
        }
        gameObject.SetActive(false);
    }

    #region  Wind Animation

    

    #endregion



}
