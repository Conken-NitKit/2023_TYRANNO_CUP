using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.GameManager.UI
{
    public class ConditionsUI : MonoBehaviour
    {
        [SerializeField] 
        private PuzzleManager _puzzleManager;
        
        [SerializeField]
        private ConditionsDescription _conditionsDescription;

        private string _description;

        private Text _descriptionText;

        public void Initialize()
        {
            
        }

        public void OnClicked()
        {
            
        }
    }
}