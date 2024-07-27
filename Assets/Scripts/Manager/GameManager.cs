using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

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
    
    [SerializeField] private GameObject enemyFireballs;
    [SerializeField] private GameObject winds;

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

    private bool stageTransitionOver =false;
    public bool StageTransitionOver { get { return stageTransitionOver;} set { stageTransitionOver = value; } }


    private bool animationLaunched = false;
    public bool AnimationLaunched { get { return animationLaunched;} set { animationLaunched = value;}}
   
    private bool stopGameFlow = false;
    public bool StopGameFlow { get {return stopGameFlow;} set {stopGameFlow = value;}}

    private bool charcDead = false;
    public bool CharcDead { get { return charcDead;} set { charcDead = value;}}

    private bool isWaitScreenExit = false;
    public bool IsWaitScreenExit { get { return isWaitScreenExit;} set { isWaitScreenExit = value;}}

    [SerializeField] private float holdForSeconds;
    public float HoldForSeconds { get { return holdForSeconds;}}

    [SerializeReference] private float time = 0f;
    public float _Time {get { return time;}}

    private int numberCollectedCoins = 0;
    public int NumberCollectedCoins { get { return numberCollectedCoins;} set { numberCollectedCoins = value;}}


    #region  Character Fireball
    [SerializeField] private GameObject[] characterFireballObjects;
    [SerializeField] private GameObject fireball;
    public GameObject Fireball { get { return fireball; } }
    [SerializeField] private Transform fireballPosition;

    private bool againFireball = false;
    public bool AgainFireball {get {return againFireball;}}

    private int characterFireballIndex = 0;

    #endregion

    #region WindObject members
    [SerializeField] private Sprite windSprite;
    [SerializeField] private Sprite windSprite2;
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
        musicPlayer.time = SaveManager.GetLastMusicTime();
        numberCollectedCoins = 0;
        
        fireball = Resources.Load<GameObject>("Prefabs/Fireball/fireball");
        
        numberCollectedCoins = SaveManager.GetCoinCounter();

        LeftWindPosition = transform.GetChild(0);
        RightWindPosition = transform.GetChild(1);
        
        Create(windObjects,winds,"Wind","Prefabs/Side_Tools/Wind");
        
        Create(enemyFireballObjects,enemyFireballs,"EnemyFireball","Prefabs/Fireball/enemyFireball");
        
        
    }

    private void Create(GameObject[] createObjects,GameObject parentObject,string name,string prafabPath)
    {
        for (int i = 0; i < createObjects.Length; i++)
        {
            GameObject objectPrafab = Resources.Load<GameObject>(prafabPath);
            GameObject newObject = Instantiate(objectPrafab);
            createObjects[i] = newObject;

            newObject.SetActive(false);
            newObject.transform.SetParent(parentObject.transform);
            newObject.transform.name = $"{name}-{newObject.transform.GetSiblingIndex()}";
        }
    }

    private void Start() 
    {
       
        Spawner.Instance.SpawnCharacter();
        
        if(mainCharacter != null)
        {
            fireballPosition = mainCharacter.transform.GetChild(0).transform; 
            Create(characterFireballObjects,fireballPosition.gameObject,"Fireball","Prefabs/Fireball/fireball");
        }
    }


    void Update()
    {
        LevelPreparation();

        CharcterCheckPoint();
        CameraPositionControl();
        
        IsWindStillBlowingOrEnemyFirebal();
        
        CreateWindObject();
        
        CreateEnemyFireballObject();
        
        SpawnFireball();
        
        if(mainCharacter.IsCharacterDead)
        {
            mainCharacter.Rb2D.velocity = Vector2.zero;
        }
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
                


                CreateEnemyFireballObject();

                if (enemyFireballIndex == enemyFireballObjects.Length)
                {
                    enemyFireballIndex = 0;
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

                CreateWindObject();

                if (windIndex == windObjects.Length)
                {
                    windIndex = 0;
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
    
    private void SpawnFireball()
    {   
        if(mainCharacter != null)
        {
            if(AnimationController.Instance.fireballReady)
            {   
                if(!againFireball)
                {
                    if(characterFireballIndex != characterFireballObjects.Length)
                    {
                        characterFireballObjects[characterFireballIndex].transform.SetParent(null);
                        characterFireballObjects[characterFireballIndex].transform.localPosition = fireballPosition.position;
                        characterFireballObjects[characterFireballIndex].transform.localRotation = Quaternion.identity;
                        characterFireballObjects[characterFireballIndex].SetActive(true);

                        characterFireballObjects[characterFireballIndex].GetComponent<FireballController>().birKereYonAlindi = false;

                        AnimationController.Instance.fireballReady = false;
                        mainCharacter.jumpAnimationResume = true;

                        if(mainCharacter.fireballLocalScale)
                        {
                            characterFireballObjects[characterFireballIndex].transform.localScale = new Vector3(-1,1,1);
                        }
                        else
                        {
                            characterFireballObjects[characterFireballIndex].transform.localScale = new Vector3(1,1,1);
                        }

                        characterFireballIndex++;
                    }
                    else if(characterFireballIndex == characterFireballObjects.Length)
                    {
                        againFireball = true;
                        AnimationController.Instance.fireballReady = false;

                        
                    }
                }

                
            }
            if(Input.GetKeyDown(KeyCode.R) && againFireball)
            {
                AgainSpawnFireball();
                againFireball =  false;
            }
        }
    }

    public void AgainSpawnFireball()
    {
        characterFireballIndex = 0;
        for (int i = 0; i < characterFireballObjects.Length; i++)
        {
            characterFireballObjects[i].SetActive(false);
            characterFireballObjects[i].transform.SetParent(fireballPosition);
            characterFireballObjects[i].transform.position = fireballPosition.position;
            characterFireballObjects[i].transform.localRotation = Quaternion.identity;
            

        }
    }

    void CreateWindObject()
    {
        if(!finish && !stageTransitionReady && !UIManager.Instance.StandbyScreenWorked)
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
                            if(mainCharacter.transform.position.y >= -1)
                            {
                                windObjects[windIndex].GetComponent<SpriteRenderer>().sprite = windSprite;
                            }
                            /*
                            else if(mainCharacter.transform.position.y < 0)
                            {
                                windObjects[windIndex].GetComponent<SpriteRenderer>().sprite = windSprite2;
                            }
                            */
                            else if(mainCharacter.transform.position.y <= -1)
                            {
                                windObjects[windIndex].GetComponent<SpriteRenderer>().sprite = windSprite2;
                            }


                            windObjects[windIndex].transform.position = new Vector2(LeftWindPosition.position.x,mainCharacter.transform.position.y);
                            windObjects[windIndex++].SetActive(true);


                            leftHandSide =false;

                            windLeftGo = false;
                            windRightGo = true;

                        }
                        else
                        {
                            if(mainCharacter.transform.position.y > -1)
                            {
                                windObjects[windIndex].GetComponent<SpriteRenderer>().sprite = windSprite;
                            }
                            /*
                            else if(mainCharacter.transform.position.y < 0)
                            {
                                windObjects[windIndex].GetComponent<SpriteRenderer>().sprite = windSprite2;
                            }
                            */
                            else if(mainCharacter.transform.position.y <= -1)
                            {
                                windObjects[windIndex].GetComponent<SpriteRenderer>().sprite = windSprite2;
                            }


                            windObjects[windIndex].transform.position = new Vector2(RightWindPosition.position.x,mainCharacter.transform.position.y);
                            windObjects[windIndex++].SetActive(true);


                            leftHandSide = true;
                                
                            windLeftGo = true;
                            windRightGo = false;

                        }
                        
                        
                    }
                    createWind = false;

                }
                
            }
        }
    }
    void CreateEnemyFireballObject()
    {
        if(!finish && !stageTransitionReady && !UIManager.Instance.StandbyScreenWorked )
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
                            enemyFireballObjects[enemyFireballIndex].transform.position = new Vector2(RightWindPosition.position.x,mainCharacter.transform.position.y);
                            enemyFireballObjects[enemyFireballIndex++].SetActive(true);

                            fireballWasFired = true;
                            enemyFireballRightHandSide = false;
                            enemyFireballRightGo = false;
                            enemyFireballLeftGo = true;
                        }
                        else if(!enemyFireballRightHandSide)
                        {
                            enemyFireballObjects[enemyFireballIndex].transform.position = new Vector2(LeftWindPosition.position.x,mainCharacter.transform.position.y);
                            enemyFireballObjects[enemyFireballIndex++].SetActive(true);

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




