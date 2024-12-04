using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerLogin : MonoBehaviour
{

    //IEnumerator SendDonationToKingdomAPIURL(string sendingAmount)
    //{
    //    string url = APIStrings.getPlayerDetailAPIURL;
    //    formData = new List<KeyValuePair<string, string>>();
    //    AddPOSTField(donationIDConst, donationStatusCls.donation_id.ToString());
    //    AddPOSTField(userIDConst, LocalSettings.MainID);
    //    AddPOSTField(nameConst, Enums.Resources.eggs.ToString());
    //    AddPOSTField(amountConst, sendingAmount);


    //    WWWForm form = CreateWWWForm();

    //    using (var webRequest = UnityWebRequest.Post(url, form))
    //    {
    //        webRequest.timeout = 15;
    //        yield return SendWebRequest(webRequest);
    //        HandleError(webRequest, true);
    //        if (webRequest.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.LogError("Result: " + webRequest.result + "\nFail Json: " + webRequest.downloadHandler.text + "\n Error: " + webRequest.error + "\n Response Code: " + webRequest.responseCode + "\n Result: " + webRequest.result);
    //            RefMgr.Instance.ShowJsonMessage(webRequest.downloadHandler.text);
    //        }
    //        else
    //        {
    //            string jsonResponse = webRequest.downloadHandler.text;
    //            Debug.LogError("Get Detail Json String: " + jsonResponse);
    //            RefMgr.Instance.ShowJsonMessage(jsonResponse);
    //            StartCoroutine(GetDonationSystemStatusAPIURL(LocalSettings.AllianceID, FillDonationFields));
    //            RefreshMainUIItemsNAllianceMembersList();
    //            sendDonatinPanel.SetActive(false);
    //        }
    //    }
    //    RefMgr.Instance.ShowLoadingPanel(false);
    //}
}
