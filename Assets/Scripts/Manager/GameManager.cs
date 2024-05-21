using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;
using UnityEngine.UI;
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
    private Transform LeftWindPosition;
    private Transform RightWindPosition;
    [SerializeField] private Vector3 cameraMesafesi;
    [SerializeField] private float fireballSpeed;
    public float FireballSpeed
    {
        get { return fireballSpeed; }
    }
    [SerializeField]private float fireballTimerCounter;
    public bool isCharacterOnPoint = false;
    [SerializeField] private float cameraScaleChangeTime;

    #region WindObject members

    [SerializeField] private GameObject windPrefab; 
    [SerializeField] private GameObject aerialWindPrefab; 
    [SerializeField] private GameObject[] windObjects;

    private bool createWind= true;

    public bool CreateWind {get { return createWind; }  set { createWind = value; } }

    private bool windGo = false;
    public bool WindGo  {get{return windGo;} set { windGo = value;}}    

    private bool leftHandSide = true;

    private bool windRightGo = false;
    public bool WindRightGo { get {return windRightGo;} }

    private bool windLeftGo = false;
    public bool WindLeftGo { get {return windLeftGo;} }
    
    private bool finish = false;
    public bool Finish {get {return finish;} set {finish = value;}}
    public bool newSceneCharacterPos = false;
    private float createWindObjectTimer = 0f;

    [SerializeField] private float windSpeed;
    public float WindSpeed { get => windSpeed; }
    private int i = 0;

    #endregion
    
    #region Enemy Firebal member

    [SerializeField] private GameObject enemyFireballPrefab; 
    [SerializeField] private GameObject[]enemyFireballObjects;
    private float createEnemyFireballObjectTimer = 0f;

    [SerializeField] private float enemyFireballSpeed;
    public float EnemyFireballSpeed { get => enemyFireballSpeed; }
    private bool createEnemyFireball= true; // asıl değeri false

    public bool CreateEnemyFireball {get { return createEnemyFireball; }  set { createEnemyFireball = value; } }

    private bool enemyFireballGo = false;
    public bool EnemyFireballGo  {get{return enemyFireballGo;} set { enemyFireballGo = value;}}    

    private bool enemyFireballRightHandSide = true;

    private bool enemyFireballRightGo = false;
    public bool EnemyFireballRightGo { get {return enemyFireballRightGo;} }

    private bool enemyFireballLeftGo = false;
    public bool EnemyFireballLeftGo { get {return enemyFireballLeftGo;} }
    private bool fireballWasFired= true;
    public bool FireballWasFired { get { return fireballWasFired;} set{fireballWasFired = value;}}  
    private int j = 0;
    #endregion
    
    private void Awake() 
    {
        LeftWindPosition = transform.GetChild(0);
        RightWindPosition = transform.GetChild(1);

        
    }

    void Update()
    {
        CharcterCheckPoint();
        CameraPositionControl();
        CreateWindObject();
        CreateEnemyFireballObject();
        
        LoadScene();
    }

    private void CharcterCheckPoint()
    {
        if (!isCharacterOnPoint)
        {
            if (mainCharacter.IsCharacterDead)
            {
                mainCharacter.transform.position = CheckPointController.CheckPointPosition();
                mainCharacter.IsCharacterDead = false;
            }
            isCharacterOnPoint = true;
        }
    }

    public void RegisterMainCharacter(CharacterControl character)
    {
        mainCharacter = null;
        mainCharacter = character;
    }

    private void CameraPositionControl()
    {
        if(mainCharacter != null)
        {
            Camera.main.transform.position = mainCharacter.transform.position + cameraMesafesi;
        }
        
    }
    
    void CreateWindObject()
    {
        if(!finish)
        {
            if(createWind)
            {
                if(i < windObjects.Length)
                {
                    createWindObjectTimer += Time.deltaTime;
                    if(createWindObjectTimer <= 50)
                    {
                        createWindObjectTimer = 0f;
                        if(leftHandSide)
                        {
                            //windObjects[i++] = Instantiate(windPrefab,new Vector2(LeftWindPosition.position.x,-0.9681435f),Quaternion.identity,LeftWindPosition);
                            
                            if(!mainCharacter.IsAerialWind)
                            {
                                windObjects[i++] = Instantiate(windPrefab,new Vector2(LeftWindPosition.position.x,-0.9681435f),Quaternion.identity,LeftWindPosition);
                            }
                            else
                            {
                                windObjects[i++] = Instantiate(aerialWindPrefab,new Vector2(LeftWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,LeftWindPosition);
                            }
                            leftHandSide = false;
                            windRightGo = true;
                            windLeftGo = false;
                        }
                        else if(!leftHandSide)
                        {
                            //windObjects[i++] = Instantiate(windPrefab,new Vector2(RightWindPosition.position.x,-0.9681435f),Quaternion.identity,RightWindPosition);

                            if(!mainCharacter.IsAerialWind)
                            {
                                windObjects[i++] = Instantiate(windPrefab,new Vector2(RightWindPosition.position.x,-0.9681435f),Quaternion.identity,RightWindPosition);
                            }
                            else
                            {
                                windObjects[i++] = Instantiate(aerialWindPrefab,new Vector2(RightWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,RightWindPosition);
                            }

                            leftHandSide = true;
                            windLeftGo = true;
                            windRightGo =false;
                        }
                    }

                }
                createWind = false;
            }
        }
    }
    void CreateEnemyFireballObject()
    {
        if(!finish)
        {
            if(!createEnemyFireball)
            {
                if(j < enemyFireballObjects.Length)
                {
                    createEnemyFireballObjectTimer += Time.deltaTime;
                    if(createEnemyFireballObjectTimer <= 50)
                    {
                        createEnemyFireballObjectTimer = 0f;
                        if(enemyFireballRightHandSide)
                        {
                            //enemyFireballObjects[j++] = Instantiate(enemyFireballPrefab,new Vector2(RightWindPosition.position.x,-0.9681435f),Quaternion.identity,RightWindPosition);
                            enemyFireballObjects[j++] = Instantiate(enemyFireballPrefab,new Vector2(RightWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,RightWindPosition);

                            enemyFireballRightHandSide = false;
                            enemyFireballRightGo = false;
                            enemyFireballLeftGo = true;
                        }
                        else if(!enemyFireballRightHandSide)
                        {
                            //enemyFireballObjects[j++] = Instantiate(enemyFireballPrefab,new Vector2(LeftWindPosition.position.x,-0.9681435f),Quaternion.identity,LeftWindPosition);
                            enemyFireballObjects[j++] = Instantiate(enemyFireballPrefab,new Vector2(LeftWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,LeftWindPosition);


                            enemyFireballRightHandSide = true;
                            enemyFireballRightGo = true;
                            enemyFireballLeftGo =false;
                        }
                    }
                }
                createEnemyFireball = true;
            }
        }  
    }
    

    void LoadScene()
    {
        FinishControl();

    }

    private void FinishControl()
    {
        if (finish)
        {
            StartCoroutine(NewLevel());
            PlayerPrefs.DeleteAll();
            finish = false;
        }
    }

    IEnumerator NewLevel()
    {
        
        yield return null;
        int loadSceneIndex = SceneManager.GetActiveScene().buildIndex ; 
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);

        while(!asyncOperation.isDone)
        {
            
           yield return null;
           
        }
        if(asyncOperation.isDone)
        {
            finish = false;
            newSceneCharacterPos = true;
        }
    }

}
