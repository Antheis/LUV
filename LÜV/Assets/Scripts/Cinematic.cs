using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public Text text;
    private string m_text;
    int idx = 0;
    bool wait = false;

    void Start()
    {
        m_text = text.text;
        text.text = "";
        StartCoroutine(WaitAfterNewLine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            SceneManager.LoadScene("MainGame");
    }

    void FixedUpdate()
    {
        if (wait || idx >= m_text.Length) return;
        text.text += m_text[idx];
        ++idx;
        if (m_text[idx] == '\n')
        {
            ++idx;
            StartCoroutine(WaitAfterNewLine());
        }
    }

    IEnumerator WaitAfterNewLine()
    {
        wait = true;
        yield return new WaitForSeconds(3);
        wait = false;
        text.text = "";
    }
}
