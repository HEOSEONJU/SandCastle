
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

    





    public static GameObject GetObject(GameObject gameobject, Transform parent) //������Ʈ�� �ʿ��� �� �ٸ� ��ũ��Ʈ���� ȣ��Ǵ� �Լ�
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

        GameObject objectInPool = Instantiate(gameobject);
        objectInPool.gameObject.SetActive(true);
        objectInPool.transform.SetParent(parent);
        return objectInPool;
        
    }


}
