using System;
using System.Linq;

/* ========================================================================
   Bridges vs Articulation points ( my notes , corrected )
   ========================================================================

   What the two arrays mean :
     disc[u] -> the time at which dfs first came into u
     low[u]  -> the smallest disc that i can reach from u's subtree , by going
                down the tree edges and then taking atmost ONE back edge up.

   How to update low ( there is only one rule ) :
     tree edge ( u -> v , v not visited yet ) :
            low[u] = min ( low[u] , low[v] )
     back edge ( u -> w , w already visited and w is not my parent ) :
            low[u] = min ( low[u] , disc[w] )
                                    ^^^^^^^
     for a back edge its always disc[w] , never low[w]. ( reason in point 3 )

   The tests :
     bridge , edge (u,v) where v is child   ->  low[v] >  disc[u]   ( strict )
     articulation point , u is NOT root     ->  low[v] >= disc[u]   ( not strict )
     articulation point , u IS the root     ->  u must have 2 or more childrens
                                                in the dfs tree

   why > for an edge but >= for a vertex :
     if low[v] == disc[u] that means v's subtree can climb back upto u itself
     but not above u. so if i cut only the edge (u,v) they are still joined by
     that back edge , so its not a bridge. but if i remove the vertex u then
     that back edge also dies along with u , and the subtree loses its only
     way upward. so for a vertex even equal is enough.


   ------------------------------------------------------------------------
   1. "in bridges i took min from disc and also from low and it still worked"
   ------------------------------------------------------------------------
   the conclusion is correct but the reason i wrote earlier was not proper.
   the actual reason is like this :

     - in an undirected dfs there is no cross edge. so if a neighbour is
       already visited and it is not my parent , then it is surely my ancestor.
       ( it can also be a finished descendant but that one has a bigger disc
       so it doesnt change anything in min )

     - w is an ancestor and w is not the parent , so
            low[w] <= disc[w] < disc[parent(u)]
       means both the values are already below the parent's disc , so the test
       low[u] > disc[parent(u)] is going to fail in both the cases. same answer.

     - taking low[w] can over claim , it is saying "i can reach above w" but
       actually that road is going through w. still no harm , because just
       reaching w itself already makes every tree edge between w and u a part
       of a cycle , so those edges are anyway not bridges. and the edges above
       w are decided by w's own low , which is holding that value properly.

   so for bridges it is safe , but its more like luck and not a rule. better to
   write disc[w] in both the algos so that i dont have to think about it again.

   ------------------------------------------------------------------------
   2. my old line for the articulation condition was written in reverse
   ------------------------------------------------------------------------
   wrong  :  Node[time] > Child[backtime]
   right  :  low[child] >= disc[node]     ( same as disc[node] <= low[child] )

   and that line "removing all the other edges of this node" should be said
   like this -> when i remove a VERTEX , all of its edges also go at the same
   time , so any path which was just passing through that vertex is dead now.
   this one line is the whole difference between the two problems.

   ------------------------------------------------------------------------
   3. the actual insight ( this one i already got right , just writing clean )
   ------------------------------------------------------------------------
   lets say node X is there in cycle A and also in cycle B. dfs goes inside
   cycle A first , so low[X] becomes some disc above X. now dfs comes into
   cycle B and some node y in B is having a back edge y -> X.

     - if y takes disc[X] , y is saying "i can reach X"       -> true , safe
     - if y takes low[X]  , y is saying "i can reach above X"  -> false , because
       that road is going through X only.

   for bridges this lie doesnt harm ( point 1 ) , but for articulation point it
   fully breaks , because the question itself is "what happens after X is
   removed" and the lie is depending on X.

   ------------------------------------------------------------------------
   4. the diagram ( Br_Art.png ) , full trace , times starting from 0
   ------------------------------------------------------------------------
   edges : 0-1 , 1-2 , 2-3 , 3-4 , 3-8 , 4-8 , 8-9 , 9-10 , 10-4 , 4-5 , 5-6 ,
           6-7 , 7-4

   dfs order : 0 , 1 , 2 , 3 , 4 , 8 , 9 , 10 , then back to 4 , then 5 , 6 , 7

     node :  0   1   2   3   4   8   9  10   5   6   7
     disc :  0   1   2   3   4   5   6   7   8   9  10
     low  :  0   1   2   3   3   3   4   4   4   4   4

   low[10] = min ( 7 , disc[4]=4 )               = 4
   low[9]  = min ( 6 , low[10]=4 )               = 4
   low[8]  = min ( 5 , disc[3]=3 , low[9]=4 )    = 3
   low[4]  = min ( 4 , low[8]=3 )                = 3   <- this is the (4,3) i
                                                          wrote in the sketch
   low[7]  = min ( 10 , disc[4]=4 )              = 4   <- the (7,3) in my sketch
                                                          was the WRONG value i
                                                          was warning myself
                                                          about. it must be 4.

   now at node 4 , child 5 :  low[5] = 4 >= disc[4] = 4  ->  4 is an AP.
   if low[7] was wrongly 3 then low[5] also becomes 3 < 4 and i would MISS it.
   checking by hand : remove 4 -> { 5 , 6 , 7 } gets cut off. so yes its an AP.

   everything in this graph , for reference :
     articulation points : 1 , 2 , 3 , 4
     bridges             : 0-1 , 1-2 , 2-3

   one good thing to notice here is node 3 -> low[4] = 3 >= disc[3] = 3 so 3 is
   an articulation point , but the edge 3-4 is NOT a bridge ( 3 > 3 is false )
   because 3-4-8-3 is a cycle. so the >= and > difference is visible in one
   single place here.

   ------------------------------------------------------------------------
   5. three traps which have nothing to do with low , but all of them hit me
   ------------------------------------------------------------------------
   a) the root needs the children count rule , because root has no ancestor
      above it to get cut off from. and the childrens must be counted as "how
      many times i recursed from the root" and NOT as "how many land neighbours
      the root is having". because a later neighbour can be already visited
      from inside the first subtree , and that is exactly the cycle case where
      the root is safe.

   b) never put a break in the neighbour loop after finding a weak point. the
      remaining neighbours will stay unvisited , then the outer loop counts
      them as a second island , cnt becomes 2 and i return 0. so i found the
      answer and then threw it away.

   c) a single cell island has no articulation point at all , so the normal
      answer comes as 2. but if i remove that one cell then 0 islands are left
      which is also counted as disconnected. so answer is 1 , need a special
      case.

   ======================================================================== */


