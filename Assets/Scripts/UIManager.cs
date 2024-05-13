using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Image heartLeftImage;
    [SerializeField] private Image heartMiddleImage;
    [SerializeField] private Image heartRightImage;

    [SerializeField] private GameObject itemCreationPoint;

    [SerializeField] private Image itemImage;
    public Image ItemImage
    {
        get { return itemImage; }
    }
    Image spawnItemImage;

    bool hasItemChange = false;
    public bool HasItemChange 
    { 
        get
        {
            return hasItemChange;
        }
    }
    private bool characterLifeReset = false;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void SpawnImageAndPositionSetings()
    {
        itemImage = Resources.Load<Image>("Prefabs/Item");
        Vector3 itemImagePosition =Camera.main.WorldToScreenPoint(itemCreationPoint.transform.position);
        spawnItemImage = Instantiate(itemImage,itemImagePosition,Quaternion.identity, transform);
       
    }

    
    void Update()
    {
        UIInputControl();
        if(!characterLifeReset)
        {
            if(gameManager.mainCharacter.HitObstacle)
            {
                HeartFillAmountControl(0.02f);
            }
            if(gameManager.mainCharacter.HitEnemy)
            {
                HeartFillAmountControl(0.04f);

            }
        }
        else
        {
            HeartFillAmountReset();
            GameManager.Instance.isCharacterOnPoint = true;
            gameManager.mainCharacter.IsCharacterDead = false;
        }
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
