using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] fireballSprites;
    
    float fireballTimerCounter = 0f;
    private int fireballIndex = 0;

    SpriteRenderer fireballSPR;

    private void Awake() 
    {
        fireballSPR = GetComponent<SpriteRenderer>();    
    }

    void Start()
    {
        FireballDeletion();
    }

    void Update()
    {
        FireballAnimasyon();
    }

    void FireballDeletion()
    {
        /*
        if(transform.tag == "fireball")
        {
            Destroy(gameObject,10);
        }
        */
    }

    void FireballAnimasyon()
    {
        fireballTimerCounter += Time.deltaTime;
        
        if(fireballTimerCounter > 0.2f)
        {
            fireballTimerCounter = 0f;
            fireballSPR.sprite = fireballSprites[fireballIndex++];

            if(fireballIndex == fireballSprites.Length - 1)
            {
                fireballIndex = 0;
            }
        }
    }
}
