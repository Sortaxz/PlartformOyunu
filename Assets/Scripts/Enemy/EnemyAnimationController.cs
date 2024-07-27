using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private SpriteRenderer enemyCharacterSPR;

    [SerializeField] private Animator enemyCharacterAnimator;
    [SerializeField] private Sprite[] enemyDeadSprites;
    [SerializeField] private Sprite[] enemyAttackSprites;
    [SerializeField] private Sprite[] enemyBlockSprites;

    private bool deadAnimationStart = false; 
    public bool DeadAnimationStart {get {return deadAnimationStart;} set {deadAnimationStart = value;} }

    private bool attack = default(bool);
    public bool Attack { get { return attack;} set { attack = value; }}

    float timeEnemyDead = 0f;
    //float timeEnemyAttack = 0f;
    private float enemyDeadTimeCounter = 0f;
    private int enemyDeadSpritesIndex = 0;

    private float enemyAttackTimeCounter = 0f;

    private int enemyAttackpritesIndex = 0;

    
    private void Awake() 
    {
        enemyCharacterSPR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!UIManager.Instance.StandbyScreenWorked && !GameManager.Instance.Finish)
        {
            EnemyCharacterAnimationControl();
        }


    }

    private void EnemyDeadAnimation()
    {
        if (deadAnimationStart)
        {
            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",false);
            enemyDeadTimeCounter += Time.deltaTime;

            if (enemyDeadTimeCounter > 0.01f)
            {
                if (enemyDeadSpritesIndex < enemyDeadSprites.Length)
                {
                    enemyCharacterSPR.sprite = enemyDeadSprites[enemyDeadSpritesIndex];
                    enemyDeadSpritesIndex++;
                }
                if (enemyDeadSpritesIndex == enemyDeadSprites.Length - 1)
                {
                    enemyDeadSpritesIndex = enemyDeadSprites.Length - 1;


                }

                timeEnemyDead += Time.deltaTime;

                if (timeEnemyDead > 5)
                {
                    gameObject.SetActive(false);
                    deadAnimationStart = false;
                    if (timeEnemyDead >= 5)
                    {
                        timeEnemyDead = 0f;
                    }
                }
            }
        }
    }

    private void EnemyAttackAnimation()
    {

        if (attack)
        {
            enemyAttackTimeCounter += Time.deltaTime;
            
            if(enemyAttackTimeCounter > 0.01f)
            {
                if (enemyAttackpritesIndex < enemyAttackSprites.Length)
                {
                    enemyCharacterSPR.sprite = enemyAttackSprites[enemyAttackpritesIndex];
                    enemyAttackpritesIndex++;
                }
                if (enemyAttackpritesIndex == enemyAttackSprites.Length - 1)
                {
                    enemyAttackpritesIndex = 0;
                    attack = false;
                    enemyCharacterAnimator.enabled = true;
                }

                enemyAttackTimeCounter = 0f;
                
            }
        }
    }

    public void EnemyCharacterAnimationControl()
    {
            if(!EnemyController.Instance.LeftPosition || EnemyController.Instance.RightPosition)
            {
                enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",true);
            }
            
            EnemyDeadAnimation();
            EnemyAttackAnimation();
       
    }
    

    
    
}
