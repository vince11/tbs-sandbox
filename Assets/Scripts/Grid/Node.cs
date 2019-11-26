﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int index;
    public Vector3 worldPos;
    public Unit unit;
    public NodeTerrain terrain;
    public Node parent;
    public Material material;
}
