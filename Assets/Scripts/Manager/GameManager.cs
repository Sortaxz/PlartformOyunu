using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private UI_Elemets uI_Elemets;

    private Transform LeftWindPosition;
    private Transform RightWindPosition;
    [SerializeField] private Vector3 cameraMesafesi;

    [SerializeField] private AudioSource musicPlayer;

    [SerializeField] private float fireballSpeed;
    public float FireballSpeed
    {
        get { return fireballSpeed; }
    }

    public bool isCharacterOnPoint = false;
    private bool finish = false;
    public bool Finish {get {return finish;} set {finish = value;}}
    
    [SerializeField] private float cameraScaleChangeTime;

    private bool stageTransitionReady =false;
    public bool StageTransitionReady { get { return stageTransitionReady;} set { stageTransitionReady = value; } }

    /*
    private bool sceneReadyToLoad =false;
    public bool SceneReadyToLoad { get { return sceneReadyToLoad;} set { sceneReadyToLoad = value; } }
    */

    private bool stageTransitionOver =false;
    public bool StageTransitionOver { get { return stageTransitionOver;} set { stageTransitionOver = value; } }


    private bool animationLaunched = false;
    public bool AnimationLaunched { get { return animationLaunched;} set { animationLaunched = value;}}
   
    private bool stopGameFlow = false;
    public bool StopGameFlow { get {return stopGameFlow;} set {stopGameFlow = value;}}

    [SerializeField] private float holdForSeconds;
    public float HoldForSeconds { get { return holdForSeconds;}}

    [SerializeReference] private float time = 0f;
    public float _Time {get { return time;}}

    private int numberCollectedCoins = 0;
    public int NumberCollectedCoins { get { return numberCollectedCoins;} set { numberCollectedCoins = value;}}


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
    
    [SerializeField] private bool resumeWind;

    private bool windObject = false;
    public bool WindObject {get {return windObject;}}
    
    
    private float createWindObjectTimer = 0f;

    [SerializeField] private float windSpeed;
    public float WindSpeed { get => windSpeed; }
    private int windIndex = 0;

    #endregion
    
    #region Enemy Firebal member

    [SerializeField] private GameObject enemyFireballPrefab; 
    [SerializeField] private GameObject[]enemyFireballObjects;
    private float createEnemyFireballObjectTimer = 0f;

    [SerializeField] private float enemyFireballSpeed;
    public float EnemyFireballSpeed { get => enemyFireballSpeed; }
    private bool createEnemyFireball= false; 

    public bool CreateEnemyFireball {get { return createEnemyFireball; }  set { createEnemyFireball = value; } }

    private bool enemyFireballGo = false;
    public bool EnemyFireballGo  {get{return enemyFireballGo;} set { enemyFireballGo = value;}}    

    private bool enemyFireballRightHandSide = true;

    private bool enemyFireballRightGo = false;
    public bool EnemyFireballRightGo { get {return enemyFireballRightGo;} }

    private bool enemyFireballLeftGo = false;
    public bool EnemyFireballLeftGo { get {return enemyFireballLeftGo;} }
    
    private bool fireballWasFired= false;
    public bool FireballWasFired { get { return fireballWasFired;} set{fireballWasFired = value;}}  

    private bool enemyFireballl = false;
    public bool EnemyFireballl {get {return enemyFireballl;} }

    [SerializeField] private bool resumeEnemyFirebal;
    private bool uploadNewLevel = false;
    public  bool UploadNewLevel { get { return uploadNewLevel;} set { uploadNewLevel = value; } }

    
    private int enemyFireballIndex= 0;
    #endregion
    
    private void Awake() 
    {
        musicPlayer.volume = SaveManager.GetLastMusicVolume();
        LeftWindPosition = transform.GetChild(0);
        RightWindPosition = transform.GetChild(1);
        
        
    }
    private void Start() 
    {
       
        Spawner.Instance.SpawnCharacter();
        
        
    }
    void Update()
    {
        LevelPreparation();

        CharcterCheckPoint();
        CameraPositionControl();
        
        IsWindStillBlowingOrEnemyFirebal();
        
        CreateWindObject();
        CreateEnemyFireballObject();
        
        //Scene_Manager.Instance.LoadScene();
       


    }

    public void LevelPreparation()
    {
        if(stageTransitionReady)
        {
            UIManager.Instance.FinishLevelTransition(holdForSeconds,true);
        }
        if(stageTransitionOver)
        {
            Scene_Manager.Instance.LoadScene();
        }

    }

    private void IsWindStillBlowingOrEnemyFirebal()
    {
        if(resumeWind)
        {   
            if (windIndex == windObjects.Length)
            {
                createWind = false;
                enemyFireballl = true;

             

            }
            if (enemyFireballl)
            {
                enemyFireballIndex = 0;
                CreateEnemyFireballObject();
                if (enemyFireballIndex == enemyFireballObjects.Length - 1)
                {
                    enemyFireballl = false;
                }
            }
        }

        if(resumeEnemyFirebal)
        {
            if (enemyFireballIndex == enemyFireballObjects.Length)
            {
                createEnemyFireball = false;
                windObject = true;

               
            }
            if (windObject)
            {
                windIndex = 0;
                CreateWindObject();
                if (windIndex == windObjects.Length)
                {
                    windObject = false;
                }
            }
        }
    }
    private void CharcterCheckPoint()
    {
        if (!isCharacterOnPoint && mainCharacter != null)
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
        if(!finish && !stageTransitionReady)
        {
            if(createWind)
            {
                if(windIndex < windObjects.Length)
                {
                    createWindObjectTimer += Time.deltaTime;
                    if(createWindObjectTimer <= 50)
                    {
                        createWindObjectTimer = 0f;
                        if(leftHandSide)
                        {
                            
                            if(!mainCharacter.IsAerialWind)
                            {
                                windObjects[windIndex++] = Instantiate(windPrefab,new Vector2(LeftWindPosition.position.x,-0.9681435f),Quaternion.identity,LeftWindPosition);
                            }
                            else
                            {
                                windObjects[windIndex++] = Instantiate(aerialWindPrefab,new Vector2(LeftWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,LeftWindPosition);
                            }
                            leftHandSide = false;
                            windRightGo = true;
                            windLeftGo = false;
                        }
                        else if(!leftHandSide)
                        {

                            if(!mainCharacter.IsAerialWind)
                            {
                                windObjects[windIndex++] = Instantiate(windPrefab,new Vector2(RightWindPosition.position.x,-0.9681435f),Quaternion.identity,RightWindPosition);
                            }
                            else
                            {
                                windObjects[windIndex++] = Instantiate(aerialWindPrefab,new Vector2(RightWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,RightWindPosition);
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
        if(!finish && !stageTransitionReady )
        {
            if(createEnemyFireball)
            {
                if(enemyFireballIndex < enemyFireballObjects.Length)
                {
                    createEnemyFireballObjectTimer += Time.deltaTime;
                    if(createEnemyFireballObjectTimer <= 50)
                    {
                        createEnemyFireballObjectTimer = 0f;
                        if(enemyFireballRightHandSide)
                        {
                            enemyFireballObjects[enemyFireballIndex++] = Instantiate(enemyFireballPrefab,new Vector2(RightWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,RightWindPosition);

                            fireballWasFired = true;
                            enemyFireballRightHandSide = false;
                            enemyFireballRightGo = false;
                            enemyFireballLeftGo = true;
                        }
                        else if(!enemyFireballRightHandSide)
                        {
                            enemyFireballObjects[enemyFireballIndex++] = Instantiate(enemyFireballPrefab,new Vector2(LeftWindPosition.position.x,mainCharacter.transform.position.y),Quaternion.identity,LeftWindPosition);

                            fireballWasFired = true;
                            enemyFireballRightHandSide = true;
                            enemyFireballRightGo = true;
                            enemyFireballLeftGo =false;
                        }
                    }
                }
                createEnemyFireball = false;
            }
        }  
    }
    

    

}
