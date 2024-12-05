using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using static RoomNPlayerState;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    PhotonView _photonView;
    [SerializeField] Text _playerNameTxt;
    [SerializeField] PlayerState _playerState;
    void Start()
    {
        GameManager.instance.SetPlayerPos(gameObject);
        _photonView = GetComponent<PhotonView>();
        _playerState = GetComponent<PlayerState>();
        _playerNameTxt.text = _photonView.Controller.NickName;
        GameManager.instance.AddPlayerToList(gameObject);

        GameManager.instance.SetMyPlayer(this);
        if (_photonView.IsMine && !PhotonNetwork.IsMasterClient)
        {
            if (LocalSettings.GetCurrentRoom.GetRoomStateProperty(LocalSettings.ROOM_STATE) == ROOMSTATE.GameIsPlaying)
            {
                UpdatePlayerStateOnNetwork(PLAYERSTATE.OutOfGame);
                GamePlayHandler.instance.ResetValuesBeforeGameStart();
                GamePlayHandler.instance.startGameExecutionAction += GamePlayHandler.instance.StartPlayGame;
                BettingManager.instance.ResetThingsBettingManager();

                BettingManager.instance.placeBetBtn.interactable = false;
                BettingManager.instance.autoCashOutBtn.gameObject.SetActive(false);
            }
        }



    }

    public PLAYERSTATE GetPlayerState()
    {
        return _playerState.GetPlayerState();
    }

    public void UpdatePlayerStateOnNetwork(PLAYERSTATE state)
    {
        _playerState.SetPlayerState(state);
    }
}
