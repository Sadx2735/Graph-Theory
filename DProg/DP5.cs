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

public class Solution101 {
   public bool IsSymmetric (TreeNode root) {
      bool CheckifSame (TreeNode n1, TreeNode n2) {
         if (n1 is null && n2 is not null) return false;
         if (n1 is not null && n2 is null) return false;
         if (n1 is null && n2 is null) return true;
         if (n1.val != n2.val) return false;
         return CheckifSame (n1.left, n2.right) && CheckifSame (n1.right, n2.left);
      }
      return CheckifSame (root, root);
   }
}