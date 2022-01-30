using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamaManager : MonoBehaviour
{
    GameObject player;
    PlayerScript playerScript;

    public GamaManager enemyInScene;

    public UnityEvent OnLose = new UnityEvent();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) 
        {
            playerScript = player.GetComponent<PlayerScript>();
        }

        if(playerScript != null) 
        {
            playerScript.OnDamage.AddListener(Lose);
        }       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lose() 
    {
        OnLose.Invoke();
    }
}
