class DinicsAlgorithmMaxFlow {
   static void Main (string[] args) {
      
      int N = 4;
      int[][] Map = new int[N][];
      
      for (int i = 0; i < N; i++) Map[i] = new int[N];

      Edit (0, 1, 2);
      Edit (0, 2, 2);
      Edit (1, 2, 3);
      Edit (1, 3, 2);
      Edit (2, 3, 1);


      int fres = 0;
      while (true) {
         var Levels = FillLevels ();
         if (Levels[N - 1] == 0) break;
         while (true) {
            int res = DFS (Levels);
            if (res == 0) break;
            fres += res;
         }
      }
      Console.WriteLine ($"Max Flow by Dinics Algorithm is {fres}");


      void Edit (int from, int to, int v) { Map[from][to] = v; }

      int[] FillLevels () {
         bool[] visited = new bool[N];
         int[] Levels = new int[N];
         Queue<(int, int)> Que = [];
         Que.Enqueue ((0, 0)); visited[0] = true;
         while (Que.Count > 0) {
            var (element, level) = Que.Dequeue ();
            Levels[element] = level;
            if (element == N - 1) continue;
            for (int nbr = 0; nbr < N; nbr++) {
               if (visited[nbr] || Map[element][nbr] == 0) continue;
               visited[nbr] = true;
               Que.Enqueue ((nbr, level + 1));
            }
         }
         return Levels;
      }

      int DFS (int[] Levels) {
         bool[] visited = new bool[N];
         int DFShelper (int node, int minimum) {
            if (node == N - 1) return minimum;
            visited[node] = true;
            for (int it = 0; it < N; it++) {
               if (!visited[it] && Levels[node] + 1 == Levels[it] && Map[node][it] > 0) {
                  int r = DFShelper (it, Math.Min (minimum, Map[node][it]));
                  if (r == 0) continue;
                  Map[node][it] -= r;
                  Map[it][node] += r;
                  return r;
               }
            }
            return 0;
         }
         return DFShelper (0, int.MaxValue);
      }
   }
}