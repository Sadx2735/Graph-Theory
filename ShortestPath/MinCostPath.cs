public class SolutionDijkstra {
   public int MinCost (int maxTime, int[][] edges, int[] passingFees) {
      int N = passingFees.Length;
      List<List<(int, int)>> AdjList = new ();
      for (int i = 0; i < N; i++) AdjList.Add (new List<(int, int)> ());
      foreach (var item in edges) {
         var (from, to, weight) = (item[0], item[1], item[2]);
         AdjList[from].Add ((to, weight));
         AdjList[to].Add ((from, weight));
      }
      int[] minCost = new int[N];
      int[] minTime = new int[N];
      Array.Fill (minCost, int.MaxValue);
      Array.Fill (minTime, int.MaxValue);

      minCost[0] = passingFees[0];
      minTime[0] = 0;

      PriorityQueue<(int cost, int time, int node), int> pq = new PriorityQueue<(int, int, int), int> ();
      pq.Enqueue ((passingFees[0], 0, 0), passingFees[0]);

      while (pq.Count > 0) {
         var (currCost, currTime, u) = pq.Dequeue ();
         foreach (var (v, travelTime) in AdjList[u]) {
            int nextTime = currTime + travelTime;
            int nextCost = currCost + passingFees[v];

            if (nextTime <= maxTime) {
               if (nextCost < minCost[v] || nextTime < minTime[v]) {
                  if (nextCost < minCost[v]) minCost[v] = nextCost;
                  if (nextTime < minTime[v]) minTime[v] = nextTime;
                  pq.Enqueue ((nextCost, nextTime, v), nextCost);
               }
            }
         }
      }
      return (minCost[^1] == Int32.MaxValue) ? -1 : minCost[^1];
   }
}