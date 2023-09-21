using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Tyranno.GameManagers
{
    public enum GameState
    {
        Init,
        Ready,
        Game,
        Result
    }
    
    [Serializable]
    public class GameStateReactiveProperty : ReactiveProperty<GameState>
    {
        public GameStateReactiveProperty()
        {
        }

        public GameStateReactiveProperty(GameState initialValue)
            : base(initialValue)
        {
        }
    }
}