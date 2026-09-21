namespace DP;

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */

public class Solution1372 {
   public int LongestZigZag (TreeNode root) {
      int Maxi = 0;
      int TrackLongest (TreeNode node, int dir, int depth) {
         if (node is null) return 0;
         int byme = 0, byorder = 0;
         if (dir == 1) {
            byorder = TrackLongest (node.left, -1, depth + 1);
            byme = TrackLongest (node.right, +1, 1);
         } else if (dir == -1) {
            byorder = TrackLongest (node.right, +1, depth + 1);
            byme = TrackLongest (node.left, -1, 1);
         }
         Maxi = Math.Max (Maxi, byme);
         Maxi = Math.Max (Maxi, byorder);
         return depth;
      }
      TrackLongest (root, -1, 0);
      return Maxi;
   }
}