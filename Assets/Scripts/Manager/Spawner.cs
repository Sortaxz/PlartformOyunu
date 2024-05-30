using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Spawner : MonoBehaviour
{
    private static Spawner intance;
    public static Spawner Instance
    {
        get
        {
            if (intance == null)
            {
                intance = FindAnyObjectByType<Spawner>();
            }
            return intance;
        }
    }
    [SerializeField] private GameObject[] checkPoints;
    public GameObject[] CheckPoints { get { return checkPoints;} set { checkPoints = value; } }
    [SerializeField] private Vector2[] checkPointsPosition;
    public Vector2[] CheckPointsPosition {get { return checkPointsPosition; } set { checkPointsPosition = value; } }
    private GameManager gameManager;
    GameObject spawnCharacter;
    [SerializeField]private GameObject character;
    [SerializeField]private GameObject fireball;
    [SerializeField]private GameObject checkPointPrefab;
    
    private GameObject checkPoint;
    public GameObject CheckPoint{get {return checkPoint;} set {checkPoint = value;} }

    public GameObject Fireball
    {
        get { return fireball; }
    }

    [SerializeField]private Transform fireballPosition;

    private Transform charcterParent;
    public Transform CharacterParent { get => charcterParent; }
    private static int currentSpawnIndex = 0;
    [SerializeField] private int checkPointsIndex = 0;
    public int CheckPointsIndex { get { return checkPointsIndex; }  set { checkPointsIndex = value; } }
    private void Awake()
    {
        Resource();
        SpawnCheckPoint();
        gameManager = GameManager.Instance;
    }

    public void Resource()
    {
        character = Resources.Load<GameObject>("Prefabs/Character/Character");
        fireball = Resources.Load<GameObject>("Prefabs/Fireball/fireball");
        checkPointPrefab = Resources.Load<GameObject>("Prefabs/CheckPoint/CheckPoint");
    }

   

    void Start()
    {
        if(gameManager.mainCharacter != null)
        {
            fireballPosition = gameManager.mainCharacter.transform.GetChild(0).transform; 
        }
    }

    void Update()
    {
        SpawnFireball();
    }
    public void SpawnCharacter()
    {
        if(currentSpawnIndex >= 0 && currentSpawnIndex < checkPoints.Length)
        {
            currentSpawnIndex = PlayerPrefs.GetInt("CheckPoint");
            spawnCharacter = Instantiate(character, checkPoints[currentSpawnIndex] .transform.position, Quaternion.identity);
            
            //spawnCharacter = Instantiate(character, CheckPointController.CheckPointPosition(), Quaternion.identity);
            
            
            charcterParent = spawnCharacter.transform.parent;
            gameManager.RegisterMainCharacter(spawnCharacter.GetComponent<CharacterControl>());
        }
    }

    private void SpawnFireball()
    {   
        if(gameManager.mainCharacter != null)
        {
            if(AnimationController.Instance.fireballReady)
            {   
                GameObject newFireBall = Instantiate(fireball,fireballPosition.position,Quaternion.identity);
                newFireBall.GetComponent<FireballController>().birKereYonAlindi = false;
                
                AnimationController.Instance.fireballReady = false;
                gameManager.mainCharacter.jumpAnimationResume = true;
                if(gameManager.mainCharacter.fireballLocalScale)
                {
                    newFireBall.transform.localScale = new Vector3(-1,1,1);
                }
                else
                {
                    newFireBall.transform.localScale = new Vector3(1,1,1);
                }

            }
        }
    }


    public void SpawnCheckPoint()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            
            checkPoints[i] = Instantiate(checkPointPrefab,checkPointsPosition[i],Quaternion.identity,transform.GetChild(0).transform);
            checkPoints[i].name = "checkPoint" + checkPoints[i].transform.GetSiblingIndex();
        }
    }

    
}
#if UNITY_EDITOR
[CustomEditor(typeof(Spawner))]
[System.Serializable]
class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Spawner spawner = (Spawner)target;
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("character"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireball"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("checkPointsIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("checkPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("checkPointsPosition"));
    
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if(GUILayout.Button("Spaw Point Üret",GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
        {
            spawner.CheckPointsIndex++;
            spawner.CheckPoints = new GameObject[spawner.CheckPointsIndex];
            spawner.CheckPointsPosition = new Vector2[spawner.CheckPointsIndex];
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif