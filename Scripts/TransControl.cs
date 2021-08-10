using HighlightingSystem;
using UnityEngine;


public class TransControl : MonoBehaviour
{
    private Highlighter m_highlighter;
    private PlayerController playerController;
    private Color m_color;
    private PlayerController controller;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        m_highlighter = gameObject.AddComponent<Highlighter>();
        m_color = Color.white;
    }

    private void Update()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (controller.show_TransNPC && !playerController.enabled)
        {
            m_highlighter.ConstantOn(m_color);
            //Debug.Log(1);
        }
        else
        {

            m_highlighter.ConstantOff();
            //Debug.Log(2);
        }
    }
}