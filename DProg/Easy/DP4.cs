
namespace DP;

public class Solution112 {
   public bool HasPathSum (TreeNode root, int targetSum) {
      bool cond = false;
      MaxSum (root, targetSum);
      return cond;

      // if its a leaf then check if the needed is 0. 
      bool isLeaf (TreeNode node) => ((node.left is null) && (node.right is null));

      // take the current elements value and push it downwards to the childs
      // if the child(which is a leaf) has the target value of 0. then we have an answer. 
      void MaxSum (TreeNode node, int target) {
         if (node is null) return;
         if (isLeaf (node)) {
            if ((target - node.val) == 0) { cond = true; }
            return;
         }
         MaxSum (node.left, target - node.val);
         MaxSum (node.right, target - node.val);
      }
   }
}