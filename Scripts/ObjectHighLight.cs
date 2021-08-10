using System.Collections;
using System.Collections.Generic;
using HighlightingSystem;
using UnityEngine;

public class ObjectHighLight : MonoBehaviour
{
    private Highlighter highlighter;
    private Color h_color;
    // Start is called before the first frame update
    void Start()
    {
        highlighter = gameObject.AddComponent<Highlighter>();
        h_color = Color.yellow;
    }

    private void OnMouseOver()
    {
        highlighter.ConstantOn(h_color);
    }

    private void OnMouseExit()
    {
        highlighter.ConstantOff();   
    }
}
