using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoinManager : MonoBehaviour
{
    private static CoinManager instance;
    public static CoinManager Instance
    {
        get
        {
            instance = FindAnyObjectByType<CoinManager>();
            return instance;
        }
    }
    [SerializeField] private GameObject[] coins;
    private CoinController coinController;
    private int coinsIndex = 0;
    private void Awake()
    {
        coinController = transform.GetChild(0).parent.GetComponent<CoinController>();

    }

    
    
   
}



