﻿using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;

    public int width;
    public int height;
    public int nodeSize;
    public float offset;

    public List<Node> nodes;

    void Start()
    {
        InitialiseGrid();
    }

    public void InitialiseGrid()
    {
        int totalNodes = width * height;

        nodes = new List<Node>();

        for (int i = 0; i < totalNodes; i++)
        {
            float x = (i % width);
            float z = (i / width);

            float nodeX = (x * offset) + (x * nodeSize);
            float nodeZ = (z * offset) + (z * nodeSize);

            GameObject nodeGO = Instantiate(nodePrefab, new Vector3(nodeX, 0, nodeZ), nodePrefab.transform.rotation, transform);
            nodeGO.transform.localScale = new Vector3(nodeSize, nodeSize, 1);
        }
    }
}
