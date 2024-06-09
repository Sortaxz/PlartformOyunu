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

    private Rigidbody2D rgb2D ;

    [SerializeField] private float enemyCharcterSpeed;
    private bool leftPosition = false;
    public bool LeftPosition { get { return leftPosition; }}
    private bool rightPosition = false; 
    public bool RightPosition { get { return rightPosition; }}

    private bool enemyReadyJump = false;

    private bool enemyWalkAnimationStart = false;
    public bool EnemyWalkAnimationStart { get { return enemyWalkAnimationStart;} set{enemyWalkAnimationStart = value;}} 

    private bool enemyDead = false;
    public bool EnemyDead { get { return enemyDead; }}

    [SerializeField] private float enemyJumpPower;


    private void Awake() 
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void FixedUpdate() 
    {
        EnemyCharacterMove();    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("obstacle") || other.collider.CompareTag("Cranboline"))
        {
            enemyReadyJump = true;
        }    
          
        if(other.collider.gameObject.layer == 7)
        {
            enemyReadyJump = false;
        }
        if(other.collider.CompareTag("fireball") || other.collider.CompareTag("enemyFireball"))
        {
            print("dead");
            enemyDead = true;
        }
    }

    private void EnemyCharacterMove()
    {

        if(!enemyDead)
        {
            if(!leftPosition)
            {
                
                transform.localScale = new Vector3(1,1,1);

                transform.position += new Vector3(-10,0,0) * Time.deltaTime * enemyCharcterSpeed;
                if(transform.position.x <= -10)
                {
                    leftPosition = true;
                    rightPosition = true;

                }
            }
            if(rightPosition)
            {
                transform.localScale = new Vector3(-1,1,1);

                transform.position += new Vector3(10,0,0) * Time.deltaTime * enemyCharcterSpeed;
                if(transform.position.x >= 10)
                {
                    leftPosition = false;
                    rightPosition = false;
                }
            }

            if(enemyReadyJump)
            {
                rgb2D.AddForce(Vector2.up * enemyJumpPower );
                enemyReadyJump = false;
            }
        }
        
        
    }
}
