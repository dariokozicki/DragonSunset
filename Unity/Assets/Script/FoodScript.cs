using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject player;
    PlayerScript playerScript;

    void Start()
    {
        GetPoint();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null) 
        {
            playerScript = player.GetComponent<PlayerScript>();

            playerScript.OnAddPoint.AddListener(GetPoint);
        }        
            
    }


    void GetPoint() 
    {
        transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f));
    }

}
