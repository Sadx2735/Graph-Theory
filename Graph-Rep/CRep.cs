namespace Graphx;

class CRepresenation {
   static void RunMe (string[] args) {
      string[] cities = new string[] { "bombay", "chennai", "kolkata", "cochin", "calicut" };
      ComplexGraph Graphee = new (cities);
      Graphee.AddEdge ("bombay", "chennai", true);
      Graphee.AddEdge ("bombay", "kolkata", true);
      Graphee.AddEdge ("bombay", "calicut", true);
      Graphee.AddEdge ("cochin", "chennai", true);
      Graphee.PrintAdjList ();
   }
}
class ComplexGraph {
   int N;
   Dictionary<string, List<string>> Map;
   public ComplexGraph (string[] cities) {
      this.N = cities.Length;
      this.Map = new Dictionary<string, List<string>> ();
      for (int idx = 0; idx < N; idx++)
         Map[cities[idx]] = new List<string> ();
   }
   public void AddEdge (string from, string to, bool UnDir = true) {
      Map[from].Add (to);
      if (UnDir) Map[to].Add (from);
   }
   public void PrintAdjList () {
      foreach (var key in Map.Keys) {
         Console.Write ($"{key} => ");
         foreach (var val in Map[key]) {
            Console.Write ($"{val},");
         }
         Console.WriteLine ();
      }
   }
}