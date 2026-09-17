class Program01BFS {
   private static void Main01BFS (string[] args) {
      // 0.1 BFS 
      LinkedList<(int, int, int)> deque = [];
      deque.AddFirst ((0, 0, 0));

      int[][] grid = [
         [1,1,1,1],
         [2,2,2,2],
         [1,1,1,1],
         [2,2,2,2],
         [1,1,1,1]
      ];
      grid[0][0] *= -1;

      int[] Delx = { 0, 0, -1, 1 };
      int[] Dely = { -1, 1, 0, 0 };
      int N = grid.Length, M = grid[0].Length;

      bool valid (int xx, int yy) => 0 <= xx && xx < N && 0 <= yy && yy < M;

      Dictionary<(int, int), int> movements = [];
      movements[(0, 1)] = 1; movements[(0, -1)] = 2;
      movements[(1, 0)] = 3; movements[(-1, 0)] = 4;

      while (deque.Count > 0) {
         var (rv, cv, tv) = deque.First.Value;
         deque.RemoveFirst ();
         if (rv == N - 1 && cv == M - 1) {
            break;
         }
         for (int p = 0; p < 4; p++) {
            int xx = Delx[p] + rv;
            int yy = Dely[p] + cv;
            if (valid (xx, yy) && grid[xx][yy] > 0) {
               // If it's going to be more, just put it at the back.
               // If it's going to be 0, then add it at the front.
               // We go layer by layer, i.e., only after processing all the 0s do we move to 1.
               // In that case, we will have 0s followed by 1s. Here, (1, 2, 1) is not possible
               // because the 0s are evaluated first and 1s are added at the back. So, we only
               // have 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2.
               if (movements[(Delx[p], Dely[p])] == -grid[rv][cv]) {
                  grid[xx][yy] *= -1;
                  deque.AddFirst ((xx, yy, tv));
               } else {
                  grid[xx][yy] *= -1;
                  deque.AddLast ((xx, yy, tv + 1));
               }
            }
         }
      }
   }
}

// This cant be done for any value of N and N+1.
// for example if we do d+N and d+N+1.
// and then take the d+N and add d+N+N and put it into the first
// then d+2N > d+N+1 so its no longer sorted.