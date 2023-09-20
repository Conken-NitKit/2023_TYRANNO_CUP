using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniRx;

namespace Tyranno.GameManagers
{
    public interface IGameStateProvider
    {
        IReadOnlyReactiveProperty<GameState> CurrentGameState { get; }
    }
}