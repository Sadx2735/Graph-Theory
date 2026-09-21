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

namespace DP;

public class SolutionLC687 {
   public int LongestUnivaluePath (TreeNode root) {
      int maximum = 1;
      (int, int) TrackMaximum (TreeNode node) {
         if (node is null) return (0, -1);
         var (l1, c1) = TrackMaximum (node.left);
         var (l2, c2) = TrackMaximum (node.right);
         if (c1 == c2 && c1 == node.val)
            maximum = Math.Max (maximum, l1 + l2 + 1);
         int l = (c1 == node.val) ? l1 + 1 : 1;
         int r = (c2 == node.val) ? l2 + 1 : 1;
         maximum = Math.Max (maximum, Math.Max (l, r));
         return (Math.Max (l, r), node.val);
      }
      TrackMaximum (root);
      return maximum - 1;
   }
}