public class Solution {
   public IList<IList<int>> AllPathsSourceTarget (int[][] graph) {
      IList<IList<int>> FinalResults = new List<IList<int>> ();
      int N = graph.GetLength (0);
      void Dfs (int n, Stack<int> path) {
         if (n == (N - 1)) {
            path.Push (n);
            FinalResults.Add (new List<int> (path));
            path.Pop ();
            return;
         }
         path.Push (n);
         foreach (var item in graph[n]) {
            Dfs (item, path);
         }
         path.Pop ();
      }
      Dfs (0, new Stack<int> ());
      return FinalResults;
   }
}