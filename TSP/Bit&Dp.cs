class ProgramTSPx2 {
   static void MainTSPx2 (string[] args) {

      const int NodeCount = 4;

      int[,] costMatrix = new[,]
      {
          {  0, 20, 42, 25 },
          { 20,  0, 30, 34 },
          { 42, 30,  0, 10 },
          { 25, 34, 10,  0 }
      };

      Dictionary<(int Node, int Mask), int> memo = [];
      int optimalCost = TravellingSalesMan (currentNode: 0, visitedMask: 1);
      Console.WriteLine ($"Optimal Tour Cost: {optimalCost}");

      int TravellingSalesMan (int currentNode, int visitedMask) {
         if (visitedMask == (1 << NodeCount) - 1) {
            return costMatrix[currentNode, 0];
         }

         var state = (currentNode, visitedMask);
         if (memo.TryGetValue (state, out int cachedScore)) {
            return cachedScore;
         }

         int minScore = int.MaxValue;
         for (int nextNode = 0; nextNode < NodeCount; nextNode++) {
            if ((visitedMask & 1 << nextNode) == 0) {
               int nextMask = visitedMask | 1 << nextNode;
               int branchCost = TravellingSalesMan (nextNode, nextMask)
                  + costMatrix[currentNode, nextNode];
               minScore = Math.Min (minScore, branchCost);
            }
         }
         return memo[state] = minScore;
      }
   }
}