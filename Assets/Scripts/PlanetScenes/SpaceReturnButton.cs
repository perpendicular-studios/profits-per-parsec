using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Update()
    {

    }
    public void OnClick()
    {
        PlayerStatController.instance.SaveCameraDataForScene(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(spaceSceneName);
    }
}
