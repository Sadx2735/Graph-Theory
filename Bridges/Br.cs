public class SolutionBridges {
   public IList<IList<int>> CriticalConnections (int n, IList<IList<int>> connections) {
      // Discovery time and nearest minimum back edge time.
      int[] Time = new int[n], BackTime = new int[n];
      // To track final results
      IList<IList<int>> result = new List<IList<int>> ();
      // Adjacency matrix
      List<List<int>> Map = new List<List<int>> ();
      for (int i = 0; i < n; i++) Map.Add ([]);
      foreach (var item in connections) {
         Map[item[0]].Add (item[1]);
         Map[item[1]].Add (item[0]);
      }

      // DFS call
      int T = 1;
      (int, int) DFS (int Node, int Parent) {
         // Update the Time and BackTime with the T value
         Time[Node] = T++;
         BackTime[Node] = Time[Node];
         
         // For all the neighbors check if they are not visited if not visit
         // and track the minimum value of there exploration.
         foreach (var nbr in Map[Node]) {
            // Explore and get the minimum.
            if (nbr != Parent && Time[nbr] == 0) {
               var (DiscTime, BcTime) = DFS (nbr, Node);
               if (Time[Node] < BcTime) result.Add (new List<int> () { Node, nbr });
               BackTime[Node] = Math.Min (BackTime[Node], BcTime);
            }
            // If more than one back edge are there we need to get the minimum of them also.
            else if (nbr != Parent && Time[nbr] != 0) {
               BackTime[Node] = Math.Min (BackTime[Node], Time[nbr]);
               // We can also make the condition to be like :
               // ( nbr != Parent ) and changing the inside thing to have BackTime[nbr]
            }
         }
         return (Time[Node], BackTime[Node]);
      }
      DFS (0, -1);
      return result;
   }
}