/*
 Push the first element to the Queue
 Pop it and iterate through all of the vertices to which it is connected.
 Check if its not visited and Add it into the Queue.
 Repeat the Above till the Queue has value ( Queue.Count>0 )
*/

// Construction of Graph.
namespace BFSx1;

class ProgramxBFs1 {
   static void RunMe () {
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
      Gs.PrintBwise ();
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
   // Print Level Wise
   public void PrintBwise () {
      Queue<int> Q = [];
      bool[] Visited = new bool[N];
      Q.Enqueue (1); Visited[1] = true;
      while (Q.Count > 0) {
         var element = Q.Dequeue ();
         Console.WriteLine ($"LayerWise: {element}");
         foreach (var item in Map[element]) {
            if (!Visited[item]) {
               Q.Enqueue (item);
               Visited[item] = true;
            }
         }

      }
   }
}