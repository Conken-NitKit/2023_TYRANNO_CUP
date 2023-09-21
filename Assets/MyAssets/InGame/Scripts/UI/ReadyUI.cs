using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManager;
using Tyranno.GameManagers;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Tyranno.UI
{
    public class ReadyUI : MonoBehaviour
    {
        [SerializeField]
        private TimeManager _timeManager;

        [SerializeField] 
        private MainGameManager _mainGameManager;

        [SerializeField]
        private Text _readyCount;

        [SerializeField]
        private GameObject _resultUIs;
        
        void Start()
        {
            _timeManager.ReadySecond.Subscribe(x =>
            {
                if (x <= 0)
                {
                    _readyCount.text = "";
                }

                _readyCount.text = $"{x}";

                if (x == 0)
                {
                    _readyCount.text = "GO!";
                }
            });

            _mainGameManager.CurrentGameState.Where(x => x == GameState.Game).Subscribe(_ =>
            {
                _resultUIs.SetActive(false);
            });
        }
    }
}
