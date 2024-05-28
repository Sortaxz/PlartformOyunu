using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
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

    private GameObject floorPrefab;
    
    
    #region  SpakeObject Members

    [SerializeField] private GameObject SpakePrefab;
    [SerializeField] private GameObject[] SpakeObject;
    [SerializeField]private Vector2[] SpakeObjectPositons;
    [SerializeField] private bool[] SpakeDuration;
    private Transform spakeObjectTransform;
    [SerializeField] private float spakeDurationTime;

    #endregion
    
    #region  MovingFloor Object Members
    [SerializeField] private GameObject movingFloorPrefab;
    public GameObject MovingFloorPrefab {get { return movingFloorPrefab;} set {movingFloorPrefab = value;}}
    private GameObject movingFloor;
    public GameObject MovingFloor { get { return movingFloor;} set {movingFloor = value;}}
    [SerializeField] private GameObject[] movingFloors;
    public GameObject[] MovingFloors
    {
        get { return movingFloors; }
        set { movingFloors = value;}
    }
    
    private List<Vector2> saveMovingFloorPositions;
    public List<Vector2> SaveMovingFloorPositions {get { return saveMovingFloorPositions;} set { saveMovingFloorPositions = value;}}

    private List<bool> saveMovingFloorMovemet;
    public List<bool> SaveMovingFloorMovemet {get { return saveMovingFloorMovemet;} set { saveMovingFloorMovemet = value;}}

    [SerializeField] private int numberMovementGroundPoints;
    public int NumberMovementGroundPoints { get => numberMovementGroundPoints;  }
    
    [SerializeField] public Vector2[] movingGroundPositions;
   
   
    [SerializeField] public bool[] movingFloorDirectionMovement; 
    public bool[] MovingFloorDirectionMovement{ get => movingFloorDirectionMovement; set { movingFloorDirectionMovement = value;}}
    [SerializeField]private bool startCreateMovingFloor = false;
    public bool StartCreateMovingFloor {get {return startCreateMovingFloor;} set { startCreateMovingFloor = value;}}

    [SerializeField] private bool movingFloorbuttonClose = false;
    public bool MovingFloorButtonClose { get {return movingFloorbuttonClose;} set {movingFloorbuttonClose = value;}}
    [SerializeField] private int movingFloorsIndex = 0;
    public int MovingFloorsIndex { get {return movingFloorsIndex;} set { movingFloorsIndex = value;}}

    #endregion

    
    #region  PassableFloor
    
    [SerializeField] private GameObject passableFloorPrefab;
    public GameObject PassableFloorPrefab {get {return passableFloorPrefab;} set {passableFloorPrefab = value;}}
    
    private GameObject passableFloor;
    public GameObject PassableFloor { get {return passableFloor;} set{passableFloor =  value;}}    

    [SerializeField] private GameObject[] passableFloors;
    public  GameObject[] PassableFloors {get { return passableFloors; } set { passableFloors = value;}}

    private List<Vector2> savePassableFloorPositions;
    public List<Vector2> SavePassableFloorPositions {get { return savePassableFloorPositions;} set { savePassableFloorPositions = value;}}

    [SerializeField] public Vector2[] passableFloorPositions;

     [SerializeField]private bool startCreatePassableFloor = false;
    public bool StartCreatePassableFloor {get {return startCreatePassableFloor;} set { startCreatePassableFloor = value;}}

    [SerializeField] private bool passableFloorbuttonClose = false;
    public bool PassableFloorButtonClose { get {return passableFloorbuttonClose;} set {passableFloorbuttonClose = value;}}

    [SerializeField] private int passableFloorsIndex = 0;
    public int PassableFloorsIndex {get {return passableFloorsIndex;} set { passableFloorsIndex = value;}}

    #endregion
    

    #region SlidingFloor

    [SerializeField] private GameObject slidingFloorPrefab;
    public GameObject SlidingFloorPrefab { get {return slidingFloorPrefab;} set { slidingFloorPrefab = value;}}

    private GameObject slidingFloor;
    public GameObject SlidingFloor { get {return slidingFloor;} set { slidingFloor = value;}}

    [SerializeField] private GameObject[] slidingFloors;
    public  GameObject[] SlidingFloors {get { return slidingFloors; } set { slidingFloors = value;}}

    private List<Vector2> saveSlidingFloorPositions;
    public List<Vector2> SaveSlidingFloorPositions {get { return saveSlidingFloorPositions;} set { saveSlidingFloorPositions = value;}}

    [SerializeField] public Vector2[] slidingFloorPositions;
    
    [SerializeField]private bool startCreateSlidingFloor = false;
    public bool StartCreateSlidingFloor {get {return startCreateSlidingFloor;} set { startCreateSlidingFloor = value;}}

    [SerializeField] private bool slidingFloorbuttonClose = false;
    public bool SlidingFloorButtonClose { get {return slidingFloorbuttonClose;} set {slidingFloorbuttonClose = value;}}

    [SerializeField] private int slidingFloorsIndex = 0;
    public int SlidingFloorsIndex {get {return slidingFloorsIndex;} set { slidingFloorsIndex = value;}}

    #endregion


    #region  StonyFloor

    [SerializeField] private GameObject stonyFloorPrefab;
    public GameObject StonyFloorPrefab { get {return stonyFloorPrefab;} set { stonyFloorPrefab = value;}}
    private  GameObject stonyFloor;
    public GameObject StonyFloor  {get {return stonyFloor;} set { stonyFloor = value;}}
    [SerializeField] private GameObject[] stonyFloors;
    public  GameObject[] StonyFloors {get { return stonyFloors; } set { stonyFloors = value;}}

     private List<Vector2> saveStonyFloorPositions;
    public List<Vector2> SaveStonyFloorPositions {get { return saveStonyFloorPositions;} set { saveStonyFloorPositions = value;}}

    [SerializeField] public Vector2[] stonyFloorPositions;
     [SerializeField]private bool startCreateStonyFloor = false;
    public bool StartCreateStonyFloor {get {return startCreateStonyFloor;} set { startCreateStonyFloor = value;}}

    [SerializeField] private bool stonyFloorbuttonClose = false;
    public bool StonyFloorbuttonClose { get {return stonyFloorbuttonClose;} set {stonyFloorbuttonClose = value;}}

    [SerializeField] private int stonyFloorsIndex = 0;
    public int StonyFloorsIndex {get{return stonyFloorsIndex;} set {stonyFloorsIndex = value;}}


    #endregion


    #region  Inhaling Bomb

    [SerializeField] private GameObject inhalingBombPrefab;
    public GameObject InhalingBombPrefab { get {return inhalingBombPrefab;} set { inhalingBombPrefab = value;}}

    private GameObject inhalingBomb;
    public virtual GameObject InhalingBomb { get {return inhalingBomb;} set { inhalingBomb = value;}}

    [SerializeField] private GameObject[] inhalingBombs;
    public  GameObject[] InhalingBombs {get { return inhalingBombs; } set { inhalingBombs = value;}}

     private List<Vector2> saveInhalingBombPositions;
    public List<Vector2> SaveInhalingBombPositions {get { return saveInhalingBombPositions;} set { saveInhalingBombPositions = value;}}

    [SerializeField] public Vector2[] inhalingBombPositions;
    
    [SerializeField]private bool startCreateInhalingBomb = false;
    public bool StartCreateInhalingBomb {get {return startCreateInhalingBomb;} set { startCreateInhalingBomb = value;}}

    [SerializeField] private bool inhalingBombbuttonClose = false;
    public bool InhalingBombbuttonClose { get {return inhalingBombbuttonClose;} set {inhalingBombbuttonClose = value;}}
    [SerializeField] private int inhalingBombsIndex = 0;
    public int InhalingBombsIndex {get {return inhalingBombsIndex;} set { inhalingBombsIndex = value;}}

    #endregion


    #region  Cranboline

    [SerializeField] private GameObject cranbolinePrefab;
    public GameObject CranbolinePrefab { get {return cranbolinePrefab;} set { cranbolinePrefab = value;}}

    private GameObject cranboline;
    public GameObject Cranboline { get {return cranboline;} set {cranboline = value;}}

    [SerializeField] private GameObject[] cranbolines;
    public  GameObject[] Cranbolines{get { return cranbolines; } set { cranbolines = value;}}

     private List<Vector2> saveCranbolinePositions;
    public List<Vector2> SaveCranbolinePositions {get { return saveCranbolinePositions;} set { saveCranbolinePositions = value;}}

    [SerializeField] public Vector2[] cranbolinePositions;
    
    [SerializeField]private bool startCreateCranboline = false;
    public bool StartCreateCranboline {get {return startCreateCranboline;} set { startCreateCranboline = value;}}

    [SerializeField] private bool cranbolinebuttonClose = false;
    public bool CranbolinebuttonClose { get {return cranbolinebuttonClose;} set {cranbolinebuttonClose = value;}}

    [SerializeField] private int cranblonesIndex = 0;
    public int CranblonesIndex {get {return cranblonesIndex;} set {cranblonesIndex = value;}}

    #endregion


    private void Awake()
    {
        CreateSpakeObject();
        GetSpakeDuration();

        CreateMovingFloor();
        CreatePassableObject();
        CreateStonyObject();
        CreateSlidingObject();
        CreateInhalingBombObject();
        CreateCranbolineObject();
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
        passableFloorPrefab = Resources.Load<GameObject>("");
        for (int i = 0; i < SpakeObject.Length; i++)
        {
            SpakeObject[i] = Instantiate(SpakePrefab, SpakeObjectPositons[i], Quaternion.identity, spakeObjectTransform);
            SpakeObject[i].name = "Spake" + spakeObjectTransform.childCount;
        }


        
    }


    public void CreateMovingFloor()
    {
        for (int i = 0; i < movingFloors.Length; i++)
        {
            movingFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/Moving Grass");
            movingFloors[i] = Instantiate(movingFloorPrefab,movingGroundPositions[i],Quaternion.identity,transform.GetChild(1).transform);
            movingFloors[i].name = "Moving Floor" + movingFloors[i].transform.GetSiblingIndex();

            movingFloors[i].GetComponent<MovingFloorControl>().MovementDirectionUp = MovingFloorDirectionMovement[i];

           
        }
    }


    public void CreatePassableObject()
    {
        for (int i = 0; i < passableFloors.Length; i++)
        {
            passableFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/PassableFloor");
            passableFloors[i] = Instantiate(passableFloorPrefab,passableFloorPositions[i],Quaternion.identity,transform.GetChild(2).transform);
            passableFloors[i].name = "Passable Floor" + passableFloors[i].transform.GetSiblingIndex();
            
        }
    }


    public void CreateSlidingObject()
    {
        for (int i = 0; i < slidingFloors.Length; i++)
        {   
            slidingFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/SlidingFloor");
            slidingFloors[i] = Instantiate(slidingFloorPrefab,slidingFloorPositions[i],Quaternion.identity,transform.GetChild(3).transform);
            slidingFloors[i].name = "Sliding Floor" + slidingFloors[i].transform.GetSiblingIndex();
        }
    }


    public void CreateStonyObject()
    {
        for (int i = 0; i < stonyFloors.Length; i++)
        {
            stonyFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/StonyFloor");
            stonyFloors[i] = Instantiate(stonyFloorPrefab,stonyFloorPositions[i],Quaternion.identity,transform.GetChild(4).transform);
            stonyFloors[i].name = "Stony Floor" + stonyFloors[i].transform.GetSiblingIndex();
        }
    }


    public void CreateInhalingBombObject()
    {
        for (int i = 0; i < inhalingBombs.Length; i++)
        {
            inhalingBombPrefab = Resources.Load<GameObject>("Prefabs/Obstacles/Inhaling Bomb");
            inhalingBombs[i] = Instantiate(inhalingBombPrefab,inhalingBombPositions[i],Quaternion.identity,transform.GetChild(5).transform);
            InhalingBombs[i].name = "Inhaling Bomb" + inhalingBombs[i].transform.GetSiblingIndex();
        }
    }


    public void CreateCranbolineObject()
    {
        for (int i = 0; i < cranbolines.Length; i++)
        {
            cranbolinePrefab = Resources.Load<GameObject>("Prefabs/Side_Tools/Cranboline");
            cranbolines[i] = Instantiate(cranbolinePrefab,cranbolinePositions[i],Quaternion.identity,transform.GetChild(6).transform);
            cranbolines[i].name = "Cranboline" + cranbolines[i].transform.GetSiblingIndex();

        }
    }



    public void DataHold(ref Vector2[] data, List<Vector2> saveData)
    {
        saveData = new List<Vector2>(data);
        saveData.Add(new Vector2());
        data = saveData.ToArray();
    }

    public void DataHold(ref bool[] data, List<bool> saveData)
    {
        saveData = new List<bool>(data);
        saveData.Add(new bool());
        data = saveData.ToArray();
    }

}



