using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private float fireballSpeed;
    public float FireballSpeed
    {
        get { return fireballSpeed; }
    }
    [SerializeField]private float fireballTimerCounter;

    private void Awake() 
    {
    }

    void Start()
    {
       
    }

    void Update()
    {

    }

   
    public void RegisteMainCharacter(CharacterControl character)
    {
        mainCharacter = null;
        mainCharacter = character;
    }
    
    
}
