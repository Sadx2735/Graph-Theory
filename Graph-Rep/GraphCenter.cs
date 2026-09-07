namespace Graphee;
class Graphify {
   static void Runme (string[] args) {
      List<(int, int)> edges = [(1, 2), (5, 1), (1, 3), (1, 4)];
      Console.WriteLine (FindCentre (edges));
      int FindCentre (List<(int, int)> edgmap) {
         Dictionary<int, int> freq = [];
         foreach (var (u, v) in edgmap) {
            if (freq.ContainsKey (u)) return u;
            if (freq.ContainsKey (v)) return v;
            freq[u] = 0; freq[v] = 0;
         }
         return -1;
      }
   }

   static void Runmex2 () {
      int n = 5;
      List<(int, int)> roads = [(0, 1), (0, 3), (1, 2), (1, 3), (2, 3), (2, 4)];
      int[,] Map = new int[n, n];
      Dictionary<int, int> Freq = [];
      foreach (var item in roads) {
         int u = item.Item1, v = item.Item2;
         Map[u, v] = 1; Map[v, u] = 1;
         if (Freq.ContainsKey (u)) Freq[u]++;
         else Freq[u] = 1;
         if (Freq.ContainsKey (v)) Freq[v]++;
         else Freq[v] = 1;
      }
      int Maximum = 1 << 31;
      for (int i = 0; i < n; i++) {
         for (int j = 0; j < n; j++) {
            if (i == j) continue;
            Maximum = Math.Max (Maximum, Freq[i] + Freq[j] - Map[i, j]);
         }
      }
      Console.WriteLine (Maximum);
   }
}