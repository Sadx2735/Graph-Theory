// Travelling SalesMan Problem
class ProgramTSP {
   static void MainTSP (string[] args) {

      int Node = 4;
      int[,] CostMat = new int[,] {
         {0,20,42,25},
         {20,0,30,34},
         {42,30,0,10},
         {25,34,10,0}
      };

      int TravellingSalesMan (int N, HashSet<int> Visited) {
         if (Visited.Count == Node) return CostMat[N, 0];
         int Score = int.MaxValue;
         for (int i = 0; i < Node; i++) {
            if (!Visited.Contains (i)) {
               Visited.Add (i);
               Score = Math.Min (Score, TravellingSalesMan (i, Visited) + CostMat[N, i]);
               Visited.Remove (i);
            }
         }
         return Score;
      }

      var Visited = new HashSet<int> ();
      Visited.Add (0);
      Console.WriteLine (TravellingSalesMan (0, Visited));
   }
}