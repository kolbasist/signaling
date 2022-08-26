using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PathParser
{
    public Vector3[] Parse(Transform path, Transform target)
    {
        int count = path.childCount + target.transform.hierarchyCount * 2;
        Debug.Log(count);
        float middlePoint = 0.5f;
        Vector3[] pathChild = new Vector3[path.childCount];
        Vector3[] result = new Vector3[count];

        for (int i = 0; i < path.childCount; i++)
        {
            pathChild[i] = path.GetChild(i).transform.position;
        }

        result[0] = pathChild[0];
        result[1] = Vector3.Lerp(pathChild[0], pathChild[1], middlePoint);
        result[2] = target.position;
        result[3] = pathChild[1];

        Debug.Log($"{result[0]} {result[1]} {result[2]} {result[3]}");

        return result;
    }
}

