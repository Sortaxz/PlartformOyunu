using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private GameObject CoinCounterObject;
    [SerializeField] private CoinScriptableObject coinScriptableObject;
    private Rigidbody2D rgb2D;
    private bool coinReadyGo = false;
    private bool isCoinHit = false;
    public bool IsCoinHit { get { return isCoinHit; }  set { isCoinHit = value; } }
    private Vector3 coinCounterPosition;
    private bool isCoinTouched = false;
    private void Awake() 
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
    }


    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        CoinMove();
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            coinReadyGo = true;

        }
        if(other.CompareTag("CoinCounter"))
        {
            isCoinHit = true;
            GameManager.Instance.NumberCollectedCoins++;

            SaveManager.SetCoinCounter(GameManager.Instance.NumberCollectedCoins);
            
            SaveManager.SetCoinSiblingIndex(transform.GetSiblingIndex());

            UIManager.Instance.CoinCounterText.text = SaveManager.GetCoinCounter().ToString();
        }
    }
    private void CoinMove()
    {
        if(coinReadyGo)
        {
            if(!isCoinHit)
            {
                transform.position = Vector2.MoveTowards(transform.position,CoinCounterObject.transform.position,coinScriptableObject.coinSpeed * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
