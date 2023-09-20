using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.UI.Image;

namespace Tyranno.Puzzle.Algorithms
{

    public static class ConditionProfiles
    {
        /// <summary>
        /// 左辺から右辺までtrueのマスが連なっていることを判定します。
        /// 斜めも連なっていると判定します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsLeftToRightMaze = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);

            for (int i = 0; i < states.GetLength(1); i++)
            {

                if (!states[0, i])
                {
                    continue;
                }

                bool[,] statesClone = states;
                Stack<((int x, int y) location, int count, bool[,] statesLog)> branchStacks = new();
                (int x, int y) currentLocation = (0, i);
                (int x, int y) lastBranchLocation = (0, 0);

                for (int j = 0; j <= states.Length + 1; j++)
                {
                    if (currentLocation.x + 1 == states.GetLength(0))
                    {
                        return true;
                    }

                    int aroundCount = AroundCount(statesClone, currentLocation.x, currentLocation.y);
                    if (aroundCount == 0)
                    {
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                        {
                            currentLocation = branchOut.location;
                            statesClone = branchOut.statesLog;
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
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                        {
                            int countTmp = branchOut.location == currentLocation ? branchOut.count + 1 : 0;
                            branchStacks.Push((currentLocation, countTmp, statesClone));
                            (int x, int y) branchloc = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, countTmp);
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

        /// <summary>
        /// 上辺から下辺までtrueのマスが連なっていることを判定します。
        /// 斜めも連なっていると判定します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsUpperToBottomMaze = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);

            for (int i = 0; i < states.GetLength(1); i++)
            {

                if (!states[i, 0])
                {
                    continue;
                }

                bool[,] statesClone = states;
                Stack<((int x, int y) location, int count, bool[,] statesLog)> branchStacks = new();
                (int x, int y) currentLocation = (i, 0);
                (int x, int y) lastBranchLocation = (0, 0);

                for (int j = 0; j <= states.Length + 1; j++)
                {
                    if (currentLocation.y + 1 == states.GetLength(1))
                    {
                        return true;
                    }

                    int aroundCount = AroundCount(statesClone, currentLocation.x, currentLocation.y);
                    if (aroundCount == 0)
                    {
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                        {
                            currentLocation = branchOut.location;
                            statesClone = branchOut.statesLog;
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
                        if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                        {
                            int countTmp = branchOut.location == currentLocation ? branchOut.count + 1 : 0;
                            branchStacks.Push((currentLocation, countTmp, statesClone));
                            (int x, int y) branchloc = GetBranchByCount(statesClone, currentLocation.x, currentLocation.y, countTmp);
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

        /// <summary>
        /// 四辺にちょうど1つtrueのマスがあることを判定します。
        /// </summary>
        private static readonly Func<bool[,], bool> _IsSingleColoredWall = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);


            var counter = 0;

            //上辺
            for (int i = 0; i < states.GetLength(0); i++)
            {
                if (states[i, 0])
                {
                    counter++;
                    if (counter > 1)
                    {
                        return false;
                    }
                }
            }
            if (counter == 0) return false;
            counter = 0;
            //下辺
            for (int i = 0; i < states.GetLength(0); i++)
            {
                if (states[i, states.GetLength(1) - 1])
                {
                    counter++;
                    if (counter > 1)
                    {
                        return false;
                    }
                }
            }
            if (counter == 0) return false;
            counter = 0;
            //左辺
            for (int i = 0; i < states.GetLength(1); i++)
            {
                if (states[0, i])
                {
                    counter++;
                    if (counter > 1)
                    {
                        return false;
                    }
                }
            }
            if (counter == 0) return false;
            counter = 0;
            //右辺
            for (int i = 0; i < states.GetLength(1); i++)
            {
                if (states[states.GetLength(0) - 1, i])
                {
                    counter++;
                    if (counter > 1)
                    {
                        return false;
                    }
                }
            }
            if (counter == 0) return false;

            return true;
        };

        /// <summary>
        /// 四辺にちょうど2つtrueのマスがあることを判定します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsDoubleColoredWall = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);


            var counter = 0;

            //上辺
            for (int i = 0; i < states.GetLength(0); i++)
            {
                if (states[i, 0])
                {
                    counter++;
                }
            }
            if (counter != 2) return false;
            counter = 0;
            //下辺
            for (int i = 0; i < states.GetLength(0); i++)
            {
                if (states[i, states.GetLength(1) - 1])
                {
                    counter++;
                }
            }
            if (counter != 2) return false;
            counter = 0;
            //左辺
            for (int i = 0; i < states.GetLength(1); i++)
            {
                if (states[0, i])
                {
                    counter++;
                }
            }
            if (counter != 2) return false;
            counter = 0;
            //右辺
            for (int i = 0; i < states.GetLength(1); i++)
            {
                if (states[states.GetLength(0) - 1, i])
                {
                    counter++;
                }
            }
            if (counter != 2) return false;

            return true;
        };

        /// <summary>
        /// 図形が左右対称であるこか判定します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsSymmetry = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);
            bool[,] converted = new bool[states.GetLength(0), states.GetLength(1)];

            for (int i = 0; i < states.GetLength(0); i++)
            {
                for (int j = 0; j < states.GetLength(1); j++)
                {
                    converted[i, j] = states[states.GetLength(0) - i - 1, j];
                    if (converted[i, j] != states[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        };

        /// <summary>
        /// 図形が上下対称であるかを判定します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsVerticalSymmetry = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);
            bool[,] converted = new bool[states.GetLength(0), states.GetLength(1)];
            for (int i = 0; i < states.GetLength(0); i++)
            {
                for (int j = 0; j < states.GetLength(1); j++)
                {
                    converted[i, j] = states[i, states.GetLength(1) - j - 1];
                    if (converted[i, j] != states[i, j])
                    {
                        Debug.Log("false");
                        return false;
                    }
                }
            }
            Debug.Log("true");
            return true;
        };

        /// <summary>
        /// 図形が点対称であるかを判定します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsPointSymmetry = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);
            bool[,] converted = new bool[states.GetLength(0), states.GetLength(1)];
            for (int i = 0; i < states.GetLength(0); i++)
            {
                for (int j = 0; j < states.GetLength(1); j++)
                {
                    converted[i, j] = states[states.GetLength(0) - i - 1, states.GetLength(1) - j - 1];
                    if (converted[i, j] != states[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;

        };

        /// <summary>
        /// trueマスの個数が24以上の場合true、24未満の場合falseを返します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsQuantityLimit = originalStates =>
        {
            var limit = 24;
            return ConvertToLinear(originalStates).FindAll(x => x).Count >= limit;
        };

        /// <summary>
        /// trueマスが4つ以上固まっているとfalse、そうでなければtrueを返します。
        /// 斜めのつながりは固まっていると判定しません。
        /// </summary>
        public static readonly Func<bool[,], bool> IsTrueConnectionSizeVaild = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);
            HashSet<(int x, int y)> foundStates = new();
            int limit = 4;

            for (int i = 0; i < states.GetLength(1); i++)
            {
                for (int j = 0; j < states.GetLength(0); j++)
                {
                    foreach (var (x, y) in foundStates)
                    {
                        states[x, y] = false;
                    }

                    foundStates = new();

                    if (!states[j, i])
                    {
                        continue;
                    }

                    bool[,] statesClone = states;
                    Stack<((int x, int y) location, int count, bool[,] statesLog)> branchStacks = new();
                    (int x, int y) currentLocation = (j, i);
                    (int x, int y) lastBranchLocation = (0, 0);
                    foundStates.Add(currentLocation);

                    for (int k = 0; k <= states.Length + 1; j++)
                    {

                        int aroundCount = AroundCount4(statesClone, currentLocation.x, currentLocation.y);
                        if (aroundCount == 0)
                        {
                            if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                            {
                                currentLocation = branchOut.location;
                                statesClone = branchOut.statesLog;
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
                            currentLocation = GetBranchByCount4(statesClone, currentLocation.x, currentLocation.y, 0);
                            foundStates.Add(currentLocation);
                            if (foundStates.Count >= limit)
                            {
                                //Debug.Log(foundStates.Select(x => $"{x.x},{x.y}").Aggregate((x, y) => $"{x},{y}"));
                                return false;
                            }
                        }
                        else if (aroundCount >= 2)
                        {
                            statesClone[currentLocation.x, currentLocation.y] = false;
                            if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                            {
                                int countTmp = branchOut.location == currentLocation ? branchOut.count + 1 : 0;
                                branchStacks.Push((currentLocation, countTmp, statesClone));
                                (int x, int y) branchloc = GetBranchByCount4(statesClone, currentLocation.x, currentLocation.y, countTmp);
                                currentLocation = branchloc;
                                lastBranchLocation = branchloc;

                                foundStates.Add(currentLocation);
                                if (foundStates.Count >= limit)
                                {
                                    //Debug.Log(foundStates.Select(x => $"{x.x},{x.y}").Aggregate((x, y) => $"{x},{y}"));
                                    return false;
                                }
                            }
                            else
                            {
                                branchStacks.Push((currentLocation, 0, statesClone));
                            }

                        }
                    }
                }
            }
            return true;
        };

        /// <summary>
        /// falseマスが12つ以上固まっているとtrue、そうでなければfalseを返します。
        /// 斜めのつながりは固まっていると判定しません。
        /// </summary>
        public static readonly Func<bool[,], bool> IsFalseConnectionSizeVaild = originalStates =>
        {
            int limit = 12;

            bool[,] states = ConvertAxis(originalStates);
            for (int i = 0; i < states.GetLength(1); i++)
            {
                for (int j = 0; j < states.GetLength(0); j++)
                {
                    states[j, i] = !states[j, i];
                }
            }

            HashSet<(int x, int y)> foundStates = new();
            

            for (int i = 0; i < states.GetLength(1); i++)
            {
                for (int j = 0; j < states.GetLength(0); j++)
                {
                    foreach (var (x, y) in foundStates)
                    {
                        states[x, y] = false;
                    }

                    foundStates = new();

                    if (!states[j, i])
                    {
                        continue;
                    }

                    bool[,] statesClone = states;
                    Stack<((int x, int y) location, int count, bool[,] statesLog)> branchStacks = new();
                    (int x, int y) currentLocation = (j, i);
                    (int x, int y) lastBranchLocation = (0, 0);
                    foundStates.Add(currentLocation);

                    for (int k = 0; k <= states.Length + 1; j++)
                    {

                        int aroundCount = AroundCount4(statesClone, currentLocation.x, currentLocation.y);
                        if (aroundCount == 0)
                        {
                            if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                            {
                                currentLocation = branchOut.location;
                                statesClone = branchOut.statesLog;
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
                            currentLocation = GetBranchByCount4(statesClone, currentLocation.x, currentLocation.y, 0);
                            foundStates.Add(currentLocation);
                            if (foundStates.Count >= limit)
                            {
                                //Debug.Log(foundStates.Select(x => $"{x.x},{x.y}").Aggregate((x, y) => $"{x},{y}"));
                                return true;
                            }
                        }
                        else if (aroundCount >= 2)
                        {
                            statesClone[currentLocation.x, currentLocation.y] = false;
                            if (branchStacks.TryPeek(out ((int x, int y) location, int count, bool[,] statesLog) branchOut))
                            {
                                int countTmp = branchOut.location == currentLocation ? branchOut.count + 1 : 0;
                                branchStacks.Push((currentLocation, countTmp, statesClone));
                                (int x, int y) branchloc = GetBranchByCount4(statesClone, currentLocation.x, currentLocation.y, countTmp);
                                currentLocation = branchloc;
                                lastBranchLocation = branchloc;

                                foundStates.Add(currentLocation);
                                if (foundStates.Count >= limit)
                                {
                                    //Debug.Log(foundStates.Select(x => $"{x.x},{x.y}").Aggregate((x, y) => $"{x},{y}"));
                                    return true;
                                }
                            }
                            else
                            {
                                branchStacks.Push((currentLocation, 0, statesClone));
                            }

                        }
                    }
                }
            }
            return false;
        };

        /// <summary>
        /// 1行または1列に5個以上のtrueのマスが存在する場合false、そうでなければtrueを返します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsTrueCountInRowOrColumnValid = originalStates =>
        {
            int limit = 5;
            bool[,] states = ConvertAxis(originalStates);

            int count = 0;

            //行
            for (int i = 0; i < states.GetLength(1); i++)
            {
                for (int j = 0; j < states.GetLength(0); j++)
                {
                    if (states[j, i])
                    {
                        count++;
                        if (count >= limit)
                        {
                            return false;
                        }
                    }
                }
                count = 0;
            }
            //列
            for (int i = 0; i < states.GetLength(0); i++)
            {
                for (int j = 0; j < states.GetLength(1); j++)
                {
                    if (states[i, j])
                    {
                        count++;
                        if (count >= 5)
                        {
                            return false;
                        }
                    }
                }
                count = 0;
            }

            return true;
        };

        /// <summary>
        /// 周囲の8マスがすべてfalseであるtrueのマスが1つ以上存在する場合true、そうでなければfalseを返します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsSurroundedByFalse = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);

            for (int i = 0; i < states.GetLength(1); i++)
            {
                for (int j = 0; j < states.GetLength(0); j++)
                {
                    if (states[j, i] && AroundCount(states, j, i) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        };

        /// <summary>
        /// 縦または横に2つ以上繋がっているtrueマスが存在する場合true、そうでなければfalseを返します。
        /// </summary>
        public static readonly Func<bool[,], bool> IsHorizontallyOrVerticallyConnected = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);

            for (int i = 0; i < states.GetLength(1); i++)
            {
                for (int j = 0; j < states.GetLength(0); j++)
                {
                    if (states[j, i] && AroundCount4(states, j, i) >= 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        };





        /// <summary>
        /// 二次元配列を一次元配列に変換します。
        /// </summary>
        /// <param name="original">変換元の二次元配列</param>
        /// <returns>変換された配列</returns>
        private static List<bool> ConvertToLinear(bool[,] original)
        {
            List<bool> converted = new();
            foreach (var state in original)
            {
                converted.Add(state);
            }
            return converted;
        }

        /// <summary>
        /// 入力された多次元配列のx,y軸を反転させます。
        /// [0,0]からの距離は変化しません。
        /// </summary>
        /// <param name="original"></param>
        /// <returns>x,yが反転したbool[,]</returns>
        private static bool[,] ConvertAxis(bool[,] original)
        {
            bool[,] converted = new bool[original.GetLength(1), original.GetLength(0)];
            for (int i = 0; i < original.GetLength(0); i++)
            {
                for (int j = 0; j < original.GetLength(1); j++)
                {
                    converted[i, j] = original[j, i];
                }
            }
            return converted;
        }

        /// <summary>
        /// 指定した座標の周囲のマス(最大8)を返します。
        /// </summary>
        /// <param name="states">盤面の状態</param>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <returns>座標の値と位置をセットにしたタプル</returns>
        private static List<(bool, (int x, int y))> Around(bool[,] states, int x, int y)
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

        /// <summary>
        /// 指定した座標の上下左右のマス(最大4)を返します。
        /// </summary>
        /// <param name="states">盤面の状態</param>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <returns>座標の値と位置をセットにしたタプル</returns>
        private static List<(bool, (int x, int y))> Around4(bool[,] states, int x, int y)
        {
            List<(bool, (int x, int y))> values = new();
            
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

        /// <summary>
        /// 指定した座標の周囲のマスのうち、trueのマスの数を返します。
        /// </summary>
        /// <param name="states">盤面の状態</param>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <returns>周囲のtrueのマスの数</returns>
        private static int AroundCount(bool[,] states, int x, int y)
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

        /// <summary>
        /// 指定した座標の上下左右のマスのうち、trueのマスの数を返します。
        /// </summary>
        /// <param name="states">盤面の状態</param>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <returns>周囲のtrueのマスの数</returns>
        private static int AroundCount4(bool[,] states, int x, int y)
        {
            int count = 0;
            
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

        /// <summary>
        /// 指定した座標の周囲のtrueマスのうち、index番目のものの座標を返します。
        /// </summary>
        /// <param name="states">盤面の状態</param>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="index">取得する番号</param>
        /// <returns>index番目のtrueのマスの座標</returns>
        /// <exception cref="ArgumentException"></exception>
        private static (int x, int y) GetBranchByCount(bool[,] states, int x, int y, int index)
        {
            List<(bool, (int x, int y))> around = Around(states, x, y);
            around.RemoveAll(x => !x.Item1);
            if (index + 1 > around.Count || index < 0) throw new ArgumentException("存在するブランチの数に対応しないインデックスを指定しています");
            return around[index].Item2;
        }

        /// <summary>
        /// 指定した座標の上下左右のtrueマスのうち、index番目のものの座標を返します。
        /// </summary>
        /// <param name="states">盤面の状態</param>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="index">取得する番号</param>
        /// <returns>index番目のtrueのマスの座標</returns>
        /// <exception cref="ArgumentException"></exception>
        private static (int x, int y) GetBranchByCount4(bool[,] states, int x, int y, int index)
        {
            List<(bool, (int x, int y))> around = Around4(states, x, y);
            around.RemoveAll(x => !x.Item1);
            if (index + 1 > around.Count || index < 0) throw new ArgumentException("存在するブランチの数に対応しないインデックスを指定しています");
            return around[index].Item2;
        }

        //テスト用
        //Debug.Log($"{Tyranno.Puzzle.Algorithms.ConditionProfiles.LeftToRightMaze(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.UpperToBottomMaze(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.SingleColoredWall(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.Symmetry(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.PointSymmetry(_puzzleState.SquareArray)}");
    }
}