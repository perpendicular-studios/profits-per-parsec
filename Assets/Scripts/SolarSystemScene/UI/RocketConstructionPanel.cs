using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketConstructionPanel : MonoBehaviour
{
    public GameObject queuePanel;
    public GameObject rocketPanelForQueue;
    public Button colonialButton, touristButton, cargoButton, tradeButton;
    public RocketActivePanel rocketActivePanel;

    void Start()
    {
        colonialButton.onClick.AddListener(() => BuildRocket(RocketType.Colonial));
        touristButton.onClick.AddListener(() => BuildRocket(RocketType.Tourist));
        cargoButton.onClick.AddListener(() => BuildRocket(RocketType.Cargo));
        tradeButton.onClick.AddListener(() => BuildRocket(RocketType.Trade));

        //Check if current planet has queued rockets
        if (PlayerStatController.instance.currentPlanet.rocketConstructionQueue == null)
        {
            PlayerStatController.instance.currentPlanet.rocketConstructionQueue = new List<RocketQueueItem>();
        }
        //Load already queued rockets
        else
        {
            foreach(RocketQueueItem constructingRocket in PlayerStatController.instance.currentPlanet.rocketConstructionQueue)
            {
                //Add rocket to queue
                RocketPanelForQueue panel = Instantiate(rocketPanelForQueue, queuePanel.transform).GetComponent<RocketPanelForQueue>();
                panel.rocketQueueItem = constructingRocket;
                panel.SetText();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildRocket(RocketType rocketType)
    {
        Planet currPlanet = PlayerStatController.instance.currentPlanet;
        //Check if the planet has enough space for a new rocket
        if (!currPlanet.isRocketCapacityFull())
        {
            //Queue
            //Begin construction of a rocket
            RocketPanelForQueue panel = Instantiate(rocketPanelForQueue, queuePanel.transform).GetComponent<RocketPanelForQueue>();

            //Add rocket to queue
            RocketQueueItem newRocket = new RocketQueueItem();
            newRocket.constructionTime = 10;
            newRocket.rocketType = rocketType;
            currPlanet.rocketConstructionQueue.Add(newRocket);
            currPlanet.constructingRockets++;
            panel.rocketQueueItem = newRocket;
            panel.SetText();

        }
        //Rocket Capacity Full
        else
        {
            Debug.Log("Rocket Capacity Full!");
        }
    }
}
