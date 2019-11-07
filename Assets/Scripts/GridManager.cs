using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;

    public int width;
    public int height;
    public float offset;
    
    public Dictionary<GameObject, Node> nodes;
    public Dictionary<GameObject, Material> nodesMaterial;

    void Start()
    {
        InitialiseGrid();
    }

    public void InitialiseGrid()
    {
        int totalNodes = width * height;

        float scaleX = nodePrefab.transform.localScale.x;
        float scaleZ = nodePrefab.transform.localScale.z;

        nodes = new Dictionary<GameObject, Node>();
        nodesMaterial = new Dictionary<GameObject, Material>();

        for (int i = 0; i < totalNodes; i++)
        {
            float x = (i % width);
            float z = (i / width);

            float nodeX = (x * offset) + (x * scaleX);
            float nodeZ = (z * offset) + (z * scaleZ);

            GameObject nodeGO = Instantiate(nodePrefab, new Vector3(nodeX, 0, nodeZ), Quaternion.identity, transform);
            Node node = nodeGO.GetComponent<Node>();
            node.index = i;

            nodes.Add(nodeGO, node);
            nodesMaterial.Add(nodeGO, nodeGO.GetComponent<Renderer>().material);
        }
    }
}
