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
    [SerializeField] private GameObject SpakePrefab;
    [SerializeField] private GameObject[] SpakeObject;
    [SerializeField]private Vector2[] SpakeObjectPositons;
    [SerializeField] private bool[] SpakeDuration;
    private Transform spakeObjectTransform;
    [SerializeField] private float spakeDurationTime;

    [SerializeField] private GameObject[] MovingFloors;
    [SerializeField] private int numberMovementGroundPoints;
    
    [SerializeField] private Vector2[] movementGroundPositions;
    [SerializeField] private Vector2[] movementGroundPoints;
    public Vector2[] MovementGroundPoints {get => movementGroundPoints;}
    private void Awake()
    {
        CreateSpakeObject();
        GetSpakeDuration();
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
        if(GUILayout.Button("Generate Motion Floor",GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
        {
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("MovingFloors"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numberMovementGroundPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movementGroundPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movementGroundPoints"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}

#endif