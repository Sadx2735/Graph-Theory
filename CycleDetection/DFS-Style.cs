namespace Solx;
// Solution program.
class Solution {
   public bool isCycle (int V, int[,] edges) {
      // Initialization
      bool[] visited = new bool[V];
      List<List<int>> AdjList = new List<List<int>> ();
      for (int i = 0; i < V; i++) {
         AdjList.Add (new List<int> ());
      }
      // Edge List to Adjlist
      for (int i = 0; i < edges.GetLength (0); i++) {
         int u = edges[i, 0];
         int v = edges[i, 1];
         AdjList[u].Add (v);
         AdjList[v].Add (u);
      }
      // Search and check if its already visited if not then explore
      // else if its already visited then check if that thing is not a parent then flag.
      bool DFS (int node, int parent) {
         visited[node] = true;
         foreach (var item in AdjList[node]) {
            if (!visited[item]) {
               if (DFS (item, node)) return true;
            } else if (parent != item) {
               return true;
            }
         }
         return false;
      }
      // For NonConnected Components
      for (int i = 0; i < V; i++) {
         if (!visited[i]) {
            if (DFS (i, -1)) return true;
         }
      }
      return false;
   }
}