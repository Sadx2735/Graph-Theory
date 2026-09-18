class ProgramScc {
   static void main (String[] args) {
      int N = 5;
      List<List<int>> AdjList = [];
      for (int i = 0; i < N; i++) AdjList.Add ([]);

      void AddMe (int u, int v, bool bidir = false) {
         AdjList[u].Add (v);
         if (bidir) AdjList[v].Add (u);
      }

      AddMe (2, 1); AddMe (2, 0);
      AddMe (0, 1); AddMe (1, 4);
      AddMe (1, 3); AddMe (0, 4);

      List<int> Result = [];
      bool[] visited = new bool[N];

      void DFS (int Node) {
         visited[Node] = true;
         foreach (var nbr in AdjList[Node]) {
            if (!visited[nbr]) {
               DFS (nbr);
            }
         }
         Result.Add (Node);
      }

      DFS (0);
      for (int i = 0; i < N; i++) {
         if (!visited[i]) DFS (i);
      }

      Result.Reverse ();
      foreach (var item in Result.SkipLast (1)) Console.Write ($"{item}=>");
      Console.Write ($"{Result[^1]}");
   }
}

// Were ever we start this from this is gonna work perfectly for example
// in the case like this 3->1->2->0.
// lets say we begin from 1 so what happens is 0,2,1 will be there and then when coming to
// the value of 3. it will be added at the end : 0,2,1,3 so reversing it would give correct 
// value (so order of call doesn't matter )