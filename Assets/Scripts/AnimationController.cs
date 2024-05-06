using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] runSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] deshSprites;
    [SerializeField] private Sprite[] attackSprites;
    
    public CharacterControl character;
    
    Rigidbody2D charcterRb2D;
    [SerializeField ]SpriteRenderer characteSPR; // character'ın SpriteRenderer'ı

    private float idleSpritesTimeCounter = 0f;
    private float runSpritesTimeCounter = 0f;
    private float attackSpritesTimeCounter = 0f;
    
    

    private int idleSpritesCount = 0;
    private int runSpritesCount = 0;
    private int jumpSpritesCount = 0;
    private int deshSpritesCount = 0;
    private int attackSpritesCount = 0;

    private float horizontal;
    private void Awake() 
    {
        character = GetComponent<CharacterControl>();
        characteSPR = GetComponent<SpriteRenderer>();
        charcterRb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") ;
        AnimationControl();
    }

    void AnimationControl()
    {
        #region  Karakterimiz idle ve run animasyon kodlari
        if(horizontal == 0)
        {
            idleSpritesTimeCounter+= Time.deltaTime;
            if(idleSpritesTimeCounter > 0.25f)
            {
                idleSpritesTimeCounter = 0f;
                characteSPR.sprite = idleSprites[idleSpritesCount++];

                if(idleSpritesCount == idleSprites.Length - 1)
                {
                    idleSpritesCount = 0;
                }
            }
        }
        else if(horizontal > 0 )
        {
            runSpritesTimeCounter += Time.deltaTime;
            
            if(runSpritesTimeCounter > .1f)
            {
                runSpritesTimeCounter = 0f;
                characteSPR.sprite = runSprites[runSpritesCount++];

                if(runSpritesCount == runSprites.Length - 1)
                {
                    runSpritesCount = 0;
                }
            }
        }
        else
        {
            runSpritesTimeCounter += Time.deltaTime;
            
            if(runSpritesTimeCounter > 0.1f)
            {
                runSpritesTimeCounter = 0f;
                characteSPR.sprite = runSprites[runSpritesCount++];

                if(runSpritesCount == runSprites.Length -1 )
                {
                    runSpritesCount = 0;
                }
            }
            
        }
        #endregion

        #region  Karakterimiz'in Ziplama Animasyonu'nun kodlari

        if(character.isCharacterAbove)
        {
            characteSPR.sprite = jumpSprites[jumpSpritesCount++];

            if(jumpSpritesCount == jumpSprites.Length - 1)
            {
                jumpSpritesCount = 0;
            }
        }

        #endregion

        #region  Karakterimiz'in Desh Animasyonu'nun kodlari
        if(character.isCharacterSlidDown)
        {
            characteSPR.sprite = deshSprites[deshSpritesCount++];

            if(deshSpritesCount == deshSprites.Length - 1)
            {
                deshSpritesCount = 0;
            }
            
        }   
        #endregion

        if(character.readyToAttack)
        {
            characteSPR.sprite = attackSprites[attackSpritesCount++];

            if(attackSpritesCount == attackSprites.Length - 1)
            {
                attackSpritesCount = 0;
                character.attack = false;
            }
        }
        
    }

}
