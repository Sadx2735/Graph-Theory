public class SolutionLc994 {
   public int OrangesRotting (int[][] grid) {
      Queue<(int, int, int)> MyQue = [];
      int goodOranges = 0;
      int N = grid.Length, M = grid[0].Length;
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (grid[i][j] == 2) MyQue.Enqueue ((i, j, 0));
            else if (grid[i][j] == 1) goodOranges += 1;
         }
      }

      int[] Delx = { 0, 0, -1, 1 };
      int[] Dely = { -1, 1, 0, 0 };

      bool valid (int xx, int yy) => (0 <= xx && xx < N && 0 <= yy && yy < M);

      while (MyQue.Count > 0) {
         var (i, j, time) = MyQue.Dequeue ();
         for (int p = 0; p < 4; p++) {
            int xx = Delx[p] + i;
            int yy = Dely[p] + j;
            if (valid (xx, yy) && grid[xx][yy] == 1) {
               goodOranges -= 1;
               grid[xx][yy] = 0;
               if (goodOranges == 0) return time + 1;
               MyQue.Enqueue ((xx, yy, time + 1));
            }
         }
      }
      return (goodOranges == 0) ? 0 : -1;
   }
}