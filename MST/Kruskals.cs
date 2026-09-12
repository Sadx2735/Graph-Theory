namespace DsuMst;
class ProgramDsuMst {
   private static void Main (string[] args) {
      List<(int from, int to, int weight)> Edges = new ();
      DisjointSetMST dmst = new (4);
      Edges.Add ((0, 1, 10));
      Edges.Add ((0, 2, 6));
      Edges.Add ((0, 3, 5));
      Edges.Add ((2, 3, 4));
      Edges.Add ((1, 3, 15));
      foreach (var (f, t, w) in Edges.OrderBy (x => x.weight).ToList ()) {
         if (dmst.Merge (f, t)) Console.WriteLine ($"From : {f} To : {t} Wght : {w}");
      }
   }
}

class DisjointSetMST {
   int[] parent;
   int[] size;
   public DisjointSetMST (int N) {
      parent = new int[N];
      size = new int[N];
      for (int i = 0; i < N; i++) {
         parent[i] = i;
         size[i] = 1;
      }
   }

   public bool Merge (int ele1, int ele2) {
      int set1 = FindParent (ele1);
      int set2 = FindParent (ele2);
      if (set1 == set2) return false;
      if (size[set1] <= size[set2]) {
         parent[set1] = set2;
         size[set2] += size[set1];
      } else {
         parent[set2] = set1;
         size[set1] += size[set2];
      }
      return true;
   }

   public int FindParent (int ele) {
      if (parent[ele] == ele) {
         return ele;
      }
      return parent[ele] = FindParent (parent[ele]);
   }
}


