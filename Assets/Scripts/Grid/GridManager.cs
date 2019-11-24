using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;

    public int width;
    public int height;
    public float nodeSize;
    public float offset;

    public List<Node> nodes;

    void Awake()
    {
        InitialiseGrid();
    }

    public void InitialiseGrid()
    {
        gameObject.transform.localPosition = new Vector3(nodeSize / 2, nodeSize / 2, 0);

        int totalNodes = width * height;

        nodes = new List<Node>();

        for (int i = 0; i < totalNodes; i++)
        {
            float x = (i % width);
            float z = (i / width);

            float nodeX = x * nodeSize;
            float nodeZ = z * nodeSize;
            
            GameObject nodeGO = Instantiate(nodePrefab, transform, false);
            nodeGO.transform.localPosition = new Vector3(nodeX, nodeZ, 0);
            nodeGO.transform.localScale = new Vector3(nodeSize - offset, nodeSize - offset, 1);

            Node node = nodeGO.GetComponent<Node>();
            node.index = i;
            node.worldPos = nodeGO.transform.position;
            nodes.Add(node);
        }
    }
}
