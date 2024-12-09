using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LeaderBoardHandler : MonoBehaviour
{
    public GameObject leaderboardDetailPrefab;
    public Color[] itemClr;

    public List<GameObject> leaderboardItems;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(GetLeaderboardUsingAPI), 1f, 20f);
        //GetLeaderboardUsingAPI();
    }

    public void GetLeaderboardUsingAPI()
    {
        GetJson.instance.GetJsonFromServer(APIStrings.getLeaderBoardAPIURL, GetLeaderboardFromServer);
    }

    void GetLeaderboardFromServer(string jsonResponse, bool isSuccess)
    {
        if (isSuccess)
        {
            List<LeaderBoardDetailRootCls> users = JsonConvert.DeserializeObject<List<LeaderBoardDetailRootCls>>(jsonResponse);
            CreateUIOfLeaderboard(users);
        }
        else
        {

        }
    }

    void CreateUIOfLeaderboard(List<LeaderBoardDetailRootCls> users)
    {
        leaderboardItems = GamePlayHandler.instance.DestroyAndClearList(leaderboardItems);
        for (int i = 0; i < users.Count; i++)
        {
            GameObject item = Instantiate(leaderboardDetailPrefab);
            leaderboardItems.Add(item);
            LocalSettings.SetPosAndRect(item, leaderboardDetailPrefab.GetComponent<RectTransform>(), leaderboardDetailPrefab.transform.parent);
            item.SetActive(true);
            item.GetComponent<LeaderboardItemDetail>().FillFieldsLeaderBoard(users[i], i % 2 == 0 ? itemClr[0] : itemClr[1]);
        }
    }
}
public class LeaderBoardDetailRootCls
{
    public int rank { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string wallet_number { get; set; }
    public string wallet_balance { get; set; }
}

