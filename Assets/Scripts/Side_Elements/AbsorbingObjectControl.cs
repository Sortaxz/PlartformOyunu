using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbingObjectControl : MonoBehaviour
{
    private PlartformManager plartformManager;
    private GameObject windObject;
    [SerializeField] private GameObject WithdrawnCenter;
    [SerializeField] private SpriteRenderer WithdrawnCenterSPR;
    [SerializeField] private SpriteRenderer AbsorbingObjectSPR;
    [SerializeField] private float pullingForce;
    private void Awake()
    {
        IsAbsorbingObject();
        plartformManager = PlartformManager.Instance;
    }

    private void IsAbsorbingObject()
    {
        if (transform.tag == "AbsorbingObject")
        {
            WithdrawnCenter = transform.parent.GetChild(1).gameObject;
            WithdrawnCenterSPR = WithdrawnCenter.GetComponent<SpriteRenderer>();
            AbsorbingObjectSPR = GetComponent<SpriteRenderer>();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(transform.tag =="AbsorbingObject")
            {
                GameManager.Instance.mainCharacter.HitAbsorbingObject = true;
                if(other.GetComponent<Rigidbody2D>() != null)
                {
                    WithdrawnCenterSPR.enabled = true;
                    AbsorbingObjectSPR.enabled = true;
                    Rigidbody2D otherRb2D = other.GetComponent<Rigidbody2D>();
                    float distanceX = WithdrawnCenter.transform.position.x - other.transform.position.x;    

                    if(distanceX>0)
                    {
                        AbsorbingObjectForce(otherRb2D,Vector3.right,pullingForce);
                    }
                    else if(distanceX < 0)
                    {
                        AbsorbingObjectForce(otherRb2D,Vector3.left,pullingForce);
                    }
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(transform.tag =="AbsorbingObject")
            {
                GameManager.Instance.mainCharacter.HitAbsorbingObject = false;
            }
        }   
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(transform.tag =="AbsorbingObject")
            {
                GameManager.Instance.mainCharacter.HitAbsorbingObject = false;
                if(other.GetComponent<Rigidbody2D>() != null)
                {
                    WithdrawnCenterSPR.enabled = false;
                    AbsorbingObjectSPR.enabled = false;
                }
            }
        }    
    }

    private void AbsorbingObjectForce(Rigidbody2D rigidbody2D,Vector3 direction,float pullingForce)
    {
        rigidbody2D.AddForce(direction * pullingForce);
    }


}
