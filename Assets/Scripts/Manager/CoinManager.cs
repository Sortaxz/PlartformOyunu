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
    public GameObject[] Coins { get { return coins; } }
    private CoinController coinController;
    int coinsCounter =0;
    private void Awake()
    {
        coinController = transform.GetChild(0).parent.GetComponent<CoinController>();
        HitCoinControl();
        
    }

    
    public  void HitCoinControl()
    {
       for (int i =0; i < coins.Length; i++)
       {
            if(PlayerPrefs.HasKey($"hitCoin{i}"))
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            if(coinsCounter == coinsCounter - 1)
            {
                return;
            }
       }
            
       
    }
   
}



