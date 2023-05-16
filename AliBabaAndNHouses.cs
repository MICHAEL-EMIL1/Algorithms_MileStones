using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class AliBabaAndNHouses
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the maximum amount of money that Ali baba can get, given the number of houses (N) and a list of the net gained value for each consecutive house (V)
        /// </summary>
        /// <param name="values">Array of the values of each given house (ordered by their consecutive placement in the city)</param>
        /// <param name="N">The number of the houses</param>
        /// <returns>the maximum amount of money the Ali Baba can get </returns>
        static public int Solve_F1(int[] values, int N)
        {
            if (N == 0)
                return 0;
            if (N == 1)
                return values[0];

            int[] index = new int[N];
            index[0] = values[0];
            //index[1] = Math.Max(values[0], values[1]);
            if (values[0] >= values[1])
            {
                index[1] = values[0];
            }
            else
            {
                index[1] = values[1];
            }
            for (int i = 2; i < N; i++)
            {
                //index[i] = Math.Max(index[i - 2] + values[i], index[i - 1]);
                index[i] = (index[i - 2] + values[i]) > index[i - 1] ? (index[i - 2] + values[i]) : index[i - 1];
            }
            return index[N-1];
        }
        
        static public int SolveValue(int[] values, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            return Solve_F1(values, N);
        }
        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Array of the indices of the robbed houses (1-based and ordered from left to right) </returns>
        static public int[] ConstructSolution(int[] values, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();

            int[] index = new int[N];
            index[0] = values[0];
            index[1] = values[0] > values[1] ? values[0] : values[1];

            int[] rHouses = new int[N];
            rHouses[0] = 1;
            rHouses[1] = values[0] > values[1] ? 1 : 2 ;

            for (int i = 2; i < N; i++)
            {
                //index[i] = Math.Max(index[i - 2] + values[i], index[i - 1]);
                index[i] = (index[i - 2] + values[i]) > index[i - 1] ? (index[i - 2] + values[i]) : (index[i - 1]);
                rHouses[i] = (index[i - 2] + values[i]) > index[i - 1] ? (i + 1) : (rHouses[i - 1]);
            }
            List<int> result = new List<int>();
            int j = N - 1;
            while (j >= 0)
            {
                if (rHouses[j] == j + 1)
                {
                    result.Add(j + 1);
                    j -= 2;
                }
                else
                {
                    j--;
                }
            }
            result.Reverse();
            return result.ToArray();
        }
        #endregion

        #endregion
    }
}
