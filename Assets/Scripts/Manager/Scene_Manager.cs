using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
    private static Scene_Manager instance;
    public static Scene_Manager Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<Scene_Manager>();
            }
            return instance;
        }
    }
    [SerializeField] private Image sahneGecisEkrani;
    [SerializeField] private TextMeshProUGUI gecisEkranYazisi;
    
    private bool isStageTransition  =false;
    public bool IsStageTransition { get { return isStageTransition; }  set { isStageTransition = value; } }
    private bool endLevelTransition;
    public bool EndLevelTransition { get { return endLevelTransition;} set { endLevelTransition = value; } }
    private int activeSceneBuildingIndex;
    private string sceneName;
    public string SceneName { get { return sceneName; } }

    [SerializeField] private string animationTextPerLevel;
    [SerializeField] private string animationTextEndLevel;
    [SerializeField] private SceneAsset[] scenes;
    private int nextSceneIndex = 0;
    private void Awake()
    {
    }

    public string GetNextSceneName()
    {
        activeSceneBuildingIndex = SceneManager.GetActiveScene().buildIndex;
       
        if(endLevelTransition)
        {
            if(activeSceneBuildingIndex < scenes.Length)
            {
                //sceneName = $"{animationTextEndLevel} Level-{activeSceneBuildingIndex + 2}";
                sceneName = $"{animationTextEndLevel} Level-{activeSceneBuildingIndex + 1}";
            }
            else if(activeSceneBuildingIndex == scenes.Length)
            {
                sceneName = $"You Win The Game";

            }
        }
        else
        {
            //sceneName = $"{animationTextPerLevel} Level-{activeSceneBuildingIndex + 1}";
            sceneName = $"{animationTextPerLevel} Level-{activeSceneBuildingIndex}";
        }

        return sceneName;
        
    }


    void Update()
    {
        
    }

    public void LoadScene()
    {
        int loadSceneIndex = SceneManager.GetActiveScene().buildIndex ; 

        if(loadSceneIndex != scenes.Length-1)
        {
            nextSceneIndex = loadSceneIndex + 1;
        }
        else if(loadSceneIndex == scenes.Length-1)
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex ;
        }

       /*
        int latestSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("latestSceneIndex",latestSceneIndex);
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(3,LoadSceneMode.Single);
        if(asyncOperation.isDone)
        {
            GameManager.Instance.StageTransitionOver = false;
        }
        */

        int latestSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.SetInt("latestSceneIndex",latestSceneIndex);
        PlayerPrefs.SetInt("LastLevelIndex",latestSceneIndex);
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(4,LoadSceneMode.Single);
        if(asyncOperation.isDone)
        {
            GameManager.Instance.StageTransitionOver = false;
        }

    }

    
    
        
}
