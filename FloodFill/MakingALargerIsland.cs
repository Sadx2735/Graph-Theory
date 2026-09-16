public class SolutionLC827 {
   public int LargestIsland (int[][] grid) {
      // Size and Starting Color
      int N = grid.Length;
      int M = grid[0].Length;
      int C = 2;

      // Adjacent Movements
      int[] Delx = { 0, 0, -1, 1 };
      int[] Dely = { -1, 1, 0, 0 };

      // True if its within the Boundary
      bool valid (int xx, int yy) => (0 <= xx && xx < N && 0 <= yy && yy < M);

      // fills and returns the size of the considered Island
      int fill (int i, int j, int c) {
         grid[i][j] = c;
         int cnt = 1;
         for (int p = 0; p < 4; p++) {
            int xx = Delx[p] + i;
            int yy = Dely[p] + j;
            if (valid (xx, yy)) {
               if (grid[xx][yy] == 1) {
                  cnt += fill (xx, yy, c);
               }
            }
         }
         return cnt;
      }

      // Tracks the size and fills color
      Dictionary<int, int> Rel = [];
      int final = 0;
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (grid[i][j] == 1) {
               int size = fill (i, j, C);
               final = Math.Max (final, size);
               Rel[C] = size;
               C++;
            }
         }
      }

      // At each point we check how many unique islands the considered point be connecting if made
      // into one so the size becomes Sum-of-Size-of(Unique Islands) + 1
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (grid[i][j] == 0) {
               // To keep track of the unique Islands
               HashSet<int> hs = [];
               int score = 0;
               // Check if the grid point is connecting a island.
               for (int p = 0; p < 4; p++) {
                  int nxx = Delx[p] + i;
                  int nyy = Dely[p] + j;
                  if (valid (nxx, nyy) && grid[nxx][nyy] != 0 && !hs.Contains (grid[nxx][nyy])) {
                     hs.Add (grid[nxx][nyy]);
                     score += Rel[grid[nxx][nyy]];
                  }
               }
               final = Math.Max (final, score + 1);
            }
         }
      }
      return final;
   }
}