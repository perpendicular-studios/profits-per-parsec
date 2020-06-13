using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatController : GameController<PlayerStatController>
{
    private int _cash;

    [Range(-1f, 1f)]
    private float _growthRate;

    private int _publicRelation;

    [Range(-100, 100)]
    private int _governmentSupport;

    private int _researchSpeed = 100;

    private int _maxBuildings = 3;

    private int _numRockets = 0;

    public int cash { get { return _cash; } set { _cash = value; } }
    public float growthRate { get { return _growthRate; } set { _growthRate = value; } }
    public int governmentSupport { get { return _governmentSupport; } set { _governmentSupport = value; } }
    public int publicRelation { get { return _publicRelation; } set { _publicRelation = value; } }
    public int researchSpeed { get { return _researchSpeed; } set { _researchSpeed = value; } }

    public int maxBuildings { get { return _maxBuildings; } set { _maxBuildings = value; } }

    public int numRockets { get { return _numRockets; } set { _numRockets = value; } }

    public Planet currentPlanet;
    
    public Dictionary<string, CameraInfo> cameraList;

    public List<Advisor> advisorListBacklog;                        //List of advisors with custom advisors and randomly generated advisors, which the currentHireList will draw from

    public List<Advisor> advisorHire;                               //List to keep track of advisors available to hire

    public List<Advisor> advisorAssign;                             //List to keep track of hired advisors

    public bool CameraExistsForScene(string scene)
    {
        if(cameraList == null)
        {
            cameraList = new Dictionary<string, CameraInfo>();
        }
        
        if(!cameraList.ContainsKey(scene))
        {
            cameraList.Add(scene, new CameraInfo());
            return false;
        }

        return true;
    }

    public void SaveCameraDataForScene(string scene)
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

        cameraList[SceneManager.GetActiveScene().name] = prevCameraInfo;
    }

    public CameraInfo GetCameraInfoForScene(string scene)
    {

        if (cameraList == null)
        {
            cameraList = new Dictionary<string, CameraInfo>();
        }

        if (!cameraList.ContainsKey(scene))
        {
            cameraList.Add(scene, new CameraInfo());
        }

        return cameraList[scene];
    }
}
