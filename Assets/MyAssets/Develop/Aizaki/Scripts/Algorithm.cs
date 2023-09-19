using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tyranno
{
    namespace Puzzle
    {
        namespace Algorithms
        {
            /// <summary>
            /// 拡張メソッドにしたかったけど無理でした
            /// </summary>
            public class SquareUtils
            {
                public bool[,] states;

                SquareUtils(bool[,] states)
                {
                    this.states = states;
                }

                /// <summary>
                /// 行を取得します。
                /// </summary>
                bool[] GetRow(int index)
                {
                    var row = new bool[states.GetLength(0)];
                    for (int i = 0; i < states.GetLength(0); i++)
                    {
                        row[i] = states[index, i];
                    }
                    return row;
                }

                /// <summary>
                /// 列を取得します。
                /// </summary>
                bool[] GetColumn(int index)
                {
                    var column = new bool[states.GetLength(1)];
                    for (int i = 0; i < states.GetLength(1); i++)
                    {
                        column[i] = states[i, index];
                    }
                    return column;
                }

                /// <summary>
                /// start1からend1の範囲とstart2からend2の範囲が繋がっていることを確認します。
                /// 斜めも繋がっていると判定します。
                /// 指定した座標が範囲外の場合ArgumentOutOfRangeExceptionを発生させます。
                /// </summary>
                [Obsolete("このメソッドは未完成です", error: true)]
                bool CheckConnect((int x,int y) start1, (int x, int y) end1, (int x, int y) start2, (int x, int y) end2)
                {
                    //すべての座標が配列の範囲内にあることを確認する
                    if (start1.x >= states.GetLength(0) || end1.x >= states.GetLength(0) || start2.x >= states.GetLength(0) || end2.x >= states.GetLength(0)
                        || start1.y >= states.GetLength(1) || end1.y >= states.GetLength(1) || start2.y >= states.GetLength(1) || end2.y >= states.GetLength(1)
                        || start1.x < 0 || end1.x < 0 || start2.x < 0 || end2.x < 0 || start1.y < 0 || end1.y < 0 || start2.y < 0 || end2.y < 0) 
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    //startの座標がendの座標より大きい場合入れ替える
                    if (start1.x > end1.x)
                    {
                        (end1.x, start1.x) = (start1.x, end1.x);
                    }
                    if (start2.x > end2.x)
                    {
                        (end2.x, start2.x) = (start2.x, end2.x);
                    }
                    if (start1.y > end1.y)
                    {
                        (end1.y, start1.y) = (start1.y, end1.y);
                    }
                    if (start2.y > end2.y)
                    {
                        (end2.y, start2.y) = (start2.y, end2.y);
                    }

                    var statesTmp = states;

                    //startからendの範囲をtrueにする
                    for (int i = start1.x; i <= end1.x; i++)
                    {
                        for (int j = start1.y; j <= end1.y; j++)
                        {
                            statesTmp[i, j] = true;
                        }
                    }
                    for (int i = start2.x; i <= end2.x; i++)
                    {
                        for (int j = start2.y; j <= end2.y; j++)
                        {
                            statesTmp[i, j] = true;
                        }
                    }

                    



                    return false;
                }
            }

            public class ConditionProfiles
            {
                /*
                //static readonly Func<bool[,], bool> LeftToRightMaze = states =>
                //{
                //    List<(int x,int y)> routeTmp = new();

                //    List<(bool,(int x,int y))> Around(bool[,] states, int x, int y)
                //    {
                //        List<(bool, (int x, int y))> values = new();
                //        if (x - 1 >= 0 && y - 1 >= 0)
                //        {
                //            values.Add((states[x - 1, y - 1], (x - 1, y - 1)));
                //        }
                //        if (x + 1 < states.GetLength(0) && y - 1 >= 0)
                //        {
                //            values.Add((states[x + 1, y - 1], (x + 1, y - 1)));
                //        }
                //        if (x - 1 >= 0 && y + 1 < states.GetLength(1))
                //        {
                //            values.Add((states[x - 1, y + 1], (x - 1, y + 1)));
                //        }
                //        if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1))
                //        {
                //            values.Add((states[x + 1, y + 1], (x + 1, y + 1)));
                //        }
                //        if (x - 1 >= 0)
                //        {
                //            values.Add((states[x - 1, y], (x - 1, y)));
                //        }
                //        if (x + 1 < states.GetLength(0))
                //        {
                //            values.Add((states[x + 1, y], (x + 1, y)));
                //        }
                //        if (y - 1 >= 0)
                //        {
                //            values.Add((states[x, y - 1], (x, y - 1)));
                //        }
                //        if (y + 1 < states.GetLength(1))
                //        {
                //            values.Add((states[x, y + 1], (x, y + 1)));
                //        }
                //        return values;
                //    }

                //    (List<(bool, (int x, int y))>,int) Get3(bool[,] states, int x, int y,int orientation)
                //    {
                //        List<(bool, (int x, int y))> values = new();
                //        int count = 0;
                //        if (orientation >= 0)
                //        {
                //            if (x + 1 < states.GetLength(0) && y - 1 >= 0)
                //            {
                //                values.Add((states[x + 1, y - 1], (x + 1, y - 1))); count++;
                //            }
                //            if (x + 1 < states.GetLength(0))
                //            {
                //                values.Add((states[x + 1, y], (x + 1, y))); count++;
                //            }
                //            if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1))
                //            {
                //                values.Add((states[x + 1, y + 1], (x + 1, y + 1))); count++;
                //            }
                //        }
                //        else
                //        {
                //            if (x - 1 >= 0 && y - 1 >= 0)
                //            {
                //                values.Add((states[x - 1, y - 1], (x - 1, y - 1))); count++;
                //            }
                //            if (x - 1 >= 0)
                //            {
                //                values.Add((states[x - 1, y], (x - 1, y))); count++;
                //            }
                //            if (x - 1 >= 0 && y + 1 < states.GetLength(1))
                //            {
                //                values.Add((states[x - 1, y + 1], (x - 1, y + 1))); count++;
                //            }
                //        }
                //        return (values, count);
                //    }

                //    (List<(bool, (int x, int y))>,int) Get2(bool[,] states, int x, int y)
                //    {
                //        List<(bool, (int x, int y))> values = new();
                //        int count = 0;
                //        if (y - 1 >= 0)
                //        {
                //            values.Add((states[x, y - 1], (x, y - 1))); count++;
                //        }
                //        if (y + 1 < states.GetLength(1))
                //        {
                //            values.Add((states[x, y + 1], (x, y + 1))); count++;
                //        }
                //        return (values, count);
                //    }

                //    for (int i = 0; i < states.GetLength(1); i++)
                //    {
                //        if (!states[0,i])
                //        {
                //            continue;
                //        }

                //        var orientation = 1;
                //        var statesClone = states;
                //        var branchStacks = new Stack<((int x, int y), int count, bool[,] statesLog)>();
                //        (int x, int y) currentLocation = (0, i);

                //        for (int j = 0; j <= states.Length / 2 + 1; j++)
                //        {
                //            (List<(bool, (int x, int y) location)> data, int count) = Get3(statesClone, currentLocation.x, currentLocation.y, orientation);
                //            if (data.Count == 0)
                //            {
                //                orientation *= -1;
                //                if (currentLocation.x + 1 == states.GetLength(0))
                //                {
                //                    return true;
                //                }
                //                continue;
                //            }
                //            else if (data.Count == 1)
                //            {
                //                statesClone[currentLocation.x,currentLocation.y] = false;
                //                currentLocation = data[0].location;
                //            }
                //            else if (data.Count >= 2)
                //            {
                //                branchStacks.Push((currentLocation,)
                //            }

                //        }

                //    }

                //    return false;
                //};
                */
                static readonly Func<bool[,], bool> LeftToRightMaze = states =>
                {
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
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            count++;
                        }
                        if (x + 1 < states.GetLength(0) && y - 1 >= 0)
                        {
                            count++;
                        }
                        if (x - 1 >= 0 && y + 1 < states.GetLength(1))
                        {
                            count++;
                        }
                        if (x + 1 < states.GetLength(0) && y + 1 < states.GetLength(1))
                        {
                            count++;
                        }
                        if (x - 1 >= 0)
                        {
                            count++;
                        }
                        if (x + 1 < states.GetLength(0))
                        {
                            count++;
                        }
                        if (y - 1 >= 0)
                        {
                            count++;
                        }
                        if (y + 1 < states.GetLength(1))
                        {
                            count++;
                        }
                        return count;
                    }

                    (int x,int y) GetBranchByCount(bool[,] states, int x, int y, int count)
                    {
                        var around = Around(states, x, y);
                        if (count + 1 > around.Count || count < 0) throw new ArgumentException("存在するブランチの数に対応しないインデックスを指定しています");
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

                        for (int j = 0; j <= states.Length + 1; j++)
                        {
                            if (currentLocation.x + 1 == states.GetLength(0))
                            {
                                return true;
                            }
                            var around = Around(statesClone, currentLocation.x, currentLocation.y);
                            var aroundCount = AroundCount(statesClone, currentLocation.x, currentLocation.y);
                            if (aroundCount == 0)
                            {
                                if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) Out)
                                {
                                    currentLocation = Out.location;
                                    statesClone = Out.statesLog;
                                    var branchloc = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, Out.count);
                                    statesClone[branchloc.x, branchloc.y] = false;
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
                                currentLocation = around[0].Item2;
                            }
                            else if (aroundCount >= 2)
                            {
                                statesClone[currentLocation.x, currentLocation.y] = false;
                                var countTmp = branchStacks.Peek().location == currentLocation ? branchStacks.Peek().count + 1 : 0;
                                branchStacks.Push((currentLocation, countTmp, statesClone));
                                var branchloc = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, countTmp);
                                currentLocation = around[countTmp].Item2;
                            }
                        }

                    }

                    return false;
                };
                static readonly Func<bool[,], bool> UpperToBottomMaze = states => false;
                static readonly Func<bool[,], bool> SingleColoredWall = states => false;
                static readonly Func<bool[,], bool> Symmetry = states => false;
                static readonly Func<bool[,], bool> PointSymmetry = states => false;
            }
        }
    }
}