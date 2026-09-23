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

public class Solution437 {
   public int PathSum (TreeNode root, int targetSum) {
      Dictionary<long, int> Mem = [];
      Mem[0] = 1; int result = 0;
      long Tsum = 0;
      void TrackPath (TreeNode node) {
         if (node is null) return; 
         Tsum += node.val;
         long key = Tsum - targetSum;
         if (Mem.TryGetValue (key, out int countx)) result += countx;
         Mem[Tsum] = (Mem.TryGetValue (Tsum, out int count)) ? count + 1 : 1;
         TrackPath (node.left); TrackPath (node.right);
         Mem[Tsum] -= 1; Tsum -= node.val;
      }
      TrackPath (root);
      return result;
   }
}