using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
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

        enum SOLUTION_TYPE { NAIVE, EFFICIENT };
        static SOLUTION_TYPE solType = SOLUTION_TYPE.EFFICIENT;

        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the N tasks, design an efficient solution to schedule them to minimize the average completion time of these processes.
        /// </summary>
        /// <param name="r">release time of each process</param>
        /// <param name="p">processing time of each process</param>
        /// <returns>min average completion time</returns>

        public static double RequiredFunction(int[] r, int[] p)
        {
            int n = r.Length;
            if (n == 0) return 0;

            // Sort tasks by release time 
            Array.Sort(r, p);

            // Min-heap for available tasks (sorted by remaining processing time)

            var readyTasks = new SortedSet<(int remaining, int index)>(
            Comparer<(int remaining, int index)>.Create((a, b) =>
                a.remaining != b.remaining ? a.remaining.CompareTo(b.remaining) : a.index.CompareTo(b.index)));


            int currentTime = 0;
            int taskIndex = 0;
            double totalCompletionTime = 0;

            while (taskIndex < n || readyTasks.Count > 0)
            {
                // Add all tasks released by currentTime
                while (taskIndex < n && r[taskIndex] <= currentTime)
                {
                    readyTasks.Add((p[taskIndex], taskIndex));
                    taskIndex++;
                }

                if (readyTasks.Count == 0)
                {
                    // No tasks available, jump to next release time
                    if (taskIndex < n)
                        currentTime = r[taskIndex];
                    continue;
                }

                
                // Get the task with smallest remaining time
                var currentTask = readyTasks.Min;
                readyTasks.Remove(currentTask);

                // how long to run this task
                int timeToRun = currentTask.remaining;
                if (taskIndex < n)
                {
                    timeToRun = Math.Min(currentTask.remaining, r[taskIndex] - currentTime);
                }

                // Execute the task
                p[currentTask.index] -= timeToRun;
                currentTime += timeToRun;

                if (p[currentTask.index] == 0)
                {
                    totalCompletionTime += currentTime; // Task completed
                }
                else
                {
                    readyTasks.Add((p[currentTask.index], currentTask.index)); // Re-add to heap
                }
            }

            return Math.Round(totalCompletionTime / n, 2);
        }

        #endregion
    }
}
