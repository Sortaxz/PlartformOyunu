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
    [SerializeField] private AudioSource musicPlayer;
    AudioClip audioClip;
    private void Awake() 
    {
        audioClip = musicPlayer.clip;
        
    }
    void Start()
    {
        musicPlayer.volume = SaveManager.GetLastMusicVolume();
        musicPlayer.time = SaveManager.GetLastMusicTime();
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
        //int sceneIndex = PlayerPrefs.GetInt("LastLevelIndex");
        int sceneIndex = SaveManager.GetLastLevelIndex();

        if(sceneIndex < scenes.Length)
        {
            //int levelIndexInstall = PlayerPrefs.GetInt("LastLevelIndex")  + 1;
            int levelIndexInstall = SaveManager.GetLastLevelIndex()  + 1;
            
            SaveManager.SetLastMusicTime(musicPlayer.time);

            AsyncOperation asyncOperation=  SceneManager.LoadSceneAsync(levelIndexInstall); 
            PlayerPrefs.DeleteKey("CheckPoint");
            if(asyncOperation.isDone)
            {
                transitionOver = false;
            }
        }
        else if(sceneIndex == scenes.Length)
        {
            int levelIndexInstall =0;
            
            SaveManager.SetLastMusicTime(musicPlayer.time);

            AsyncOperation asyncOperation=  SceneManager.LoadSceneAsync(levelIndexInstall); 

            PlayerPrefs.DeleteKey("CheckPoint");
            
            if(asyncOperation.isDone)
            {
                transitionOver = false;
            }
        }
    }
}
