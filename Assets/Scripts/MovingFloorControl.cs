using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorControl : MonoBehaviour
{
    PlartformManager plartformManager;
    private Vector2[] movementGroundPoints;
    private void Awake() 
    {
        plartformManager = PlartformManager.Instance;
        movementGroundPoints= new Vector2[plartformManager.MovementGroundPoints.Length];
        for (int i = 0; i < movementGroundPoints.Length; i++)
        {
            movementGroundPoints[i] = plartformManager.MovementGroundPoints[i];
        }

        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
