using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindObjectControl : MonoBehaviour
{
    private GameManager gameManager;
    private bool toRight;
    public bool ToRight { get { return toRight; } set { toRight = value; } }
    private float windSpeed;
    private Rigidbody2D windRb2D;
    private bool rightGo= false;
    private bool leftGo= false;
    private void Awake() 
    {
        gameManager = GameManager.Instance;
        windRb2D = GetComponent<Rigidbody2D>();
        windSpeed = gameManager.WindSpeed;
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
    
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(!gameManager.WindObject)
        {
            if(other.collider.CompareTag("Player"))
            {
                gameManager.CreateWind = false;
                gameManager.CreateEnemyFireball = true;
                
                EnemyController.Instance.CharacterCollidesWind = false;
                
                gameObject.SetActive(false);
            }
        }
        
        if(gameManager.WindObject)
        {
            if(other.collider.CompareTag("Player"))
            {
                gameManager.CreateWind = true;
                gameManager.CreateEnemyFireball = false;
                
                EnemyController.Instance.CharacterCollidesWind = false;
                
                gameObject.SetActive(false);
            }
        }
       
    }
    
    public  void WindMovement()
    {
        if(!gameManager.Finish && !UIManager.Instance.StandbyScreenWorked)
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
}
