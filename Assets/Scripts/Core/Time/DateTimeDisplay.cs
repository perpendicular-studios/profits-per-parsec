using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateTimeDisplay : MonoBehaviour
{
    public Text textDisplay;
    public Button pauseButton;
    public Button slowDownButton, speedUpButton;
    public Text speedDisplay;

    public Sprite pauseImage, playImage;

    public void Awake()
    {
        pauseButton.onClick.AddListener(delegate { OnPause(); });
        slowDownButton.onClick.AddListener(delegate { SlowDown(); });
        speedUpButton.onClick.AddListener(delegate { SpeedUp(); });
    }

    public void Update()
    {
        textDisplay.text = DateTimeController.instance.GetFormattedDateTime();
        speedDisplay.text = DateTimeController.instance.speed.ToString();
        UpdatePause();
    }

    public void UpdatePause()
    {
        bool paused = DateTimeController.instance.paused;

        if (paused)
        {
            pauseButton.image.sprite = playImage;
        }
        else
        {
            pauseButton.image.sprite = pauseImage;
        }
    }

    public void OnPause()
    {
        bool paused = DateTimeController.instance.paused;

        if (paused) {
            pauseButton.image.sprite = pauseImage;
            DateTimeController.instance.Play();
        }
        else
        { 
            pauseButton.image.sprite = playImage;
            DateTimeController.instance.Pause();
        }
    }

    public void SlowDown()
    {
        DateTimeController.instance.SlowDown();
    }

    public void SpeedUp()
    {
        DateTimeController.instance.SpeedUp();
    }

}
