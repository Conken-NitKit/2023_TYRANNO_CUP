using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayFabController : MonoBehaviour
{
    const string STATISTICS_NAME = "TimeAttack";

    //bool getted = false;
    string id = "";

    //static public PlayFabUserData s_myUserData { private set; get; } = null;
    /*
    static public void SetUserData(string _id = null, string _name = null, int _rankingValue = -1)
    {
        if (null == s_myUserData)
        {
            s_myUserData = new PlayFabUserData();
        }

        if (null != _id)
        {
            s_myUserData.id = _id;
        }
        if (null != _name)
        {
            s_myUserData.name = _name;
        }
        if (-1 != _rankingValue)
        {
            s_myUserData.rankingValue = _rankingValue;
        }
    }
    */

    void Start()
    {
        //StartCoroutine(UpdateRanking("b", 200));
        Debug.Log(id);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(UpdateRanking("b", 200));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(UpdateRanking("aaa", 100));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RequestLeaderBoard();
        }
        
    }

    void RequestLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = STATISTICS_NAME,
                StartPosition = 0,
                MaxResultsCount = 10
            },
            result =>
            {
                result.Leaderboard.ForEach(
                    x => Debug.Log(string.Format("{0}位:{1} スコア{2}", x.Position + 1, x.DisplayName, x.StatValue))
                    );
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }

    public IEnumerator UpdateRanking(string name, int time)
    {
        //GameCommonManager.GetInstance().m_loadingMarkManager.ActiveBlueLoadingMark();

        // コールバック受け取り待ちフラグ
        //getted = false;

        id = GameUtility.GenerateCustomID();
        Debug.Log(id);
        //isNewID = true;

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest 
            {
                CustomId = id,
                TitleId = STATISTICS_NAME,
                CreateAccount = true
            }
            , result => 
            {
                Debug.Log("ログイン成功 id=" + id);
                //getted = true;
            }
            , error => 
            {
                Debug.Log(error.GenerateErrorReport());
                //getted = true;
            });

        yield return new WaitForSeconds(1f);

        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = STATISTICS_NAME,
                        Value = time
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信");
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );

        //GameCommonManager.GetInstance().m_loadingMarkManager.DeactiveBlueLoadingMark();

        //yield break;
    }
}

public class GameUtility
{
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";

    //IDを生成する
    static public string GenerateCustomID()
    {
        int idLength = 32;
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();

        //ランダムにIDを生成
        for (int i = 0; i < idLength; i++)
        {
            stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
        }

        return stringBuilder.ToString();
    }
}
