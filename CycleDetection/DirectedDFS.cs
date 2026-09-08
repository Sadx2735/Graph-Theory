namespace CycleDetectionx4;
// MakeEdges
class Programx4 {
   static void Mainx4 () {
      int n = 6;
      bool cond = false;
      GraphStructure Gs = new GraphStructure (n);
      Gs.AddNode (0, 1, cond);
      Gs.AddNode (1, 2, cond);
      Gs.AddNode (2, 3, cond);
      Gs.AddNode (3, 0, cond);
      Gs.AddNode (0, 4, cond);
      Gs.AddNode (0, 5, cond);
      Gs.PrintAdjList ();
      Console.WriteLine ("Contains Cycle " + (Gs.HasCyclesDirected () ? "Yes" : "No"));
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

   public void PrintAdjList () {
      for (int i = 0; i < N; i++) {
         Console.Write ($"{i} => ");
         foreach (var val in Map[i]) {
            Console.Write ($"{val},");
         }
         Console.WriteLine ();
      }
   }
   public bool HasCycles () {
      bool DFS (int n, int p, bool[] vis) {
         vis[n] = true;
         foreach (var nbr in Map[n]) {
            if (!vis[nbr]) {
               bool nbrval = DFS (nbr, n, vis);
               if (nbrval) return true;
            } else if (nbr != p) return true;
         }
         return false;
      }
      return DFS (0, -1, new bool[N]);
   }

   public bool HasCyclesDirected () {
      bool[] visited = new bool[N];
      bool[] cStack = new bool[N];
      bool DFS (int n, bool[] visited, bool[] cStack) {
         visited[n] = true; cStack[n] = true;
         foreach (var item in Map[n]) {
            if (!visited[item] && DFS (item, visited, cStack))
               return true;
            else if (visited[item] && cStack[item]) return true;
         }
         cStack[n] = false;
         return false;
      }
      return DFS (0, visited, cStack);
   }
}