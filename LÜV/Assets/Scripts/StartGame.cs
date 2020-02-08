using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject screen;
    public GameObject game;
    bool begin = false;

    // Update is called once per frame
    void Update()
    {
        if (!begin && Input.anyKey)
        {
            begin = true;
            screen.SetActive(false);
            game.SetActive(true);
        }
    }
}