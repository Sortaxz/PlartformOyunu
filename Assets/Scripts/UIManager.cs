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
    private void Awake()
    {
        SpawnImageAndPositionSetings();
    }

    private void SpawnImageAndPositionSetings()
    {
        itemImage = Resources.Load<Image>("Prefabs/Item");
        Vector3 itemImagePosition =Camera.main.WorldToScreenPoint(itemCreationPoint.transform.position);
        spawnItemImage = Instantiate(itemImage,itemImagePosition,Quaternion.identity, transform);
       
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(2) )
        {
            hasItemChange = !hasItemChange;
        }
    }
}
