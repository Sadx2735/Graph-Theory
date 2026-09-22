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

public class Solution1448 {
   public int GoodNodes (TreeNode root) {
      int countme (TreeNode node, int maximum) {
         if (node is null) return 0;
         int nmax = Math.Max (maximum, node.val);
         int l = countme (node.left, nmax);
         int r = countme (node.right, nmax);
         return l + r + ((node.val >= nmax) ? 1 : 0);
      }
      return countme (root, Int32.MinValue);
   }
}