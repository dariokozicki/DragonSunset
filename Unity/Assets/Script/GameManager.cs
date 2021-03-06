using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;
    PlayerScript playerScript;

    public UnityEvent OnLose = new UnityEvent();
    float totalLife;
    float actualLife = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }

        if (playerScript != null)
        {
            playerScript.OnDamage.AddListener(Lose);
        }

    }


    public void Lose()
    {
        Debug.Log("OnLose Invocado");
        OnLose.Invoke();
        actualLife--;
        if(actualLife <= 0) 
        {
            Defeat();
        }
    }

    public void Defeat() 
    {
        SceneManager.LoadScene("LoseScreen");
    }
}
