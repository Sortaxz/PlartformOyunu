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
}
