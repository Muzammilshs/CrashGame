
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public static class LocalSettings
{
    public static string TimeFormat(double timeLeft)
    {
        double timeLeftInSeconds = SceneManager.GetActiveScene().name != "Game" ? timeLeft * 60 : timeLeft;


        int hours = (int)(timeLeftInSeconds / 3600);
        int minutes = (int)((timeLeftInSeconds % 3600) / 60);
        int seconds = (int)(timeLeftInSeconds % 60);


        string second = SceneManager.GetActiveScene().name == "Game" ? $":{seconds:D2}s" : "";

        string formattedTimeLeft = $"{hours:D2}h:{minutes:D2}m{second}";


        Debug.Log("Formatted Time Left: " + formattedTimeLeft);
        return formattedTimeLeft;

    }


    public static void SetPosAndRect(GameObject InstantiatedObj, RectTransform ALReadyObjPos, Transform Parentobj)
    {
        InstantiatedObj.transform.parent = Parentobj;
        RectTransform MyPlayerRectTransform = InstantiatedObj.GetComponent<RectTransform>();
        MyPlayerRectTransform.localScale = ALReadyObjPos.localScale;
        MyPlayerRectTransform.localPosition = ALReadyObjPos.localPosition;
        MyPlayerRectTransform.anchorMin = ALReadyObjPos.anchorMin;
        MyPlayerRectTransform.anchorMax = ALReadyObjPos.anchorMax;
        MyPlayerRectTransform.anchoredPosition = ALReadyObjPos.anchoredPosition;
        MyPlayerRectTransform.sizeDelta = ALReadyObjPos.sizeDelta;
        MyPlayerRectTransform.localRotation = ALReadyObjPos.localRotation;

    }

    const string totalAmount = "total_amount";
    const string defaultAmount = "1000";
    public static double TotalAmount
    {
        get => Convert.ToDouble(PlayerPrefs.GetString(totalAmount, "1000"));
        set => PlayerPrefs.SetString(totalAmount, value.ToString());
    }

}
