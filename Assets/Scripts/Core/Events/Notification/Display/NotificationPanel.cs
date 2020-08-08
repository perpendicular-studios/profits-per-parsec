using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    public NotificationInfo notificationInfo;
    public Image notificationPanelIcon;
    public Image notificationPanelBacking;

    public void Setup(NotificationInfo notificationInfo)
    {
        this.notificationInfo = notificationInfo;
        if(notificationInfo != null)
        {
            notificationPanelIcon.sprite = notificationInfo.notificationIcon;
            notificationPanelBacking.sprite = notificationInfo.notificationBacking;
        }
    }

}
