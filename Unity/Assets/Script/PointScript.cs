using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    public bool zenMode;
    ColorChange playerColor;
    ColorChange pointColor;
    PlayerScript playerScript;

    void Start()
    {
        playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<ColorChange>();
        pointColor = GameObject.FindGameObjectWithTag("Point").GetComponent<ColorChange>();
        playerScript = GetComponent<PlayerScript>();

        playerScript.OnAddPoint.AddListener(OnPointCheck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPointCheck() 
    {
        if(zenMode == false) 
        {
            if (pointColor.colorWhite == true && playerColor.colorWhite == pointColor.colorWhite)
            {
                Debug.Log("Sumaste un punto blanco");
            }
            else if (pointColor.colorWhite == true && playerColor.colorWhite != pointColor.colorWhite)
            {
                Debug.Log("Perdiste un punto blanco");
            }

            if (pointColor.colorWhite == false && playerColor.colorWhite == pointColor.colorWhite)
            {
                Debug.Log("Sumaste un punto negro");
            }
            else if (pointColor.colorWhite == false && playerColor.colorWhite != pointColor.colorWhite)
            {
                Debug.Log("Perdiste un punto negro");
            }
        } else if (zenMode == true) 
        {
            if (pointColor.colorWhite == true && playerColor.colorWhite == pointColor.colorWhite)
            {
                Debug.Log("Sumaste 2 puntos blanco");
            }
            else if (pointColor.colorWhite == true && playerColor.colorWhite != pointColor.colorWhite)
            {
                Debug.Log("Sumaste 1 punto blanco");
            }

            if (pointColor.colorWhite == false && playerColor.colorWhite == pointColor.colorWhite)
            {
                Debug.Log("Sumaste 2 puntos negro");
            }
            else if (pointColor.colorWhite == false && playerColor.colorWhite != pointColor.colorWhite)
            {
                Debug.Log("Sumaste 1 punto negro");
            }
        }
        
    }
}
