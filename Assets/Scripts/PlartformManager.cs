using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
//0.5 / 2 / 0 localScale
public class PlartformManager : MonoBehaviour
{
    private static PlartformManager instance;
    public static PlartformManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PlartformManager>();
            }
            return instance;
        }
    }

    #region  SpakeObject Members

    [SerializeField] private GameObject SpakePrefab;
    [SerializeField] private GameObject[] SpakeObject;
    [SerializeField]private Vector2[] SpakeObjectPositons;
    [SerializeField] private bool[] SpakeDuration;
    private Transform spakeObjectTransform;
    [SerializeField] private float spakeDurationTime;

    #endregion
    
    #region  MovingFloor Object Members
    [SerializeField] private GameObject MovingFloorPrefab;
    [SerializeField] private List<GameObject> movingFloors;
    public List<GameObject> MovingFloors
    {
        get { return movingFloors; }
        set { movingFloors = value;}
    }
    [SerializeField] private int numberMovementGroundPoints;
    public int NumberMovementGroundPoints { get => numberMovementGroundPoints;  }
    
    [SerializeField] private Vector2[] movingGroundPositions;
    public Vector2[] MovingGroundPositions
    {
        get { return movingGroundPositions; }
        set { movingGroundPositions = value;}
    }
    [SerializeField] private Vector2[] movementGroundPoints;
    public Vector2[] MovementGroundPoints {get => movementGroundPoints; set { movementGroundPoints = value;}}
    [SerializeField] private bool[] movingFloorDirectionMovement; 
    public bool[] MovingFloorDirectionMovement{ get => movingFloorDirectionMovement; set { movingFloorDirectionMovement = value;}}

    #endregion

    private void Awake()
    {
        CreateSpakeObject();
        GetSpakeDuration();
        CreateMovingFloor();
        //SetMovingFloorPoints();
    }

    private void CreateMovingFloor()
    {
        for (int i = 0; i < movingFloors.Count; i++)
        {
            movingFloors[i] = Instantiate(MovingFloorPrefab,movingGroundPositions[i],Quaternion.identity,movingFloors[i].transform);
            movingFloors[i].transform.position = movingGroundPositions[i];
            movingFloors[i].GetComponent<GroundControl>().MovementDirectionUp = MovingFloorDirectionMovement[i];
        }
    }

    private void SetMovingFloorPoints()
    {
       // int j = 0;
        for (int i = 0; i < MovementGroundPoints.Length; i++)
        {
            if(i % 2 == 0)
            {
                movementGroundPoints[i] = movingGroundPositions[i];
            }
            else
            {
                continue;
            }

            if(i == movementGroundPoints.Length - 1)
            {
                break;
            }
        }
    }

    private void GetSpakeDuration()
    {
        for (int i = 0; i < SpakeDuration.Length; i++)
        {
            SpakeController spakeController = SpakeObject[i].GetComponent<SpakeController>();
            spakeController.SpakeDuration = SpakeDuration[i];
            spakeController.SpakeDurationTime = spakeDurationTime;
        }
    }

    private void CreateSpakeObject()
    {
        spakeObjectTransform = transform.GetChild(0);
        for (int i = 0; i < SpakeObject.Length; i++)
        {
            SpakeObject[i] = Instantiate(SpakePrefab, SpakeObjectPositons[i], Quaternion.identity, spakeObjectTransform);
            SpakeObject[i].name = "Spake" + spakeObjectTransform.childCount;
        }


        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    

}
#if UNITY_EDITOR
[CustomEditor(typeof(PlartformManager))]
[System.Serializable]

class PlartformManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakePrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakeObject"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakeObjectPositons"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakeDuration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spakeDurationTime"));

        PlartformManager plartformManager = (PlartformManager)target;
        if(GUILayout.Button("Generate Motion Floor",GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
        {
            GameObject movingFloor = new GameObject();
            movingFloor.transform.parent = plartformManager.transform.GetChild(1);
            movingFloor.transform.name = "MovingFloor" +" " +movingFloor.transform.GetSiblingIndex();
            plartformManager.MovingFloors.Add(movingFloor);
            
            plartformManager.MovingGroundPositions = new Vector2[plartformManager.MovingFloors.Count];
            plartformManager.MovementGroundPoints = new Vector2[plartformManager.MovingFloors.Count * plartformManager.NumberMovementGroundPoints];
            
            plartformManager.MovingFloorDirectionMovement = new bool[plartformManager.MovingFloors.Count];

          
        }


        EditorGUILayout.PropertyField(serializedObject.FindProperty("MovingFloorPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloors"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numberMovementGroundPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingGroundPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movementGroundPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloorDirectionMovement"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}

#endif