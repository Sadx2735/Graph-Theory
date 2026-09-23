public class Solution212 {
   public IList<string> FindWords (char[][] board, string[] words) {

      // Init Setup - For DFS
      int N = board.Length, M = board[0].Length;
      bool[][] visited = new bool[N][];
      for (int i = 0; i < N; i++) visited[i] = new bool[M];

      // Init Setup - For Trie
      var Try = new Trie ();
      foreach (var word in words) Try.Insert (word);

      // Init Setup - For Capturing Intersection
      HashSet<string> Done = [];
      List<string> result = [];

      // Delta for moving around from a point
      int[] delx = { 0, 0, -1, 1 };
      int[] dely = { -1, 1, 0, 0 };

      // Helper for Checking if the point in the grid exists.
      bool valid (int rv, int cv) {
         if (0 <= rv && rv < N && 0 <= cv && cv < M) {
            return true;
         }
         return false;
      }

      // DFS Main Function.
      void DFS (int r, int c, Node node, string word) {
         // for avoiding using things again.
         visited[r][c] = true;
         // if the current track is a word then you can add it.
         if (node.IsEnd ()) Done.Add (word);
         // for all the movement from current point and if its valid.
         // and if its is not visited.
         for (int d = 0; d < 4; d++) {
            int nx = r + delx[d], ny = c + dely[d];
            if (valid (nx, ny) && !visited[nx][ny]) {
               char ch = board[nx][ny];
               Node next = node.GetNext (ch);
               if (next is not null) {
                  DFS (nx, ny, next, word + ch);
               }
            }
         }
         // Getting back.
         visited[r][c] = false;
      }

      // if the starting letter exists then you can start a call from him.
      for (int i = 0; i < N; i++) {
         for (int j = 0; j < M; j++) {
            if (Try.StartsWith ($"{board[i][j]}")) {
               DFS (i, j, Try.root.GetNext (board[i][j]), $"{board[i][j]}");
            }
         }
      }

      // Intersection of search words and found words
      foreach (var word in words.OrderBy (n => n)) {
         if (Done.Contains (word)) {
            result.Add (word);
         }
      }

      // end
      return result;
   }
}

// Every Component of the Trie
public class Node {
   Node[] Links = new Node[26];
   bool Flag = false;
   public void SetEnd () => Flag = true;
   public void SetNext (char ch, Node nde) => Links[ch - 'a'] = nde;
   public Node GetNext (char ch) => Links[ch - 'a'];
   public bool IsEnd () => Flag;
}

public class Trie {
   public Node root = new Node ();

   // Check if the Next exist else create it.
   // At the end set as End.
   public void Insert (string word) {
      Node temp = root;
      foreach (var ch in word) {
         if (temp.GetNext (ch) is null)
            temp.SetNext (ch, new Node ());
         temp = temp.GetNext (ch);
      }
      temp.SetEnd ();
   }

   // check if you could go till end. 
   public bool StartsWith (string word) {
      Node temp = root;
      foreach (var ch in word) {
         if (temp.GetNext (ch) is null)
            return false;
         temp = temp.GetNext (ch);
      }
      return true;
   }

   // go till end and see if its a end.
   public bool SearchWord (string word) {
      Node temp = root;
      foreach (var ch in word) {
         if (temp.GetNext (ch) is null)
            return false;
         temp = temp.GetNext (ch);
      }
      return temp.IsEnd ();
   }
}