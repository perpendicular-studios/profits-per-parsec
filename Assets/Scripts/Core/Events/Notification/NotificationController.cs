using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : GameController<NotificationController>
{
    public static event AddNotification OnNotificationAdded;
    public delegate void AddNotification(NotificationInfo notificationInfo);

    public static event DeleteNotification OnNotificationDeleted;
    public delegate void DeleteNotification(NotificationInfo notificationObject);

    public List<NotificationInfo> allNotifications;

    public void Awake()
    {
        allNotifications = new List<NotificationInfo>();
    }

    public void CreateNotification(NotificationInfo notificationInfo)
    {
        allNotifications.Add(notificationInfo);
    }

    public void UpdateNotifications()
    {
        foreach(NotificationInfo notificationInfo in allNotifications)
        {
            GameObject notificationPredicate = Instantiate(notificationInfo.notificationPredicatePrefab, transform);
            bool notificationSatisfied = notificationPredicate.GetComponent<INotificationPredicate>().EvaluatePredicate();
            if(notificationSatisfied)
            {
                Debug.Log("Notification Satisfied!");
                OnNotificationAdded?.Invoke(notificationInfo);
            }
            else
            {
                Debug.Log("Notification Not Satisfied!");
                OnNotificationDeleted?.Invoke(notificationInfo);
            }
        }
    }
}
