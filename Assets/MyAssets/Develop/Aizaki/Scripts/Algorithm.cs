using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tyranno.Puzzle.Algorithms 
{
    public class ConditionProfiles
    {
        public static readonly Func<bool[,], bool> LeftToRightMaze = originalStates =>
        {
            bool[,] states = new bool[originalStates.GetLength(1), originalStates.GetLength(0)];
            for (int i = 0; i < originalStates.GetLength(0); i++)
            {
                for (int j = 0; j < originalStates.GetLength(1); j++)
                {
                    states[i, j] = originalStates[j, i];
                }
            }

            List<(bool, (int x, int y))> Around(bool[,] states, int x, int y)
            {
                List<(bool, (int x, int y))> values = new();
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    values.Add((states[x - 1, y - 1], (x - 1, y - 1)));
                }
                if (x + 1 < states.GetLength(0) && y - 1 >= 0)
                {
                    values.Add((states[x + 1, y - 1], (x + 1, y - 1)));
                }
                if (x - 1 >= 0 && y + 1 < states.GetLength(1))
                {
                    values.Add((states[x - 1, y + 1], (x - 1, y + 1)));
                }
                if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1))
                {
                    values.Add((states[x + 1, y + 1], (x + 1, y + 1)));
                }
                if (x - 1 >= 0)
                {
                    values.Add((states[x - 1, y], (x - 1, y)));
                }
                if (x + 1 < states.GetLength(0))
                {
                    values.Add((states[x + 1, y], (x + 1, y)));
                }
                if (y - 1 >= 0)
                {
                    values.Add((states[x, y - 1], (x, y - 1)));
                }
                if (y + 1 < states.GetLength(1))
                {
                    values.Add((states[x, y + 1], (x, y + 1)));
                }
                return values;
            }

            int AroundCount(bool[,] states, int x, int y)
            {
                int count = 0;
                if (x - 1 >= 0 && y - 1 >= 0 && states[x - 1, y - 1])
                {
                    count++;
                }
                if (x + 1 < states.GetLength(0) && y - 1 >= 0 && states[x + 1, y - 1])
                {
                    count++;
                }
                if (x - 1 >= 0 && y + 1 < states.GetLength(1) && states[x - 1, y + 1])
                {
                    count++;
                }
                if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1) && states[x + 1, y + 1])
                {
                    count++;
                }
                if (x - 1 >= 0 && states[x - 1, y])
                {
                    count++;
                }
                if (x + 1 < states.GetLength(0) && states[x + 1, y])
                {
                    count++;
                }
                if (y - 1 >= 0 && states[x, y - 1])
                {
                    count++;
                }
                if (y + 1 < states.GetLength(1) && states[x, y + 1])
                {
                    count++;
                }
                return count;
            }

            (int x, int y) GetBranchByCount(bool[,] states, int x, int y, int count)
            {
                var around = Around(states, x, y);
                if (count + 1 > around.Count || count < 0) throw new ArgumentException("存在するブランチの数に対応しないインデックスを指定しています");
                around.RemoveAll(x => !x.Item1);
                return around[count].Item2;
            }

            for (int i = 0; i < states.GetLength(1); i++)
            {

                if (!states[0, i])
                {
                    continue;
                }

                var statesClone = states;
                var branchStacks = new Stack<((int x, int y) location, int count, bool[,] statesLog)>();
                (int x, int y) currentLocation = (0, i);
                (int x, int y) lastBranchLocation = (0, 0);

                for (int j = 0; j <= states.Length + 1; j++)
                {
                    if (currentLocation.x + 1 == states.GetLength(0))
                    {
                        return true;
                    }

                    var aroundCount = AroundCount(statesClone, currentLocation.x, currentLocation.y);
                    if (aroundCount == 0)
                    {
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) Out))
                        {
                            currentLocation = Out.location;
                            statesClone = Out.statesLog;
                            statesClone[lastBranchLocation.x,lastBranchLocation.y] = false;
                            branchStacks.Pop();
                            continue;
                        }
                        else
                        {
                            break;
                        }

                    }
                    else if (aroundCount == 1)
                    {
                        statesClone[currentLocation.x, currentLocation.y] = false;
                        currentLocation = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, 0);
                    }
                    else if (aroundCount >= 2)
                    {
                        statesClone[currentLocation.x, currentLocation.y] = false;
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) Out))
                        {
                            var countTmp = Out.location == currentLocation ? Out.count + 1 : 0;
                            branchStacks.Push((currentLocation, countTmp, statesClone));
                            var branchloc = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, countTmp);
                            currentLocation = branchloc;
                            lastBranchLocation = branchloc;
                        }
                        else
                        {
                            branchStacks.Push((currentLocation, 0, statesClone));
                        }

                    }
                }

            }

            return false;
        };
        public static readonly Func<bool[,], bool> UpperToBottomMaze = originalStates =>
        {
            bool[,] states = new bool[originalStates.GetLength(1), originalStates.GetLength(0)];
            for (int i = 0; i < originalStates.GetLength(0); i++)
            {
                for (int j = 0; j < originalStates.GetLength(1); j++)
                {
                    states[i, j] = originalStates[j, i];
                }
            }

            List<(bool, (int x, int y))> Around(bool[,] states, int x, int y)
            {
                List<(bool, (int x, int y))> values = new();
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    values.Add((states[x - 1, y - 1], (x - 1, y - 1)));
                }
                if (x + 1 < states.GetLength(0) && y - 1 >= 0)
                {
                    values.Add((states[x + 1, y - 1], (x + 1, y - 1)));
                }
                if (x - 1 >= 0 && y + 1 < states.GetLength(1))
                {
                    values.Add((states[x - 1, y + 1], (x - 1, y + 1)));
                }
                if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1))
                {
                    values.Add((states[x + 1, y + 1], (x + 1, y + 1)));
                }
                if (x - 1 >= 0)
                {
                    values.Add((states[x - 1, y], (x - 1, y)));
                }
                if (x + 1 < states.GetLength(0))
                {
                    values.Add((states[x + 1, y], (x + 1, y)));
                }
                if (y - 1 >= 0)
                {
                    values.Add((states[x, y - 1], (x, y - 1)));
                }
                if (y + 1 < states.GetLength(1))
                {
                    values.Add((states[x, y + 1], (x, y + 1)));
                }
                return values;
            }

            int AroundCount(bool[,] states, int x, int y)
            {
                int count = 0;
                if (x - 1 >= 0 && y - 1 >= 0 && states[x - 1, y - 1])
                {
                    count++;
                }
                if (x + 1 < states.GetLength(0) && y - 1 >= 0 && states[x + 1, y - 1])
                {
                    count++;
                }
                if (x - 1 >= 0 && y + 1 < states.GetLength(1) && states[x - 1, y + 1])
                {
                    count++;
                }
                if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1) && states[x + 1, y + 1])
                {
                    count++;
                }
                if (x - 1 >= 0 && states[x - 1, y])
                {
                    count++;
                }
                if (x + 1 < states.GetLength(0) && states[x + 1, y])
                {
                    count++;
                }
                if (y - 1 >= 0 && states[x, y - 1])
                {
                    count++;
                }
                if (y + 1 < states.GetLength(1) && states[x, y + 1])
                {
                    count++;
                }
                return count;
            }

            (int x, int y) GetBranchByCount(bool[,] states, int x, int y, int count)
            {
                var around = Around(states, x, y);
                if (count + 1 > around.Count || count < 0) throw new ArgumentException("存在するブランチの数に対応しないインデックスを指定しています");
                around.RemoveAll(x => !x.Item1);
                return around[count].Item2;
            }

            for (int i = 0; i < states.GetLength(1); i++)
            {

                if (!states[i, 0])
                {
                    continue;
                }

                var statesClone = states;
                var branchStacks = new Stack<((int x, int y) location, int count, bool[,] statesLog)>();
                (int x, int y) currentLocation = (i, 0);
                (int x, int y) lastBranchLocation = (0, 0);

                for (int j = 0; j <= states.Length + 1; j++)
                {
                    if (currentLocation.y + 1 == states.GetLength(1))
                    {
                        return true;
                    }

                    var aroundCount = AroundCount(statesClone, currentLocation.x, currentLocation.y);
                    if (aroundCount == 0)
                    {
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) Out))
                        {
                            currentLocation = Out.location;
                            statesClone = Out.statesLog;
                            statesClone[lastBranchLocation.x, lastBranchLocation.y] = false;
                            branchStacks.Pop();
                            continue;
                        }
                        else
                        {
                            break;
                        }

                    }
                    else if (aroundCount == 1)
                    {
                        statesClone[currentLocation.x, currentLocation.y] = false;
                        currentLocation = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, 0);
                    }
                    else if (aroundCount >= 2)
                    {
                        statesClone[currentLocation.x, currentLocation.y] = false;
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) Out))
                        {
                            var countTmp = Out.location == currentLocation ? Out.count + 1 : 0;
                            branchStacks.Push((currentLocation, countTmp, statesClone));
                            var branchloc = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, countTmp);
                            currentLocation = branchloc;
                            lastBranchLocation = branchloc;
                        }
                        else
                        {
                            branchStacks.Push((currentLocation, 0, statesClone));
                        }

                    }
                }

            }

            return false;
        };
        public static readonly Func<bool[,], bool> SingleColoredWall = states => false;
        public static readonly Func<bool[,], bool> Symmetry = states => false;
    }
}