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

        GameObject focus = GameObject.FindGameObjectWithTag("Focus");
        PerspectiveZoomStrategy zoomStrategy = (focus.GetComponent<CameraController>().zoomStrategy) as PerspectiveZoomStrategy;
        //Set prev cam info
        CameraInfo prevCameraInfo = new CameraInfo(
                focus.transform.position.x,
                focus.transform.position.y,
                focus.transform.position.z,
                focus.transform.eulerAngles.x,
                focus.transform.eulerAngles.y,
                focus.transform.eulerAngles.z,
                Camera.main.transform.localPosition.x,
                Camera.main.transform.localPosition.y,
                Camera.main.transform.localPosition.z,
                zoomStrategy.currentZoomLevel
                ); 
        PlayerStatController.instance.cameraList[(SceneManager.GetActiveScene().name)] = prevCameraInfo;

        SceneManager.LoadScene(spaceSceneName);
    }
}
