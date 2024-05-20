using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutFormControl : MonoBehaviour
{
    GameManager gameManager;

    private void Awake() 
    {
        gameManager = GameManager.Instance;    
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            gameManager.CreateWind = false;
            gameManager.CreateEnemyFireball = false;
            gameManager.FireballWasFired = true;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("enemyFireball"))
        {
            gameManager.CreateEnemyFireball = true;
            gameManager.CreateWind = true;
            Destroy(other.gameObject);
        }  
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.mainCharacter.IsCharacterDead = true;
            gameManager.isCharacterOnPoint = false;
        }
    }
}
