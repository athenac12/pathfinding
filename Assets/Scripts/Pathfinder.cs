using System.Collections.Generic;

public class Pathfinder<T>
{
    readonly Dictionary<T, HashSet<T>> map;

    public Pathfinder(Dictionary<T, HashSet<T>> map)
    {
        this.map = map;
    }

    public string PathAsString(T from, T to)
    {
        var pathAsString = string.Empty;
        var path = FindPath(from, to);

        if (path != null)
        {
            path.ForEach(t => pathAsString += $"{t} ");
            return pathAsString.Trim(); //adicionado o Trim que antes nao tinha para remover qualquer espaço em branco extra
        }
        else
        {
            return "path not possible";
        }
    }

    public List<T> FindPath(T from, T to)
    {
        if (!map.ContainsKey(from) || !map.ContainsKey(to))
        {
            return null;
        }

        var visited = new HashSet<T>();
        var parents = new Dictionary<T, T>();
        var toVisit = new Queue<T>();

        toVisit.Enqueue(from);
        visited.Add(from); //colocou esse Add(from), tirando o debaixo, para ser visitado imediatamente

        while (toVisit.Count > 0)
        {
            var current = toVisit.Dequeue();
            //tinha aq o: visited.Add(current);, porém tirou e colocou em cima, ao inves de current que significa no momento/atualmente, modificou para from que significa "de"
            if (current.Equals(to))
            {
                return BuildPath(from, to, parents); //antes aq tinha o break, mas foi tirado para o BuildPath com os nós de destino, to, from e parents
            }

            foreach (var neighbor in map[current])
            {
                if (!visited.Contains(neighbor))
                {
                    toVisit.Enqueue(neighbor);
                    visited.Add(neighbor);
                    parents[neighbor] = current;
                }
            }
        }
        // foi tirado a condição do if
        return null; // no path found
    }

    private List<T> BuildPath(T from, T to, Dictionary<T, T> parents) // privatizou 
    {
        var path = new List<T>();
        var current = to;

        while (!current.Equals(from))
        {
            path.Add(current);
            current = parents[current];
        }

        path.Add(from);
        path.Reverse();

        return path;
    }
}