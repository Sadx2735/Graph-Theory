// Rerooting problems
// Problem goes like this choose a Node make it as the Root node.
// From that node travel to the adjacent one's. and each time you go
// to a node the subtree becomes Score.
// In Simple words :
// for what node considered as root the subtree count is going to be maximum.
// TC : O^2
/*
Sample tree:
         0
        / \
       1   2
      / \
     3   4
    / \
   5   6
*/

class ProgramNaive {
   static void MainNaive (string[] args) {
      int N = 7;

      bool[] visited = new bool[N];
      List<List<int>> AdjNode = [];
      for (int i = 0; i < N; i++) AdjNode.Add ([]);

      AdjNode[0].AddRange (new[] { 1, 2 });
      AdjNode[1].AddRange (new[] { 0, 3, 4 });
      AdjNode[2].Add (0);
      AdjNode[3].AddRange (new[] { 1, 5, 6 });
      AdjNode[4].Add (1);
      AdjNode[5].Add (3);
      AdjNode[6].Add (3);

      int total = 0;
      for (int nde = 0; nde < N; nde++) {
         total = 0;
         visited = new bool[N];
         dfs (nde);
         Console.WriteLine ($"Score when choosing start as {nde} is {total}");
      }

      int dfs (int node) {
         visited[node] = true;
         int cnt = 1;
         foreach (var nbr in AdjNode[node]) {
            if (visited[nbr]) continue;
            cnt += dfs (nbr);
         }
         total += cnt;
         return cnt;
      }
   }
}