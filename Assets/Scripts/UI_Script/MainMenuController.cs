using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject LevelsMenu;
    [SerializeField] private GameObject SettingsMenu;
    private GameObject levelSelectionButtonPrefab;
    [SerializeField] private SceneAsset[] scenes;
    [SerializeField] private Button startButton;
    [SerializeField] private Button ressartButton;
    [SerializeField] private AudioSource musicPlayer;
    private List<GameObject> spawnedsLevelSelectionButton = new List<GameObject>();
    private int scenesIndex = 0;
    private bool create = false;
    int loadSceneBuildingIndex;
    private bool end = false;
    public bool End { get { return end; }  set { end = value; } }
    
    private void Awake() 
    {
    }

    private void Start() 
    {
        SaveManager.FirstTimeSaveMethod();

        musicPlayer.volume = SaveManager.GetLastMusicVolume();    
        musicPlayer.time = SaveManager.GetLastMusicTime();

        loadSceneBuildingIndex = SaveManager.GetLastLevelIndex();
       
        if(PlayerPrefs.HasKey("recorded"))
        {
            if(loadSceneBuildingIndex == 3)
            {
                startButton.interactable = false;
                ressartButton.interactable = true;
            }
        }
        else if(loadSceneBuildingIndex >= 1)
        {
            PlayerPrefs.DeleteKey("recorded");
            startButton.interactable = true;
        }
    }


    public void ChooseButtonMethod()
    {
        if(!LevelsMenu.activeSelf)
        {
            LevelsMenu.gameObject.SetActive(true);

        }
        if(LevelsMenu.activeSelf)
        {
            Buttons.gameObject.SetActive(false);
        }

        if(create)
           return;
            
        
            for (int i = scenesIndex; i < scenes.Length; i++)
            {
                levelSelectionButtonPrefab = Resources.Load<GameObject>("Prefabs/UI_Object/Level_Button");
                GameObject spawnedLevelSelectionButton = Instantiate(levelSelectionButtonPrefab,LevelsMenu.transform.GetChild(0));
                
                spawnedLevelSelectionButton.name = $"Level-{i}_Button";
                spawnedLevelSelectionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level-{i+1}";
                
                spawnedsLevelSelectionButton.Add(spawnedLevelSelectionButton);
                
                spawnedLevelSelectionButton.GetComponent<Button>().onClick.AddListener
                (
                    delegate
                    {
                        int loadSceneIndex = spawnedLevelSelectionButton.transform.GetSiblingIndex()+1;

                        SaveManager.LastHeartSaveMethod();

                        SaveManager.SetLastMusicTime(musicPlayer.time);

                        SaveManager.ResetHitCoinIndex(4);
                        
                        SaveManager.SetCoinCounter(0);

                        SaveManager.SetNextLevelIndex(loadSceneIndex);
                    

                        SceneManager.LoadScene(loadSceneIndex);
                    }
                );
                create = true; 
            }
        

    }

    public void SettingsButtonMethod()
    {
        if(!SettingsMenu.activeSelf)
        {
            SettingsMenu.gameObject.SetActive(true);
        }
        if(SettingsMenu.activeSelf)
        {
            Buttons.gameObject.SetActive(false);
        }
    }

    private void OnEnable() 
    {
        
    }
    
    public void StartButtonMethod()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        loadSceneBuildingIndex = SaveManager.GetLastLevelIndex();
        
        SaveManager.SetLastMusicTime(musicPlayer.time);
        SceneManager.LoadScene(loadSceneBuildingIndex);
    }

    public void LevelMenuCancelButtonMethod()
    {
        LevelsMenu.SetActive(false);
        Buttons.SetActive(true);
    }

    public void RessartButtonMethod()
    {
        
        SaveManager.LastHeartSaveMethod();


        SaveManager.SetLastMusicTime(musicPlayer.time);


        SaveManager.ResetHitCoinIndex(4);
        
        SaveManager.SetCoinCounter(0);

        loadSceneBuildingIndex = 1;

        SaveManager.SetNextLevelIndex(loadSceneBuildingIndex);

        SceneManager.LoadScene(loadSceneBuildingIndex);
    }

    public void ExitButton()
    {
        
        PlayerPrefs.DeleteAll();
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
