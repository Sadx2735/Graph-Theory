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
public class Solution543 {
   public int DiameterOfBinaryTree (TreeNode root) {
      int maximum = 0;
      int dfs (TreeNode node) {
         if (node is null) return 0;
         int l1 = dfs (node.left);
         int l2 = dfs (node.right);
         maximum = Math.Max (maximum, l1 + l2);
         return Math.Max (l1, l2) + 1;
      }
      dfs (root);
      return maximum;
   }
}