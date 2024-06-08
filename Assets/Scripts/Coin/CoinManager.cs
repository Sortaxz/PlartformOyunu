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

    private void CoinAlignment()
    {
        
    }

    //DENEME ASAMASINDA

    
    IEnumerator Control()
    {
        if(coinsIndex < coins.Length)
        {
            if(PlayerPrefs.HasKey("isCoinTouched") && coins[coinsIndex] != null)
            {
                if(coins[coinsIndex].transform.GetSiblingIndex() == SaveManager.GetCoinCounter())
                {
                    Destroy(coins[coinsIndex].gameObject);
                }
            }
            else
            {
                coinsIndex++;
            }

            if(coinsIndex == coins.Length - 1)
            {
                StopCoroutine(Control());
            }
        }
        yield return new WaitForSeconds(2);
    }
}



