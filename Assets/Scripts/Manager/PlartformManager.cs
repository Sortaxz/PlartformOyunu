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
    [SerializeField] private List<GameObject> movingFloors;
    public List<GameObject> MovingFloors
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
    
    [SerializeField] private Vector2[] movingGroundPositions;
    public Vector2[] MovingGroundPositions
    {
        get { return movingGroundPositions; }
        set { movingGroundPositions = value;}
    }
   
    [SerializeField] private bool[] movingFloorDirectionMovement; 
    public bool[] MovingFloorDirectionMovement{ get => movingFloorDirectionMovement; set { movingFloorDirectionMovement = value;}}
    [SerializeField]private bool startCreateMovingFloor = false;
    public bool StartCreateMovingFloor {get {return startCreateMovingFloor;} set { startCreateMovingFloor = value;}}

    [SerializeField] private bool movingFloorbuttonClose = false;
    public bool MovingFloorButtonClose { get {return movingFloorbuttonClose;} set {movingFloorbuttonClose = value;}}

    #endregion

    
    #region  PassableFloor
    
    [SerializeField] private GameObject passableFloorPrefab;
    public GameObject PassableFloorPrefab {get {return passableFloorPrefab;} set {passableFloorPrefab = value;}}
    
    private GameObject passableFloor;
    public GameObject PassableFloor { get {return passableFloor;} set{passableFloor =  value;}}    

    [SerializeField] private List<GameObject> passableFloors;
    public  List<GameObject> PassableFloors {get { return passableFloors; } set { passableFloors = value;}}

    private List<Vector2> savePassableFloorPositions;
    public List<Vector2> SavePassableFloorPositions {get { return savePassableFloorPositions;} set { savePassableFloorPositions = value;}}

    [SerializeField] private Vector2[] passableFloorPositions;
    public Vector2[] PassableFloorPositions {get { return passableFloorPositions; } set { passableFloorPositions = value;}}

     [SerializeField]private bool startCreatePassableFloor = false;
    public bool StartCreatePassableFloor {get {return startCreatePassableFloor;} set { startCreatePassableFloor = value;}}

    [SerializeField] private bool passableFloorbuttonClose = false;
    public bool PassableFloorButtonClose { get {return passableFloorbuttonClose;} set {passableFloorbuttonClose = value;}}

    #endregion
    

    #region SlidingFloor

    [SerializeField] private GameObject slidingFloorPrefab;
    public GameObject SlidingFloorPrefab { get {return slidingFloorPrefab;} set { slidingFloorPrefab = value;}}

    private GameObject slidingFloor;
    public GameObject SlidingFloor { get {return slidingFloor;} set { slidingFloor = value;}}

    [SerializeField] private List<GameObject> slidingFloors;
    public  List<GameObject> SlidingFloors {get { return slidingFloors; } set { slidingFloors = value;}}

     private List<Vector2> saveSlidingFloorPositions;
    public List<Vector2> SaveSlidingFloorPositions {get { return saveSlidingFloorPositions;} set { saveSlidingFloorPositions = value;}}

    [SerializeField] private Vector2[] slidingFloorPositions;
    public Vector2[] SlidingFloorPositions {get { return slidingFloorPositions;} set { slidingFloorPositions = value;}}
     [SerializeField]private bool startCreateSlidingFloor = false;
    public bool StartCreateSlidingFloor {get {return startCreateSlidingFloor;} set { startCreateSlidingFloor = value;}}

    [SerializeField] private bool slidingFloorbuttonClose = false;
    public bool SlidingFloorButtonClose { get {return slidingFloorbuttonClose;} set {slidingFloorbuttonClose = value;}}

    #endregion


    #region  StonyFloor

    [SerializeField] private GameObject stonyFloorPrefab;
    public GameObject StonyFloorPrefab { get {return stonyFloorPrefab;} set { stonyFloorPrefab = value;}}
    private  GameObject stonyFloor;
    public GameObject StonyFloor  {get {return stonyFloor;} set { stonyFloor = value;}}
    [SerializeField] private List<GameObject> stonyFloors;
    public  List<GameObject> StonyFloors {get { return stonyFloors; } set { slidingFloors = value;}}

     private List<Vector2> saveStonyFloorPositions;
    public List<Vector2> SaveStonyFloorPositions {get { return saveStonyFloorPositions;} set { saveStonyFloorPositions = value;}}

    [SerializeField] private Vector2[] stonyFloorPositions;
    public Vector2[] StonyFloorPositions {get { return stonyFloorPositions;} set { stonyFloorPositions = value;}}
     [SerializeField]private bool startCreateStonyFloor = false;
    public bool StartCreateStonyFloor {get {return startCreateStonyFloor;} set { startCreateStonyFloor = value;}}

    [SerializeField] private bool stonyFloorbuttonClose = false;
    public bool StonyFloorbuttonClose { get {return stonyFloorbuttonClose;} set {stonyFloorbuttonClose = value;}}

    #endregion


    #region  Inhaling Bomb

    [SerializeField] private GameObject inhalingBombPrefab;
    public GameObject InhalingBombPrefab { get {return inhalingBombPrefab;} set { inhalingBombPrefab = value;}}

    private GameObject inhalingBomb;
    public virtual GameObject InhalingBomb { get {return inhalingBomb;} set { inhalingBomb = value;}}

    [SerializeField] private List<GameObject> inhalingBombs;
    public  List<GameObject> InhalingBombs {get { return inhalingBombs; } set { inhalingBombs = value;}}

     private List<Vector2> saveInhalingBombPositions;
    public List<Vector2> SaveInhalingBombPositions {get { return saveInhalingBombPositions;} set { saveInhalingBombPositions = value;}}

    [SerializeField] private Vector2[] inhalingBombPositions;
    public Vector2[] InhalingBombsPositions {get { return inhalingBombPositions;} set { inhalingBombPositions = value;}}
     [SerializeField]private bool startCreateInhalingBomb = false;
    public bool StartCreateInhalingBomb {get {return startCreateInhalingBomb;} set { startCreateInhalingBomb = value;}}

    [SerializeField] private bool inhalingBombbuttonClose = false;
    public bool InhalingBombbuttonClose { get {return inhalingBombbuttonClose;} set {inhalingBombbuttonClose = value;}}

    #endregion


    #region  Cranboline

    [SerializeField] private GameObject cranbolinePrefab;
    public GameObject CranbolinePrefab { get {return cranbolinePrefab;} set { cranbolinePrefab = value;}}

    private GameObject cranboline;
    public GameObject Cranboline { get {return cranboline;} set {cranboline = value;}}

    [SerializeField] private List<GameObject> cranbolines;
    public  List<GameObject> Cranbolines{get { return cranbolines; } set { cranbolines = value;}}

     private List<Vector2> saveCranbolinePositions;
    public List<Vector2> SaveCranbolinePositions {get { return saveCranbolinePositions;} set { saveCranbolinePositions = value;}}

    [SerializeField] private Vector2[] cranbolinePositions;
    public Vector2[] CranbolinePositions {get { return cranbolinePositions;} set { cranbolinePositions = value;}}
     [SerializeField]private bool startCreateCranboline = false;
    public bool StartCreateCranboline {get {return startCreateCranboline;} set { startCreateCranboline = value;}}

    [SerializeField] private bool cranbolinebuttonClose = false;
    public bool CranbolinebuttonClose { get {return cranbolinebuttonClose;} set {cranbolinebuttonClose = value;}}

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
        for (int i = 0; i < movingFloors.Count; i++)
        {
            movingFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/Moving Grass");
            movingFloor = Instantiate(movingFloorPrefab,movingGroundPositions[i],Quaternion.identity,movingFloors[i].transform);
            movingFloors[i] = movingFloor;

            movingFloors[i].GetComponent<MovingFloorControl>().MovementDirectionUp = MovingFloorDirectionMovement[i];

           
        }
    }

    public void CreatePassableObject()
    {
        for (int i = 0; i < passableFloors.Count; i++)
        {
            passableFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/PassableFloor");
            GameObject passableFloor = Instantiate(passableFloorPrefab,passableFloorPositions[i],Quaternion.identity,passableFloors[i].transform);
            
            
        }
    }
    
    public void CreateSlidingObject()
    {
        for (int i = 0; i < slidingFloors.Count; i++)
        {   
            slidingFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/SlidingFloor");
            slidingFloor = Instantiate(slidingFloorPrefab,slidingFloorPositions[i],Quaternion.identity,slidingFloors[i].transform);

        }
    }
    
    public void CreateStonyObject()
    {
        for (int i = 0; i < stonyFloors.Count; i++)
        {
            stonyFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/StonyFloor");
            stonyFloor = Instantiate(stonyFloorPrefab,stonyFloorPositions[i],Quaternion.identity,stonyFloors[i].transform);
           
        }
    }


    public void CreateInhalingBombObject()
    {
        for (int i = 0; i < inhalingBombs.Count; i++)
        {
            inhalingBombPrefab = Resources.Load<GameObject>("Prefabs/Obstacles/Inhaling Bomb");
            inhalingBomb = Instantiate(inhalingBombPrefab,inhalingBombPositions[i],Quaternion.identity,inhalingBombs[i].transform);

        }
    }

    public void CreateCranbolineObject()
    {
        for (int i = 0; i < cranbolines.Count; i++)
        {
            cranbolinePrefab = Resources.Load<GameObject>("Prefabs/Side_Tools/Cranboline");
            cranboline = Instantiate(cranbolinePrefab,cranbolinePositions[i],Quaternion.identity,cranbolines[i].transform);

        }
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
                
                plartformManager.MovingFloor = new GameObject();
                plartformManager.MovingFloors.Add(plartformManager.MovingFloor);

                plartformManager.MovingFloor .transform.parent = plartformManager.transform.GetChild(1);
                plartformManager.MovingFloor .transform.name = "MovingFloor" +" " +plartformManager.MovingFloor.transform.GetSiblingIndex();
                
                plartformManager.SaveMovingFloorPositions = new List<Vector2>(plartformManager.MovingGroundPositions);
                plartformManager.SaveMovingFloorPositions.Add(new Vector2()); 
                plartformManager.MovingGroundPositions = plartformManager.SaveMovingFloorPositions.ToArray();

                plartformManager.SaveMovingFloorMovemet = new List<bool>(plartformManager.MovingFloorDirectionMovement);
                plartformManager.SaveMovingFloorMovemet.Add(new bool()); 
                plartformManager.MovingFloorDirectionMovement = plartformManager.SaveMovingFloorMovemet.ToArray();
            }
            
        }
        
        #endregion
        

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();    

        #region  PassableFloor has  Members
        
        EditorGUILayout.PropertyField (serializedObject.FindProperty("passableFloorPrefab"));
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
                /*
                plartformManager.PassableFloorPrefab = Resources.Load<GameObject>("Prefabs/Ground/PassableFloor");
                GameObject passableFloor = Instantiate(plartformManager.PassableFloorPrefab);
                */
                plartformManager.PassableFloor = new GameObject();
                plartformManager.PassableFloors.Add(plartformManager.PassableFloor);

                plartformManager.PassableFloor.transform.parent = plartformManager.transform.GetChild(2);
                plartformManager.PassableFloor.transform.name = "PassableFloor" +" " +plartformManager.PassableFloor.transform.GetSiblingIndex();

               
                plartformManager.SavePassableFloorPositions = new List<Vector2>(plartformManager.PassableFloorPositions);
                plartformManager.SavePassableFloorPositions.Add(new Vector2()); 
                plartformManager.PassableFloorPositions = plartformManager.SavePassableFloorPositions.ToArray();
            }
            

            

        }


        #endregion



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 


        #region  SlidingFloor has  Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("slidingFloorPrefab"));
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

                plartformManager.SlidingFloor = new GameObject();

                plartformManager.SlidingFloors.Add(plartformManager.SlidingFloor);

                plartformManager.SlidingFloor.transform.parent = plartformManager.transform.GetChild(3);
                plartformManager.SlidingFloor.transform.name = "SlidingFloor" +" " +plartformManager.SlidingFloor.transform.GetSiblingIndex();


                plartformManager.SaveSlidingFloorPositions = new List<Vector2>(plartformManager.SlidingFloorPositions);
                plartformManager.SaveSlidingFloorPositions.Add(new Vector2()); 
                plartformManager.SlidingFloorPositions = plartformManager.SaveSlidingFloorPositions.ToArray();
                
            }

        }

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 

        #region  StonyFloor has  Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("stonyFloorPrefab"));
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

                plartformManager.StonyFloor = new GameObject();

                plartformManager.StonyFloor.transform.parent = plartformManager.transform.GetChild(4);
                plartformManager.StonyFloor.transform.name = "StonyFloor" +" " +plartformManager.StonyFloor.transform.GetSiblingIndex();

                plartformManager.StonyFloors.Add(plartformManager.StonyFloor);

                plartformManager.SaveStonyFloorPositions = new List<Vector2>(plartformManager.StonyFloorPositions);
                plartformManager.SaveStonyFloorPositions.Add(new Vector2()); 
                plartformManager.StonyFloorPositions = plartformManager.SaveStonyFloorPositions.ToArray();
                
            }

        }


        #endregion



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 

        #region  InhalingBomb has Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("inhalingBombPrefab"));
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
                
                plartformManager.InhalingBomb = new GameObject();
                plartformManager.InhalingBombs.Add(plartformManager.InhalingBomb);

                plartformManager.InhalingBomb.transform.parent = plartformManager.transform.GetChild(5);
                plartformManager.InhalingBomb.transform.name = "InhalingBomb" + plartformManager.InhalingBomb.transform.GetSiblingIndex();


                plartformManager.SaveInhalingBombPositions = new List<Vector2>(plartformManager.InhalingBombsPositions);
                plartformManager.SaveInhalingBombPositions.Add(new Vector2());
                plartformManager.InhalingBombsPositions = plartformManager.SaveInhalingBombPositions.ToArray();

            }

        }
        

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space(); 


        #region  Cranboline has a Members

        EditorGUILayout.PropertyField (serializedObject.FindProperty("cranbolinePrefab"));
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
                plartformManager.Cranboline = new GameObject();
                plartformManager.Cranbolines.Add(plartformManager.Cranboline);

                plartformManager.Cranboline.transform.parent = plartformManager.transform.GetChild(6);
                plartformManager.Cranboline.transform.name = "Cranboline" + plartformManager.Cranboline.transform.GetSiblingIndex();


                plartformManager.SaveCranbolinePositions = new List<Vector2>(plartformManager.CranbolinePositions);
                plartformManager.SaveCranbolinePositions.Add(new Vector2());
                plartformManager.CranbolinePositions = plartformManager.SaveCranbolinePositions.ToArray();

            }

        }


        #endregion

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}

#endif