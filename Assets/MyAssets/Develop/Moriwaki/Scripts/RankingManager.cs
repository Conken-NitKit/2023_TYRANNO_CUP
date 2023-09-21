using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

namespace TyrannoCup.Ranking
{
    public class RankingManager : MonoBehaviour
    {
        [SerializeField]
        private InputField inputFieldName;

        [SerializeField]
        private InputField inputFieldScore;

        public void OnClickUpdateRanking()
        {
            UpdateRanking(inputFieldName.text, int.Parse(inputFieldScore.text));
        }

        public void UpdateRanking(string userName, int score)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = CreateNewPlayerId(),
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request,
                (result) =>
                    {
                            // 既に作成済みだった場合
                            if (!result.NewlyCreated)
                            {
                                Debug.LogWarning("already account");

                                // 再度ログイン
                                UpdateRanking(userName, score);
                                
                                return;
                            }
                
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
                },
                (error) =>
                {
                        Debug.LogError("Send Ranking Score Failed...");
                    Debug.LogError(error.GenerateErrorReport());
                });
        }

        private string CreateNewPlayerId()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
