using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using System.Text;

public class Ranking_Manager : MonoBehaviour
{
    public InputField inputFieldName;
    public InputField inputFieldScore;


    public void OnClick()
    {
        StartCoroutine(UpdateRanking(inputFieldName.text, int.Parse(inputFieldScore.text)));
    }

    public IEnumerator UpdateRanking(string userName, int score)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = GameUtility.GenerateCustomID(),
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
                },
                (error) =>
                {
                    Debug.LogError("Create Account Failed...");
                    Debug.LogError(error.GenerateErrorReport());
                });

        yield return new WaitForSeconds(1f);
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest { DisplayName = userName},
            (result) =>
            {
                Debug.Log("Save Display Name Success!!");
            },
            (error) =>
            {
                Debug.LogError("Save Display Name Failed...");
                Debug.LogError(error.GenerateErrorReport());
            });
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "TimeAttack",
                        Value = score // スコア
                    }
                }
            },
            (result) =>
            {
                // スコア送信完了
                Debug.Log("Send Ranking Score Success!!");
            },
            (error) =>
            {
                    Debug.LogError("Send Ranking Score Failed...");
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    private string CreateNewPlayerId()
    {
        return Guid.NewGuid().ToString("N");
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
}
