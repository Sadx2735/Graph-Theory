// Wrapping things inside a class
namespace Graphee;

class Representation {
   static void RunMe (string[] args) {
      var Graphee = new Graph (4);
      Graphee.AddEdge (0, 1, true);
      Graphee.AddEdge (0, 2, true);
      Graphee.AddEdge (0, 3, true);
      Graphee.AddEdge (1, 2, true);
      Graphee.PrintAdjList ();
   }
}

class Graph {
   int N;
   List<List<int>> Mem;

   public Graph (int N) {
      this.N = N;
      Mem = new List<List<int>> (N);
      for (int i = 0; i < N; i++) {
         Mem.Add (new List<int> ());
      }
   }

   public void AddEdge (int i, int j, bool UnDir = true) {
      Mem[i].Add (j);
      if (UnDir) Mem[j].Add (i);
   }

   public void PrintAdjList () {
      for (int n = 0; n < N; n++) {
         Console.Write ($"Connections of Node : {n} --> ");
         foreach (var item in Mem[n]) Console.Write ($" {item},");
         Console.WriteLine ("");
      }
   }
}