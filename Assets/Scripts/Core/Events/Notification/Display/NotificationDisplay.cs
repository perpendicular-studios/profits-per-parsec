using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationDisplay : MonoBehaviour
{
    public GameObject notificationPanelPrefab;
    public NotificationAssetList notificationList;

    public List<GameObject> activeNotifications;
   

    private void Awake()
    {
        activeNotifications = new List<GameObject>();

        if (notificationList != null)
        {
            foreach (NotificationInfo notificationInfo in notificationList.assetList)
            {
                NotificationController.instance.CreateNotification(notificationInfo);
            }
        }

    }

    public void OnEnable()
    {
        NotificationController.OnNotificationAdded += OnNotificationAdded;
        NotificationController.OnNotificationDeleted += OnNotificationDeleted;
    }

    public void OnDisable()
    {
        NotificationController.OnNotificationAdded -= OnNotificationAdded;
        NotificationController.OnNotificationDeleted -= OnNotificationDeleted;
    }

    private void OnNotificationAdded(NotificationInfo notificationInfo)
    {
        GameObject newNotificationObject = Instantiate(notificationPanelPrefab, transform);
        newNotificationObject.GetComponent<NotificationPanel>().Setup(notificationInfo);
        activeNotifications.Add(newNotificationObject);
    }

    private void OnNotificationDeleted(NotificationInfo notificationInfo)
    {
        foreach(GameObject notificationObject in activeNotifications)
        {
            NotificationInfo notificationInfoInObject = notificationObject.GetComponent<NotificationPanel>().notificationInfo;
            if(notificationInfoInObject == notificationInfo)
            {
                activeNotifications.Remove(notificationObject);
            }
        }
    }
}
