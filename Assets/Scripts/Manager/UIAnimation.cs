using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    private static UIAnimation instance;
    public static UIAnimation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIAnimation>();
            }
            return instance;
        }
    }

    private UIManager uIManager;
    [SerializeField]private Image canvasImage;
    [SerializeField]private Sprite swordItemSprite;
    [SerializeField]private Sprite fireballItemsSprite;

    private bool swordAttackBegin = false;
    public bool SwordAttackBegin 
    {
        get { return swordAttackBegin;}
    }


    private bool leftHeart = false;
    private bool middleHeart = false;
    private bool rightHeart = false;
    
    private float timeCounter = 0f;

    private void Awake() 
    {
        uIManager = UIManager.Instance;
    }


    void Start()
    {
        canvasImage = transform.GetChild(0).GetComponent<Image>();

        
    }

    void Update()
    {
        UIAnimationController();
        HealtAnimation();

    }

    private void HealtAnimation()
    {
        if (GameManager.Instance.mainCharacter.HitObstacle)
        {
            timeCounter = 0f;
            if (uIManager.HeartLeftImage.fillAmount < 1f && uIManager.HeartLeftImage.fillAmount > 0f)
            {
                uIManager.HeartLeftImage.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 130);
                leftHeart = true;
            }
            else if (uIManager.HeartMiddleImage.fillAmount < 1f && uIManager.HeartMiddleImage.fillAmount >= 0f)
            {
                uIManager.HeartMiddleImage.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 130);
                middleHeart = true;
            }
            else if (uIManager.HeartRightImage.fillAmount < 1f && uIManager.HeartRightImage.fillAmount >= 0f)
            {
                uIManager.HeartRightImage.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 130);
                rightHeart = true;
            }


        }
        else
        {
            
            timeCounter += Time.deltaTime;


            if (timeCounter >= .3f)
            {
                if (leftHeart)
                {
                    uIManager.HeartLeftImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    leftHeart = false;
                }

                if (middleHeart)
                {
                    uIManager.HeartMiddleImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                }

                if (rightHeart)
                {
                    uIManager.HeartRightImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                }
            }
        }
    }

    void UIAnimationController()
    {
        if(uIManager.HasItemChange == true)
        {
            swordAttackBegin = true;
            canvasImage.color = new Color(255,255,255,255);
            canvasImage.sprite = swordItemSprite;
        }
        else if(uIManager.HasItemChange == false)
        {
            swordAttackBegin = false;
            canvasImage.color = new Color(255,255,255,255);
            canvasImage.sprite = fireballItemsSprite;
        }
    }

    private void HeartAnimation()
    {
        
       
    }
    

    

   
}
