
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

public class Solution100 {
   public bool IsSameTree (TreeNode p, TreeNode q) {
      bool IsSame (TreeNode p, TreeNode q) {
         if (p is null && q is null) return true;
         if (p is null && q is not null) return false;
         if (p is not null && q is null) return false;
         if ((p.val == q.val) && IsSame (p.left, q.left) && IsSame (p.right, q.right))
            return true;
         return false;
      }
      return IsSame (p, q);
   }
}