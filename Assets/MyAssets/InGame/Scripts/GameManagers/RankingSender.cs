using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManager;
using TyrannoCup.Ranking;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.GameManager
{
    public class RankingSender : MonoBehaviour
    {
        [SerializeField] 
        private TimeManager _timeManager;

        [SerializeField] 
        private RankingManager _rankingManager;
        
        [SerializeField]
        private InputField _inputFieldName;

        public void OnClicked()
        {
            _rankingManager.UpdateRanking(_inputFieldName.text, _timeManager.GameSecond.Value);
        }
    }
}