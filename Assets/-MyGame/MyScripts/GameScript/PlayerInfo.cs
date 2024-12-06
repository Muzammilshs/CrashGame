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
                GameManager.instance.ShowWaitingOrMultiPlierBoxInGame(false);
            }
            else
            {
                UpdatePlayerStateOnNetwork(PLAYERSTATE.Waiting);
                GamePlayHandler.instance.ResetValuesBeforeGameStart();
                BettingManager.instance.ResetThingsBettingManager();
                GameManager.instance.ShowWaitingOrMultiPlierBoxInGame(true);
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

    public void ShowCashOutPointToOtherPlayers()
    {
        GamePlayHandler.instance.RocketPosXY(out float x, out float y);
        float xVal = x;
        float yVal = y;
        Debug.LogError($"Positions got: x: {x}, y: {y}");
        _photonView.RPC(nameof(ShowCashOutPointToOtherPlayersRPC), RpcTarget.All, xVal, yVal);
    }

    [PunRPC]
    public void ShowCashOutPointToOtherPlayersRPC(float x, float y)
    {
        Debug.LogError($"All players get: {x}, y: {y}");

        GameObject sign = GamePlayHandler.instance.GetCashOutPlayerSign();
        sign.transform.GetChild(1).GetComponent<Text>().text = _photonView.Controller.NickName;
        RectTransform signRect = sign.GetComponent<RectTransform>();

        signRect.anchoredPosition = new Vector2(x, y);

    }
}
