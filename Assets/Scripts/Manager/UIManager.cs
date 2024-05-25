using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [SerializeField] private Image heartLeftImage;
    [SerializeField] private Image heartMiddleImage;
    [SerializeField] private Image heartRightImage;

    [SerializeField] private GameObject itemCreationPoint;

    [SerializeField] private Image itemImage;
    
    [SerializeField] private Image sahneGecisEkrani;
    [SerializeField] private TextMeshProUGUI gecisEkranYazisi;

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
    private bool characterLifeReset = false;
    public bool CharacterLifeReset { get; set; }

    private bool isTransitionOver  = false;
    public bool IsTransitionOver { get { return isTransitionOver;}}

    private bool transitionScreenOver = false;
    public bool TransitionScreenOver { get { return transitionScreenOver;} set { transitionScreenOver = value; } }

    private bool stageTransition =false;
    public bool StageTransition { get { return stageTransition;} set { stageTransition = value; } }


    private float imageRgbA  =0f;
    private float textRgbA  =0f;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        scene_Manager = Scene_Manager.Instance;
        
    }

    void Update()
    {
        UIInputControl();
        LifeDecreaseAndReset();

        SceneGecisi();
    }

    private void SceneGecisi()
    {
        if(stageTransition)
        {
            
                if(!transitionScreenOver)
                {

                    if(imageRgbA < 1.0f)
                    {
                        imageRgbA += 0.2f * Time.deltaTime;
                        sahneGecisEkrani.color = new Color(sahneGecisEkrani.color.r, sahneGecisEkrani.color.g, sahneGecisEkrani.color.b,imageRgbA);
                    
                        
                        textRgbA += 0.2f * Time.deltaTime;
                        gecisEkranYazisi.text = scene_Manager.GetNextSceneName();
                        gecisEkranYazisi.color = new Color(gecisEkranYazisi.color.r, gecisEkranYazisi.color.g, gecisEkranYazisi.color.b,textRgbA);

                        if(imageRgbA >= 1.0f && textRgbA >= 1.0f)
                        {
                            transitionScreenOver = true;
                        }
                    }
                        
                }
                else
                {
                    if(imageRgbA >= 0f)
                    {
                        imageRgbA -= 0.2f * Time.deltaTime;
                        sahneGecisEkrani.color = new Color(sahneGecisEkrani.color.r, sahneGecisEkrani.color.g, sahneGecisEkrani.color.b,imageRgbA);

                        textRgbA -= 0.2f * Time.deltaTime;
                        gecisEkranYazisi.color = new Color(gecisEkranYazisi.color.r, gecisEkranYazisi.color.g, gecisEkranYazisi.color.b,textRgbA);

                        if(imageRgbA <= 0f)
                        {
                            transitionScreenOver = false;
                            stageTransition =false;
                            scene_Manager.IsStageTransition = true;
                                
                        }
                    }
                }

           
        }
           
    }


    private void LifeDecreaseAndReset()
    {
        if (!characterLifeReset)
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
        }
        else
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

}
