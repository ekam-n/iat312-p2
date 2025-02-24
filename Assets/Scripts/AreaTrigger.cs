using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class AreaTrigger : MonoBehaviour
{
    public GameObject UIObject;
    public GameObject trigger;
    
    void Start()
    {
        UIObject.SetActive(false);  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HI");
        UIObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }

    void Update()
    {
        
    }
}
