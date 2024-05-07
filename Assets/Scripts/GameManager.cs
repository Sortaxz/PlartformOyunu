using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }
            return instance;
        }
    }
    public CharacterControl mainCharacter;
    //[SerializeField]private Transform fireballPosition;
    //[SerializeField]private GameObject fireball;

    private void Awake() 
    {
        //fireball = Resources.Load<GameObject>("Prefabs/fireball");    
    }

    void Start()
    {
        //fireballPosition = mainCharacter.transform.GetChild(0).transform;
       
    }

    void Update()
    {
        //FireballCreation();
    }


    public void RegisteMainCharacter(CharacterControl character)
    {
        mainCharacter = null;
        mainCharacter = character;
    }

    
    /*
    void FireballCreation()
    {
        if(mainCharacter.fireballReady)
        {
            GameObject newFireBall = Instantiate(fireball,fireballPosition.position,Quaternion.identity);
            newFireBall.name = "fireball" + fireballPosition.childCount;
            mainCharacter.fireballReady = false;
        }
    }
    */
}
