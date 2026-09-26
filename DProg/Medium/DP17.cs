using DP;

// Representing tree as a graph for solving problems easily.
public class Solution863 {
   public IList<int> DistanceK (TreeNode root, TreeNode target, int k) {
      Dictionary<int, List<int>> AdjList = [];
      void MakeGraph (TreeNode Node, TreeNode Parent) {
         if (Node is null) return;
         if (Parent is null) AdjList[Node.val] = new List<int> ();
         if (Parent is not null) {
            if (!AdjList.ContainsKey (Parent.val)) AdjList.Add (Parent.val, [Node.val]);
            else AdjList[Parent.val].Add (Node.val);
            if (!AdjList.ContainsKey (Node.val)) AdjList.Add (Node.val, [Parent.val]);
            else AdjList[Node.val].Add (Parent.val);
         }
         MakeGraph (Node.left, Node);
         MakeGraph (Node.right, Node);
      }
      MakeGraph (root, null);
      foreach (var (key, vals) in AdjList) {
         Console.WriteLine ($"Parent is {key}");
         foreach (var val in vals) {
            Console.WriteLine ($"Child is {val}");
         }
         Console.WriteLine ("-----");
      }
      var result = new List<int> ();
      Dictionary<int, bool> Visited = [];
      foreach (var (ke, v) in AdjList) {
         Visited[ke] = false;
      }
      void DFS (int node, int parent, int distance) {
         Console.WriteLine ($"Call for {node} {parent}");
         Visited[node] = true;
         if (distance == k) result.Add (node);
         foreach (var child in AdjList[node]) {
            if (Visited[child] || child == parent) continue;
            DFS (child, node, distance + 1);
         }
      }
      DFS (target.val, -1, 0);
      return result;
   }
}