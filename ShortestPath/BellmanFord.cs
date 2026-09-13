public class SolutionxBF {
   public List<int> bellmanFord (int V, int[,] edges, int src) {
      // Initializations for the algo
      int[] dist = new int[V];
      Array.Fill (dist, Int32.MaxValue);
      dist[src] = 0;

      int numEdges = edges.GetLength (0); 

      // Run for N-1 and do the relaxation.
      for (int i = 0; i < V - 1; i++) {
         for (int j = 0; j < numEdges; j++) {
            int from = edges[j, 0];
            int to = edges[j, 1];
            int weight = edges[j, 2];
            // if we get to see a better shorter distance.
            if (dist[from] != Int32.MaxValue && dist[from] + weight < dist[to]) {
               dist[to] = dist[from] + weight;
            }
         }
      }

      // Negative Cycles make it to change even after Relaxing as the 
      // Rotational sum will have a smaller value each time it travels around.
      for (int j = 0; j < numEdges; j++) {
         int from = edges[j, 0];
         int to = edges[j, 1];
         int weight = edges[j, 2];

         if (dist[from] != Int32.MaxValue && dist[from] + weight < dist[to]) {
            return new List<int> { -1 };
         }
      }

      return dist.ToList ();
   }
}
