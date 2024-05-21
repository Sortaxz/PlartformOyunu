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

    private Transform charcterParent;
    public Transform CharacterParent { get => charcterParent; }
    private static int currentSpawnIndex = 0;
    GameObject spawnCharacter;
    private void Awake()
    {
        Resource();
        SpawPointDizileme();
        SpawnCheckPoint();
        gameManager = GameManager.Instance;
        SpawnCharacter();
        print("Aweake");
    }

    public void Resource()
    {
        character = Resources.Load<GameObject>("Prefabs/Character/Character");
        fireball = Resources.Load<GameObject>("Prefabs/Fireball/fireball");
        checkPoint = Resources.Load<GameObject>("Prefabs/CheckPoint/CheckPoint");
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
        fireballPosition = gameManager.mainCharacter.transform.GetChild(0).transform; 
    }

    void Update()
    {
        SpawnFireball();
    }
    public void SpawnCharacter()
    {
        if(currentSpawnIndex >= 0 && currentSpawnIndex < spawnPoints.Count)
        {
                currentSpawnIndex = PlayerPrefs.GetInt("SpawnPoint");
                spawnCharacter = Instantiate(character, spawnPoints[currentSpawnIndex].position, Quaternion.identity);
            
            
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


    private void SpawnCheckPoint()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject spawnCheckPoint = Instantiate(checkPoint,spawnPoints[i].transform.position,Quaternion.identity,spawnPoints[i].transform);
            spawnCheckPoint.transform.name = "Check Point"  + i;

            if(i == spawnPoints.Count - 1)
            {
                spawnCheckPoint.layer = LayerMask.NameToLayer("Finally");
            }
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

            spawnPoint.transform.tag = "SpawnPoint";
            spawnPoint.transform.name = spawner.transform.childCount.ToString();
            spawnPoint.transform.parent = spawner.transform;
            spawnPoint.transform.position = spawner.transform.position;

            spawnPoint.AddComponent<CircleCollider2D>();
            spawnPoint.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireball"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnPoints"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif