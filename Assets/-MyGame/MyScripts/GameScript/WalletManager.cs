using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletManager : MonoBehaviour
{
    #region Creating Instance
    private static WalletManager _instance;


    public static WalletManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindFirstObjectByType<WalletManager>();
            return _instance;
        }
    }
    #endregion


    [SerializeField] Text[] _totalAmountTxt;




    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    void Start()
    {
        UpdateTotalAmountTexts();
    }

    // From server we will get total amount here.
    public double GetTotalAmount()
    {
        return LocalSettings.TotalAmount;
    }

    public void AddToTotalAmount(double amount)
    {
        LocalSettings.TotalAmount += amount;
        UpdateTotalAmountTexts();
    }

    public void UpdateTotalAmountTexts()
    {
        foreach (var txt in _totalAmountTxt)
        {
            txt.text = LocalSettings.TotalAmount.ToString();
        }
    }

}
