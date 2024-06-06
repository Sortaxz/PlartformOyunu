using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController instance;
    public static EnemyController Instance
    {
        get
        {
            instance =  FindAnyObjectByType<EnemyController>();
            return instance;
        }
        
    }
    [SerializeField] private float enemyCharcterSpeed;
    private bool leftPosition = false;
    public bool LeftPosition { get { return leftPosition; }}
    private bool rightPosition = false; 
    public bool RightPosition { get { return rightPosition; }}
    private void Awake() 
    {
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void FixedUpdate() 
    {
        EnemyCharacterMove();    
    }
    private void OnDrawGizmos() 
    {
        
    }

    private void EnemyCharacterMove()
    {

        if(!leftPosition)
        {
            
            transform.localScale = new Vector3(1,1,1);

            transform.position += new Vector3(-10,0,0) * Time.deltaTime * enemyCharcterSpeed;
            if(transform.position.x <= -10)
            {
                leftPosition = true;
                rightPosition = true;

            }
        }
        if(rightPosition)
        {
            transform.localScale = new Vector3(-1,1,1);

            transform.position += new Vector3(10,0,0) * Time.deltaTime * enemyCharcterSpeed;
            if(transform.position.x >= 10)
            {
                leftPosition = false;
                rightPosition = false;
            }
        }

    }
}
