using Google.GData.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class InGameMapMove : MonoBehaviour
{

    [SerializeField]
    float height=24f;


    [SerializeField]
    Transform parent;

    [SerializeField]
    bool UpDown;

    public void Awake()
    {
        parent = transform.root;
        sort();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {



            sort();
            if (UpDown)//위에작동
            {

                if (parent.GetChild(0).transform == this.transform.parent)
                {
                    
                    Transform temp = parent.GetChild(parent.childCount - 1).transform;
                    Vector3 t = transform.parent.position;
                    t.y += height;
                    temp.position = t;
                }
                
                
            }
            else//밑에작동
            {
                
                if (parent.GetChild(parent.childCount - 1).transform == this.transform.parent)
                {
                    
                    Transform temp = parent.GetChild(0).transform;
                    Vector3 t = transform.parent.position;
                    t.y -= height;
                    temp.position = t;
                }
            }




            
        }
    }

    public void sort()
    {
        List<Transform> childlist = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            childlist.Add(parent.GetChild(i));
        }
        childlist = childlist.OrderBy(x => x.transform.position.y).ToList();
        
        
        for (int i = childlist.Count-1; i >=0; i--)
        {
            childlist[i].transform.SetAsLastSibling();
            
        }
    }






}
