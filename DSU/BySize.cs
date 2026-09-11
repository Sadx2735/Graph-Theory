class DisjointSetx1 {
   int[] parent;
   int[] size;
   public DisjointSetx1 (int N) {
      parent = new int[N];
      size = new int[N];
      for (int i = 0; i < N; i++) {
         parent[i] = i;
         size[i] = 1;
      }
   }

   public void Merge (int ele1, int ele2) {
      int set1 = FindParent (ele1);
      int set2 = FindParent (ele2);
      if (set1 == set2) return;
      if (size[set1] <= size[set2]) {
         parent[set1] = set2;
         size[set2] += size[set1];
      } else {
         parent[set2] = set1;
         size[set1] += size[set2];
      }
   }

   public int FindParent (int ele) {
      if (parent[ele] == ele) {
         return ele;
      }
      return parent[ele] = FindParent (parent[ele]);
   }
}