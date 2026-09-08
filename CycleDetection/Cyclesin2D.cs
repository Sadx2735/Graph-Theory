namespace Grapheex5;
public class SolutionMakex1 {
   public bool ContainsCycle (char[][] grid) {
      bool[][] visited = new bool[grid.Length][];
      for (int i = 0; i < grid.Length; i++) visited[i] = new bool[grid[0].Length];

      int[] delx = { 0, 0, -1, 1 };
      int[] dely = { -1, 1, 0, 0 };

      bool valid (int rv, int cv) {
         if (0 <= rv && rv < grid.Length && 0 <= cv && cv < grid[0].Length) return true;
         return false;
      }

      bool dfs (int r, int c, int rp, int cp, bool[][] visited) {
         visited[r][c] = true;
         char reference = grid[r][c];
         for (int i = 0; i < delx.Length; i++) {
            int newx = r + delx[i], newy = c + dely[i];
            if (valid (newx, newy) && grid[newx][newy] == reference) {
               if (!visited[newx][newy] && dfs (newx, newy, r, c, visited)) {
                  return true;
               } else if (!(newx == rp && newy == cp)) {
                  return true;
               }
            }
         }
         return false;
      }

      for (int i = 0; i < grid.Length; i++) {
         for (int j = 0; j < grid[0].Length; j++) {
            if (!visited[i][j] && dfs (i, j, -1, -1, visited)) {
               return true;
            }
         }
      }
      return false;
   }
}