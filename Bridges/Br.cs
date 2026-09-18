using System;
using System.Collections.Generic;

/* LeetCode 1192 -> critical connections , means find all the bridges.

   disc[u] -> the time at which dfs first came into u
   low[u]  -> the smallest disc reachable from u's subtree using atmost one
              back edge up

   bridge test , for a tree edge ( u -> v ) where v is the child of u :
        low[v] > disc[u]        <- strict

   why strict : if low[v] == disc[u] that means v's subtree is having a back
   edge which is landing on u itself , so even after cutting the edge (u,v)
   they are still joined by that back edge. only when low[v] > disc[u] the
   subtree cant touch u or anything above u except from this one edge.

   for articulation points the same code is used but with >= , and also the
   root needs the children count rule. full writeup is in Arpts.cs */

public class SolutionBridges {
   public IList<IList<int>> CriticalConnections (int n, IList<IList<int>> connections) {
      int[] disc = new int[n];          // 0 means not visited
      int[] low = new int[n];
      IList<IList<int>> result = new List<IList<int>> ();

      List<List<int>> Map = new List<List<int>> ();
      for (int i = 0; i < n; i++) Map.Add (new List<int> ());
      foreach (var e in connections) {
         Map[e[0]].Add (e[1]);
         Map[e[1]].Add (e[0]);
      }

      int T = 1;
      void DFS (int node, int parent) {
         disc[node] = T++;
         low[node] = disc[node];

         foreach (var nbr in Map[node]) {
            if (nbr == parent) continue;

            if (disc[nbr] == 0) {
               // ---- tree edge ----
               DFS (nbr, node);
               low[node] = Math.Min (low[node], low[nbr]);
               if (low[nbr] > disc[node])
                  result.Add (new List<int> { node, nbr });
            } else {
               // ---- back edge ----
               // take disc[nbr] , not low[nbr].
               //
               // my old note said "you can also use low[nbr] here" and for
               // BRIDGES that is actually true , it gives the same answers.
               // ( proof is in Arpts.cs point 1 -> low[nbr] <= disc[nbr] <
               // disc[parent(node)] , so the strict test fails in both the
               // cases anyway , and the over claim is only touching the edges
               // which are already inside a cycle )
               //
               // but its just a coincidence of this problem , and the same
               // thing fully breaks the articulation points. so better keep
               // disc in both , then the two algos are exactly same except
               // the test line itself.
               low[node] = Math.Min (low[node], disc[nbr]);
            }
         }
      }

      DFS (0, -1);   // LC1192 says the graph is connected. if not then loop over
                     // all i where disc[i] == 0 and call DFS(i , -1)
      return result;
   }
}

/* one thing about  if (nbr == parent) continue;
   this is skipping the parent by the VERTEX ID , so it is assuming there is
   atmost one edge between two vertices. if there are parallel edges like u-v
   coming twice , then that pair is really not a bridge but this code will
   still report it. the fix is to pass the EDGE INDEX i came from instead of
   the parent vertex :

   void DFS (int node, int inEdge) { ... if (edgeId == inEdge) continue; ... }

   LC1192 doesnt allow duplicate connections so the simple one is fine here. */