namespace MSTree;

class ProgramMST {
   static void Main() {
      // Refer Dia2 in Media folder.
      List<List<(int, int)>> AdjList = new List<List<(int, int)>> ();
      int N = 4;
      for (int i = 0; i < N; i++) AdjList.Add (new ());
      AdjList[0].AddRange (new List<(int, int)> () { (1, 10), (2, 6), (3, 5) });
      AdjList[1].AddRange (new List<(int, int)> () { (0, 10), (3, 15) });
      AdjList[2].AddRange (new List<(int, int)> () { (0, 6), (3, 4) });
      AdjList[3].AddRange (new List<(int, int)> () { (0, 5), (1, 15), (2, 4) });

      for (int i = 0; i < N; i++) {
         foreach (var (nbr, weight) in AdjList[i]) {
            Console.WriteLine ($"Fr : {i} , To : {nbr} , W : {weight}");
         }
      }

      // To Track
      bool[] visited = new bool[N];
      PriorityQueue<(int, int), int> pQ = new ();
      pQ.Enqueue ((-1, 0), 0);

      while (pQ.Count > 0) {
         var result = pQ.Dequeue ();
         var (parent, node) = (result.Item1, result.Item2);

         // If they have been marked as visited.
         if (visited[node]) continue;
         visited[node] = true;

         if (parent != -1) Console.WriteLine ($"{parent} => {node}");

         // Explore the unexplored neighbours
         foreach (var (nbr, weight) in AdjList[node]) {
            if (!visited[nbr]) {
               pQ.Enqueue ((node, nbr), weight);
            }
         }
      }
   }
}