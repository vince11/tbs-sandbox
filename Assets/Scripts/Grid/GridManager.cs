using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject selectorPrefab;

    public int width;
    public int height;
    public float nodeSize;
    public float offset;

    public List<Node> nodes;
    public Selector selector;

    void Awake()
    {
        InitialiseGrid();
    }

    public void InitialiseGrid()
    {
        gameObject.transform.localPosition = new Vector3(nodeSize / 2,  0, nodeSize / 2);
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.center = new Vector3((nodeSize * width) / 2 - (nodeSize / 2), 0, (nodeSize * height) / 2 - (nodeSize / 2));
        collider.size = new Vector3(nodeSize * width, 1, nodeSize * height);

        GameObject selectorGO = Instantiate(selectorPrefab, Vector3.zero, Quaternion.identity, transform);
        selector = selectorGO.GetComponent<Selector>();


        int totalNodes = width * height;

        nodes = new List<Node>();

        for (int i = 0; i < totalNodes; i++)
        {
            float x = (i % width);
            float z = (i / width);

            float nodeX = x * nodeSize;
            float nodeZ = z * nodeSize;
            
            GameObject nodeGO = Instantiate(nodePrefab, transform, false);
            nodeGO.transform.localPosition = new Vector3(nodeX, 0, nodeZ);
            nodeGO.transform.localScale = new Vector3(nodeSize - offset, nodeSize - offset, 1);
            nodeGO.transform.localRotation = nodePrefab.transform.rotation;
            nodeGO.name = i.ToString();

            Node node = nodeGO.GetComponent<Node>();
            node.index = i;
            node.worldPos = nodeGO.transform.position;
            nodes.Add(node);
        }

    }

    public void PlaceUnits(List<Unit> units)
    {
        int i = 0;
        foreach(Unit unit in units)
        {
            unit.node = nodes[i];
            unit.transform.position = new Vector3(nodes[i].worldPos.x, unit.transform.position.y, nodes[i].worldPos.z);
            nodes[i].unit = unit;
            i++;
        }

        selector.MoveTo(nodes[0].worldPos);
    }
}
