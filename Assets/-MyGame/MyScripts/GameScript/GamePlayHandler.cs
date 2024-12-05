using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayHandler : MonoBehaviour
{
    #region Creating Instance
    private static GamePlayHandler _instance;

    public static GamePlayHandler instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GamePlayHandler>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    [SerializeField] GameObject _rocketObj;
    [SerializeField] GameObject _rocketStartPos;
    [SerializeField] Text _currentElapsedTimeTxt;
    [SerializeField] Text _currentMultiplierTxt;

    public Action startGameExecutionAction;
    float _currentMultiplierPoint;
    float _currentGameTime;
    float _currentGameTimeToShow;
    float _timeToStartCrashPoint;

    float _finalCrashPoint;
    bool _isGameCrashed;
    PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _finalCrashPoint = 3.5f;
        _timeToStartCrashPoint = 2f;
        ResetValuesBeforeGameStart();


    }
    private void Update()
    {
        if (startGameExecutionAction == null)
            return;
        if (PhotonNetwork.IsMasterClient)
        {
            startGameExecutionAction?.Invoke();
        }

    }
    public void StartPlayGame()
    {
        _currentGameTime += Time.deltaTime;
        if (_currentGameTime > _timeToStartCrashPoint)
        {
            _currentMultiplierPoint += (Time.deltaTime / 4.3f);
            _currentGameTimeToShow += Time.deltaTime;
            if (_currentMultiplierPoint >= _finalCrashPoint)
            {
                _isGameCrashed = true;
            }
            _photonView.RPC(nameof(UpdateValuesToAllPlayersOnNetwork), RpcTarget.All, _currentGameTime, _currentGameTimeToShow, _currentMultiplierPoint, _isGameCrashed);
        }
    }

    RectTransform _rocketRectTransform;
    [PunRPC]
    public void UpdateValuesToAllPlayersOnNetwork(float currentGameTime, float currentGameTimeToShow, float currentCrashPoint, bool isGameCrashed)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            _currentMultiplierPoint = currentCrashPoint;
            _currentGameTime = currentGameTime;
            _currentGameTimeToShow = currentGameTimeToShow;
        }

        _currentElapsedTimeTxt.text = _currentGameTimeToShow.ToString("F0");
        _currentMultiplierTxt.text = _currentMultiplierPoint.ToString("F2");
        _isGameCrashed = isGameCrashed;
        //_rocketObj.transform.position += new Vector3((_rocketObj.transform.position.x + _currentGameTimeToShow), _rocketObj.transform.position.y + _currentMultiplierPoint, _rocketObj.transform.position.z);
        if (_rocketRectTransform == null)
            _rocketRectTransform = _rocketObj.GetComponent<RectTransform>();
        _rocketRectTransform.anchoredPosition = new Vector2(_currentGameTimeToShow * 50f, _currentMultiplierPoint * 60f);
        if (isGameCrashed)
        {
            GameCrash();
        }
    }

    void GameCrash()
    {
        Debug.LogError(" 2 _______game crashed: ");

        startGameExecutionAction = null;
        _rocketObj.SetActive(false);
    }
    public bool IsGameCrashed()
    {
        return _isGameCrashed;
    }

    public void ResetValuesBeforeGameStart()
    {
        _currentMultiplierPoint = 1;
        _currentGameTime = 0;
        _currentGameTimeToShow = 0;
        _isGameCrashed = false;
        _rocketObj.transform.localPosition = _rocketStartPos.transform.localPosition;
        LocalSettings.SetPosAndRect(_rocketObj, _rocketStartPos.GetComponent<RectTransform>(), _rocketStartPos.transform.parent);
    }
}
