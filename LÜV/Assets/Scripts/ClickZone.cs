using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickZone : MonoBehaviour
{
    public GameManager game;
    private bool clicked = false;

    public void OnMouseDown()
    {
        if (!clicked)
        {
            clicked = true;
            game.OnScreenClick();
        }
    }

    public void OnMouseUp()
    {
        clicked = false;
    }
}
