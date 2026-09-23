namespace DP;
public class Solution198 {
   public int Rob (int[] nums) {
      Dictionary<int, int> Mem = [];
      int Maxi (int idx) {
         if (idx >= nums.Length) return 0;
         if (Mem.ContainsKey (idx)) return Mem[idx];
         return Mem[idx] = Math.Max (nums[idx] + Maxi (idx + 2), Maxi (idx + 1));
      }
      return Maxi (0);
   }
}