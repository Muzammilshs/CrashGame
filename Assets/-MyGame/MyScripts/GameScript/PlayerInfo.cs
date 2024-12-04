using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    PhotonView _photonView;
    [SerializeField] Text _playerNameTxt;
    void Start()
    {
        GameManager.instance.SetPlayerPos(gameObject);
        _photonView = GetComponent<PhotonView>();
        _playerNameTxt.text = _photonView.Controller.NickName;
        Debug.LogError("Player in room:  " + _photonView.Controller.NickName);
        GameManager.instance.AddPlayerToList(gameObject);
    }


}
