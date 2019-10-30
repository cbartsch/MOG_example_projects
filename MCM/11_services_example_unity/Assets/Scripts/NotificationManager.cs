using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif
using UnityEngine;

public class NotificationManager : MonoBehaviour {
    public string notificationIcon;

    private bool iosAuthorized = false;

    // Start is called before the first frame update
    void Start() {
#if UNITY_ANDROID
        AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel() {
            Id = "channel_id", Name = "Default Channel",
            Importance = Importance.High, Description = "Generic notifications",
        });

        AndroidNotificationCenter.OnNotificationReceived += (identifier, notification, channel) => {
            // app was opened via notification
            Debug.Log("Received notification: " + notification.Text);
        };
#endif
    }

    // Update is called once per frame
    void Update() {
    }

    public void ShowNotification(string message) {
#if UNITY_ANDROID
        var notification = new AndroidNotification {
            Title = message,
            FireTime = System.DateTime.Now.AddSeconds(1),
            SmallIcon = notificationIcon,
            ShouldAutoCancel = true
        };

        Debug.Log("Send Android notification.");
        var identifier = AndroidNotificationCenter.SendNotification(notification, "channel_id");
#endif

#if UNITY_IOS
        StartCoroutine(ShowIosNotification(message));
#endif
    }

#if UNITY_IOS
    private IEnumerator ShowIosNotification(string message) {
        if (!iosAuthorized) {
            yield return StartCoroutine(GetIosAuthorization());
        }

        if (iosAuthorized) {
            iOSNotificationCenter.ScheduleNotification(new iOSNotification {
                Identifier = "_notification_01",
                Title = message,
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.NotificationPresentationOptionAlert,
                CategoryIdentifier = "category_a",
                Trigger = new iOSNotificationTimeIntervalTrigger {
                    TimeInterval = new TimeSpan(0, 0, 1),
                    Repeats = false
                },
            });
        }
    }

    private IEnumerator GetIosAuthorization() {
        var options = AuthorizationOption.AuthorizationOptionAlert |
                      AuthorizationOption.AuthorizationOptionBadge;
        using (var req = new AuthorizationRequest(options, true)) {
            while (!req.IsFinished) {
                yield return new WaitForSeconds(1);
            }

            iosAuthorized = req.Granted;
        }
    }
#endif
}