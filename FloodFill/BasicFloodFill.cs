namespace FFill;

class ProgramFill {
   static int[,] Matrix = new int[,] {
         {1,0,0,0,0,0,0,1 },
         {1,1,0,0,1,0,1,1 },
         {1,1,0,0,0,0,1,1 },
         {0,0,0,0,0,0,0,1 },
         {0,0,0,0,0,0,0,1 },
         {0,0,1,0,0,0,0,1 },
         {1,1,1,0,0,0,1,1 },
         {1,1,0,0,0,0,0,1 },
   };

   static int[] Deltax = { 0, 0, -1, 1 };
   static int[] Deltay = { -1, 1, 0, 0 };

   static int N = Matrix.GetLength (0);
   static int M = Matrix.GetLength (1);

   static void MainFill () {
      FindIslands ();
      FillColor ();
      FindBiggestIsland ();
   }

   // fills an Spreads out in all directions from the given point and changes there color
   // In the below example lets keep the 0's to be land.
   // Now we are gonna color each Island to a color ( something like how Paint app colors )
   static void Fill (int[,] Matrixcpy, int i, int j, int c) {
      Matrixcpy[i, j] = c;
      for (int p = 0; p < 4; p++) {
         int nx = i + Deltax[p], ny = j + Deltay[p];
         if (0 <= nx && nx < N && 0 <= ny && ny < M) {
            if (Matrixcpy[nx, ny] == 1) Fill (Matrixcpy, nx, ny, c);
         }
      }
   }

   static void FindIslands () {
      int cnt = 0;
      var Mat = (int[,])(Matrix.Clone ());
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (Mat[i, j] == 1) {
               Fill (Mat, i, j, 0);
               cnt++;
            }
         }
      }
      Console.WriteLine ();
      Console.WriteLine ($"There are {cnt} Islands");
      Console.WriteLine ();
      PrintMatrix (Mat);
   }

   static void FillColor () {
      int cnt = 2;
      var Mat = (int[,])(Matrix.Clone ());
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (Mat[i, j] == 1) {
               Fill (Mat, i, j, cnt);
               cnt++;
            }
         }
      }
      Console.WriteLine ();
      Console.WriteLine ($"Color Filling Completed..");
      Console.WriteLine ();
      PrintMatrix (Mat);
   }

   static void PrintMatrix (int[,] Matrix) {
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            Console.Write (j == M - 1 ? $"{Matrix[i, j]}" : $"{Matrix[i, j]},");
         }
         Console.WriteLine ();
      }
   }

   static void FindBiggestIsland () {
      int cnt = 0;
      var Mat = (int[,])(Matrix.Clone ());
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (Mat[i, j] == 1) {
               cnt = Math.Max (cnt, TrackSize (Mat, i, j, 2));
            }
         }
      }
      Console.WriteLine ();
      Console.WriteLine ($"Biggest Island is of Size {cnt}");
      Console.WriteLine ();

      static int TrackSize (int[,] Matrixcpy, int i, int j, int c) {
         int cnt = 1;
         Matrixcpy[i, j] = c;
         for (int p = 0; p < 4; p++) {
            int nx = i + Deltax[p], ny = j + Deltay[p];
            if (0 <= nx && nx < N && 0 <= ny && ny < M) {
               if (Matrixcpy[nx, ny] == 1) {
                  cnt += TrackSize (Matrixcpy, nx, ny, c);
               }
            }
         }
         return cnt;
      }
   }
}