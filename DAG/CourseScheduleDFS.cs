class Solutionx11 {
    public List<int> topoSort(int V, int[,] edges) {
        bool[] visited = new bool[V]; 
        List<List<int>> Map = new List<List<int>>(); 
        for (int i = 0; i < V; i++) Map.Add(new List<int>());
        for (int i = 0; i < edges.GetLength(0); i++) 
        { 
            int u = edges[i, 0]; 
            int v = edges[i, 1];
            Map[u].Add(v); 
        }
        List<int> finalans = new();
        void dfs(int n) {
            visited[n]=true;
            foreach(var item in Map[n]) {
                if(!visited[item]) dfs(item);
            }
            finalans.Add(n);
        }
        for(int i=0;i<V;i++) {
            if(!visited[i]) { dfs(i); }
        }
        finalans.Reverse(); 
        return finalans;
    }
}