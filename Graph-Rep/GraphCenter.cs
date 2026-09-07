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
}