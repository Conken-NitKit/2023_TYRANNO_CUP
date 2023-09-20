using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/GameSetting")]
public class GameSetting : ScriptableObject
{
    public int WaveNum;
    public int StartConditionsNum;
    public bool isTimeAttack;
}
