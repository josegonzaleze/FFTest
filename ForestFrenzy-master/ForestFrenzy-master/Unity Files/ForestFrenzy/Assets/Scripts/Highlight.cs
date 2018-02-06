using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

    // Use this for initialization

    private Material defaultMaterial;
    public Material highlighMaterial;

    void Start() {
        defaultMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {

    }

    public void HighlightBlock()
    {
        //Debug.Log("HighlightBlock called");
        gameObject.GetComponent<Renderer>().material = highlighMaterial;
    }

    public void UnHighlightBlock()
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
        //Debug.Log("Undo highlight");
    }
}
