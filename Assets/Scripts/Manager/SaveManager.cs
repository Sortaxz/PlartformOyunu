using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static void FirstTimeSaveMethod()
    {
        if(!PlayerPrefs.HasKey("LastMusicVolume"))
        {
            PlayerPrefs.SetFloat("LastMusicVolume",0.1f);
        }
        if(!PlayerPrefs.HasKey("LastLevelIndex"))
        {
            PlayerPrefs.SetFloat("LastLevelIndex",1);
        }
    }

   
    #region  Getters

    public static int GetLastLevelIndex()
    {
        return PlayerPrefs.HasKey("LastLevelIndex") ? PlayerPrefs.GetInt("LastLevelIndex") : 1;
    }

    public static float GetLastMusicVolume()
    {
        return PlayerPrefs.GetFloat("LastMusicVolume");
    }

    public static float GetLastMusicTime()
    {
        return PlayerPrefs.GetFloat("musicPlayerTime");
    }

    public static int GetCheckPointIndex()
    {
        return PlayerPrefs.GetInt("CheckPoint");
    }
    
    public static int GetCoinCounter()
    {
        return PlayerPrefs.HasKey("coinCounter") ? PlayerPrefs.GetInt("coinCounter") : 0;
    }
    
    public static int GetCoinSiblingIndex()
    {
        return PlayerPrefs.GetInt("isCoinTouched");
    }

    public static float GetLifeValue(string whichLife,float heartValue = 0)
    {
        
        if(whichLife == "leftHeart") 
        {
            heartValue= PlayerPrefs.HasKey("leftHeart")  ? PlayerPrefs.GetFloat("leftHeart") : 1f;
        } 
        if(whichLife == "middleHeart")
        {
            heartValue =  PlayerPrefs.HasKey("middleHeart") ? PlayerPrefs.GetFloat("middleHeart") : 1f;
        }
        if(whichLife == "rightHeart")
        {
            heartValue =PlayerPrefs.HasKey("rightHeart") ? PlayerPrefs.GetFloat("rightHeart") : 1f;
        }
        return heartValue;
    }
    
    #endregion


    #region  Setters

    public static void SetLastLevelIndex(int value)
    {
        PlayerPrefs.SetInt("LastLevelIndex", value);
    }

    public static void SetLastMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("LastMusicVolume", value);
    }
    public static void SetLastMusicTime(float timeValue)
    {
        PlayerPrefs.SetFloat("musicPlayerTime", timeValue);
    }
    
    public static void SetCheckPointIndex(int checkPointIndex)
    {
        PlayerPrefs.SetInt("CheckPoint",checkPointIndex);
    }
    public static void SetCoinCounter(int coinCounter)
    {
        PlayerPrefs.SetInt("coinCounter", coinCounter);
    }
    public static void SetCoinSiblingIndex(int coinSiblingIndex)
    {
        PlayerPrefs.SetInt("isCoinTouched",coinSiblingIndex);
    }

    public static void SetLifeValue(string whichLife,float lifeValue)
    {
        PlayerPrefs.SetFloat(whichLife,lifeValue);
    }

    public static void SetHitCoinIndex(int hitCoinIndex)
    {
        PlayerPrefs.SetInt($"hitCoin{hitCoinIndex}",hitCoinIndex);
    }
    

    #endregion

    
    public static void ResetHitCoinIndex(int coinCounter)
    {
        for (int i = 0; i < coinCounter; i++)
        {
            PlayerPrefs.DeleteKey($"hitCoin{i}");
            
        }
    }

    public static void LastHeartSaveMethod()
    {
        PlayerPrefs.SetFloat("leftHeart",1f);
        PlayerPrefs.SetFloat("middleHeart",1f);
        PlayerPrefs.SetFloat("rightHeart",1f);
    }

    
}
