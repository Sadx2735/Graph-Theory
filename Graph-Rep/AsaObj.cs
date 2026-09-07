// Wrapping things inside a class
namespace Graphee;
class Representation {
   static void RunMe (string[] args) {
      var Graphee = new SimpleGraph (4);
      Graphee.AddEdge (0, 1, true);
      Graphee.AddEdge (0, 2, true);
      Graphee.AddEdge (0, 3, true);
      Graphee.AddEdge (1, 2, true);
      Graphee.PrintAdjList ();
   }
}

class SimpleGraph {
   int N;
   List<List<int>> Mem;
   // Initialization of Graph.
   public SimpleGraph (int N) {
      this.N = N;
      Mem = new List<List<int>> (N);
      for (int i = 0; i < N; i++) {
         Mem.Add (new List<int> ());
      }
   }
   // Add u,v and v,u if Undirected else only u,v
   public void AddEdge (int i, int j, bool UnDir = true) {
      Mem[i].Add (j);
      if (UnDir) Mem[j].Add (i);
   }
   // Printing the Adjacency List
   public void PrintAdjList () {
      for (int n = 0; n < N; n++) {
         Console.Write ($"Connections of Node : {n} --> ");
         foreach (var item in Mem[n]) Console.Write ($" {item},");
         Console.WriteLine ("");
      }
   }
}