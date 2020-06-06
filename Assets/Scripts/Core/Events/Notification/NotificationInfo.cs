using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Notification")]
public class NotificationInfo : ScriptableObject
{
    public Sprite notificationIcon;
    public Sprite notificationBacking;
    public string defaultNotificationDescription;
    public GameObject notificationPredicatePrefab;
}