// LeetCode 2568 -> minimum number of days to disconnect island.
// answer can never be more than 2 ( drown any 2 cells and the island breaks ) ,
// so the only real question is -> does this island have an articulation point ?
public class SolutionLC2568 {
   public int MinDays (int[][] grid) {
      int N = grid.Length, M = grid[0].Length;
      int[][] disc = new int[N][];      // discovery time , 0 means not visited
      int[][] low = new int[N][];       // lowest disc reachable from my subtree
      for (int i = 0; i < N; i++) {
         disc[i] = new int[M];
         low[i] = new int[M];
      }

      int[] Delx = { 0, 0, -1, 1 };
      int[] Dely = { -1, 1, 0, 0 };
      bool valid (int xx, int yy) => 0 <= xx && xx < N && 0 <= yy && yy < M;

      bool hasCutCell = false;          // did i find any articulation point
      int Time = 1;                     // start from 1 so that 0 = not visited
      int rootChildren = 0;             // dfs childrens of the current root

      void dfs (int r, int c, int rp, int cp) {
         bool isRoot = (rp == -1 && cp == -1);

         disc[r][c] = Time++;
         low[r][c] = disc[r][c];        // atleast i can always reach myself

         for (int p = 0; p < 4; p++) {
            int xx = Delx[p] + r, yy = Dely[p] + c;
            if (!valid (xx, yy) || grid[xx][yy] == 0) continue;

            if (disc[xx][yy] == 0) {
               // ---- tree edge , this neighbour is becoming my child ----
               if (isRoot) rootChildren++;
               dfs (xx, yy, r, c);

               // take whatever the child's subtree was able to reach
               low[r][c] = Math.Min (low[r][c], low[xx][yy]);

               // AP test for a non root -> >= and not >
               // ( low == disc still means the child cant go PAST me )
               if (!isRoot && low[xx][yy] >= disc[r][c]) hasCutCell = true;

               // no break here , if i break the island count itself gets wrong
            } else if (xx != rp || yy != cp) {
               // ---- back edge , already visited and its not my parent ----
               // disc and never low , reason is in point 3.
               // in a grid there is atmost one edge between two cells , so
               // checking the parent by coordinates is fine ( no parallel edge )
               low[r][c] = Math.Min (low[r][c], disc[xx][yy]);
            }
         }
      }

      int islands = 0;
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (grid[i][j] == 1 && disc[i][j] == 0) {
               islands++;
               rootChildren = 0;
               dfs (i, j, -1, -1);
               if (rootChildren >= 2) hasCutCell = true;
            }
         }
      }

      // 0 islands or more than 1 island -> already disconnected , nothing to do
      if (islands != 1) return 0;
      // only one land cell -> no AP exists , but drowning it gives 0 islands
      if (grid.SelectMany (row => row).Sum () == 1) return 1;
      return hasCutCell ? 1 : 2;
   }
}

/* time O(N*M) and space O(N*M).
   the recursion depth can go upto N*M if the island is snake shaped , around
   900 for a 30x30 grid which is fine here. for a bigger grid better to use an
   explicit stack. */