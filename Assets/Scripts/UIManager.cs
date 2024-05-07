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
    private void Awake()
    {
        SpawnImageAndPositionSetings();
    }

    private void SpawnImageAndPositionSetings()
    {
        itemImage = Resources.Load<Image>("Prefabs/Item");
        spawnItemImage = Instantiate(itemImage, transform);
        //spawnItemImage.rectTransform.position = new Vector3(516.5f, 188.5f, 0.00f);
        //spawnItemImage.rectTransform.localPosition = new Vector3(900, 400, 0.00f);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(2) )
        {
            hasItemChange = !hasItemChange;
        }
       
    }

}
