using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerLogin : ES3Cloud
{
    #region Creating Instance
    public PlayerLogin(string url, string apiKey) : base(url, apiKey)
    {
    }
    private static PlayerLogin _instance;

    public static PlayerLogin instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<PlayerLogin>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    const string USERNAME = "username";
    const string EMAIL = "email";
    const string WALLETNUMBER = "wallet_number";
    public void GetPlayerData()
    {
        formData = new List<KeyValuePair<string, string>>();
        string playerName = "Muzammil New";
        string email = "muzammilNew@gmail.com";
        string walletNumber = "1234567890";

        AddPOSTField(USERNAME, playerName);
        AddPOSTField(EMAIL, email);
        AddPOSTField(WALLETNUMBER, walletNumber);
        GetJson.instance.PostDataAndGetResponseFromServer(APIStrings.getPlayerDetailAPIURL, formData, ParseJson);
    }


    public void ParseJson(string json, bool isSuccess)
    {
        Debug.LogError($"Json Response: {json}     \nSuccess status: {isSuccess}");
        if (!isSuccess)
            return;
        PlayerLoginRootCls playerLoginRootCls = JsonConvert.DeserializeObject<PlayerLoginRootCls>(json);
        LocalSettings.userName = playerLoginRootCls.data.username;
        LocalSettings.emailID = playerLoginRootCls.data.email;
        LocalSettings.walletID = playerLoginRootCls.data.wallet_number;
        LocalSettings.walletAmount = Convert.ToDouble(playerLoginRootCls.data.wallet_balance);
        LocalSettings.isBlocked = playerLoginRootCls.data.status == "Active" ? false : true;

        GameStartManager.instance.GetDelayTimeBetweenRounds();
    }
}

public class UserDetailCls
{
    public string username { get; set; }
    public string email { get; set; }
    public string wallet_number { get; set; }
    public string wallet_balance { get; set; }
    public string status { get; set; }
}

public class PlayerLoginRootCls
{
    public bool success { get; set; }
    public string message { get; set; }
    public UserDetailCls data { get; set; }
}

