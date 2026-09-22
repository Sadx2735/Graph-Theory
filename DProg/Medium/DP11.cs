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

public class Solution1026 {
   public int MaxAncestorDiff (TreeNode root) {
      int result = 0;
      (int, int) GetMaximum (TreeNode node, int maximum, int minimum) {
         if (node is null) return (maximum, minimum);
         var (r11, r12) = GetMaximum (node.left, Math.Max (maximum, node.val), Math.Min (minimum, node.val));
         var (r21, r22) = GetMaximum (node.right, Math.Max (maximum, node.val), Math.Min (minimum, node.val));
         result = Math.Max (result, Math.Max (Math.Abs (r11 - r12), Math.Abs (r21 - r22)));
         return (Math.Max (maximum, node.val), Math.Min (minimum, node.val));
      }
      var res = GetMaximum (root, Int32.MinValue, Int32.MaxValue);
      return result;
   }
}