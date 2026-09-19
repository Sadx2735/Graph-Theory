// Constructing a sample tree:
//      0
//     / \
//    1   2
//   / \
//  3   4
// /
// 5

class ProgramEulerTour {
   static void MainEulerTour (string[] args) {
      int N = 6;
      int[] timein = new int[N];
      int[] timeout = new int[N];
      bool[] visited = new bool[N];

      List<List<int>> AdjNode = new List<List<int>> ();
      for (int i = 0; i < N; i++) AdjNode.Add (new List<int> ());

      AdjNode[0].AddRange (new[] { 1, 2 });
      AdjNode[1].AddRange (new[] { 0, 3, 4 });
      AdjNode[2].Add (0);
      AdjNode[3].AddRange (new[] { 1, 5 });
      AdjNode[4].Add (1);
      AdjNode[5].Add (3);

      int T = 0;

      void dfs (int node, int parent) {
         timein[node] = T++;
         visited[node] = true;

         foreach (var nbr in AdjNode[node]) {
            if (!visited[nbr]) {
               dfs (nbr, node);
            }
         }
         timeout[node] = T++;
      }

      bool IsAncestor (int u, int v) {
         return timein[u] <= timein[v] && timeout[u] >= timeout[v];
      }

      dfs (0, -1);

      Console.WriteLine ($"Is 1 an ancestor of 5? {IsAncestor (1, 5)}");
      Console.WriteLine ($"Is 2 an ancestor of 4? {IsAncestor (2, 4)}");
      Console.WriteLine ($"Is 0 an ancestor of 3? {IsAncestor (0, 3)}");
   }
}