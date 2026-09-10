public class Solution {
   public bool CanFinish (int numCourses, int[][] prerequisites) {
      int[] indegree = new int[numCourses];
      List<List<int>> adjMap = new List<List<int>> ();

      for (int i = 0; i < numCourses; i++) {
         adjMap.Add (new List<int> ());
      }

      foreach (var item in prerequisites) {
         int u = item[0], v = item[1];
         adjMap[v].Add (u);
         indegree[u]++;
      }

      Queue<int> MyQueue = new Queue<int> ();
      for (int i = 0; i < numCourses; i++) {
         if (indegree[i] == 0) MyQueue.Enqueue (i);
      }
      List<int> ans = new List<int> ();
      while (MyQueue.Count > 0) {
         int val = MyQueue.Dequeue ();
         ans.Add (val);
         foreach (var item in adjMap[val]) {
            indegree[item]--;
            if (indegree[item] == 0) MyQueue.Enqueue (item);
         }
      }
      if (ans.Count () != numCourses) return false;
      return true;
   }
}
