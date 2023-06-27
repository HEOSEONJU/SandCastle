using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Nav : MonoBehaviour
{
    //[SerializeField]
    //NavMeshSurface surface;
    


    private void Awake()
    {
        //GenerateNavmesh();
    }
    [SerializeField]
    private GameObject _mapPrefab;

    [SerializeField]
    private Vector3 _generatePos = new Vector3(0, 0, 0);



    

    private void GenerateNavmesh()
    {
        GameObject obj = Instantiate(_mapPrefab, _generatePos, Quaternion.identity, transform);
        _generatePos += new Vector3(50, 0, 50);

        UnityEngine.AI.NavMeshSurface[] surfaces = gameObject.GetComponentsInChildren<UnityEngine.AI.NavMeshSurface>();

        foreach (var s in surfaces)
        {
            s.RemoveData();
            s.BuildNavMesh();
        }

    }


}
