using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Help : MonoBehaviour
{
    public Text text;
    bool visible; 
    // Start is called before the first frame update
    void Start()
    {
        visible = false;
        text.gameObject.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() {
        visible = !visible;
        text.gameObject.SetActive(visible);
    }
}
