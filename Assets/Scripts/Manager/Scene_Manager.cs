using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private int nextSceneIndex;
    private void Awake()
    {
    }

    public string GetNextSceneName()
    {
        activeSceneBuildingIndex = SceneManager.GetActiveScene().buildIndex;
        int index = activeSceneBuildingIndex;
        switch (activeSceneBuildingIndex)
        {
            case var x when index == activeSceneBuildingIndex:
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

        nextSceneIndex = (loadSceneIndex != scenes.Length-1) ? loadSceneIndex + 1 : SceneManager.GetActiveScene().buildIndex ;

        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(nextSceneIndex,LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                yield return null;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
            
        }
    }
    
        
}
