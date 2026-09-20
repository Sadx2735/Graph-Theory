/*
   Lowest Common Ancestor (LCA)
   ----------------------------
   The LCA of v1 and v2 is the bottom-most node that is an ancestor of both,
   and it lies on the shortest path between them.

   Three approaches implemented below:
     1) Naive two-pointer walk  -- O(N) build,        O(depth) per query
     2) Binary lifting          -- O(N log N) build,  O(log N) per query
     3) Euler tour + sparse RMQ -- O(N log N) build,  O(1) per query ( *Yet to be Done* )

   Sample tree:
           0
          / \
         1   2
        / \
       3   4
      /
     5
      \
       6
*/

class ProgramLCA {
   static void MainLCA (string[] args) {
      int N = 7;

      List<List<int>> AdjNode = [];
      for (int i = 0; i < N; i++) AdjNode.Add ([]);

      AdjNode[0].AddRange (new[] { 1, 2 });
      AdjNode[1].AddRange (new[] { 0, 3, 4 });
      AdjNode[2].Add (0);
      AdjNode[3].AddRange (new[] { 1, 5 });
      AdjNode[4].Add (1);
      AdjNode[5].AddRange (new[] { 3, 6 });
      AdjNode[6].Add (5);

      var visited = new bool[N];
      var depth = new int[N];
      var parent = new int[N];

      // Single DFS that fills depth[], parent[] and the Euler tour together.
      void Traverse (int node, int par, int d) {
         visited[node] = true;
         parent[node] = par;
         depth[node] = d;

         foreach (var nbr in AdjNode[node]) {
            if (!visited[nbr]) {
               Traverse (nbr, node, d + 1);
            }
         }
      }

      // The root is its own parent. Using a self-loop (instead of -1) keeps every
      // entry of LiftMap a valid index, so an over-shooting jump simply clamps at
      // the root and can never index out of range.
      Traverse (0, 0, 0);

      // ---------- 1) Naive ----------
      int FindLcaNaive (int n1, int n2) {
         if (depth[n1] < depth[n2]) (n1, n2) = (n2, n1);

         int dDiff = depth[n1] - depth[n2];
         for (int i = 0; i < dDiff; i++) n1 = parent[n1];

         while (n1 != n2) {
            n1 = parent[n1];
            n2 = parent[n2];
         }
         return n1;
      }

      // ---------- 2) Binary lifting ----------
      int LogN = (int)Math.Log2 (N) + 1;
      int[,] LiftMap = new int[N, LogN];

      for (int i = 0; i < N; i++) LiftMap[i, 0] = parent[i];

      // Build COLUMN by COLUMN. LiftMap[i, j] reads row LiftMap[i, j - 1], which is
      // an arbitrary node index, so the whole (j - 1) column must already be filled.
      // Looping over i on the outside only works by accident when every parent
      // happens to have a smaller index than its child.
      for (int j = 1; j < LogN; j++) {
         for (int i = 0; i < N; i++) {
            LiftMap[i, j] = LiftMap[LiftMap[i, j - 1], j - 1];
         }
      }

      int FindLcaBinlift (int n1, int n2) {
         if (depth[n1] < depth[n2]) (n1, n2) = (n2, n1);

         // Lift the deeper node using the set bits of the depth difference.
         int dDiff = depth[n1] - depth[n2];
         for (int i = LogN - 1; i >= 0; i--) {
            if ((dDiff & (1 << i)) > 0) n1 = LiftMap[n1, i];
         }

         if (n1 == n2) return n1;

         // Both nodes now sit at the same depth d, so no ancestor is more than d
         // steps away and the tallest useful jump is 2^floor(log2(d)).
         // Iterating from LogN - 1 also works -- those extra rounds compare two
         // clamped-at-the-root entries, which are equal, so nothing moves.
         int maxJ = depth[n1] == 0 ? 0 : (int)Math.Log2 (depth[n1]);
         for (int i = maxJ; i >= 0; i--) {
            if (LiftMap[n1, i] != LiftMap[n2, i]) {
               n1 = LiftMap[n1, i];
               n2 = LiftMap[n2, i];
            }
         }
         return parent[n1];
      }

      // ---------- Output ----------
      for (int i = 0; i < N; i++) {
         Console.WriteLine ($"Node : {i} Depth : {depth[i]} Parent : {parent[i]}");
      }

      Console.WriteLine ("\n-----LiftMap-----");
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < LogN; j++) Console.Write ($"{LiftMap[i, j],2} |");
         Console.WriteLine ();
      }

      Console.WriteLine ("\n-----Queries-----");
      var (i1, i2) = (6, 4);
      Console.WriteLine ($"({i1},{i2}) naive : {FindLcaNaive (i1, i2)}");
      Console.WriteLine ($"({i1},{i2}) lift  : {FindLcaBinlift (i1, i2)}");
   }
}