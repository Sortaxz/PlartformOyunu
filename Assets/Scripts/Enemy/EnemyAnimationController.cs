using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator enemyCharacterAnimator;
    [SerializeField] private Sprite[] enemyDeadSprites;
    [SerializeField] private Sprite[] enemyAttackSprites;
    [SerializeField] private Sprite[] enemyBlockSprites;
    float timeEnemyDead = 0f;
    private void Awake() 
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(!UIManager.Instance.StandbyScreenWorked && !GameManager.Instance.Finish)
        {
            EnemyCharacterAnimationControl();
        }
       
        
    }

    private void EnemyCharacterAnimationControl()
    {
        if(!EnemyController.Instance.LeftPosition || EnemyController.Instance.RightPosition)
        {
            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",true);
        }
        
        if(EnemyController.Instance.EnemyAttackAnimation)
        {
            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",false);

            enemyCharacterAnimator.SetBool("IsMaleEnemyAttack",true);
            
            GameManager.Instance.mainCharacter.CharacterHealthDecrease = true;
            GameManager.Instance.mainCharacter.HitEnemy = true;
            EnemyController.Instance.SwordStrike = true;
        }
        else
        {
            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",true);

            enemyCharacterAnimator.SetBool("IsMaleEnemyAttack",false);
            
            
        }
        

        if(EnemyController.Instance.EnemyDead)
        {
            timeEnemyDead += Time.deltaTime;

            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",false);
            enemyCharacterAnimator.SetBool("IsMaleEnemyDead",true);
            
            if(AnimationStateControl("Male_Enemy_Died"))
            {
                enemyCharacterAnimator.SetBool("IsMaleEnemyDead",false);

            }
            

            if(timeEnemyDead > 5f)
            {
                if(EnemyController.Instance.Healt == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    

    private bool AnimationStateControl(string animationName)
    {
        return enemyCharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
    
}
