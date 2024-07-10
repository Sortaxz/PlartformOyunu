using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CranbolineControl : MonoBehaviour
{
    private int sayi = 0;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("enemyFireball"))
        {
            sayi++;
            if(sayi == 3)
            {
                transform.gameObject.SetActive(false);
            }
        }    
    }
}
