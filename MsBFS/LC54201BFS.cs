// Given an m x n binary matrix mat, return the distance of the nearest 0 for each cell.
// The distance between two cells sharing a common edge is 1.
// We can solve this by looping through every point in pixel and then spreading across from there
// and checking the distance with every other thing with Tc of (M^2*N^2).
// but with multisource bfs we can solve this problem in M*N Tc.

class ProgramMBFS {
   static void MainMBFS (string[] args) {
      int[][] mat = [[0, 0, 0], [0, 1, 0], [1, 1, 1]];
      var ans = UpdateMatrix (mat);

      int N = ans.Length, M = ans[0].Length;
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            Console.Write ($"{ans[i][j]} ");
         }
         Console.WriteLine ();
      }

      int[][] UpdateMatrix (int[][] Matrix) {
         // Grid boundary.
         int N = Matrix.Length, M = Matrix[0].Length;
         int[][] Result = new int[N][];

         // Adjacent grid points
         int[] Delx = { 0, 0, -1, 1 };
         int[] Dely = { -1, 1, 0, 0 };

         for (int i = 0; i < N; i++) Result[i] = new int[M];
         Queue<(int, int, int)> Que = [];
         for (int i = 0; i < N; i++) {
            for (int j = 0; j < M; j++) {
               Result[i][j] = -1;
               if (Matrix[i][j] == 0) {
                  Que.Enqueue ((i, j, 0));
                  // Just to make sure that no other nbr puts this into the queue.
                  Result[i][j] = 0;
               }
            }
         }

         // Get one by one out and do the processing.
         while (Que.Count > 0) {
            var (rv, cv, tv) = Que.Dequeue ();
            for (int p = 0; p < 4; p++) {
               int xx = Delx[p] + rv;
               int yy = Dely[p] + cv;
               if (valid (xx, yy) && Result[xx][yy] == -1) {
                  Result[xx][yy] = tv + 1;
                  Que.Enqueue ((xx, yy, tv + 1));
               }
            }
         }
         return Result;

         // true if inside the bound else false;
         bool valid (int xx, int yy) => 0 <= xx && xx < N && 0 <= yy && yy < M;
      }
   }
}