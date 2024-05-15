using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    [SerializeField] private BoxCollider2D passebleBoxCollide2D;
    [SerializeField] private bool movementDirectionUp;
    public bool MovementDirectionUp { get=> movementDirectionUp; set=> movementDirectionUp = value; }
    private bool moveUp = false;
    private bool goUp = true;
    private bool moveRight = false;
    private bool goRight = true;
    private bool characterParentChanged = false;
    float positionMaxX;
    float positionMinX;
    private void Awake()
    {
        PassableFloorColliderControl();
        FindMovingFloorMaxMinX();
    }
    private void Start() 
    {
           
    }
    private void FindMovingFloorMaxMinX()
    {
        if (!movementDirectionUp)
        {
            positionMaxX = transform.position.x + 10;
            positionMinX = transform.position.x - 10;
        }
    }

    private void PassableFloorColliderControl()
    {
        if (transform.tag == "PassableFloor")
        {
            passebleBoxCollide2D = GetComponent<BoxCollider2D>();
        }
    }

    private void Update() 
    {
        /*
        if(characterParentChanged)
        {
            GameManager.Instance.mainCharacter.transform.SetParent(GameManager.Instance.transform.parent);
        }
        */
    }

    void FixedUpdate()
    {
        MovingFloorMovement();
    }

    private void MovingFloorMovement()
    {
        if (movementDirectionUp && transform.tag == "Moving Grass")
        {
            if (moveUp)
            {
                StartCoroutine(MoveUp());
            }
        }
        else if(!movementDirectionUp && transform.tag == "Moving Grass")
        {
            if (moveRight)
            {
                StartCoroutine(MoveRight());
            }
        }
    }

    #region  MovingFloor Right or Left movement
    IEnumerator MoveRight()
    {
        if(goRight && transform.position.x < positionMaxX)
        {
            transform.Translate(1f * Time.deltaTime,0,0);
        }
        else if(goRight && transform.position.x >= positionMaxX)
        {
            goRight = false;
        }

        if(!goRight && transform.position.x > positionMinX)
        {
            transform.Translate(-1f * Time.deltaTime,0,0);
        }
        else if(!goRight && transform.position.x <= positionMinX)
        {
            goRight = true;
        }


        yield return new WaitForSeconds(Time.fixedDeltaTime);
    }

    #endregion

    #region  MovingFloor Up or Down movement
    IEnumerator MoveUp()
    {
        
        if(goUp && transform.localPosition.y < 5 )
        {
            transform.Translate(0,1f*Time.deltaTime,0);
        }
        else if(goUp && transform.localPosition.y >= 5)
        {
            goUp = false;
        }

        if(!goUp && transform.localPosition.y > 0)
        {
            transform.Translate(0,-1f*Time.deltaTime,0);
        }
        else if(!goUp && transform.localPosition.y <= 0)
        {
            goUp = true;
        }

        yield return new WaitForSeconds(Time.fixedDeltaTime);
    }

    #endregion

    #region  Control with Collision and Trigger
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "SlidingFloor")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 12;
            }
            if(transform.tag == "StonyFloor ")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 4;
            }
            if(movementDirectionUp == true && transform.tag == "Moving Grass")
            {
                moveUp = true;
            }
            if(!movementDirectionUp && transform.tag == "Moving Grass")
            {
                moveRight = true;
                if(gameObject.activeInHierarchy)
                {
                    other.transform.SetParent(transform);
                }
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(!movementDirectionUp && transform.tag == "Moving Grass")
            {
                if(gameObject.activeInHierarchy)
                {
                    other.transform.SetParent(transform);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.tag == "SlidingFloor")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 8;
            }
            if(transform.tag == "StonyFloor ")
            {
                other.transform.GetComponent<CharacterControl>().CharacterSpeed = 8;
            }
            if(movementDirectionUp == true && transform.tag == "Moving Grass")
            {
                moveUp = false;
            }
            if(!movementDirectionUp && transform.tag == "Moving Grass")
            {
                moveRight = false;
                if(gameObject.activeInHierarchy)
                {
                    other.transform.SetParent(transform.parent.parent.parent.parent);
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(transform.tag == "PassableFloor")
            {
                passebleBoxCollide2D.isTrigger = true;
            }
        }    
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
         if(other.CompareTag("Player"))
        {
            if(transform.tag == "PassableFloor")
            {
                passebleBoxCollide2D.isTrigger = false;
            }
        }   
    }
    #endregion
}
