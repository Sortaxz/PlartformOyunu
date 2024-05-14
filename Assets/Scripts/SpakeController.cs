using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpakeController : MonoBehaviour
{
    //-2
    [SerializeField]private bool spakeDuration;
    public bool SpakeDuration { get => spakeDuration; set => spakeDuration = value; }
    [SerializeField] private float spakeDurationTime;
    private bool distortion  =false;
    public float SpakeDurationTime { get => spakeDurationTime; set => spakeDurationTime = value; }
    private PolygonCollider2D spakePolygonCollider;

    private void Awake() 
    {
        spakePolygonCollider = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        SpakePositionAndScale();
    }

    private void SpakePositionAndScale()
    {
        if (spakeDuration)
        {
            transform.localScale = new Vector3(.5f, 0.1f, 0);
            transform.position = new Vector3(transform.position.x, -2.31f, transform.position.z);
            spakePolygonCollider.isTrigger = true;
        }
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
         if(spakeDuration && !distortion)
        {
            StartCoroutine(SpakeDurationMovement(spakeDurationTime));
            distortion =true;
        }     
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(spakeDuration)    
        {
            distortion = false;
        }
    }
   
    IEnumerator SpakeDurationMovement(float spakeDurationTime)
    {
        yield return new WaitForSeconds(1);
        transform.position = new Vector3(transform.position.x,-2,transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x,2);
        spakePolygonCollider.isTrigger = false;
        yield return new WaitForSeconds(spakeDurationTime);
        transform.position = new Vector3(transform.position.x,-2.31f,transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x,0.1f);
        spakePolygonCollider.isTrigger = true;
    }
    
    
}

    
