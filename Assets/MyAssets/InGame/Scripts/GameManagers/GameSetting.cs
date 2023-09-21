using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/GameSetting")]
public class GameSetting : ScriptableObject
{
    public int WaveNum;
    public int StartConditionsNum;
    public int MaxConditionsNum;
    public bool IsTimeAttack;
    public int Score;
    public string Comment;
}
