using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject GameManager;
    GameManager gameManagerScript;
    void Start()
    {
        GameObject GameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (GameManager != null)
        {
            gameManagerScript = GameManager.GetComponent<GameManager>();

        }

        if (gameManagerScript != null)
        {
            gameManagerScript.OnLose.AddListener(OnDestroy);
        }

    }


    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
