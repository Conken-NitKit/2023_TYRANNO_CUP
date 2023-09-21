using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tyranno.Common
{
    public enum SceneName
    {
        Title,
        Easy,
        Normal,
        Hard,
        HardCore,
        TimeAttack,
        Ranking
    }
    
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] 
        private int _waitSeconds;

        [SerializeField]
        private SceneName _sceneName;

        public void OnClicked()
        {
            StartCoroutine(SceneLoadCoroutine());
        }

        IEnumerator SceneLoadCoroutine()
        {
            yield return new WaitForSeconds(_waitSeconds);
            
            SceneManager.LoadScene(_sceneName.ToString());
        }
    }
}