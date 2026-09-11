class DisjointSetx2 {
   int[] parent;
   int[] rank;
   public DisjointSetx2 (int N) {
      parent = new int[N];
      rank = new int[N];
      for (int i = 0; i < N; i++) {
         parent[i] = i;
      }
   }

   public void Merge (int ele1, int ele2) {
      int set1 = FindParent (ele1);
      int set2 = FindParent (ele2);
      if (set1 == set2) return;
      if (rank[set1] == rank[set2]) {
         parent[set1] = set2;
         rank[set2]++;
      } else if (rank[set1] < rank[set2]) {
         parent[set1] = set2;
      } else {
         parent[set2] = set1;
      }
   }

   public int FindParent (int ele) {
      if (parent[ele] == ele) {
         return ele;
      }
      return parent[ele] = FindParent (parent[ele]);
   }
}