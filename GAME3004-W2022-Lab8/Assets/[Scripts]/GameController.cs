using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
