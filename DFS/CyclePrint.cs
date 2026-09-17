/*
 Call Recursively if its not explored!
*/

// Construction of Graph.
namespace CycleDfs;
class ProgramCycleDFS {
   static void MainDFS () {
      int n = 7;
      GraphStructure Gs = new GraphStructure (n);
      Gs.AddNode (1, 0);
      Gs.AddNode (2, 1);
      Gs.AddNode (3, 2);
      Gs.AddNode (0, 4);
      Gs.AddNode (4, 3);
      Gs.AddNode (4, 5);
      Gs.AddNode (5, 6);
      Gs.DFS (0, -1, new int[n], new int[n]);
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
   public void AddNode (int from, int to, bool BothDir = false) {
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
   public void DFS (int Node, int Parent, int[] visited, int[] parent) {
      visited[Node] = 1;
      parent[Node] = Parent;
      foreach (var nbr in Map[Node]) {
         if (visited[nbr] == 0) {
            DFS (nbr, Node, visited, parent);
         } else if (visited[nbr] == 1) {
            int Cur = Node, Anc = nbr;
            while (Cur != Anc) {
               Console.Write ($"{Cur} ");
               Cur = parent[Cur];
            }
            Console.Write ($"{Cur} ");
         }
      }
      visited[Node] = 2;
   }
}