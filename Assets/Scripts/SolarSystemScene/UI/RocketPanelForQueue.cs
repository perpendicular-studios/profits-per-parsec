using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketPanelForQueue : MonoBehaviour
{
    public Text constructionTimeText, rocketTypeText;
    public RocketQueueItem rocketQueueItem;
    public Button deleteButton;

    // Start is called before the first frame update
    void Start()
    {
        deleteButton.onClick.AddListener(() => DeleteItem());
    }

    private void OnEnable()
    {
        DateTimeController.OnDailyTick += UpdateText;
    }

    private void OnDisable()
    {
        DateTimeController.OnDailyTick -= UpdateText;
    }

    public void SetText()
    {
        rocketTypeText.text = rocketQueueItem.rocketType.ToString() + " Rocket";
        constructionTimeText.text = rocketQueueItem.constructionTime.ToString() + " Days";
    }

    public void UpdateText()
    {
        if(rocketQueueItem.constructionTime <= 0)
        {
            Destroy(gameObject);
        }
        constructionTimeText.text = rocketQueueItem.constructionTime.ToString() + " Days";
    }

    public void DeleteItem()
    {
        PlayerStatController.instance.currentPlanet.rocketConstructionQueue.Remove(rocketQueueItem);
        PlayerStatController.instance.currentPlanet.constructingRockets--;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
