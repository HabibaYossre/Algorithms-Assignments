using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE 

        public enum SOLUTION_TYPE { NAIVE, EFFICIENT };
        public static SOLUTION_TYPE solType = SOLUTION_TYPE.EFFICIENT;

        //Your Code is Here:
        //==================
        /// <summary>
        /// find the total number of possible paths to move the robot from the bottm-left corner to the top-right corner
        /// </summary>
        /// <param name="grid">n x m grid wth obstacles marked as 'x'</param>
        /// <returns>total number of possible paths</returns>
        public static long RequiredFunction(char[,] grid)
        {
            int n = grid.GetLength(0); 
            int m = grid.GetLength(1);

            // Create a DP table to store number of ways to reach each cell
            long[,] dp = new long[n, m];

            // Initialize starting position
            dp[n - 1, 0] = grid[n - 1, 0] == 'x' ? 0 : 1;

            // Fill the DP table from bottom-left to top-right
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < m; j++)
                {
                    // Skip if it's the starting cell or an obstacle
                    if ((i == n - 1 && j == 0) || grid[i, j] == 'x')
                        continue;

                    long fromRight;
                    long fromBelow;
                    long fromDiag;
                    if (j > 0)
                    {
                        fromRight = dp[i, j - 1]; // Move right
                    }
                    else
                    {
                        fromRight = 0;
                    }
                    if (i < n - 1)
                    {
                        fromBelow = dp[i + 1, j]; // Move up
                    }
                    else
                    {
                        fromBelow = 0;
                    }

                    if (i < n - 1 && j > 0)
                    {
                        fromDiag = dp[i + 1, j - 1]; // Diagonal
                    }
                    else
                    {
                        fromDiag = 0;
                    }
                    dp[i, j] = fromRight + fromBelow + fromDiag;
                }
            }

            // Return the value at top-right corner
            return dp[0, m - 1];
        }
        #endregion
    }
}
