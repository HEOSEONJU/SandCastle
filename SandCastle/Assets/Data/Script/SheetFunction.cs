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
    [SerializeField]
    int checkCount = 0;
    public static SheetFunction  Instacne
    {
        get { return instance; }
        
    }
    bool error;
    public bool Error
    {
        get { return error; }
        set { error = value; }
    }

    IEnumerator ErrorCorountine;
    IEnumerator onLineCorountine;
    private void Awake()
    {
        if(instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if(Active)
            {
                checkCount = 0;
                onLineCorountine = OnlienReadSheets();
                StartCoroutine(onLineCorountine);
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





    IEnumerator OnlienReadSheets()
    {
        Debug.Log("온라인리딩작동");
        foreach (ObjectTable table in tables) 
        {
            yield return null;
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
    public void ErrorCheck()
    {
        Error = true;
    }
    public void Check()
    {
        checkCount++;
        if (checkCount == tables.Count)
        {
            checkCount = 0;
            ErrorCorountine = newWait();
            StartCoroutine(ErrorCorountine);
        }
    }
    IEnumerator newWait()
    {
        if(Error)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("재시도");
            Error = false;
            checkCount = 0;
            StopCoroutine(onLineCorountine);
            onLineCorountine = OnlienReadSheets();
            StartCoroutine(onLineCorountine);

            yield break ;
        }


        while (true)
        {
            Debug.Log("넘어가는거 기다리는중"+"/ 마지막테이블카운트 "+tables.Last().ViewTableList.Count);
            yield return new WaitForSeconds(1f);



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
