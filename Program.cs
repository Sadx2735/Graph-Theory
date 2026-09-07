// Graph: Media/Dia1.png
// Representing Graph via Adjacency List
Console.WriteLine ("Adjacency List -------------------------------");
List<List<int>> Map = [];
Map.Add ([1, 2, 3]); Map.Add ([0, 2]);
Map.Add ([0, 1]); Map.Add ([0]);
for (int n = 0; n < Map.Count; n++) {
   Console.Write ($"Connections of Node : {n} -> ");
   foreach (var item in Map[n]) Console.Write ($" {item} |");
   Console.WriteLine ("");
}
// Representing Graph via Adjacency Matrix
Console.WriteLine ("Adjacency Matrix -----------------------------");
int[,] AdjMat = new int[4, 4];
for (int idx = 0; idx < Map.Count; idx++) {
   foreach (var item in Map[idx]) AdjMat[idx, item] = 1;
}
for (int i = 0; i < 4; i++) {
   for (int j = 0; j < 4; j++) {
      Console.Write ($" {AdjMat[i, j]} |");
   }
   Console.WriteLine ("");
}
// Representing Graph via Edge List
Console.WriteLine ("Edge List ------------------------------------");
List<(int, int, int)> EdgeList = [];
for (int n = 0; n < Map.Count; n++) {
   foreach(var item in Map[n]) {
      // Assuming Weight to be 0.
      EdgeList.Add ((n, item, n));
   }
}
// Used in Minimum Spanning Tree for considering minimum edges first.
foreach(var (u,v,w) in EdgeList.OrderBy(ch=>ch.Item1).Reverse())
   Console.WriteLine ($"{u} => {v} with edge weight of {w}");

// Implicit Graph
// In pathfinding problems
// (e.g., maximizing profit or minimizing distraction),
// we treat the matrix itself as a graph where each coordinate
// (x, y) is implicitly connected to
// (x+1, y), (x-1, y), (x, y-1), and (x, y+1).