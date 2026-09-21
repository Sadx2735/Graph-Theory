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
public class Solution110 {
   public bool IsBalanced (TreeNode root) {
      bool cond = false;
      int Depth (TreeNode node) {
         if (node is null) return 0;
         int l = Depth (node.left);
         int r = Depth (node.right);
         if (Math.Abs (l - r) > 1) cond = true;
         return Math.Max (l, r) + 1;
      }
      Depth (root);
      return !cond;
   }
}