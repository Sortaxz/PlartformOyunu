using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
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
}
