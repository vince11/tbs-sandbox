using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;
using System.IO;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject selectorPrefab;
    public GameObject arrowPrefab;

    public string mapName;
    public float nodeSize;
    public float offset;
    
    public List<Color> colors;
    public List<Texture> arrowTextures;

    [System.NonSerialized]
    public int width, height;

    [System.NonSerialized]
    public List<Node> nodes;

    [System.NonSerialized]
    public Selector selector;

    private List<Arrow> arrows;
    
    private TerrainDatabase terrainsDB;
    private GridData gridData;

    void Awake()
    {
        terrainsDB = new TerrainDatabase();
        string path = Path.Combine(Application.streamingAssetsPath, "Databases/Grids/" + mapName + ".json");
        string data = File.ReadAllText(path);
        gridData = JsonUtility.FromJson<GridData>(data);
        gridData.Initialise();
        width = gridData.width;
        height = gridData.height;
        InitialiseGrid();
    }

    public void InitialiseGrid()
    {
        gameObject.transform.localPosition = new Vector3(nodeSize / 2,  0, nodeSize / 2);

        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.center = new Vector3((nodeSize * width) / 2 - (nodeSize / 2), 0, (nodeSize * height) / 2 - (nodeSize / 2));
        collider.size = new Vector3(nodeSize * width, 1, nodeSize * height);

        GameObject selectorGO = Instantiate(selectorPrefab, selectorPrefab.transform.position, selectorPrefab.transform.rotation, transform);
        selectorGO.transform.localScale = new Vector3(nodeSize, nodeSize, 1);
        selector = selectorGO.GetComponent<Selector>();
        selector.size = (int) nodeSize;

        GameObject arrowParentGO = new GameObject("Arrow");
        arrowParentGO.transform.SetParent(transform);
        arrowParentGO.transform.position = transform.localPosition;

        int totalNodes = width * height;

        nodes = new List<Node>();
        arrows = new List<Arrow>();

        for (int i = 0; i < totalNodes; i++)
        {
            int x = (i % width);
            int z = (i / width);

            float nodeX = x * nodeSize;
            float nodeZ = z * nodeSize;
            
            GameObject nodeGO = Instantiate(nodePrefab, transform, false);
            nodeGO.transform.localPosition = new Vector3(nodeX, 0, nodeZ);
            nodeGO.transform.localScale = new Vector3(nodeSize - offset, nodeSize - offset, 1);
            nodeGO.transform.localRotation = nodePrefab.transform.rotation;
            nodeGO.name = i.ToString();

            Node node = nodeGO.GetComponent<Node>();
            node.index = i;
            node.row = z;
            node.column = x;
            node.worldPos = nodeGO.transform.position;
            node.terrain = terrainsDB.GetTerrain(gridData.terrains[i]);
            node.material = nodeGO.GetComponent<Renderer>().material;
            nodes.Add(node);

            GameObject arrowGO = Instantiate(arrowPrefab, arrowParentGO.transform, false);
            arrowGO.transform.localPosition = new Vector3(nodeX, 0.25f, nodeZ);
            arrowGO.transform.localScale = new Vector3(nodeSize, nodeSize, 1);
            arrowGO.transform.localRotation = arrowPrefab.transform.rotation;

            Arrow arrow = arrowGO.GetComponent<Arrow>();
            arrow.material = arrowGO.GetComponent<Renderer>().material;
            arrows.Add(arrow);
            arrowGO.SetActive(false);

        }
    }

    public void PlaceUnits(List<Unit> units)
    {
        Node node;
        Unit unit;
        for(int i = 0; i < gridData.startPositions.Length; i++)
        {
            if(i < units.Count)
            {
                node = nodes[gridData.startPositions[i]];
                unit = units[i];

                unit.node = node;
                unit.transform.position = new Vector3(node.worldPos.x, unit.transform.position.y, node.worldPos.z);
                node.unit = unit;
            }
        }

        selector.MoveTo(nodes[gridData.startPositions[0]].worldPos);
    }

    public List<Node> GetMovementNodes(Node node)
    {
        Dictionary<Node, int> moveCost = new Dictionary<Node, int>();
        List<Node> unvisited = new List<Node>();
        List<Node> visited = new List<Node>();

        node.parent = null;

        unvisited.Add(node);
        moveCost.Add(node, 0);

        while (unvisited.Count > 0)
        {
            Node currentNode = unvisited[0];
            unvisited.Remove(currentNode);
            visited.Add(currentNode);

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!visited.Contains(neighbour))
                {
                    int costToNeighbour = moveCost[currentNode] + neighbour.terrain.GetCost(node.unit.MoveType);
                    if (costToNeighbour <= node.unit.MoveRange)
                    {
                        if (!unvisited.Contains(neighbour)) unvisited.Add(neighbour);

                        if (!moveCost.ContainsKey(neighbour) || costToNeighbour < moveCost[neighbour])
                        {
                            moveCost[neighbour] = costToNeighbour;
                            neighbour.parent = currentNode;
                        }
                    }
                }
            }
        }
        return visited;
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        int index = node.index;
        int maxNodes = width * height;

        if ((index + width) < maxNodes) neighbours.Add(nodes[index + width]); //north
        if ((index - width) >= 0) neighbours.Add(nodes[index - width]); //south
        if ((index + 1) < maxNodes && (index + 1) / width == (index / width)) neighbours.Add(nodes[index + 1]); //east
        if ((index - 1) >= 0 && (index - 1) / width == (index / width)) neighbours.Add(nodes[index - 1]); //west

        return neighbours;
    }

    public List<Node> GetAttackNodes(Node node, Unit unit)
    {
        List<Node> unvisited = new List<Node>();
        List<Node> visited = new List<Node>();
        List<Node> attackNodes = new List<Node>();

        unvisited.Add(node);

        while (unvisited.Count > 0)
        {
            Node currentNode = unvisited[0];
            unvisited.Remove(currentNode);
            visited.Add(currentNode);

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!visited.Contains(neighbour))
                {
                    int distance = GetDistance(node, neighbour);
                    int attackRange = unit.AttackRange;

                    if (distance <= attackRange)
                    {
                        if (!unvisited.Contains(neighbour)) unvisited.Add(neighbour);

                        if (distance <= attackRange && distance >= attackRange && !attackNodes.Contains(neighbour))
                        {
                            if (neighbour.terrain.GetTerrainType() != TerrainType.Obstacle) attackNodes.Add(neighbour);
                        }
                    }
                }
            }
        }
        return attackNodes;
    }

    private int GetDistance(Node A, Node B)
    {
        int distanceX = (int)Mathf.Abs(A.worldPos.x - B.worldPos.x);
        int distanceZ = (int)Mathf.Abs(A.worldPos.z - B.worldPos.z);

        return (int) ((distanceX + distanceZ)/nodeSize);
    }

    /**
     * 0: clear
     * 1: blue
     * 2: red
     */
    public void Highlight(List<Node> nodes, int index)
    {   
        foreach (Node node in nodes)
        {
            node.material.color = colors[index];
        }
    }

    public List<Node> GetPath(Node destination)
    {
        List<Node> path = new List<Node>();
        Node currentNode = destination;
        while(currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        return path;
    }

    //Direction of B from A
    public NodeDirection GetNeighborDirection(Node A, Node B)
    {
        int maxNodes = width * height;

        if (A.index + width == B.index) return NodeDirection.North;
        else if (A.index - width == B.index) return NodeDirection.South;
        else if ((A.index) / width == (B.index / width) && A.index + 1 == B.index) return NodeDirection.East;
        else if ((A.index) / width == (B.index / width) && A.index - 1 == B.index) return NodeDirection.West;

        return NodeDirection.NotNeighbor;
    }

    public void DrawArrow(List<Node> path)
    {
        Node current, parent, next;
        Texture texture;
        Vector3 rotation;
        NodeDirection parentDir, nextDir;

        for (int i = 0; i < path.Count; i++)
        {
            current = path[i];
            parent = path[i].parent;
            next = (i + 1) < path.Count ? path[i + 1] : null;
            rotation = new Vector3(90, 0, 0);
            if (parent == null && next == null)
            {
                texture = arrowTextures[3]; //cursor on unit - platform
            }
            else if (parent == null && next != null)
            {
                texture = arrowTextures[3]; //arrow tail
                nextDir = GetNeighborDirection(current, next);
                if (nextDir == NodeDirection.North) rotation.z = 90;
                else if (nextDir == NodeDirection.West) rotation.z = 180;
                else if (nextDir == NodeDirection.South) rotation.z = 270;

            }
            else if (parent != null && next == null)
            {
                texture = arrowTextures[0]; //arrow head
                parentDir = GetNeighborDirection(current, parent);
                if (parentDir == NodeDirection.North) rotation.z = 270;
                else if (parentDir == NodeDirection.East) rotation.z = 180;
                else if (parentDir == NodeDirection.South) rotation.z = 90;
            }
            else if (parent.row == next.row || parent.column == next.column)
            {
                texture = arrowTextures[1]; //arrow body
                if (parent.column == next.column) rotation.z = 90;

            }
            else
            {
                texture = arrowTextures[2]; //arrow curve
                parentDir = GetNeighborDirection(current, parent);
                nextDir = GetNeighborDirection(current, next);

                if((parentDir == NodeDirection.West && nextDir == NodeDirection.South) ||
                    (parentDir == NodeDirection.South && nextDir == NodeDirection.West))
                {
                    rotation.z = 270;
                }
                else if ((parentDir == NodeDirection.West && nextDir == NodeDirection.North) ||
                        (parentDir == NodeDirection.North && nextDir == NodeDirection.West))
                {
                    rotation.z = 180;
                }
                else if ((parentDir == NodeDirection.East && nextDir == NodeDirection.North) ||
                        (parentDir == NodeDirection.North && nextDir == NodeDirection.East))
                {
                    rotation.z = 90;
                }
            }

            arrows[path[i].index].material.mainTexture = texture;
            arrows[path[i].index].gameObject.transform.eulerAngles = rotation;
            arrows[path[i].index].gameObject.SetActive(true);
        }
    }

    public void ClearArrows()
    {
        foreach (Arrow arrow in arrows) arrow.gameObject.SetActive(false);
    }
}
