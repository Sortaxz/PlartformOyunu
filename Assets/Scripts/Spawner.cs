using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Spawner : MonoBehaviour
{
    GameManager gameManager;
    Transform[] spawnPoint;
    [SerializeField]private GameObject character;
    [SerializeField]private GameObject fireball;
    [SerializeField]private Transform fireballPosition;

    [SerializeField] private Vector3 cameraMesafesi;
    private int currentSpawnIndex = 0;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        Resource();
        SpawPointDizileme();
        SpawnCharacter();
    }

    private void Resource()
    {
        character = Resources.Load<CharacterControl>("Prefabs/Character").gameObject;
        fireball = Resources.Load<GameObject>("Prefabs/fireball");
    }

    private void SpawPointDizileme()
    {
        spawnPoint = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnPoint[i] = transform.GetChild(i).transform;
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
        GameObject spawnCharacter = Instantiate(character, spawnPoint[currentSpawnIndex].position,Quaternion.identity);
        Camera.main.transform.parent =spawnCharacter.transform;
        Camera.main.transform.localPosition = cameraMesafesi;
        gameManager.RegisteMainCharacter(spawnCharacter.GetComponent<CharacterControl>());
    }

    void SpawnFireball()
    {
        if(GameManager.Instance.mainCharacter.fireballReady)
        {
            GameObject newFireBall = Instantiate(fireball,fireballPosition.position,Quaternion.identity);
            GameManager.Instance.mainCharacter.fireballReady = false;
            
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

        if(GUILayout.Button("Spaw Poin Üret",GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
        {
            GameObject spawnPoint = new GameObject();
            spawnPoint.transform.name = spawner.transform.childCount.ToString();
            spawnPoint.transform.parent = spawner.transform;
            spawnPoint.transform.position = spawner.transform.position;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraMesafesi"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireball"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}


#endif