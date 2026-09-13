internal class ProgramFloydWarshell {
   private static void MainWarshell () {
      // for Media/Dia3.png
      int N = 4;
      int[][] Map = new int[N][];
      for (int i = 0; i < N; i++) Map[i] = new int[N];
      for (int i = 0; i < N; i++) Array.Fill (Map[i], int.MaxValue);
      for (int i = 0; i < N; i++) Map[i][i] = 0;
      Map[0][1] = 3; Map[1][0] = 2;
      Map[0][3] = 5; Map[1][3] = 4;
      Map[3][2] = 2; Map[2][1] = 6;

      // for every intermediate step k we check if there is an path
      // that is travelling from i->j via k.
      for (int k = 0; k < N; k++) {
         for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
               if (Map[i][k] == int.MaxValue || Map[k][j] == int.MaxValue)
                  continue;
               Map[i][j] = Math.Min (Map[i][j], Map[i][k] + Map[k][j]);
            }
         }
      }

      // Prints
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < N; j++) {
            Console.WriteLine ($"To Go from {i} to {j} the distance is {Map[i][j]}");
         }
      }
   }
}