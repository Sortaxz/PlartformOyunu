using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Enemy enemy;
    SpriteRenderer enemySPR;
    [SerializeField]private Sprite[] deadSprites;
    float deadTime = 0;
    int deadSpritesCount = 0;
    private void Awake() 
    {
        enemy = GetComponent<Enemy>();
        enemySPR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        EnemyAnimation();
    }

    void EnemyAnimation()
    {
        if(enemy.isEnemyDead)
        {
            deadTime += Time.deltaTime;
            if(deadTime > 0.1f)
            {
                deadTime = 0f;
                enemySPR.sprite = deadSprites[deadSpritesCount++];
                
                if(deadSpritesCount == deadSprites.Length-1)
                {
                    deadSpritesCount = 0;
                    enemy.isEnemyDead =false;
                    Destroy(enemy.gameObject,1);
                }
            }
        }
    }
}
