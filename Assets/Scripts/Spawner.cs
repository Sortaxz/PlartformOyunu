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
    public List<Transform> spawnPoints = new List<Transform>();
    private GameManager gameManager;
    [SerializeField]private GameObject character;
    [SerializeField]private GameObject fireball;
    [SerializeField]private GameObject checkPoint;
    public GameObject Fireball
    {
        get { return fireball; }
    }

    [SerializeField]private Transform fireballPosition;

    [SerializeField] private Vector3 cameraMesafesi;

    private static int currentSpawnIndex = 0;
    
    private void Awake()
    {
        Resource();
        SpawPointDizileme();
        SpawnCheckPoint();
        gameManager = GameManager.Instance;

    }

    private void Resource()
    {
        character = Resources.Load<GameObject>("Prefabs/Character");
        fireball = Resources.Load<GameObject>("Prefabs/fireball");
        checkPoint = Resources.Load<GameObject>("Prefabs/CheckPoint");
    }

    private void SpawPointDizileme()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i] = transform.GetChild(i).transform;
        }
    }

    void Start()
    {
        SpawnCharacter();
        fireballPosition = gameManager.mainCharacter.transform.GetChild(0).transform; 
    }

    void Update()
    {
        SpawnFireball();
    }

    public void SpawnCharacter()
    {
        currentSpawnIndex = PlayerPrefs.GetInt("SpawnPoint");
        GameObject spawnCharacter = Instantiate(character, spawnPoints[currentSpawnIndex].position,Quaternion.identity);
        Camera.main.transform.parent =spawnCharacter.transform;
        Camera.main.transform.localPosition = cameraMesafesi;
        
        gameManager.RegisterMainCharacter(spawnCharacter.GetComponent<CharacterControl>());
    }

    private void SpawnFireball()
    {   
        if(AnimationController.Instance.fireballReady)
        {
            GameObject newFireBall = Instantiate(fireball,fireballPosition.position,Quaternion.identity);
            newFireBall.GetComponent<FireballController>().birKereYonAlindi = false;
            
            AnimationController.Instance.fireballReady = false;
            
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


    private void SpawnCheckPoint()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject spawnCheckPoint = Instantiate(checkPoint,spawnPoints[i].transform.position,Quaternion.identity,spawnPoints[i].transform);
            spawnCheckPoint.transform.name = "Check Point"  + i;
        }
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
       for (int i = 0; i < transform.childCount; i++)
       {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).position,.5f);
       }
    }
    #endif

    
}


#if UNITY_EDITOR
[CustomEditor(typeof(Spawner))]
[System.Serializable]
class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Spawner spawner = (Spawner)target;
    
        if(GUILayout.Button("Spaw Point Üret",GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
        {
            GameObject spawnPoint = new GameObject();

            Spawner.Instance.spawnPoints.Add(spawnPoint.transform);

            spawnPoint.transform.tag = "CheckPoint";
            spawnPoint.transform.name = spawner.transform.childCount.ToString();
            spawnPoint.transform.parent = spawner.transform;
            spawnPoint.transform.position = spawner.transform.position;

            spawnPoint.AddComponent<CircleCollider2D>();
            spawnPoint.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraMesafesi"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireball"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}


#endif