using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Creating Instance
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GameManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion
    [SerializeField] GameObject _loadingPanel;

    [SerializeField] GameObject _messagePanelPrefab;
    [SerializeField] Text _messageTxt;
    void Start()
    {

    }


    public void ShowLoadingPanel(bool isShow)
    {
        _loadingPanel.SetActive(isShow);
    }

    public void ShowMessage(string message)
    {
        _messageTxt.text = message;
        _messagePanelPrefab.SetActive(true);
    }

}
