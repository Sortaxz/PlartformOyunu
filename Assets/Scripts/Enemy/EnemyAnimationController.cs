using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator enemyCharacterAnimator;
    private void Awake() 
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void EnemyCharacterAnimationControl()
    {
        if(!EnemyController.Instance.LeftPosition)
        {

        }
    }
  
}
