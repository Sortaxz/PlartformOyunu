using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITransitionControl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private  bool transitionOver = false;
    [SerializeField] private SceneAsset[] scenes;
    private void Awake() 
    {
        

    
    }
    void Start()
    {

    }

    void Update()
    {
        Method();

        if(transitionOver)
        {
            LoadLevel();
        }
    }

    private void Method()
    {
        int layerIndex = 0;
        bool isPlaying = animator.GetCurrentAnimatorStateInfo(layerIndex).IsName("Aweake_Progress");
        if(isPlaying)
        {
            transitionOver = true;
        }
    }
    private void LoadLevel()
    {
        //int sceneIndex = PlayerPrefs.GetInt("latestSceneIndex");
        int sceneIndex = PlayerPrefs.GetInt("LastLevelIndex");
        if(sceneIndex < scenes.Length)
        {
            int levelIndexInstall = PlayerPrefs.GetInt("LastLevelIndex")  + 1;
            
            AsyncOperation asyncOperation=  SceneManager.LoadSceneAsync(levelIndexInstall); 
            PlayerPrefs.DeleteKey("CheckPoint");
            if(asyncOperation.isDone)
            {
                transitionOver = false;
            }
        }
        else if(sceneIndex == scenes.Length)
        {
            //int levelIndexInstall = PlayerPrefs.GetInt("LastLevelIndex");
            int levelIndexInstall =0;
            SaveManager.LastHeartSaveMethod();
            AsyncOperation asyncOperation=  SceneManager.LoadSceneAsync(levelIndexInstall); 
            PlayerPrefs.DeleteKey("CheckPoint");
            
            if(asyncOperation.isDone)
            {
                transitionOver = false;
            }
        }
    }
}
