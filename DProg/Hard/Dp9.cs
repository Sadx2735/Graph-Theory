

using DP;

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

public class Solution124 {
   public int MaxPathSum (TreeNode root) {
      int maxi = int.MinValue;
      int Down (TreeNode node) {
         if (node is null) return 0;
         int l = Math.Max (0, Down (node.left));
         int r = Math.Max (0, Down (node.right));
         maxi = Math.Max (maxi, node.val + l + r);
         return node.val + Math.Max (l, r);
      }
      Down (root);
      return maxi;
   }
}