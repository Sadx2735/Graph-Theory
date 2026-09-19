/*
Why we do what we do?

Say we have a graph like the one in "SCC.png". If we start from 0, we can reach
every other SCC — 1st SCC to 2nd to 3rd and so on. But if we start from node 3
(the 2nd SCC), we can't come back to the 1st SCC. Same story for node 5 and node 7.

So the trick is: run one DFS from each SCC, but in the right order — always start
from an SCC that nobody else can reach yet. The problem is, how do we know 0's SCC
comes first and 7's SCC comes last?

We use finish times for this. The node that finishes LAST in the first DFS is
always inside a "source" SCC (an SCC with no incoming edges from other SCCs).
So we take the finish order, reverse it, and 0 comes to the front.

But why do we also reverse the graph? Because if we DFS from 0 on the original
graph, we'd flood everything (0 => 1 => 2 => 3 ...) and count just 1 SCC.
On the reversed graph, edges between SCCs flip (0 <= 1 <= 2 <= 3), so the DFS
from 0's SCC gets trapped inside 0's SCC and can't leak into the next one.
Inside an SCC, reversing changes nothing — it's still strongly connected.
That's why every DFS in the second pass covers exactly one SCC.
*/

class ProgramKrju {
   static void MainKrju (String[] args) {
      // Inits
      int N = 8;
      List<List<int>> AdjList = [];
      for (int i = 0; i < N; i++) AdjList.Add ([]);

      // Recreating a graph.
      List<List<int>> AdjListRev = [];
      for (int i = 0; i < N; i++) AdjListRev.Add ([]);

      // Adds Components
      void AddMe (int u, int v, bool bidir = false) {
         AdjList[u].Add (v);
         if (bidir) AdjList[v].Add (u);
      }

      // Graph construction
      AddMe (0, 1); AddMe (1, 2);
      AddMe (2, 0); AddMe (2, 3);
      AddMe (3, 4); AddMe (4, 5);
      AddMe (5, 6); AddMe (6, 4);
      AddMe (4, 7); AddMe (6, 7);

      List<int> Order = [];
      bool[] visited = new bool[N];
      bool[] visitedx2 = new bool[N];

      // Topological sort
      void DFS (int Node) {
         visited[Node] = true;
         foreach (var nbr in AdjList[Node]) {
            if (!visited[nbr]) {
               DFS (nbr);
            }
         }
         Order.Add (Node);
      }

      // Topological sort
      void DFSx2 (int Node) {
         visitedx2[Node] = true;
         foreach (var nbr in AdjListRev[Node]) {
            if (!visitedx2[nbr]) {
               DFSx2 (nbr);
            }
         }
      }

      DFS (0);
      for (int i = 0; i < N; i++) {
         if (!visited[i]) DFS (i);
      }

      Order.Reverse ();
      Console.Write ("Topo Order : ");
      foreach (var item in Order.SkipLast (1)) {
         Console.Write ($"{item} ");
      }
      Console.Write (Order[^1]);

      for (int i = 0; i < N; i++) {
         for (int j = 0; j < AdjList[i].Count; j++) {
            AdjListRev[AdjList[i][j]].Add (i);
         }
      }

      int cnt = 0;
      foreach (var item in Order) {
         if (!visitedx2[item]) {
            cnt++;
            DFSx2 (item);
         }
      }
      Console.WriteLine ($"\nThere are {cnt} Scc");
   }
}

/*
 Doubt 1: can't we just use the order as-is (starting from 7) on the original graph?
 
 Claude (Fable):

 No, and here's the thing — it actually works on your graph by luck, but it's wrong in general. 
 Try this graph: 0↔1 and 0→2. DFS from 0 gives finish order: 1, 2, 0. 
 Now take the order as-is, so start from node 1 on the original graph. 
 From 1 you reach 0 and 2 — everything gets visited in one shot, cnt = 1. 
 But the real answer is 2 SCCs ({0,1} and {2}).

 The reason: the last node to finish is guaranteed to be in a source SCC. 
 The first node to finish has no such guarantee — node 1 finished first but it sits in the source SCC, not a sink. 
 So "smallest finish time = deepest SCC" is just not a true statement, it only feels true.

 Doubt 2: why not just take unique low values in Tarjan?

   * Because low doesn't always point at the ultimate root directly — it points at some node inside the same SCC, 
     which might itself be a non-root. Take 1↔2,  2↔3 (all one SCC): node 3 ends with low = disc of 2, not of 1. 
     Unique low values would give you {1, 2} = 2 SCCs, but the answer is 1.

   * So raw low is off by "one hop" sometimes. That's exactly what your earlier idea fixed — follow the pointer again 
     (low of low) until it stops moving, and  then take unique values. 

     That works, because for every non-root low < disc, 
     so the chain always slides down to the one node where low == disc, the root  of the SCC.

   * Two things to remember if you implement it that way:

   * The min must only be taken over neighbors that are still on the stack. If you relax over any visited node, 
     low can leak into a different, already-  finished SCC and merge two components that are separate. 
     
     Standard Tarjan :
     already gives you this for free — the moment low == disc, it pops the stack and everything popped is one SCC. 
     So the "unique of resolved low" version is a nice way to understand what low means, 
     but it's extra work, not a shortcut.
 */