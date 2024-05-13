using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//0.5 / 2 / 0 localScale
public class PlartformManager : MonoBehaviour
{
    [SerializeField] private GameObject SpakePrefab;
    [SerializeField] private GameObject[] SpakeObject;
    [SerializeField]private Vector2[] SpakeObjectPositons;
    [SerializeField] private bool[] SpakeDuration;
    private Transform spakeObjectTransform;
    [SerializeField] private float spakeDurationTime;
    private void Awake()
    {
        CreateSpakeObject();
        for (int i = 0; i < SpakeDuration.Length; i++)
        {
            SpakeController spakeController = SpakeObject[i].GetComponent<SpakeController>();
            spakeController.SpakeDuration = SpakeDuration[i];
            spakeController.SpakeDurationTime = spakeDurationTime;
        }
    }


    private void CreateSpakeObject()
    {
        spakeObjectTransform = transform.GetChild(0);
        for (int i = 0; i < SpakeObject.Length; i++)
        {
            SpakeObject[i] = Instantiate(SpakePrefab, SpakeObjectPositons[i], Quaternion.identity, spakeObjectTransform);
            SpakeObject[i].name = "Spake" + spakeObjectTransform.childCount;
        }


        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    

}
