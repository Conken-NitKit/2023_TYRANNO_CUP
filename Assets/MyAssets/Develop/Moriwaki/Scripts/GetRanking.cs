using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

namespace TyrannoCup.Ranking
{
    public class GetRanking : MonoBehaviour
    {
        [SerializeField]
        private Text[] UserNameTexts;

        [SerializeField]
        private Text[] UserScoreTexts;

        void Start()
        {
            IndicationRanking();
        }

        public void IndicationRanking()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = "loginID",
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request,
                (result) =>
                    {   
                        // アカウント作成完了
                        Debug.Log("Create Account Success!!");

                        GetLeaderboard();
                    },
                    (error) =>
                    {
                        Debug.LogError("Create Account Failed...");
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
                    UserNameTexts[item.Position].text = item.DisplayName;
                    UserScoreTexts[item.Position].text = secondToMinute(item.StatValue * -1);

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

        public string secondToMinute(int time)
        {
            int TimeOfMinute = (int)Mathf.Floor(time / 60f);
            int TimeOfSecond = (int)Mathf.Floor(time % 60f);
            return TimeOfMinute.ToString().PadLeft(2, '0') + "." + TimeOfSecond.ToString().PadLeft(2, '0');
        }
    }
}
