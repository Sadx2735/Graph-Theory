/*
 Call Recursively if its not explored!
*/

// Construction of Graph.
class Dummy {
   static void RunMain () {
      int n = 7;
      GraphStructure Gs = new GraphStructure (n);
      Gs.AddNode (1, 0);
      Gs.AddNode (1, 2);
      Gs.AddNode (2, 3);
      Gs.AddNode (0, 4);
      Gs.AddNode (4, 3);
      Gs.AddNode (4, 5);
      Gs.AddNode (3, 5);
      Gs.AddNode (5, 6);
      Gs.DFS (1, new bool[n]);
   }
}
// Graph Data Structure.
class GraphStructure {
   int N;
   List<List<int>> Map;
   public GraphStructure (int N) {
      Map = new (); this.N = N;
      for (int i = 0; i < N; i++) {
         Map.Add (new List<int> ());
      }
   }
   public void AddNode (int from, int to, bool BothDir = true) {
      Map[from].Add (to);
      if (BothDir) Map[to].Add (from);
   }
   // Normal Printing..
   public void PrintAdjList () {
      for (int i = 0; i < N; i++) {
         Console.Write ($"{i} => ");
         foreach (var val in Map[i]) {
            Console.Write ($"{val},");
         }
         Console.WriteLine ();
      }
   }
   // DoDFS()
   public void DFS (int node, bool[] visited) {
      Console.WriteLine (node);
      visited[node] = true;
      foreach (var item in Map[node]) {
         if (!visited[item]) DFS (item, visited);
      }
   }
}