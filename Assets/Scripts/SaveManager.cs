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
        return PlayerPrefs.GetInt("LastLevelIndex");
    }

    public static float GetLastMusicVolume()
    {
        return PlayerPrefs.GetFloat("LastMusicVolume");
    }

    public static int GetLevelSceneButtonIndex()
    {
        return PlayerPrefs.GetInt("LevelSceneButtonIndex");
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
    public static void SetLevelSceneButtonIndex(int value)
    {
        PlayerPrefs.SetFloat("LevelSceneButtonIndex", value);

    }
    #endregion
}
