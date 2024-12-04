using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoomStateManager : MonoBehaviourPunCallbacks
{
    #region Creating Instance
    private static RoomStateManager _instance;
    public static RoomStateManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<RoomStateManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

    }
    #endregion


    public RoomNPlayerState.ROOMSTATE currentRoomState;

    void Start()
    {

    }

    public RoomNPlayerState.ROOMSTATE getCurrentRoomState => currentRoomState;


    public void UpdateCurrentRoomState(RoomNPlayerState.ROOMSTATE state)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateRoomStateProperty(state);
            photonView.RPC(nameof(UpdateThisStateOnNetwork), RpcTarget.All, state, "");
        }
    }

    [PunRPC]
    public void UpdateThisStateOnNetwork(RoomNPlayerState.ROOMSTATE state, string info)
    {

        currentRoomState = state;
        Debug.Log("Current State is Set to " + state);
        OnUpdateCurrentState(state, info);
    }
    void UpdateRoomStateProperty(RoomNPlayerState.ROOMSTATE state)
    {
        if (PhotonNetwork.IsConnectedAndReady)
            LocalSettings.GetCurrentRoom.SetRoomStateProperty(LocalSettings.ROOM_STATE, state);
    }

    void OnUpdateCurrentState(RoomNPlayerState.ROOMSTATE state, string infoText)
    {
        switch (state)
        {
            case RoomNPlayerState.ROOMSTATE.Waiting:
                TriggerStateWaitingForPlayers();
                break;

            case RoomNPlayerState.ROOMSTATE.GameIsPlaying:
                TriggerStateGameIsPlaying();
                break;

        }
    }

    void TriggerStateWaitingForPlayers()
    {

    }

    void TriggerStateGameIsPlaying()
    {

    }
    public void SetRoomStateFromNetworkCustomProperty()
    {
        UpdateCurrentRoomState(LocalSettings.GetCurrentRoom.GetRoomStateProperty(LocalSettings.ROOM_STATE));
    }
    public bool isGameStarted => currentRoomState == RoomNPlayerState.ROOMSTATE.GameIsPlaying;

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
            SetRoomStateFromNetworkCustomProperty();
        else
        {
            UpdateCurrentRoomState(RoomNPlayerState.ROOMSTATE.Waiting);
        }
    }
}
