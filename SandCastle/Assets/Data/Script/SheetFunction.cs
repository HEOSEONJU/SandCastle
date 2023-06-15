using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheetFunction : MonoBehaviour
{
    static SheetFunction instance;
    

    [SerializeField]
    List<ObjectTable> tables;
    
    [SerializeField]
    bool Active;

    [SerializeField]
    string mainSceneName;
    
    int checkCount = 0;
    public static SheetFunction  Instacne
    {
        get { return instance; }
        
    }

    public bool error = false;

    private void Awake()
    {
        if(instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if(Active)
            {
                checkCount = 0;
                
                OnlienReadSheets();
                //OnlienReadSheets();

            }
            else
            {
                StartCoroutine(delay());
                checkCount=OffLineReadSheets();
                
            }

            return;
        }
        
        Destroy(gameObject);
    }

    IEnumerator delay()
    {
        while (checkCount!=tables.Count)
        {
            yield return null;
        }
        
        if(SceneMoveManager.Instance !=null)
        SceneMoveManager.Instance.ImmediatelyChangeScne(mainSceneName);
    }

    
        


    public void OnlienReadSheets()
    {
        Debug.Log("온라인리딩작동");
            
        foreach (ObjectTable table in tables) 
        {
            table.OnLineReading();
        }

        
        
    }
    public int OffLineReadSheets()
    {
        int c = 0;
        foreach (ObjectTable table in tables)
        {
             c+=table.OffLineReading();
            

        }
        return c;


    }

    public void Check()
    {
        checkCount++;
        if (checkCount == tables.Count)
        {
            StartCoroutine(newWait());
        }
    }
    IEnumerator newWait()
    {
        if(error)
        {
            Debug.Log("재시도");
            error = false;
            checkCount = 0;
            OnlienReadSheets();
            
            yield break ;
        }


        while (true)
        {
            yield return new WaitForSeconds(3f);

            if(!(tables.Last().ViewTableList == null || tables.Last().ViewTableList.Count == 0))
            {

                Active = false;
                SceneMoveManager.Instance.ImmediatelyChangeScne(mainSceneName);
                yield break;
            }
        }
    }
    IEnumerator WaitSceneMove()
    {
        Debug.Log(tables.Last().associatedWorksheet);
        while (tables.Last().ViewTableList==null || tables.Last().ViewTableList.Count==0)
        {
            yield return null;
        }
        Active = false;
        yield return new WaitForSeconds(1.5f);
        SceneMoveManager.Instance.ImmediatelyChangeScne(mainSceneName);
    }
}
