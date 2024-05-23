using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorControl : MonoBehaviour
{
    [SerializeField] private bool movementDirectionUp;
    public bool MovementDirectionUp { get=> movementDirectionUp; set=> movementDirectionUp = value; }
    private bool moveUp = false; //normalde false
    private bool goUp = true;
    private bool moveRight = false;
    private bool goRight = true;
    float positionMaxX;
    float positionMinX;
    private void Awake() 
    {
        FindMovingFloorMaxMinX();

    }
   
    void FixedUpdate()
    {
        MovingFloorMovement();
    }

    private void FindMovingFloorMaxMinX()
    {
        if (!movementDirectionUp)
        {
            positionMaxX = transform.position.x + 10;
            positionMinX = transform.position.x - 10;
        }
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
            transform.Translate(5f * Time.deltaTime,0,0);
        }
        else if(goRight && transform.position.x >= positionMaxX)
        {
            goRight = false;
        }

        if(!goRight && transform.position.x > positionMinX)
        {
            transform.Translate(-5f * Time.deltaTime,0,0);
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
            transform.Translate(0,5f*Time.deltaTime,0);
        }
        else if(goUp && transform.localPosition.y >= 5)
        {
            goUp = false;
        }

        if(!goUp && transform.localPosition.y > 0)
        {
            transform.Translate(0,-5f*Time.deltaTime,0);
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
        
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            if(movementDirectionUp )
            {
                moveUp = true;
                if(gameObject.activeInHierarchy)
                {
                    other.transform.SetParent(transform);
                }
            }
            if(!movementDirectionUp )
            {
                moveRight = true;
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

            
            
            if(movementDirectionUp == true )
            {
                moveUp = false;
                if(other.gameObject.activeInHierarchy)
                    other.transform.SetParent(transform.parent.parent.parent);
            }
            if(!movementDirectionUp )
            {
                moveRight = false;
                
                if(other.gameObject.activeInHierarchy)
                    other.transform.SetParent(transform.parent.parent.parent);
            }
        }
   
    }
    #endregion
}
