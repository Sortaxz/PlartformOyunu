using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static AnimationController instance;
    public static AnimationController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<AnimationController>();
            }
            return instance;
        }
    }

    private Animator characterAnimator;

    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] runSprites;
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] deshSprites;
    [SerializeField] private Sprite[] attackSprites;
    [SerializeField] private Sprite[] fireballSkillSprites;
    [SerializeField] private Sprite[] fireballSprites;
    [SerializeField] private Sprite[] hurtSprites;
    [SerializeField] private Sprite[] strikeAttackSprites;

    public CharacterControl character;
    
    [SerializeField ]SpriteRenderer characteSPR; // character'ın SpriteRenderer'ı

    public bool fireballReady = false;
    private bool birKere = false;


    private float idleSpritesTimeCounter = 0f;
    private float runSpritesTimeCounter = 0f;
    private float fireballSkillSpritesTimeCounter = 0f;
    private float attackSpritesTimeCounter = 0f;
    private float strikeSpritesTimeCounter = 0f;
    private float hurtSpritesTimeCounter = 0f;

    private float horizontal;
    public float Horizontal{get{ return horizontal;}}

    private int idleSpritesCount = 0;
    private int runSpritesCount = 0;
    private int jumpSpritesCount = 0;
    private int deshSpritesCount = 0;
    private int attackSpritesCount = 0;
    private int fireballSkillSpritesCount = 0;
    private int jumpingContinueIndex =0;
    private int hurtSpritesIndex = 0; 
    private int strikeAttackSpritesIndex = 0;
    private void Awake() 
    {
        characteSPR = GetComponent<SpriteRenderer>();
        jumpingContinueIndex = jumpSprites.Length-1;
    }
    void Start()
    {
        character = GameManager.Instance.mainCharacter;
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationControl();
    }

    void AnimationControl()
    {
        
        if(!GameManager.Instance.StageTransitionReady && !UIManager.Instance.StandbyScreenWorked)
        {
            horizontal = Input.GetAxis("Horizontal") ;
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
            if(character.isCharacterAbove )
            {
                characteSPR.sprite = jumpSprites[jumpSpritesCount++];

                

                if(jumpSpritesCount == jumpSprites.Length - 1)
                {
                    jumpSpritesCount = 0;
                }
            }

            #endregion

            if(character.jumpAnimationResume)
            {
                characteSPR.sprite = jumpSprites[jumpingContinueIndex--];

                

                if(jumpingContinueIndex == 0)
                {
                    jumpingContinueIndex = jumpSprites.Length - 1;
                }
            }

            #region  Karakterimiz'in Desh Animasyonu'nun kodlari
            if(!character.isCharacterAbove)
            {
                if(character.isCharacterSlidDown)
                {
                    characteSPR.sprite = deshSprites[deshSpritesCount++];

                    if(deshSpritesCount == deshSprites.Length - 1)
                    {
                        deshSpritesCount = 0;
                    }
                    
                }   
            }
            #endregion
            
            #region  Karakterimiz'in Atak Animasyonu'nun kodlari
            if(character.readyToAttack)
            {
                attackSpritesTimeCounter += Time.deltaTime;
                
                if(attackSpritesTimeCounter > 0.07f)
                {
                    gameObject.GetComponent<PolygonCollider2D>().enabled =true;
                    characteSPR.sprite = attackSprites[attackSpritesCount++];

                    if(attackSpritesCount == attackSprites.Length - 1)
                    {
                        attackSpritesCount = 0;
                        character.readyToAttack = false;

                        
                    }
                    attackSpritesTimeCounter = 0f;
                }
            }
            
            #endregion


            #region  Karakterimiz'in Ateş Topu Atmaya Hazilanma Animasyonu
            if(character.readyToFireballAttack)
            {
                fireballSkillSpritesTimeCounter += Time.deltaTime;
                if(fireballSkillSpritesTimeCounter > 0.05f)
                {
                    characteSPR.sprite = fireballSkillSprites[fireballSkillSpritesCount++];

                    if(fireballSkillSpritesCount == fireballSkillSprites.Length - 1)
                    {
                        fireballSkillSpritesCount = 0;
                        character.readyToFireballAttack = false;
                        fireballReady = true;
                    }
                    fireballSkillSpritesTimeCounter = 0f;

                }

            }
            #endregion

            #region  Karakterimiz'in Hurt Animasyonu

            if(character.StartHurtAnimation)
            {
                hurtSpritesTimeCounter += Time.deltaTime;
                if(hurtSpritesTimeCounter > 0.03f)
                {
                    if(hurtSpritesIndex < hurtSprites.Length)
                    {
                        characteSPR.sprite = hurtSprites[hurtSpritesIndex++];
                        
                        if(hurtSpritesIndex == hurtSprites.Length - 1)
                        {
                            hurtSpritesIndex = 0;

                            character.StartHurtAnimation = false;
                        }
                    }
                    hurtSpritesTimeCounter = 0f;
                }
            }

            #endregion

            #region  Karakterimiz'in Strike attak hazirlama kodu
            
            if(character.ReadyToStrikeAttack)
            {
                strikeSpritesTimeCounter += Time.deltaTime;

                if(strikeSpritesTimeCounter > 0.05f)
                {
                    characteSPR.sprite = strikeAttackSprites[strikeAttackSpritesIndex++];

                    if(strikeAttackSpritesIndex == strikeAttackSprites.Length - 1)
                    {
                        strikeAttackSpritesIndex = 0;

                        character.ReadyToStrikeAttack = false;
                    }
                    strikeSpritesTimeCounter = 0f;
                }
            }

            #endregion
        }
    }   
   
}
