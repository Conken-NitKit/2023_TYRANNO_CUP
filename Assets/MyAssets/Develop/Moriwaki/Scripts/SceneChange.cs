using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public SceneFadeManager sceneFadeManager;

    public void OnClickSceneChange()
    {
        var scene = SceneManager.GetActiveScene ();
        if (scene.name == "Moriwaki") sceneFadeManager.fadeOutStart(0, 0, 0, 0, "RankingTest");
        else sceneFadeManager.fadeOutStart(0, 0, 0, 0, "Moriwaki");
    }
}
