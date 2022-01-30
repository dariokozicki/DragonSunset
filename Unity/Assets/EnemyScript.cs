using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    ColorChange colorChange;
    public float timeToDestroy = .1f;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject GameManager;
    GamaManager gameManagerScript;
    void Start()
    {
       GameObject GamaManager = GameObject.FindGameObjectWithTag("GameManager");

        if(GameManager != null) 
        {
            gameManagerScript = GamaManager.GetComponent<GamaManager>();

        }

        if(gameManagerScript != null) 
        {
           // gameManagerScript.OnLose.AddListener(OnDestroy);
        }

        colorChange = GetComponent<ColorChange>(); 
        if(colorChange == null) 
        {
            return;
        }
        
    }

      
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            ColorChange playerColor = collision.GetComponent<ColorChange>();
            if(playerColor == null) 
            {
                return;
            }
            
            if(colorChange.colorWhite == playerColor.colorWhite) 
            {
                OnDeath();
            }
            
        }
    }



    private void OnDeath()
    {
        Invoke("OnDestroy", timeToDestroy);
        Debug.Log("Has matado a un enemigo");
        AkSoundEngine.PostEvent("AddPoint", gameObject);
    }

    private void OnDestroy()
    {
        Destroy(parent);
    }
}
