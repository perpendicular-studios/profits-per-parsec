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
            bool notificationSatisfied = notificationInfo.notificationPredicatePrefab.GetComponent<INotificationPredicate>().EvaluatePredicate();
            if(notificationSatisfied)
            {
                OnNotificationAdded?.Invoke(notificationInfo);
            }
            else
            {
                OnNotificationDeleted?.Invoke(notificationInfo);
            }
        }
    }
}
