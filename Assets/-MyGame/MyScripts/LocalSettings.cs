
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Photon.Pun;
using Photon.Realtime;


public static class LocalSettings
{
    public const string ROOM_STATE = "ROOMSTATE";




    public static string TimeFormat(float timeLeft)
    {
        float timeLeftInSeconds = timeLeft;

        int hours = (int)(timeLeftInSeconds / 3600);
        int minutes = (int)(timeLeftInSeconds / 60);
        int seconds = (int)(timeLeftInSeconds % 60);

        string formattedTimeLeft;
        if (hours > 0)
            formattedTimeLeft = $"{hours}:{minutes:D2}:{seconds:D2}";
        else if (minutes > 0)
            formattedTimeLeft = $"{minutes:D2}:{seconds:D2}";
        else if (seconds >= 0)
            formattedTimeLeft = $"{seconds:D2}s";
        else
            formattedTimeLeft = $"0s";

        return formattedTimeLeft;
    }


    public static void SetPosAndRect(GameObject InstantiatedObj, RectTransform ALReadyObjPos, Transform Parentobj)
    {
        InstantiatedObj.transform.parent = Parentobj;
        RectTransform myPlayerRectTransform = InstantiatedObj.GetComponent<RectTransform>();
        myPlayerRectTransform.localScale = ALReadyObjPos.localScale;
        myPlayerRectTransform.localPosition = ALReadyObjPos.localPosition;
        myPlayerRectTransform.anchorMin = ALReadyObjPos.anchorMin;
        myPlayerRectTransform.anchorMax = ALReadyObjPos.anchorMax;
        myPlayerRectTransform.anchoredPosition = ALReadyObjPos.anchoredPosition;
        myPlayerRectTransform.sizeDelta = ALReadyObjPos.sizeDelta;
        myPlayerRectTransform.localRotation = ALReadyObjPos.localRotation;

    }

    const string TOTAL_AMOUNT = "total_amount";
    const string DEFAULT_AMOUNT = "1000";
    public static double TotalAmount
    {
        get => Convert.ToDouble(PlayerPrefs.GetString(TOTAL_AMOUNT, "1000"));
        set => PlayerPrefs.SetString(TOTAL_AMOUNT, value.ToString());
    }
    public static Room GetCurrentRoom
    {
        get => PhotonNetwork.CurrentRoom;
    }
}
