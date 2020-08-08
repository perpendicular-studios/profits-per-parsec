using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpaceReturnButton : MonoBehaviour
{
    public string spaceSceneName;

    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClick(); });
    }
    
    public void OnClick()
    {
        PlayerStatController.instance.SaveCameraDataForScene(SceneManager.GetActiveScene().name);
        PlayerStatController.instance.currentPlanet = null;
        
        // Clear notifications when transitioning scenes
        NotificationController.instance.ClearNotifications();
        
        SceneManager.LoadScene(spaceSceneName);
    }
}
