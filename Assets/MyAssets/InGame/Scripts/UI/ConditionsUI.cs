using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tyranno.GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.UI
{
    public class ConditionsUI : MonoBehaviour
    {
        [SerializeField] 
        private PuzzleManager _puzzleManager;
        
        [SerializeField]
        private GameSetting _gameSetting;
        
        [SerializeField]
        private ConditionsDescription _conditionsDescription;

        private string _description;

        [SerializeField]
        private Text _descriptionText;
        

        public void Initialize()
        {
            for (int i = 0; i < _puzzleManager.ConditionNum; i++)
            {
                _description += $"\n{_conditionsDescription.DescriptionTexts[_puzzleManager.ConditionsOrder[i]]}\n";
            }

            _descriptionText.text = _description;
        }

        public void OnClicked()
        {
            StartCoroutine(DisplayCoroutine());
        }

        IEnumerator DisplayCoroutine()
        {
            _description = "";
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < _puzzleManager.ConditionNum; i++)
            {
                if (!_puzzleManager.JudgmentConditions[i])
                {
                    _description += $"\n<color=#000000>{_conditionsDescription.DescriptionTexts[_puzzleManager.ConditionsOrder[i]]}</color>\n";
                }
                else
                {
                    _description += $"\n<color=#4db56a>{_conditionsDescription.DescriptionTexts[_puzzleManager.ConditionsOrder[i]]}</color>\n";
                }
            }

            _descriptionText.text = _description;
        }
    }
}