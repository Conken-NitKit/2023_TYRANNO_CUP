using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
//using System;
using UnityEngine.UI;
//using System.Text;

public class Ranking_Manager : MonoBehaviour
{
    public InputField inputFieldName;
    public InputField inputFieldScore;

    public void OnClick()
    {
        UpdateRanking(inputFieldName.text, int.Parse(inputFieldScore.text));
    }

    public void UpdateRanking(string userName, int score)
    {
        //GameUtility gameUtility = new GameUtility();

        var request = new LoginWithCustomIDRequest
        {
            CustomId = CreateNewPlayerId(),
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            (result) =>
                {
                        /*
                        // 既に作成済みだった場合
                        if (!result.NewlyCreated)
                        {
                            Debug.LogWarning("already account");

                            // 再度IDを取得し直してログイン
                            var newId = CreateNewPlayerId();
                            
                            return;
                        }
                        */
            
                        // アカウント作成完了
                    Debug.Log("Create Account Success!!");

                    SetUserName(userName, score);
                },
                (error) =>
                {
                    Debug.LogError("Create Account Failed...");
                    Debug.LogError(error.GenerateErrorReport());
                });
    }

    public void SetUserName(string userName, int score)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest { DisplayName = userName },
            (result) =>
            {
                Debug.Log("Save Display Name Success!!");

                SendUserScore(score);
            },
            (error) =>
            {
                Debug.LogError("Save Display Name Failed...");
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    public void SendUserScore(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "TimeAttack",
                        Value = score * -1 // スコア
                    }
                }
            },
            (result) =>
            {
                // スコア送信完了
                Debug.Log("Send Ranking Score Success!!");

                GetLeaderboard();
            },
            (error) =>
            {
                    Debug.LogError("Send Ranking Score Failed...");
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    public void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "TimeAttack"
        }, result =>
        {
            foreach (var item in result.Leaderboard)
            {
                Debug.Log($"{item.Position + 1}位:{item.DisplayName} " + $"スコア {item.StatValue * -1}");
                if(item.Position >= 10)
                {
                    break;
                }
            }
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    private string CreateNewPlayerId()
    {
        return System.Guid.NewGuid().ToString("N");
    }

    /*
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";
    //IDを生成する
    public string GenerateCustomID()
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
    */
}

