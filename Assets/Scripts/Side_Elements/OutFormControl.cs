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
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.mainCharacter.IsCharacterDead = true;
            gameManager.isCharacterOnPoint = false;
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }


        if (other.gameObject.CompareTag("Wind"))
        {
            gameManager.CreateWind = false;
            gameManager.CreateEnemyFireball = true;
            gameManager.FireballWasFired = true;
            other.gameObject.SetActive(false);
        }

        if(gameManager.WindObject)
        {
           
            if (other.gameObject.CompareTag("Wind"))
            {
                gameManager.CreateWind = true;
                gameManager.CreateEnemyFireball = false;
                gameManager.FireballWasFired = false;
                other.gameObject.SetActive(false);
            } 
        }

        if(other.gameObject.CompareTag("enemyFireball"))
        {
            gameManager.CreateEnemyFireball = false;
            gameManager.CreateWind = true;

            other.gameObject.SetActive(false);            
        } 
        if(gameManager.EnemyFireballl)
        {
            if(other.gameObject.CompareTag("enemyFireball"))
            {
                gameManager.CreateEnemyFireball = true;
                gameManager.CreateWind = false;

                other.gameObject.SetActive(false);            
            }  
        }

        if(other.gameObject.CompareTag("fireball"))
        {
            other.gameObject.SetActive(false);
        }

        if(other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
        
    }
}
