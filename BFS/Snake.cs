public class Solution {
   public int SnakesAndLadders (int[][] board) {
      // Inits
      int N = board[0].Length, cnt = 0;
      int[] Linearized = new int[N * N];
      // Board to (0,N*N-1)
      while (cnt < N) {
         if (cnt % 2 == 0) {
            for (int i = 0; i < N; i++) {
               int value = board[N - 1 - cnt][i];
               Linearized[cnt * N + i] = (value == -1) ? -1 : value - 1;
            }
         } else {
            for (int i = 0; i < N; i++) {
               int value = board[N - 1 - cnt][N - 1 - i];
               Linearized[cnt * N + i] = (value == -1) ? -1 : value - 1;
            }
         }
         cnt++;
      }
      // Printing
      // for(int idx=0;idx<N*N;idx++) Console.WriteLine($"At {idx} : {Linearized[idx]}");
      int[] Score = new int[N * N];
      Array.Fill (Score, Int32.MaxValue);

      bool[] Visited = new bool[N * N];
      Queue<(int, int)> Q = [];
      Q.Enqueue ((0, 0));
      Visited[0] = true;
      if (Linearized[0] != -1) {
         Q.Enqueue ((0, Linearized[0]));
         Visited[Linearized[0]] = true;
      }

      while (Q.Count > 0) {
         var (dist, ele) = Q.Dequeue ();
         //Console.WriteLine($"{dist} {ele}");
         if (ele == N * N - 1) return dist;
         for (int move = 1; move <= 6; move++) {
            if ((ele + move) >= N * N) break;
            int val = Linearized[ele + move];
            if (val != -1 && !Visited[val]) {
               Q.Enqueue ((dist + 1, val));
               Visited[val] = true;
            } else if (val == -1 && !Visited[ele + move]) {
               Q.Enqueue ((dist + 1, ele + move));
               Visited[ele + move] = true;
            }
         }
      }
      return -1;
   }
}