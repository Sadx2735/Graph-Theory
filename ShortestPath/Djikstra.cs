namespace Shortestpath;
class DijkstraPathFinder {
   static void MainDPath () {
      // Dijkstra algorithm
      var graph = new List<List<(int to, int weight)>>
      {
          new List<(int, int)> { (1, 10), (2, 3) },
          new List<(int, int)> { (2, 1), (3, 2) },
          new List<(int, int)> { (1, 4), (3, 8), (4, 2) },
          new List<(int, int)> { (4, 7) },
          new List<(int, int)> { (3, 9) }
      };

      int[] exptime = new int[graph.Count];
      Array.Fill (exptime, int.MaxValue);
      exptime[0] = 0;

      PriorityQueue<(int, int), int> Pq = new ();
      Pq.Enqueue ((0, 0), 0);

      while (Pq.Count > 0) {
         var (element, distance) = Pq.Dequeue ();
         if (exptime[element] < distance) continue;
         Console.WriteLine ($"Element {element} : {distance}");
         foreach (var (nbr, dist) in graph[element]) {
            int newDist = dist + distance;
            if (exptime[nbr] > newDist) {
               exptime[nbr] = newDist;
               Pq.Enqueue ((nbr, newDist), newDist);
            }
         }
      }

      int cnt = 0;
      foreach (var item in exptime)
         Console.WriteLine ($"To go to {cnt++} needs {item}");
   }
}