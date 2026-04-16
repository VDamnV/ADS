import java.util.*;

class Edge {
    int source;
    int destination;

    public Edge(int source, int destination) {
        this.source = source;
        this.destination = destination;
    }
}

public class HT_1 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        
        System.out.print("Enter the number of vertices in the graph (N): ");
        int n = scanner.nextInt();
        
        System.out.print("Enter the number of ribs: ");
        int m = scanner.nextInt();

        List<Edge> edges = new ArrayList<>();

        System.out.println("Enter edges in the format ‘from_which_vertex to_which’ (for example, 1 2):");
        for (int i = 0; i < m; i++) {
            int src = scanner.nextInt();
            int dest = scanner.nextInt();
            edges.add(new Edge(src, dest));
        }

        System.out.println("\n--- Representation of a graph (Edge array) ---");
        for (Edge edge : edges) {
            System.out.println("Rib: " + edge.source + " -> " + edge.destination);
        }

        System.out.print("\nEnter the vertex from which the breadth-first search will start (BFS): ");
        int startVertex = scanner.nextInt();

        System.out.println("\nThe result of a breadth-first search (BFS):");
        bfs(edges, n, startVertex);
        
        scanner.close();
    }

    private static void bfs(List<Edge> edges, int n, int startVertex) {
        boolean[] visited = new boolean[n + 1];
        Queue<Integer> queue = new LinkedList<>();

        visited[startVertex] = true;
        queue.add(startVertex);

        while (!queue.isEmpty()) {
            int current = queue.poll();
            System.out.print(current + " ");

            for (Edge edge : edges) {
                if (edge.source == current && !visited[edge.destination]) {
                    visited[edge.destination] = true;
                    queue.add(edge.destination);
                }
            }
        }
        System.out.println();
    }
}