
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{

    static ObjectPooling instance = null;

    public static ObjectPooling Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

   
    public GameObject GetObject(GameObject prefab, Transform parent) //오프젝트가 필요할 때 다른 스크립트에서 호출되는 함수
    {
        for(int i=0;i< parent.childCount;i++)
        {
            if(parent.GetChild(i).gameObject.activeSelf is false)
            {
                var temp = parent.GetChild(i).gameObject;
                temp.SetActive(true);
                return temp ;
                
            }


        }

        GameObject objectInPool = Instantiate(prefab) as GameObject;
        Debug.Log(objectInPool.name + "?" + parent.name);
        objectInPool.transform.SetParent(parent);
        //objectInPool.gameObject.SetActive(true);
        
        return objectInPool;
        
    }


}
