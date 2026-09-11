public class Solutionx {
   public int NumIslands (char[][] grid) {
      int M = grid.Length, N = grid[0].Length;
      bool[,] Visited = new bool[M, N];

      bool Valid (int r, int c) {
         if ((0 <= r) && (r < M) && (0 <= c) && (c < N)) {
            return true;
         }
         return false;
      }

      void DFS (int r, int c) {
         Visited[r, c] = true;
         char reference = grid[r][c];
         int[] delx = { 0, 0, -1, 1 };
         int[] dely = { -1, 1, 0, 0 };
         for (int i = 0; i < 4; i++) {
            int newx = r + delx[i];
            int newy = c + dely[i];
            if (Valid (newx, newy) && grid[newx][newy] == reference && !Visited[newx, newy]) {
               DFS (newx, newy);
            }
         }
      }
      int result = 0;
      for (int row = 0; row < M; row++) {
         for (int col = 0; col < N; col++) {
            if (Visited[row, col]) continue;
            if (grid[row][col] == '1') { DFS (row, col); result += 1; }
            ;
         }
      }
      return result;
   }
}