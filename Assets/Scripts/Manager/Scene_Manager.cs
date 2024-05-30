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
    private bool endScene = false;
    public bool EndScene { get { return endScene;}}
    private int activeSceneBuildingIndex;
    private string sceneName;
    public string SceneName { get { return sceneName; } }
    [SerializeField] private SceneAsset[] scenes;
    private int nextSceneIndex = 0;
    private void Awake()
    {
    }

    //düzeltilicek kod
    public string GetNextSceneName()
    {
        activeSceneBuildingIndex = SceneManager.GetActiveScene().buildIndex;
        int index = activeSceneBuildingIndex;
        switch (activeSceneBuildingIndex)
        {
            case var x when index == activeSceneBuildingIndex && activeSceneBuildingIndex <= scenes.Length-1:
                sceneName = $"Level-{x+1}";
                break;
        }
        return sceneName;
        
    }


    void Update()
    {
        
    }

    public void LoadScene()
    {
        FinishControl();
    }

    private void FinishControl()
    {
        if(isStageTransition)
        {
            StartCoroutine(NewLevel());
            PlayerPrefs.DeleteAll();
            isStageTransition = false;
        }
    }

    
    
    IEnumerator NewLevel()
    {
        yield return null;
        int loadSceneIndex = SceneManager.GetActiveScene().buildIndex ; 

        if(loadSceneIndex != scenes.Length-1)
        {
            nextSceneIndex = loadSceneIndex + 1;
        }
        else if(loadSceneIndex == scenes.Length-1)
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex ;
        }


        
        
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(nextSceneIndex,LoadSceneMode.Single);
        
        if(asyncOperation.isDone)
        {
            GameManager.Instance.LevelStartAnimation = true;
        }

    }
    
        
}
