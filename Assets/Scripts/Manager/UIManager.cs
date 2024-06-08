using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
            }
            return instance;
        }
    }
    private GameManager gameManager;
    private Scene_Manager scene_Manager;

    [SerializeField] private UI_Elemets uI_Elemets;
    public UI_Elemets UI_Elemets { get { return uI_Elemets;}}
    
    [SerializeField] private Animator uI_ElemetsAnimator;

    [SerializeField] private Slider progress;
    [SerializeField] private TextMeshProUGUI gecisEkranYazisi;

    [SerializeField] private Image heartLeftImage;
    public Image HeartLeftImage { get { return heartLeftImage;} set { heartLeftImage = value;}}

    [SerializeField] private Image heartMiddleImage;
    public Image HeartMiddleImage { get { return heartMiddleImage;} set { heartMiddleImage = value;}}

    [SerializeField] private Image heartRightImage;
    public Image HeartRightImage { get { return heartRightImage;} set { heartRightImage = value;}}

    [SerializeField] private GameObject itemCreationPoint;

    [SerializeField] private Image itemImage;
    
    [SerializeField] private Image sahneGecisEkrani;
    [SerializeField] private Image coinCounteImage;
    public Image ItemImage
    {
        get { return itemImage; }
    }

    bool hasItemChange = false;
    public bool HasItemChange 
    { 
        get
        {
            return hasItemChange;
        }
    }

    [SerializeField] private GameObject WaltScreen;
    
    [SerializeField] private float proggresAddedValue; 
    private bool characterLifeReset = false;
    public bool CharacterLifeReset { get; set; }

    private bool transitionScreenOver = false;
    public bool TransitionScreenOver { get { return transitionScreenOver;} set { transitionScreenOver = value; } }
    
    private bool stageTransitionAnimationStarts = true;
    public  bool StageTransitionAnimationStarts { get { return stageTransitionAnimationStarts;} set { stageTransitionAnimationStarts = value;}}
    
    private bool stageTransitionAnimationEnds = false;
    public bool StageTransitionAnimationEnds { get { return stageTransitionAnimationEnds;} set{ stageTransitionAnimationEnds = value; } }
    
    private bool transitionTextAnimationStarts  =false;
    public bool TransitionTextAnimationStarts { get { return transitionTextAnimationStarts;} set { transitionTextAnimationStarts = value;}}

    private bool isProgresStart = false;
    public bool IsProgresStart { get { return isProgresStart;} set { isProgresStart = value;}}

    private bool secondsReadyStart = false;
    private bool startUpProgresTransition = false;
    public bool StartUpProgresTransition { get { return startUpProgresTransition;} set { startUpProgresTransition = value;}}
    
    private bool finishStartUpProgress = false;
    public bool FinishStartUpProgress { get { return finishStartUpProgress;} set { finishStartUpProgress = value;}}

    private bool standbyScreenWorked = false;
    public bool StandbyScreenWorked { get { return standbyScreenWorked;}}
    [SerializeField] private TextMeshProUGUI coinCounterText;
    public TextMeshProUGUI CoinCounterText {get { return coinCounterText;} set { coinCounterText= value;}}
    private float seconds = 0f;


    private void Awake()
    {
        gameManager = GameManager.Instance;
        scene_Manager = Scene_Manager.Instance;


        heartLeftImage.fillAmount = SaveManager.GetLifeValue("leftHeart");
        heartMiddleImage.fillAmount = SaveManager.GetLifeValue("middleHeart");
        heartRightImage.fillAmount = SaveManager.GetLifeValue("rightHeart");

    }

    void Update()
    {
        NewMethod();

        UIInputControl();
        LifeDecreaseAndReset();

    }

    private void NewMethod()
    {
        if (gameManager.CharcDead)
        {
            SaveManager.LastHeartSaveMethod();
            heartLeftImage.fillAmount = SaveManager.GetLifeValue("leftHeart");
            heartMiddleImage.fillAmount = SaveManager.GetLifeValue("middleHeart");
            heartRightImage.fillAmount = SaveManager.GetLifeValue("rightHeart");

            if (heartRightImage.fillAmount >= 1f)
            {
                gameManager.CharcDead = false;
            }
        }
    }

    public void FinishLevelTransition(float holdForSeconds,bool endLevelTransition)
    {
        scene_Manager.EndLevelTransition = endLevelTransition == true ? true : false;
        coinCounterText.enabled = false;
        coinCounteImage.enabled = false;
        if(stageTransitionAnimationStarts)
        {
            uI_ElemetsAnimator.SetBool("startTransition",true);
            gameManager.StopGameFlow = true;
        }
        if(!stageTransitionAnimationStarts)
        {

            uI_ElemetsAnimator.SetBool("startTransition",false);
        }
        
        if(isProgresStart)
        {
            uI_Elemets.ProgresInstallation(proggresAddedValue);
        }

        if(transitionTextAnimationStarts)
        {
            uI_ElemetsAnimator.SetBool("endTextTransition",true);
        }


        if(stageTransitionAnimationEnds)
        {
            uI_ElemetsAnimator.SetBool("endTextTransition",false);
            secondsReadyStart = true;
            stageTransitionAnimationEnds = false;
        }

        
        if(secondsReadyStart)
        {
            seconds += Time.deltaTime;
            
            if(seconds > holdForSeconds)
            {
                
                if(gameManager.StageTransitionReady)
                {
                    gameManager.StageTransitionOver = true;
                    gameManager.StageTransitionReady = false;
                    
                }
                
            }
        }

    }

    

    private void LifeDecreaseAndReset()
    {
        if (!characterLifeReset && gameManager.mainCharacter != null )
        {
            if (gameManager.mainCharacter.HitObstacle)
            {
                HeartFillAmountControl(0.02f);
            }
            if (gameManager.mainCharacter.HitEnemy)
            {
                HeartFillAmountControl(0.04f);

            }
            if (gameManager.mainCharacter.HitAbsorbingObject)
            {
                HeartFillAmountControl(0.03f);
            }
            if (gameManager.mainCharacter.HitEnemyFireball)
            {
                HeartFillAmountControl(0.045f);

            }
            if (gameManager.mainCharacter.LifeDwindling)
            {
                HeartFillAmountControl(0.001f);
            }
            else
            {
                gameManager.mainCharacter.LifeDwindling = false;
            }
        }
        else if(characterLifeReset )
        {

            GameManager.Instance.isCharacterOnPoint = true;
            gameManager.mainCharacter.IsCharacterDead = false;
            HeartFillAmountReset();
            ReducingBooleanControl();
            characterLifeReset = false;
        }
    }

    private void ReducingBooleanControl()
    {
        gameManager.mainCharacter.HitObstacle = false;
        gameManager.mainCharacter.HitAbsorbingObject = false;
        gameManager.mainCharacter.LifeDwindling = false;
        gameManager.mainCharacter.HitEnemy = false;
        gameManager.mainCharacter.HitEnemyFireball = false;
    }

    private void UIInputControl()
    {
        if (Input.GetMouseButtonDown(2))
        {
            hasItemChange = !hasItemChange;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            standbyScreenWorked = !standbyScreenWorked;
            if(standbyScreenWorked)
            {
                WaltScreen.SetActive(true);
            }
            else
            {
                WaltScreen.SetActive(false);

            }
        }
    }

    private void HeartFillAmountControl(float hearatValueMinimization)
    {
        if(heartLeftImage.fillAmount > 0)
        {
            heartLeftImage.fillAmount -= hearatValueMinimization;
        }
        else if(heartMiddleImage.fillAmount > 0)
        {
            heartMiddleImage.fillAmount -= hearatValueMinimization;
        }
        else if(heartRightImage.fillAmount > 0)
        {
            heartRightImage.fillAmount -= hearatValueMinimization;
        }
        else
        {
            characterLifeReset = true;  
            GameManager.Instance.isCharacterOnPoint = false;
            gameManager.mainCharacter.IsCharacterDead = true;
        }
    }
    private void HeartFillAmountReset()
    {
        heartLeftImage.fillAmount = 1f;
        heartMiddleImage.fillAmount = 1f;
        heartRightImage.fillAmount = 1f;
        characterLifeReset = true;
        
    }

    public void ResumeButtonMethod()
    {
        WaltScreen.SetActive(false);
        standbyScreenWorked = false;
    }

    public void ExitButtonMethod()
    {
        SceneManager.LoadScene(0);
        
    }
    
}
