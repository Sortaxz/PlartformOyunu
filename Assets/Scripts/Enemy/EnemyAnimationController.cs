using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator enemyCharacterAnimator;
    [SerializeField] private Sprite[] enemyDeadSprites;
    [SerializeField] private Sprite[] enemyAttackSprites;
    [SerializeField] private Sprite[] enemyBlockSprites;
    private void Awake() 
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        EnemyCharacterAnimationControl();
    }

    private void EnemyCharacterAnimationControl()
    {
        if(!EnemyController.Instance.LeftPosition)
        {
            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",true);
        }

        if(EnemyController.Instance.EnemyDead)
        {
            enemyCharacterAnimator.SetBool("IsMaleEnemyWalk",false);
            enemyCharacterAnimator.SetBool("IsMaleEnemyDead",true);
            if(AnimationStateControl("IsMaleEnemyDead"))
            {
                enemyCharacterAnimator.SetBool("IsMaleEnemyDead",false);
            }
        }
    }
    

    private bool AnimationStateControl(string animationName)
    {
        return enemyCharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}
