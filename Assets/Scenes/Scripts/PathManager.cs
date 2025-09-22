using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PathManager : MonoBehaviour
{

    [HideInInspector]
    [SerializeField] public List<wayPoint> path;

    public GameObject prefab;
    int currentPointIndex = 0;
    public List<GameObject> prefabPoints;


    public void Start()
    {
        prefabPoints = new List<GameObject>();
        // create prefab colliders for the path locations
        foreach (wayPoint p in path)
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = p.pos;
            prefabPoints.Add(go);
        }
    } 
    public void Update()  
    {
        // update all of the prefabs to the waypoint locations
        for (int i = 0; i < path.Count; i++)
        {
            wayPoint p = path[i];
            GameObject g = prefabPoints[i];
            g.transform.position = p.pos; 
        }
            

    }
    public wayPoint GetNextTarget()
    {
        int nextPointIndex = (currentPointIndex + 1) % (path.Count);
        currentPointIndex = nextPointIndex;
        return path[nextPointIndex];
    }


    public List<wayPoint> GetPath()
    {
        if (path == null) 
            path = new List<wayPoint>();
        return path;
    }

    public void CreateAddPoint()
    {
        wayPoint go = new wayPoint();
        path.Add(go);  
    }
}
