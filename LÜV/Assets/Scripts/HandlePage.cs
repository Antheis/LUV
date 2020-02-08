using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePage : MonoBehaviour
{
    public GameObject m_page;

    private GameObject m_handler;
    public GameObject m_opposite;

    private bool m_selected = false;

    void Start()
    {
        m_handler = gameObject;
        m_selected = m_page.activeInHierarchy;
        //OnMouseDown();
    }

    public void OnMouseDown()
    {
        if (!m_selected)
            Select();
    }

    public void Select()
    {
        m_selected = true;
        m_page.SetActive(true);

        m_opposite.GetComponent<HandlePage>().Deselected();

        Color col = m_handler.GetComponent<Image>().color;
        col.a = 1.0f;
        m_handler.GetComponent<Image>().color = col;

        Color tex = m_handler.GetComponentInChildren<Text>().color;
        tex.a = 1.0f;
        m_handler.GetComponentInChildren<Text>().color = tex;
    }

    public void Deselected()
    {
        m_selected = false;
        m_page.SetActive(false);

        Color col = m_handler.GetComponent<Image>().color;
        col.a = 0.2f;
        m_handler.GetComponent<Image>().color = col;

        Color tex = m_handler.GetComponentInChildren<Text>().color;
        tex.a = 0.2f;
        m_handler.GetComponentInChildren<Text>().color = tex;
    }
}
