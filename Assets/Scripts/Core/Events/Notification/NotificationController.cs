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
    public List<NotificationInfo> addedNotifications;

    public void Awake()
    {
        allNotifications = new List<NotificationInfo>();
        addedNotifications = new List<NotificationInfo>();
    }

    public void OnEnable()
    {
        DateTimeController.OnDailyTick += UpdateNotifications;
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
                if (!addedNotifications.Contains(notificationInfo))
                {
                    OnNotificationAdded?.Invoke(notificationInfo);
                    addedNotifications.Add(notificationInfo);
                }
            }
            else
            {
                Debug.Log("Notification Not Satisfied!");
                if (allNotifications.Contains(notificationInfo))
                {
                    OnNotificationDeleted?.Invoke(notificationInfo);
                    addedNotifications.Remove(notificationInfo);
                }
            }
        }
    }
}
