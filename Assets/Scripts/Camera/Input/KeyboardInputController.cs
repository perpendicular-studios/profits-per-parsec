using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInputController : InputController
{
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotate;
    public static event ZoomInputHandler OnZoom;
    public GameObject ResearchDisplay;
    public GameObject AdvisorDisplay;
    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnMoveInput?.Invoke(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SolarSystemTest");
        }

        ToggleUI(KeyCode.T, ResearchDisplay);
        ToggleUI(KeyCode.Y, AdvisorDisplay);
    }

    void ToggleUI(KeyCode key, GameObject display)
    {
        if (Input.GetKeyDown(key))
        {
            //Close screen if currently active, else open screen
            if (display.active)
            {
                display.SetActive(false);
            }
            else
            {
                //Trigger a function that closes all other screens 
                CloseAllUI();
                display.SetActive(true);
            }
        }
    }

    void CloseAllUI()
    {
        ResearchDisplay.SetActive(false);
        AdvisorDisplay.SetActive(false);
    }

}
