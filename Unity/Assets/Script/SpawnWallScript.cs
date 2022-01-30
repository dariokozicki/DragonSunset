using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallScript : MonoBehaviour
{
    [SerializeField] GameObject enemyWall;
    GameObject prefab;
    [SerializeField] GameObject enemeyParent;
    
    public float rotacionActual;  

    public float timerActual;
    public float timerMax;
    public float timerActualDisminucion;
    public float timerMaxDisminucion;

    void Start()
    {
       prefab = enemyWall;
    }

    // Update is called once per frame
    void Update()
    {

        timerActual += Time.deltaTime;
        timerActualDisminucion += Time.deltaTime;

        if(timerActual >= timerMax) 
        {

            timerActual = 0;
            CreateEnemy();
        
        }

        if(timerActualDisminucion >= timerMaxDisminucion) 
        {
            timerActualDisminucion = 0;
            if (timerMax > 2f)
            {
                timerMax = timerMax - 0.5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            CreateEnemy();
        }


        
    }

    void CreateEnemy() 
    {
        
        rotacionActual += 90;
        Debug.Log(rotacionActual);
        
        Instantiate(prefab,enemeyParent.transform);
        

        prefab.transform.position = Vector2.zero;
        prefab.transform.position += new Vector3(Random.Range(-5.5f, 5.5f), Random.Range(-3f, 3f));
        prefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotacionActual));
        
    }

    
}
