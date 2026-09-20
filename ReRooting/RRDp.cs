/* DP version ReRooting.
Sample tree:
         0
        / \
       1   2
      / \
     3   4
*/

class ProgramRRDP {
   static void MainRRDP (string[] args) {
      int N = 5;

      bool[] visited = new bool[N];
      List<List<int>> AdjNode = [];
      for (int i = 0; i < N; i++) AdjNode.Add ([]);

      AdjNode[0].AddRange (new[] { 1, 2 });
      AdjNode[1].AddRange (new[] { 0, 3, 4 });
      AdjNode[2].Add (0);
      AdjNode[3].Add (1);
      AdjNode[4].Add (1);

      int[] DP = new int[N];
      int[] SB = new int[N];

      for (int i = 0; i < N; i++)
         Console.WriteLine ($"{i} : {DP[i]} , {SB[i]}");

      Console.WriteLine ();
      dfs (0); DfsPower (0, -1);

      (int, int) dfs (int node) {
         visited[node] = true;
         int cnt = 1;
         foreach (var nbr in AdjNode[node]) {
            if (visited[nbr]) continue;
            var (ct, dp) = dfs (nbr);
            cnt += ct; DP[node] += dp;
         }
         SB[node] = cnt;
         DP[node] += SB[node];
         return (cnt, DP[node]);
      }

      void DfsPower (int node, int parent) {
         Console.WriteLine ($"node : {node} dp : {DP[node]}");
         foreach (var nbr in AdjNode[node]) {
            if (nbr != parent) {
               // from parent as root to child as root.
               DP[node] -= DP[nbr];
               DP[node] -= SB[nbr];
               SB[node] -= SB[nbr];

               SB[nbr] += SB[node];
               DP[nbr] += DP[node];
               DP[nbr] += SB[node];

               DfsPower (nbr, node);

               // from child as root to parent as root.
               SB[nbr] -= SB[node];
               DP[nbr] -= DP[node];
               DP[nbr] -= SB[node];

               DP[node] += DP[nbr];
               DP[node] += SB[nbr];
               SB[node] += SB[nbr];
            }
         }
      }
   }
}