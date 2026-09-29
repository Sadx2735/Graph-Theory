internal class ProgramRMQ {
   private static void MainRMQ (string[] args) {
      int[] NUMS = [1, 2, 0, 6, 7, 7, 3];
      int LOGN = (int)Math.Log2 (NUMS.Length) + 1;

      Console.WriteLine ($"There are {NUMS.Length} elements");
      Console.WriteLine ($"Max storage req is {LOGN}");

      // O(N-LOG(N))
      int[][] MAP = new int[NUMS.Length][];
      for (int i = 0; i < NUMS.Length; i++) MAP[i] = new int[LOGN];

      // from me to me + 2^0 is 1. the minimum sum is me only
      for (int i = 0; i < NUMS.Length; i++) MAP[i][0] = NUMS[i];

      // The for each of the nested ones.
      for (int j = 1; j < LOGN; j++) {
         // for capturing 4 units minimum its
         // minimum from me to 2. then from me+2 to ([me+2]+2) => me+4
         for (int i = 0; i + (1 << j) <= NUMS.Length; i++) {
            MAP[i][j] = Math.Min (MAP[i][j - 1], MAP[i + (1 << j - 1)][j - 1]);
         }
      }

      // Query
      Console.WriteLine ($"Minimum between this range of (1,1) is {AnswerQuery (1, 1)}");
      Console.WriteLine ($"Minimum between this range of (0,6) is {AnswerQuery (0, 6)}");

      // Q*O(1)
      int AnswerQuery (int l, int r) {
         int LENGTH = r - l + 1;
         // Say we have 6 elements (2,8)
         // we can split this by 2=>6 and 4=>8;
         int LOG = (int)Math.Log2 (LENGTH);
         int start2 = r - (1 << LOG) + 1;
         return Math.Min (MAP[l][LOG], MAP[start2][LOG]);
      }
   }
}
