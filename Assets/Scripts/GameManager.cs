using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }
            return instance;
        }
    }
    public CharacterControl mainCharacter;
    [SerializeField] private Vector3 cameraMesafesi;
    [SerializeField] private float fireballSpeed;
    public float FireballSpeed
    {
        get { return fireballSpeed; }
    }
    [SerializeField]private float fireballTimerCounter;
    public bool isCharacterOnPoint = false;
    [SerializeField] private float cameraScaleChangeTime;
    void Update()
    {
        if(!isCharacterOnPoint)
        {
            if(mainCharacter.IsCharacterDead)
            {
                mainCharacter.transform.position = CheckPointController.CheckPointPosition();

            }
            isCharacterOnPoint = true;
        }
        CameraPositionControl();
    }
    public void RegisterMainCharacter(CharacterControl character)
    {
        mainCharacter = null;
        mainCharacter = character;
    }

    private void CameraPositionControl()
    {
        Camera.main.transform.position = mainCharacter.transform.position + cameraMesafesi;
    }
    
}
