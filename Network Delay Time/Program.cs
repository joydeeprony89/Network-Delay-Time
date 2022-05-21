using System;
using System.Collections.Generic;

namespace Network_Delay_Time
{
  class Program
  {
    static void Main(string[] args)
    {
      int[][] times = new int[3][] { new int[] { 2, 1, 1, }, new int[] { 2, 3, 1 }, new int[] { 3, 4, 1 } };
      Solution s = new Solution();
      int result = s.NetworkDelayTime(times, 4, 2);
      Console.WriteLine(result);
    }
  }

  public class Solution
  {
    public int NetworkDelayTime(int[][] times, int n, int k)
    {
      Dictionary<int, List<int[]>> adj = new Dictionary<int, List<int[]>>();
      foreach (var time in times)
      {
        int source = time[0];
        int dest = time[1];
        int cost = time[2];
        if (!adj.ContainsKey(source))
          adj.Add(source, new List<int[]>());
        var existing = adj[source];
        existing.Add(new int[] { dest, cost });
        adj[source] = existing;
      }

      PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
      pq.Enqueue(k, 0);
      HashSet<int> visited = new HashSet<int>();
      while (pq.Count > 0)
      {
        pq.TryDequeue(out var currentNode, out var currentDistance);
        if (visited.Contains(currentNode)) continue;

        visited.Add(currentNode);
        if (visited.Count == n) return currentDistance;
        if (adj.ContainsKey(currentNode))
        {
          foreach (var neighbours in adj[currentNode])
          {
            int nextNode = neighbours[0];
            int cost = neighbours[1];
            if (visited.Contains(nextNode)) continue;
            pq.Enqueue(nextNode, (currentDistance + cost));
          }
        }
      }

      return -1;
    }
  }
}
