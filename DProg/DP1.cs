// DP introduction.
// DP must not have the loop for example lets say we have a function
// f(n) = f(n-1) + f(n-2) here in that case this problem wont be solved
// if the f(n-1) depend on the f(n).

// Usual Way of applying Dp over the tree:
// This has 3 kinds of problems involved :
// 1. Top Down approach.
// 2. Bottom Up approach.
// 3. Hybrid approach.

// Way to solve it :
// Step 1 – Ask: "What does my parent need to know about me?"
// for example in the below problem we only need to know what is the max path length
// Step 2 - Write the rules: which child states are allowed under each parent state?
// for example in the below problem if i get pairs then i need to get the max of them
// and do +1 to the maximum result as we can only have the current node connected to
// either of one in the above.
// Step 3 – Base case.
// Decide what a null node returns.
// Step 4 - Try and Verify the things on a small graph through hands.

namespace DP;

public class TreeNode {
   public int val;
   public TreeNode left;
   public TreeNode right;
   public TreeNode (int val = 0, TreeNode left = null, TreeNode right = null) {
      this.val = val;
      this.left = left;
      this.right = right;
   }
}
public class Solution {
   public int MaxDepth (TreeNode root) {
      int CalcDepth (TreeNode node) {
         if (node is null) return 0;
         int l = CalcDepth (node.left);
         int r = CalcDepth (node.right);
         return Math.Max (l, r) + 1;
      }
      return CalcDepth (root);
   }
}