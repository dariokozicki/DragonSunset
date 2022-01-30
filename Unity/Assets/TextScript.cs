using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScript : MonoBehaviour
{
    public Text score;
    GameObject player;
    PointScript pointScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) 
        {
            pointScript = player.GetComponent<PointScript>();
            if(pointScript != null) 
            {
                pointScript.OnAddPoint.AddListener(OnRefreshText);
                pointScript.OnLosePoint.AddListener(OnRefreshText);
            }
        }
    }


    void OnRefreshText() 
    {
       score.text = $"{pointScript.score}";        
    }
}
