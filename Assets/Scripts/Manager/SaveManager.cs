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
        //return PlayerPrefs.GetInt("LastLevelIndex");
        return PlayerPrefs.HasKey("LastLevelIndex") ? PlayerPrefs.GetInt("LastLevelIndex") : 1;
    }

    public static float GetLastMusicVolume()
    {
        return PlayerPrefs.GetFloat("LastMusicVolume");
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
   
    #endregion

    public static float GetLifeValue(string whichLife,float heartValue = 0)
    {
        
        if(whichLife == "leftHeart") 
        {
            heartValue= PlayerPrefs.GetFloat("leftHeart");
        } 
        if(whichLife == "middleHeart")
        {
            heartValue = PlayerPrefs.GetFloat("middleHeart");
        }
        if(whichLife == "rightHeart")
        {
            heartValue = PlayerPrefs.GetFloat("rightHeart");
        }
        return heartValue;
    }

    public static void SetLifeValue(string whichLife,float lifeValue)
    {
        PlayerPrefs.SetFloat(whichLife,lifeValue);
    }
    public static void FirstHeartSaveMethod()
    {
        if(!PlayerPrefs.HasKey("leftHeart"))
        {
            PlayerPrefs.SetFloat("leftHeart",1f);
        }
        if(!PlayerPrefs.HasKey("middleHeart"))
        {
            PlayerPrefs.SetFloat("middleHeart",1f);
        }
        if(!PlayerPrefs.HasKey("rightHeart"))
        {
            PlayerPrefs.SetFloat("rightHeart",1f);
        }
    }
    public static void LastHeartSaveMethod()
    {
        PlayerPrefs.SetFloat("leftHeart",1f);
        PlayerPrefs.SetFloat("middleHeart",1f);
        PlayerPrefs.SetFloat("rightHeart",1f);
    }
}
