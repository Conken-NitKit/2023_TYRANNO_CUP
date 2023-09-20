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
        /// ���ӂ���E�ӂ܂�true�̃}�X���A�Ȃ��Ă��邱�Ƃ𔻒肵�܂��B
        /// �΂߂��A�Ȃ��Ă���Ɣ��肵�܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> LeftToRightMaze = originalStates =>
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
        /// ��ӂ��牺�ӂ܂�true�̃}�X���A�Ȃ��Ă��邱�Ƃ𔻒肵�܂��B
        /// �΂߂��A�Ȃ��Ă���Ɣ��肵�܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> UpperToBottomMaze = originalStates =>
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
        /// �l�ӂɂ��ꂼ��ЂƂ���true�̃}�X�����邱�Ƃ𔻒肵�܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> SingleColoredWall = originalStates =>
        {
            bool[,] states = ConvertAxis(originalStates);


            var counter = 0;

            //���
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
            //����
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
            //����
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
            //�E��
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
        /// �}�`�����E�Ώ̂ł��邱�����肵�܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> Symmetry = originalStates =>
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
        /// �}�`���㉺�Ώ̂ł��邩�𔻒肵�܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> VerticalSymmetry = originalStates =>
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
        /// �}�`���_�Ώ̂ł��邩�𔻒肵�܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> PointSymmetry = originalStates =>
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
        /// true�}�X�̌���24�ȏ�̏ꍇtrue�A24�����̏ꍇfalse��Ԃ��܂��B
        /// </summary>
        public static readonly Func<bool[,], bool> QuantityLimit = originalStates =>
        {
            var limit = 24;
            return ConvertToLinear(originalStates).FindAll(x => x).Count >= limit;
        };

        /// <summary>
        /// �񎟌��z����ꎟ���z��ɕϊ����܂��B
        /// </summary>
        /// <param name="original">�ϊ����̓񎟌��z��</param>
        /// <returns>�ϊ����ꂽ�z��</returns>
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
        /// ���͂��ꂽ�������z���x,y���𔽓]�����܂��B
        /// [0,0]����̋����͕ω����܂���B
        /// </summary>
        /// <param name="original"></param>
        /// <returns>x,y�����]����bool[,]</returns>
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
        /// �w�肵�����W�̎��͂̃}�X(�ő�8)��Ԃ��܂��B
        /// </summary>
        /// <param name="states">�Ֆʂ̏��</param>
        /// <param name="x">x���W</param>
        /// <param name="y">y���W</param>
        /// <returns>���W�̒l�ƈʒu���Z�b�g�ɂ����^�v��</returns>
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
        /// �w�肵�����W�̎��͂̃}�X�̂����Atrue�̃}�X�̐���Ԃ��܂��B
        /// </summary>
        /// <param name="states">�Ֆʂ̏��</param>
        /// <param name="x">x���W</param>
        /// <param name="y">y���W</param>
        /// <returns>���͂�true�̃}�X�̐�</returns>
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
        /// �w�肵�����W�̎��͂�true�}�X�̂����Aindex�Ԗڂ̂��̂̍��W��Ԃ��܂��B
        /// </summary>
        /// <param name="states">�Ֆʂ̏��</param>
        /// <param name="x">x���W</param>
        /// <param name="y">y���W</param>
        /// <param name="index">�擾����ԍ�</param>
        /// <returns>index�Ԗڂ�true�̃}�X�̍��W</returns>
        /// <exception cref="ArgumentException"></exception>
        private static (int x, int y) GetBranchByCount(bool[,] states, int x, int y, int index)
        {
            List<(bool, (int x, int y))> around = Around(states, x, y);
            if (index + 1 > around.Count || index < 0) throw new ArgumentException("���݂���u�����`�̐��ɑΉ����Ȃ��C���f�b�N�X���w�肵�Ă��܂�");
            around.RemoveAll(x => !x.Item1);
            return around[index].Item2;
        }

        //�e�X�g�p
        //Debug.Log($"{Tyranno.Puzzle.Algorithms.ConditionProfiles.LeftToRightMaze(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.UpperToBottomMaze(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.SingleColoredWall(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.Symmetry(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.PointSymmetry(_puzzleState.SquareArray)}");
    }
}