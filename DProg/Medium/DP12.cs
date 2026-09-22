
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

public class Solution988 {
   public string SmallestFromLeaf (TreeNode root) {
      List<string> res = [];
      void capture (TreeNode node, string name) {
         if (node is null) return;
         if (node.left is null && node.right is null) {
            string ename = (char)(node.val + 'a') + name;
            res.Add (ename);
            return;
         }
         string nname = (char)(node.val + 'a') + name;
         capture (node.left, nname);
         capture (node.right, nname);
      }
      capture (root, "");
      res.Sort ();
      return res[0];
   }
}