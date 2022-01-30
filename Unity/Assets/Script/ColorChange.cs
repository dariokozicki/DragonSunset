using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    bool playerControl;
    public bool colorWhite;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {

        animator = GetComponent<Animator>();

        if(animator == null) 
        {
            return;
        }
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer == null)
        {
            return;
        }

        if (gameObject.tag == "Player") 
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

        colorWhite = animator.GetBool("colorWhite");
        OnColorSelect();
    }

    private void Update()
    {
        if(playerControl == true) 
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                ChangeColorManual();
            }
        }
    }

    void ChangeColorManual() 
    {
    
        if(colorWhite == true) 
        {
            colorWhite = false;
            animator.SetBool("ColorWhite", colorWhite);
        } else if(colorWhite == false)
        {
            colorWhite = true;
            animator.SetBool("ColorWhite", colorWhite);
        }

    }
    

    void OnColorSelect() 
    {

        float randomNumber = Random.Range(0f, 1f);

        colorWhite = randomNumber >= 0.5;

        animator.SetBool("colorWhite", colorWhite);
    }
}