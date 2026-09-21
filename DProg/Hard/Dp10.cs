// LeetCode 834. Sum of Distances in Tree
// https://leetcode.com/problems/sum-of-distances-in-tree/
//
// Problem:
//   Given an undirected, connected tree with n nodes (0..n-1) and a list of
//   edges, return an array `answer` where answer[i] = sum of the distances
//   (in edges) between node i and every other node in the tree.
//
// Technique: Rerooting via "mutate, recurse, restore".
//
//   Pass 1 (CaptureDepth, post-order, rooted arbitrarily at node 0):
//     For every node v, compute two things about v's own subtree:
//       - edmap[v] = number of nodes in v's subtree, minus 1
//                    (i.e. how many edges/nodes hang below v)
//       - depth[v] = sum of distances from v to every node in v's subtree
//
//     Recurrence, combining each child `nbr`:
//       edmap[v] += edmap[nbr] + 1        (nbr's subtree size, plus nbr itself)
//       depth[v] += depth[nbr] + edmap[nbr] + 1
//         (every node in nbr's subtree is 1 edge farther from v than it is
//          from nbr, so we add "1 extra step" for each of those
//          edmap[nbr] + 1 nodes, on top of nbr's own internal distance sum)
//
//   Pass 2 (TrackChanges, pre-order, "reroot" from parent to each child):
//     depth[0] (from pass 1) is already the correct final answer for the
//     root. For every other node we derive its answer from its parent's
//     answer instead of recomputing from scratch:
//
//       When the "center" of the tree moves from `node` to a child `nbr`:
//         - every node inside nbr's subtree becomes 1 step CLOSER
//         - every node outside nbr's subtree (the rest of the tree) becomes
//           1 step FARTHER
//
//     We temporarily flip edmap[]/depth[] to describe the tree as if it were
//     rooted at `nbr` (with `node` now hanging below it), recurse to solve
//     nbr's own children the same way, and then restore the original values
//     before moving on to node's other children. This mutate/recurse/restore
//     pattern avoids allocating a second array for "size" and does the
//     reroot in place.
//
// Complexity: O(n) time, O(n) extra space (recursion stack + arrays).
// Note: recursion depth can hit n on a skewed (path-shaped) tree; for very
// large n an iterative DFS would be safer against stack overflow.

namespace DP;
public class Solution834 {
   public int[] SumOfDistancesInTree (int n, int[][] edges) {
      // Build adjacency list for the (undirected) tree.
      var adjList = new List<int>[n];
      for (int i = 0; i < n; i++) adjList[i] = new List<int> ();
      foreach (var edge in edges) {
         int u = edge[0], v = edge[1];
         adjList[u].Add (v);
         adjList[v].Add (u);
      }

      // depth[v] = sum of distances from v to every node currently
      //            considered "in v's subtree" (relative to whatever
      //            node is currently treated as the tree's root).
      // edmap[v] = (size of v's subtree) - 1, i.e. node count below v.
      int[] depth = new int[n];
      int[] edmap = new int[n];

      // ---- Pass 1: post-order, tree rooted at node 0 ----
      // Fills depth[] and edmap[] for every node's subtree under this
      // fixed rooting. Returns edmap[node] to the caller for convenience.
      int CaptureDepth (int node, int parent) {
         int subtreeSize = 0; // edmap[node]: nodes below `node`
         int distSum = 0;     // depth[node]: distance sum within subtree

         foreach (var nbr in adjList[node]) {
            if (nbr == parent) continue;

            subtreeSize += CaptureDepth (nbr, node); // nbr's own subtree size
            subtreeSize += 1;                        // plus nbr itself

            distSum += depth[nbr];  // distances that stay the same
            distSum += edmap[nbr];  // "+1 step" for each node in nbr's subtree
            distSum += 1;           // "+1 step" to reach nbr itself
         }

         edmap[node] = subtreeSize;
         depth[node] = distSum;
         return subtreeSize;
      }
      CaptureDepth (0, -1);

      // depth[0] is now already correct: it's the true answer for node 0,
      // since the whole tree IS node 0's subtree under this rooting.
      int[] result = new int[n];

      // ---- Pass 2: pre-order, reroot node-by-node ----
      // At entry, depth[node]/edmap[node] are correct for `node` treated
      // as the current root of the (conceptually re-rooted) tree.
      void TrackChanges (int node, int parent) {
         // depth[node] is now the true final answer for this node.
         result[node] = depth[node];

         foreach (var nbr in adjList[node]) {
            if (nbr == parent) continue;

            // Save node's and nbr's current values so we can restore
            // them after we're done exploring nbr's side of the tree.
            int savedDepthNode = depth[node];
            int savedEdmapNode = edmap[node];
            int savedDepthNbr = depth[nbr];
            int savedEdmapNbr = edmap[nbr];

            // --- Reroot from `node` to `nbr` ---
            // Step A: remove nbr's whole subtree from node's totals,
            // since after rerooting, nbr (and everything below it)
            // is no longer "below" node.
            depth[node] -= depth[nbr];
            depth[node] -= edmap[nbr];
            depth[node] -= 1;

            edmap[node] -= edmap[nbr];
            edmap[node] -= 1;

            // edmap[nbr] must now reflect "everything except nbr's
            // own original subtree", i.e. the rest of the whole tree.
            edmap[nbr] = n - 1;

            // Step B: node (with its now-shrunken subtree) hangs below
            // nbr, so nbr absorbs node's updated totals, plus the extra
            // "+1 step" for every node in that shrunken subtree.
            depth[nbr] += 1;
            depth[nbr] += depth[node];
            depth[nbr] += edmap[node];

            // Recurse into nbr's side with the tree now rooted at nbr.
            TrackChanges (nbr, node);

            // --- Restore, so node's siblings see the original values ---
            depth[nbr] = savedDepthNbr;
            edmap[nbr] = savedEdmapNbr;
            depth[node] = savedDepthNode;
            edmap[node] = savedEdmapNode;
         }
      }
      TrackChanges (0, -1);

      return result;
   }
}