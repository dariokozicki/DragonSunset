using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    bool playerControl;
    bool colorWhite;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(gameObject.tag == "Player") 
        {
            playerControl = true;
            Debug.Log("Soy El Player");
        }   else 
        
        {
            playerControl = false;
            Debug.Log("No soy El Player");
            PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            playerScript.OnAddPoint.AddListener(OnColorSelect);
        }


    }

    
    void Update()
    {
        
    }

    void OnColorSelect() 
    {

        int randomNumber = Random.Range(-2, 2);

        if(randomNumber <= 0) 
        {
            colorWhite = true;
                    
        }
        
        else 
        {
            colorWhite = false;
        }

        animator.SetBool("colorWhite", colorWhite);
    }
}
