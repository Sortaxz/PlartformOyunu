using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private Animator checkPointAnimator;
    private SpriteRenderer checkPointSPR;
    [SerializeField] private Sprite[] checkPointSprites;
    private static Vector2 checkPointTransfom;
    private int checkPointIndex = 0;
    private float checkPointTimerCounter = 0f;
    private bool checkpointAnimasonUnlocked = false;
    private bool  checkpointAnimasonShutDown = false;
    private int siblingIndex = 0;
    private int checkPointSiblingIndex =0;

    private void Awake() 
    {
        checkPointSPR = GetComponent<SpriteRenderer>();    
        checkPointSiblingIndex = transform.GetSiblingIndex();
        checkPointAnimator = GetComponent<Animator>();
    }

   

    void Update()
    {
    }
    
    private void CheckPointControl()
    {
        if (checkPointSiblingIndex > siblingIndex)
        {
            siblingIndex = checkPointSiblingIndex;
            checkPointTransfom = transform.position;
        }
        else
        {

        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            
            checkPointAnimator.SetBool("StartCheckPointAnimation",true);
            
            CheckPointControl();
        }

    }


    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            checkpointAnimasonUnlocked = false;
            checkpointAnimasonShutDown = true;
            checkPointAnimator.SetBool("EndCheckPointAnimation",true);
            checkPointAnimator.SetBool("StartCheckPointAnimation",false);
        }    
    }
    
    public static Vector2 CheckPointPosition()
    {
        return checkPointTransfom;
    }


    void CheckPointAnimation()
    {
        if(checkpointAnimasonUnlocked)
        {
            checkPointTimerCounter += Time.deltaTime;

            if(checkPointTimerCounter > .1f)
            {
                checkPointTimerCounter = 0f;
                
                checkPointSPR.sprite = checkPointSprites[checkPointIndex];

                if(checkPointIndex != checkPointSprites.Length-1)
                {
                    checkPointIndex++;
                }
                else if(checkPointIndex == checkPointSprites.Length - 1)
                {
                    checkPointIndex = checkPointSprites.Length - 1;
                    checkpointAnimasonUnlocked =false;
                }
            }
        }

        if(checkpointAnimasonShutDown)
        {
            checkPointTimerCounter += Time.deltaTime;
            if(checkPointTimerCounter > .1f)
            {
                checkPointTimerCounter = 0f;
                
                checkPointSPR.sprite = checkPointSprites[checkPointIndex];

                if(checkPointIndex > 0)
                {
                    checkPointIndex--;
                }
                else if(checkPointIndex == 0)
                {
                    checkPointIndex = 0;
                    checkpointAnimasonShutDown = false;
                }
            }
        }
    }
}
