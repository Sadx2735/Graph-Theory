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

public class Solution337 {

   // So basically its something like.
   // for the current node considered 
   // 1) We can include me and max of grandchild (or) you can just not do anything and move the child
   // if the have the better score than including me.
   // 2) And at the same time we can NotInclude the current Node and take the child sum.
   // or if the grandchild have better sum.

   public int Rob (TreeNode root) {
      (int, int) MaxiSum (TreeNode node) {
         if (node is null) return (0, 0);
         var (lft, lftgc) = MaxiSum (node.left);
         var (rht, rhtgc) = MaxiSum (node.right);
         int NotIncludingMe = Math.Max (lft + rht, lftgc + rhtgc);
         int IncludingMe = Math.Max (NotIncludingMe, lftgc + rhtgc + node.val);
         return (IncludingMe, NotIncludingMe);
      }
      var (r1, r2) = MaxiSum (root);
      return Math.Max (r1, r2);
   }
}