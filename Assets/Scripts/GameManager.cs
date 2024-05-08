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
    private Transform[] checkPointsTransform;
    [SerializeField] private float fireballSpeed;
    public float FireballSpeed
    {
        get { return fireballSpeed; }
    }
    [SerializeField]private float fireballTimerCounter;
    private static int checkPointIndex = 0;
    bool ileriMi = true;
    private void Awake() 
    {
    }

    void Start()
    {
        CheckAndFillPoints();
    }
    void CheckAndFillPoints()
    {
        checkPointsTransform = new Transform[Spawner.Instance.SpawnPoint.Length];
        for (int i = 0; i < checkPointsTransform.Length; i++)
        {
            checkPointsTransform[i] =Spawner.Instance.SpawnPoint[i];
        }
    }

    void Update()
    {
    }

   
    public void RegisterMainCharacter(CharacterControl character)
    {
        mainCharacter = null;
        mainCharacter = character;
    }
    
    
}
