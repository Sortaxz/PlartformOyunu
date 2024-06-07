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
        coins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
            if(i == transform.childCount -1)
            {
        
                break;
            }
        }
    }
    private void Start() 
    {
        StartCoroutine(Control());        
    }
    private void Update() 
    {
    }
    IEnumerator Control()
    {
        if(coinsIndex < coins.Length)
        {
            if(PlayerPrefs.HasKey("isCoinTouched") && coins[coinsIndex] != null)
            {
                if(coins[coinsIndex].transform.GetSiblingIndex() == SaveManager.GetCoinSiblingIndex())
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



