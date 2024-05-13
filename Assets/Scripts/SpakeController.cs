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
    void Start()
    {
        if(spakeDuration)
        {
            transform.localScale = new Vector3(.5f,0.1f,0);
            transform.position = new Vector3(transform.position.x,-2.31f,transform.position.z);
        }
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(spakeDuration && !distortion)
        {
            print(other.collider.name);
            StartCoroutine(SpakeDurationMovement(spakeDurationTime));
            distortion =true;
        }    
    }
    private void OnCollisionExit2D(Collision2D other) 
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
        yield return new WaitForSeconds(spakeDurationTime);
        transform.position = new Vector3(transform.position.x,-2.31f,transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x,0.1f);
    }
    
    
}

    
