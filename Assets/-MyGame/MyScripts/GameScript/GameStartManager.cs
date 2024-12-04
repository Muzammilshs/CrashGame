using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class GameStartManager : MonoBehaviourPunCallbacks
{
    #region Creating Instance
    private static GameStartManager _instance;

    public static GameStartManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GameStartManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion


    //const int GAMESTARTWAITTIME = 120;
    [SerializeField] Text _gameStartWaitTimeTxt;

    const int GAMESTARTWAITTIME = 0;
    int _gameStartWaitTime = 0;

    float _remainingGameStartTime;
    PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        GetGameStartWaitTimeFromServer();
        ResetRemainingWaitTime();
        _gameStartWaitTimeTxt.text = "Waiting";
    }

    void ResetRemainingWaitTime()
    {
        _gameStartWaitTime = GAMESTARTWAITTIME;
        _remainingGameStartTime = _gameStartWaitTime;
        GetDelayTimeBetweenRounds();
    }
    public void GetGameStartWaitTimeFromServer()
    {

    }

    #region Update section
    private void Update()
    {
        if (_gameStartWaitTime <= 0)
            return;

        if (!PhotonNetwork.InRoom)
            return;

        if (RoomStateManager.instance.getCurrentRoomState != RoomNPlayerState.ROOMSTATE.Waiting)
            return;

        if (PhotonNetwork.IsMasterClient)
        {
            _remainingGameStartTime -= Time.deltaTime;
            _photonView.RPC(nameof(UpDateRemainingTimeToAllPlayers), RpcTarget.All, _remainingGameStartTime);

            if (_remainingGameStartTime < 0)
            {

            }
        }
    }
    [PunRPC]
    public void UpDateRemainingTimeToAllPlayers(float remTime)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            _remainingGameStartTime = remTime;
        }
        string timeString = LocalSettings.TimeFormat(_remainingGameStartTime);
        _gameStartWaitTimeTxt.text = "Starting in " + timeString;
    }

    #endregion


    #region Getting time delay between rounds
    public void GetDelayTimeBetweenRounds()
    {
        GetJson.instance.GetJsonFromServer(APIStrings.getDelayTimeBetweenRoundsAPIURL, ParseJson);
    }

    public void ParseJson(string json, bool isSuccess)
    {
        Debug.LogError($"Json Response: {json}     \nSuccess status: {isSuccess}");
        RoundDelayTimeCls roundDelayTimeCls = JsonConvert.DeserializeObject<RoundDelayTimeCls>(json);
        _gameStartWaitTime = int.Parse(roundDelayTimeCls.delay_time);
        if (_gameStartWaitTime == 1)
        {
            _gameStartWaitTime = 0;
            _gameStartWaitTimeTxt.text = "Waiting";
        }
        _remainingGameStartTime = _gameStartWaitTime;


    }

    #endregion
}
[Serializable]
public class RoundDelayTimeCls
{
    public string delay_time { get; set; }
}
