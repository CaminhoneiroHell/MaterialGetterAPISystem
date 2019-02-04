using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    //public Shader highlight;
    //public Material normalItem;
    //public GameObject item;
    //public IRayCaster rayCaster;
    
    [SerializeField] Renderer rend;
    [SerializeField] GameObject GO;
    [SerializeField] bool isSelected;
    void SelectionEFX()
    {
        float shininess = Mathf.PingPong(Time.time/5.5f, 0.050f);
        rend.material.SetFloat("_Outline", shininess);
    }

    private void OnMouseEnter()
    {
        if (GO == this.gameObject)
            return;

        GO = this.gameObject;
        rend = GO.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/Outline");
        isSelected = true;
        print("Dentro");
    }
    
    private void Update()
    {
        if (isSelected)
            SelectionEFX();
    }
    
    private void OnMouseExit()
    {
        GO = this.gameObject;
        Renderer rend = GO.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Standard");
        isSelected = false;
        GO = null;
        print("Fora");
    }
}
