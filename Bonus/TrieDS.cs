class ProgramTrie {
   static void MainTrie (string[] args) {
      var trie = new Trie ();
      trie.Insert ("app");
      trie.Insert ("apple");
      Console.WriteLine ($"For Searching app {trie.SearchWord ("app")}");
      Console.WriteLine ($"For Searching appl {trie.SearchWord ("appl")}");
      Console.WriteLine ($"For Starts With appl {trie.StartsWith ("appl")}");
      Console.WriteLine ($"For Searching apple {trie.SearchWord ("apple")}");
      foreach (var word in trie.GetSuggestion ("appl")) {
         Console.WriteLine ($"This might be a word. : {word}");
      }
   }

   class Node {
      Node[] Links;
      bool End;
      public Node () {
         Links = new Node[26];
      }
      public Node GetNode (char ch) => Links[ch - 'a'];
      public void PutNode (char ch, Node nde) => Links[ch - 'a'] = nde;
      public void SetEnd () => End = true;
      public bool IsEnd () => End;
      public void RevokeEnd () => End = false;
   }

   class Trie {
      Node root;
      public Trie () {
         root = new Node ();
      }

      public void Insert (string word) {
         Node temp = root;
         foreach (var ch in word) {
            if (temp.GetNode (ch) is null) {
               temp.PutNode (ch, new Node ());
            }
            temp = temp.GetNode (ch);
         }
         temp.SetEnd ();
      }

      public bool SearchWord (string word) {
         Node temp = root;
         foreach (var ch in word) {
            if (temp.GetNode (ch) is null) {
               return false;
            }
            temp = temp.GetNode (ch);
         }
         return temp.IsEnd ();
      }

      public bool StartsWith (string word) {
         Node temp = root;
         foreach (var ch in word) {
            if (temp.GetNode (ch) is null) {
               return false;
            }
            temp = temp.GetNode (ch);
         }
         return true;
      }

      public void Delete (string word) {
         root = DeleteHelper (root, word, 0);

         bool HasNoChildren (Node node) {
            for (char c = 'a'; c <= 'z'; c++) {
               if (node.GetNode (c) is not null) return false;
            }
            return true;
         }

         Node DeleteHelper (Node node, string word, int idx) {
            if (idx == word.Length) {
               if (!node.IsEnd ()) {
                  return node;
               }
               node.RevokeEnd ();
               return HasNoChildren (node) ? null : node;
            }

            char ch = word[idx];
            Node child = node.GetNode (ch);
            if (child is null) return node;

            Node result = DeleteHelper (child, word, idx + 1);
            node.PutNode (ch, result);
            return (HasNoChildren (node) && !node.IsEnd ()) ? null : node;
         }
      }

      public List<string> GetSuggestion (string word) {
         var results = new List<string> ();

         if (!StartsWith (word)) return results;
         Node temp = root;
         foreach (var ch in word) temp = temp.GetNode (ch);
         DFS (temp, word);

         void DFS (Node node, string prefix) {
            if (node.IsEnd ()) results.Add (prefix);
            for (char st = 'a'; st <= 'z'; st++) {
               var nde = node.GetNode (st);
               if (nde != null) {
                  DFS (nde, prefix + st);
               }
            }
         }

         return results;
      }
   }
}