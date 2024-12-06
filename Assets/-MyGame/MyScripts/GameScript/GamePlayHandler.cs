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
    [SerializeField] GameObject _cashoutPlayerSign;

    public Action startGameExecutionAction;
    float _currentMultiplierPoint;
    float _currentGameTime;
    float _currentGameTimeToShow;
    float _timeToStartCrashPoint;

    float _finalCrashPoint;
    bool _isGameCrashed;
    PhotonView _photonView;

    float _gameResetDelayTime = 5f;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _finalCrashPoint = 3.5f;
        _timeToStartCrashPoint = 2f;
        ResetValuesBeforeGameStart();


    }
    float _currentGameRestDelayTime = 0;
    private void Update()
    {
        if (_currentGameRestDelayTime > 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _currentGameRestDelayTime -= Time.deltaTime;
                _photonView.RPC(nameof(GameResetDelayTimeRPC), RpcTarget.All, _currentGameRestDelayTime);
                if (_currentGameRestDelayTime <= 0.2f)
                {
                    _currentGameRestDelayTime = -1f;
                    RoomStateManager.instance.UpdateCurrentRoomState(RoomNPlayerState.ROOMSTATE.Waiting);
                }
            }
        }
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
    float _rocketX, _rocketY;
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

        if (_rocketRectTransform == null)
            _rocketRectTransform = _rocketObj.GetComponent<RectTransform>();
        _rocketX = _currentGameTimeToShow * 50f;
        _rocketY = _currentMultiplierPoint * 60f;
        _rocketRectTransform.anchoredPosition = new Vector2(_rocketX, _rocketY);
        if (isGameCrashed)
        {
            GameCrash();
        }
    }
    [PunRPC]
    public void GameResetDelayTimeRPC(float delayTime)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            _currentGameRestDelayTime = delayTime;
        }
    }

    public void RocketPosXY(out float x, out float y)
    {
        x = _rocketX;
        y = _rocketY;
    }
    public float GetCurrentMultiplierPointOnCashOut()
    {
        return _currentMultiplierPoint;
    }
    void GameCrash()
    {
        Debug.LogError(" 2 _______game crashed: ");

        startGameExecutionAction = null;
        _rocketObj.SetActive(false);
        BettingManager.instance.DisableCashOutbtn(false);
        _currentGameRestDelayTime = LocalSettings.GAME_RESET_DELAY_TIME;
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
        _signsOfPlayers = DestroyAndClearList(_signsOfPlayers);
    }

    List<GameObject> _signsOfPlayers = new List<GameObject>();
    public GameObject GetCashOutPlayerSign()
    {
        GameObject obj = Instantiate(_cashoutPlayerSign);
        obj.SetActive(true);
        LocalSettings.SetPosAndRect(obj, _rocketStartPos.GetComponent<RectTransform>(), _rocketStartPos.transform.parent);
        _signsOfPlayers.Add(obj);
        return obj;
    }
    public List<GameObject> DestroyAndClearList(List<GameObject> list)
    {
        if (list == null)
            return list = new List<GameObject>();
        if (list.Count == 0)
            return list;
        foreach (GameObject obj in list)
        {
            if (obj != null)
                Destroy(obj);
        }
        list.Clear();
        return list;
    }
}
