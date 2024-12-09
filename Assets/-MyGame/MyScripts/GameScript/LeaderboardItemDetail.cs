using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItemDetail : MonoBehaviour
{
    [SerializeField] Text _rankTxt;
    [SerializeField] Text _userNameTxt;
    [SerializeField] Text _walletAmountTxt;

    int _rank;
    string _userName;
    string _walletAmount;
    public void FillFieldsLeaderBoard(LeaderBoardDetailRootCls ldrc, Color bgClr)
    {
        GetComponent<Image>().color = bgClr;
        _rank = ldrc.rank;
        _userName = ldrc.username;
        _walletAmount = ldrc.wallet_balance;


        _rankTxt.text = _rank.ToString();
        _userNameTxt.text = _userName.ToString();
        _walletAmountTxt.text = _walletAmount.ToString();
    }
}
