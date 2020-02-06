using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestGraph
{
    class Program
    {
        static void add_edge(Dictionary<int, Dictionary<int, bool>> adj, int src, int dest)
        {
            adj[src][dest] = true;
            adj[dest][src] = true;
        }

        static void Main(string[] args)
        {

            // array of vectors is used to store the graph 
            // in the form of an adjacency list
            Dictionary<int, Dictionary<int, bool>> adj = new Dictionary<int, Dictionary<int, bool>>();
            for(int i=0;i<10;i++)
                for(int j=0;j<10;j++)
                {
                    if (!adj.ContainsKey(i))
                        adj[i] = new Dictionary<int, bool>();

                    adj[i][j] = false;
                }

            // Creating graph given in the above diagram. 
            // add_edge function takes adjacency list, source  
            // and destination vertex as argument and forms 
            // an edge between them. 
            add_edge(adj, 0, 1);
            add_edge(adj, 0, 3);
            add_edge(adj, 1, 2);
            add_edge(adj, 3, 4);
            add_edge(adj, 3, 7);
            add_edge(adj, 4, 5);
            add_edge(adj, 4, 6);
            add_edge(adj, 4, 7);
            add_edge(adj, 5, 6);
            add_edge(adj, 6, 7);

            int source = 2, dest = 2;

            int v = 8;

            printShortestDistance(adj, source, dest, v);

            //return 0;
        }



        static bool BFS(Dictionary<int, Dictionary<int, bool>> adj, int src, int dest, int v,
                            Dictionary<int, int> pred, Dictionary<int, int> dist)
        {
            // a queue to maintain queue of vertices whose 
            // adjacency list is to be scanned as per normal 
            // DFS algorithm 
            Queue queue = new Queue();

            // boolean array visited[] which stores the 
            // information whether ith vertex is reached 
            // at least once in the Breadth first search 
            Dictionary<int, bool> visited = new Dictionary<int, bool>();

            // initially all vertices are unvisited 
            // so v[i] for all i is false 
            // and as no path is yet constructed 
            // dist[i] for all i set to infinity 
            for (int i = 0; i < v; i++)
            {
                visited[i] = false;
                dist[i] = 10000;
                pred[i] = -1;
            }

            // now source is first to be visited and 
            // distance from source to itself should be 0 
            visited[src] = true;
            dist[src] = 0;
            queue.Enqueue(src);

            // standard BFS algorithm 
            while (queue.Count != 0)
            {
                int u = (int)queue.Dequeue();

                for (int i = 0; i < 10; i++)
                {
                    if (adj[u][i] == true && visited[i] == false)
                    {
                        visited[i] = true;
                        dist[i] = dist[u] + 1;
                        pred[i] = u;
                        queue.Enqueue(i);

                        // We stop BFS when we find 
                        // destination. 
                        if (i == dest)
                            return true;
                    }
                }
            }

            return false;
        }

        static void printShortestDistance(Dictionary<int, Dictionary<int, bool>> adj, int s, int dest, int v)
        {
            // predecessor[i] array stores predecessor of 
            // i and distance array stores distance of i 
            // from s 
            Dictionary<int, int> pred = new Dictionary<int, int>();
            Dictionary<int, int> dist = new Dictionary<int, int>();
            

            if (BFS(adj, s, dest, v, pred, dist) == false)
            {
                //cout << "Given source and destination"
                 //    << " are not connected";
                return;
            }

            // vector path stores the shortest path 
            List<int> path = new List<int>();
            int crawl = dest;
            path.Add(crawl);
            while (pred[crawl] != -1)
            {
                path.Add(pred[crawl]);
                crawl = pred[crawl];
            }

            // distance from source is in distance array 
            //cout << "Shortest path length is : "
            //   << dist[dest];

            // printing path from source to destination 
            //cout << "\nPath is::\n";
            for (int i = path.Count - 1; i >= 0; i--)
                Console.WriteLine(path[i] + " , ");
                //cout << path[i] << " ";
        }
    }
}
