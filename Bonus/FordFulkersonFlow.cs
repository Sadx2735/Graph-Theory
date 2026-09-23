/* This is gonna be about a short code and explanation about
 * how i understood this ford fulkerson algorithm (from my intuition, corrected after checking).
 *
 * THE IDEA
 * we have pipes with a limit. water starts at source and we want the max that can reach the sink.
 * the algo is basically "find a path via brute force (dfs) and remove possible flow from it" and repeat
 * until there is no non zero path from source to sink. every successful removal is a real amount of water
 * that reached the sink, so we just add them up. order doesnt matter for the final answer, only for speed.
 *
 * WHY WE NEED THE BACK EDGE (this is the part i had wrong)
 * take the graph in FF.png. source=>A 2, source=>B 2, A=>B 3, A=>sink 2, B=>sink 1.
 * if dfs first goes source=>A=>B=>sink it uses 1 and now B=>sink is full. later the 2 sitting at source=>B
 * has nowhere to go because B=>sink is 0. but the real answer is 3 not 2. the fix is: the 1 that went
 * A=>B=>sink should not have gone through B, it should have gone A=>sink directly so that B=>sink is free
 * for the water from source=>B. so we need a way to say "cancel that A=>B decision". thats the back edge.
 * Map[B][A] holds how much is currently flowing A=>B, and a path is allowed to walk B=>A to cancel it.
 * no water actually flows backwards. it just gets rerouted. every arrow still points forward at the end.
 *
 * SO EACH EDGE HAS TWO NUMBERS
 * Map[u][v] = how much MORE can go u=>v (remaining)
 * Map[v][u] = how much is CURRENTLY going u=>v (and can be cancelled)
 * they always add up to the original limit. pushing val moves val from one to the other.
 *
 * WHY WE CANT JUST MAKE IT UNDIRECTED
 * 1. one shared number per edge can only go down. it can never reopen a slot for a reroute, so the
 *    diamond case (source=>A 1, source=>B 1, A=>B 1, A=>sink 1, B=>sink 1) gives 1 instead of 2.
 * 2. it lets water go against a one way pipe. eg source=>A 1, B=>A 1, B=>sink 1. the real max flow is 0
 *    (A has no way to reach sink) but undirected dfs finds source=>A=>B=>sink and says 1. wrong.
 * so the reverse direction must START at 0 and only grow when we push forward.
 *
 * WHY IT STOPS AT THE RIGHT ANSWER
 * when dfs cant find a path, look at the nodes it could still reach from source. every edge leaving that
 * group is full (else dfs would have crossed it) and every edge entering it carries 0 (else the back edge
 * would let dfs cross). so all the flow is squeezing through a full wall and nobody can beat a full wall.
 * for FF.png the wall ends up being source=>A (2) + B=>sink (1) = 3. thats why max is 3 and not 4
 * even though source has 4 coming out.
 *
*/

namespace Graph.Bonus;

class FordFulkersonFlow {
   static void MainFlow () {
      int N = 4;
      int[][] Map = new int[N][];
      for (int i = 0; i < N; i++) Map[i] = new int[N];

      // FF.png : 0 = source, 1 = A, 2 = B, 3 = sink
      Edit (0, 1, 2);
      Edit (0, 2, 2);
      Edit (1, 2, 3);
      Edit (1, 3, 2);
      Edit (2, 3, 1);

      bool[] visited = new bool[N];
      int result = 0;
      int iteration = 1;

      PrintBoard ("start");
      while (true) {
         visited = new bool[N];                   // fresh for every path search
         int r = DFS (0, Int32.MaxValue);         // r = bottleneck of the path found, 0 if none
         if (r == 0) break;                       // no non zero path from source to sink -> done
         result += r;                             // add ONCE per path (fix 3)
         PrintBoard ($"iteration {iteration++} pushed {r}, total {result}");
      }
      Console.WriteLine ($"Max Value is {result}");

      // finds one path from node to sink, pushes the bottleneck along it, returns the bottleneck
      int DFS (int node, int min) {
         if (node == N - 1) return min;           // reached sink, min is the bottleneck of this path
         visited[node] = true;
         for (int nbr = 0; nbr < N; nbr++) {
            if (visited[nbr] || Map[node][nbr] == 0) continue;   // Map[node][nbr] > 0 means door is open
                                                                 // (this is true for forward edges with room
                                                                 //  AND back edges with flow to cancel)
            int val = DFS (nbr, Math.Min (min, Map[node][nbr]));
            if (val == 0) continue;               // dead end, try the next neighbour (fix 4)
            Map[node][nbr] -= val;                // less room going forward
            Map[nbr][node] += val;                // more room to undo (fix 2, the back edge)
            return val;                           // every node returns, including source (fix 5)
         }
         return 0;                                // no way to sink from here
      }

      void PrintBoard (string title) {
         Console.WriteLine ($"--------- {title}");
         for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) Console.Write ($"{Map[i][j]} . ");
            Console.WriteLine ();
         }
      }

      // one direction only. the reverse starts at 0 and grows as we push (fix 1)
      void Edit (int from, int to, int v) { Map[from][to] = v; }
   }
}


/* WHAT THIS PRINTS FOR FF.png (traced by hand)
 *
 * iteration 1: source=>A=>B=>sink, bottleneck min(2,3,1) = 1. total 1.
 *              Map[2][3] = 0 (B=>sink full), Map[3][2] = 1, Map[2][1] = 1 (back edge B=>A is now open)
 * iteration 2: source=>A=>B is tried, B is a dead end (B=>sink 0), so continue -> source=>A=>sink, min(1,2) = 1. total 2.
 * iteration 3: source=>A is 0 now. source=>B (2) => B=>A using the BACK EDGE (1) => A=>sink (1). bottleneck 1. total 3.
 *              this cancels the 1 that was going A=>B and sends it A=>sink instead, and the water from
 *              source=>B takes over B=>sink. exactly the reroute from the explanation.
 * iteration 4: source=>A 0, source=>B has 1 but from B: B=>A 0, B=>sink 0. no path. stop.
 *
 * final real flow: source=>A 2, source=>B 1, A=>B 0, A=>sink 2, B=>sink 1. total 3.
 * check every middle node: A in 2 out 2. B in 1 out 1. good.
 * check the wall: from source we can still reach B only. edges leaving {source,B} are source=>A (2) and
 * B=>sink (1). both full. 2 + 1 = 3 = max flow.
 */