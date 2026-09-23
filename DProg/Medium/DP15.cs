
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
namespace DP;

public class Solution113 {
   public IList<IList<int>> PathSum (TreeNode root, int targetSum) {
      IList<IList<int>> Result = new List<IList<int>> ();
      void TrackMe (TreeNode node, List<int> result, int target) {
         if (node is null) return;
         if (node.left is null && node.right is null) {
            result.Add (node.val);
            if (target - node.val == 0) Result.Add (new List<int> (result));
            result.RemoveAt (result.Count - 1);
            return;
         }
         result.Add (node.val);
         TrackMe (node.left, result, target - node.val);
         TrackMe (node.right, result, target - node.val);
         result.RemoveAt (result.Count - 1);
      }
      TrackMe (root, new List<int> (), targetSum);
      return Result;
   }
}