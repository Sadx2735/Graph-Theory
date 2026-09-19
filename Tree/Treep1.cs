// if a given graph contains cycles then its not a TREE
// (or) it is a connected graph which has no cycles
// Forest is a graph in which there are multiple connected components
// And each component is a tree.

// if there are n nodes then m=n-1 ( edges )
// this is because they wont have cycles.

// Root Node : Start of the Tree
// Left Node : Node with No children.
// Parent Node -> Child Node is when a we travel from a Node
// to Some other of its connected Node. (its Arbitary as we consider).

// Ancestors : Node that were explored before coming to the current Node
// also in the same path.

// Subtree : if x is the ancestor of y then y is the subtree of x.

// All these are subjected entirely on what we consider to be the root.
// 1->2 , 1->3 can also be seen as 3->1 and 1->2.

class ProgramTree {
   static void MainTree (string[] args) {
      
      List<List<int>> AdjList = [];
      int[] parentarr = [];

      void DFS (int node, int parent) {
         Console.Write ($"{node}");
         foreach (var nbr in AdjList[node]) {
            if (nbr != parent) DFS (nbr, node);
         }
      }

      void PrintPath (int node, int parent) {
         Console.Write ($"{node}");
         parentarr[node] = parent;
         foreach (var nbr in AdjList[node]) {
            if (nbr != parent) DFS (nbr, node);
         }
      }

      void GetParent (int node) {
         if (parentarr[node] == -1) return;
         Console.WriteLine (node);
         node = parentarr[node];
      }
   }
}