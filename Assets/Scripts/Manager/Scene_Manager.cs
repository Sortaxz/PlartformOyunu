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
    public SceneAsset[] Scenes { get { return scenes; } }
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
                sceneName = $"{animationTextEndLevel} Level-{activeSceneBuildingIndex + 1}";
            }
            else if(activeSceneBuildingIndex == scenes.Length)
            {
                sceneName = $"You Win The Game";

            }
        }
        else
        {
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

        SaveManager.SetNextLevelIndex(SceneManager.GetActiveScene().buildIndex);
       

        

        SaveManager.SetLifeValue("leftHeart",UIManager.Instance.HeartLeftImage.fillAmount);
        SaveManager.SetLifeValue("middleHeart",UIManager.Instance.HeartMiddleImage.fillAmount);
        SaveManager.SetLifeValue("rightHeart",UIManager.Instance.HeartRightImage.fillAmount);

        SaveManager.ResetHitCoinIndex(CoinManager.Instance.Coins.Length);

        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(4,LoadSceneMode.Single);
        if(asyncOperation.isDone)
        {
            GameManager.Instance.StageTransitionOver = false;
        }

    }

    
    
        
}
