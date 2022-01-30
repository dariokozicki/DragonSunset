using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointScript : MonoBehaviour
{
    public bool zenMode;
    ColorChange playerColor;
    ColorChange pointColor;
    PlayerScript playerScript;


    AkSoundEngine akSound;

    public UnityEvent OnAddPoint = new UnityEvent();
    public UnityEvent OnLosePoint = new UnityEvent();

    public float score;

    void Start()
    {
        playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<ColorChange>();
        pointColor = GameObject.FindGameObjectWithTag("Point").GetComponent<ColorChange>();
        playerScript = GetComponent<PlayerScript>();

        playerScript.OnAddPoint.AddListener(OnPointCheck);
    }


    void OnPointCheck() 
    {
        if(zenMode == false) 
        {
            if (pointColor.colorWhite == true && playerColor.colorWhite == pointColor.colorWhite)
            {
                Debug.Log("Sumaste un punto blanco");
                AddPoint();
            }
            else if (pointColor.colorWhite == true && playerColor.colorWhite != pointColor.colorWhite)
            {
                Debug.Log("Perdiste un punto blanco");
                LosePoint();
            }

            if (pointColor.colorWhite == false && playerColor.colorWhite == pointColor.colorWhite)
            {
                Debug.Log("Sumaste un punto negro");
                AddPoint();
            }
            else if (pointColor.colorWhite == false && playerColor.colorWhite != pointColor.colorWhite)
            {
                Debug.Log("Perdiste un punto negro");
                LosePoint();
            }
        } 
        
        
    }

    

    void AddPoint() 
    {
        OnAddPoint.Invoke();
        score++;
        AkSoundEngine.PostEvent("AddPoint", gameObject);
    }

    void LosePoint() 
    {
        OnLosePoint.Invoke();
        score--;
        AkSoundEngine.PostEvent("losePoint", gameObject);
    }
}