#if UNITY_EDITOR
[CustomEditor(typeof(PlartformManager))]
[System.Serializable]

class PlartformManagerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        #region  SpakeObject has  Members

        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakePrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakeObject"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakeObjectPositons"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpakeDuration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spakeDurationTime"));

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        #region  MovingFloor has  Members

        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloorPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloorsIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloors"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numberMovementGroundPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingGroundPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloorDirectionMovement"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startCreateMovingFloor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("movingFloorbuttonClose"));


        EditorGUILayout.Space();
        EditorGUILayout.Space();

        PlartformManager plartformManager = (PlartformManager)target;
       
        if(!plartformManager.MovingFloorButtonClose)
        {
            if(GUILayout.Button("Generate Motion Floor",GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
            {   
                
                plartformManager.MovingFloorsIndex++;
                plartformManager.MovingFloors = new GameObject[plartformManager.MovingFloorsIndex];
                
                plartformManager.DataHold(ref plartformManager.movingGroundPositions,plartformManager.SaveMovingFloorPositions);

                plartformManager.DataHold(ref plartformManager.movingFloorDirectionMovement,plartformManager.SaveMovingFloorMovemet);
                /*
                plartformManager.SaveMovingFloorMovemet = new List<bool>(plartformManager.MovingFloorDirectionMovement);
                plartformManager.SaveMovingFloorMovemet.Add(new bool()); 
                plartformManager.MovingFloorDirectionMovement = plartformManager.SaveMovingFloorMovemet.ToArray();
                */
            }
            
        }
        
        #endregion
        

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        #region  PassableFloor has  Members
        
        EditorGUILayout.PropertyField (serializedObject.FindProperty("passableFloorPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("passableFloorsIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("passableFloors"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("passableFloorPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startCreatePassableFloor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("passableFloorbuttonClose"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        if(!plartformManager.PassableFloorButtonClose)
        {
            if(GUILayout.Button("Create PassableFloor Object",GUILayout.MinWidth(100), GUILayout.MaxWidth(300)))
            {
                plartformManager.PassableFloorsIndex++;
                plartformManager.PassableFloors = new GameObject[plartformManager.PassableFloorsIndex];
                
            
                plartformManager.DataHold(ref plartformManager.passableFloorPositions,plartformManager.SavePassableFloorPositions);
            }
            

            

        }


        #endregion



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 


        #region  SlidingFloor has  Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("slidingFloorPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("slidingFloorsIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("slidingFloors"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("slidingFloorPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startCreateSlidingFloor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("slidingFloorbuttonClose"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        if(!plartformManager.SlidingFloorButtonClose)
        {
            if(GUILayout.Button("Create SlidingFloor Object",GUILayout.MinWidth(100), GUILayout.MaxWidth(300)))
            {

                plartformManager.SlidingFloorsIndex++;
                plartformManager.SlidingFloors = new GameObject[plartformManager.SlidingFloorsIndex];

                
                plartformManager.DataHold(ref plartformManager.slidingFloorPositions,plartformManager.SaveSlidingFloorPositions);
            }

        }

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 

        #region  StonyFloor has  Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("stonyFloorPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stonyFloorsIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stonyFloors"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stonyFloorPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startCreateStonyFloor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stonyFloorbuttonClose"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        if(!plartformManager.StonyFloorbuttonClose)
        {
            if(GUILayout.Button("Create StonyFloor Object",GUILayout.MinWidth(100), GUILayout.MaxWidth(300)))
            {
                plartformManager.StonyFloorsIndex++;
                plartformManager.StonyFloors = new GameObject[plartformManager.StonyFloorsIndex];

                plartformManager.DataHold(ref plartformManager.stonyFloorPositions,plartformManager.SaveStonyFloorPositions);

                
            }

        }


        #endregion



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 

        #region  InhalingBomb has Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("inhalingBombPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inhalingBombsIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inhalingBombs"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inhalingBombPositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startCreateInhalingBomb"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inhalingBombbuttonClose"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        if(!plartformManager.InhalingBombbuttonClose)
        {
            if(GUILayout.Button("Create InhalingBomb Object" , GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
            {
                plartformManager.InhalingBombsIndex++;
                plartformManager.InhalingBombs = new GameObject[plartformManager.InhalingBombsIndex];

                plartformManager.DataHold(ref plartformManager.inhalingBombPositions,plartformManager.SaveInhalingBombPositions);


            }

        }
        

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 


        #region  Cranboline has a Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("cranbolinePrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cranblonesIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cranbolines"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cranbolinePositions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startCreateCranboline"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cranbolinebuttonClose"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        if(!plartformManager.CranbolinebuttonClose)
        {
            if(GUILayout.Button("Create Cranboline Object" , GUILayout.MinWidth(100),GUILayout.MaxWidth(300)))
            {
                plartformManager.CranblonesIndex++;
                plartformManager.Cranbolines = new GameObject[plartformManager.CranblonesIndex];

                plartformManager.DataHold(ref plartformManager.cranbolinePositions,plartformManager.SaveCranbolinePositions);

               
            }

        }


        #endregion

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}

#endif