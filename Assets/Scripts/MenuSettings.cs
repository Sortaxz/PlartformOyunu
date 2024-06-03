using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject ButtonsMenu;


    private void OnEnable() 
    {
        musicPlayer.volume = SaveManager.GetLastMusicVolume();    
    }

    public void ChangeMusicPlayerVolume()
    {
        musicPlayer.volume = slider.value;
    }
    public void ApplyButtonMethod()
    {
        SaveManager.SetLastMusicVolume(slider.value);
    }
    
    public void CancelButtonMethod()
    {
        musicPlayer.volume = SaveManager.GetLastMusicVolume();
        ButtonsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
