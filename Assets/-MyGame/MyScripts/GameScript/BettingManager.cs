using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour
{
    public InputField betAmountTxt;
    public InputField multiplierTxt;
    WalletManager _wm;
    double _currentBetAmount = 0;
    float _currentMultiplier = 0;
    void Start()
    {
        _wm = WalletManager.instance;
        _currentBetAmount = 0;
        _currentMultiplier = 2.0f;
        betAmountTxt.text = _currentBetAmount.ToString();
        multiplierTxt.text = _currentMultiplier.ToString();
    }

    #region Current bet setting
    public void SetBetAmount(bool isAdd)
    {
        if (isAdd)
        {
            double tempAmount = _currentBetAmount;
            tempAmount += 10;
            if (tempAmount > _wm.GetTotalAmount())
            {
                GameManager.instance.ShowMessage("Not Enough cash");
                return;
            }

            _currentBetAmount = tempAmount;
            betAmountTxt.text = _currentBetAmount.ToString();

        }
        else
        {
            double tempAmount = _currentBetAmount;
            tempAmount -= 10;
            if (tempAmount < 0)
                tempAmount = 0;
            _currentBetAmount = tempAmount;
            betAmountTxt.text = _currentBetAmount.ToString();
        }
    }

    public void OnValueChangeBetAmount()
    {
        double amount = Convert.ToDouble(betAmountTxt.text);
        if (amount > _wm.GetTotalAmount())
        {
            amount = _wm.GetTotalAmount();
        }
        else if (amount < 0)
        {
            amount = 0;
        }
        _currentBetAmount = amount;
        betAmountTxt.text = _currentBetAmount.ToString();
        ;
    }
    #endregion

    #region Current multiplier setting

    public void SetMultiplier(bool isAdd)
    {
        if (isAdd)
        {
            float tempMultiplier = _currentMultiplier;
            tempMultiplier += 0.1f;
            if (tempMultiplier > 100)
            {
                tempMultiplier = 100;
            }

            _currentMultiplier = tempMultiplier;
            multiplierTxt.text = _currentMultiplier.ToString();

        }
        else
        {
            float tempMultiplier = _currentMultiplier;
            tempMultiplier -= 0.1f;
            if (tempMultiplier < 0)
                tempMultiplier = 0;
            _currentMultiplier = tempMultiplier;
            multiplierTxt.text = _currentMultiplier.ToString();
        }
    }

    public void OnValueChangeMultiplier()
    {
        float multiplier = 0;
        float.TryParse(multiplierTxt.text, out multiplier);
        if (multiplier > 100)
        {
            multiplier = 100f;
        }
        else if (multiplier < 0)
        {
            multiplier = 0;
        }
        _currentMultiplier = multiplier;
        multiplierTxt.text = _currentMultiplier.ToString();
        ;
    }

    #endregion
}
