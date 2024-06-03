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
    [SerializeField] private AudioSource musicPlayer;
    private List<GameObject> spawnedsLevelSelectionButton = new List<GameObject>();
    private int scenesIndex = 0;
    private bool create = false;
    int loadSceneBuildingIndex;
    private void Start() 
    {
        SaveManager.FirstTimeSaveMethod();
        musicPlayer.volume = SaveManager.GetLastMusicVolume();    
        loadSceneBuildingIndex = SaveManager.GetLastLevelIndex();
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


    public void StartButtonMethod()
    {
        loadSceneBuildingIndex = SaveManager.GetLastLevelIndex()+1;
        SceneManager.LoadScene(loadSceneBuildingIndex);
    }
    public void LevelMenuCancelButtonMethod()
    {
        LevelsMenu.SetActive(false);
        Buttons.SetActive(true);
    }
}
