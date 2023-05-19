using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;

using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class ObjectTable : ScriptableObject
{
    [SerializeField]
    const string associatedSheet = "1SXq75YN6BygXd7AUDisqXGDNykMw2OtvlAqvVhyZe5s";

    
    [SerializeField]
    public string associatedWorksheet = "";






    public List<Hashtable> hashTableList;
    [SerializeField]
    public Hashtable last;


    [Header ("시작하는A1의 인덱스")]
    public string startINDEX_A1  = "DB_FIELD_NAMES";
    [Header("무시할A열의 값들")]
    public List<string> igonoreNames;
    [Header("읽어올 A열의 값들")]
    public List<string> values;
    public List<string> hashKeyList;


    public string AssociatedSheet
    {
        get { return associatedSheet; }
    }


    [SerializeField]
    int listCount;


    public void INIT()
    {
        if (hashTableList is null)
        {
            hashTableList = new List<Hashtable>();
        }
        hashTableList.Clear();
        if (igonoreNames is null)
        {
            igonoreNames = new List<string>();
        }
        if (hashKeyList is null)
        {
            hashKeyList = new List<string>();
        }
        hashKeyList.Clear();
    }

    internal void UpdateStats(List<GSTU_Cell> list)
    {
        if(startINDEX_A1 is null)
        {
            return;
        }

        
        
        
        
        Hashtable ht = new Hashtable();

        foreach (GSTU_Cell cell in list)
        {
            if ( (!cell.value.Equals("") ) &&  (!igonoreNames.Contains(cell.columnId)) )
            {
                ht.Add(cell.columnId, cell.value);
                if(hashKeyList.Contains(cell.columnId)==false)
                    hashKeyList.Add(cell.columnId);
            }
        }
        hashTableList.Add(ht);
        last = ht;
        listCount =hashTableList.Count;


    }
    public string FindData(string Key, string colum)//  Key Value의 찾을오브젝트의아이디   colum 해시키리스트의 해시값
    {
        
        if(hashTableList == null)
        {
            
            return null;
        }

        return hashTableList.Find(x => x[startINDEX_A1].ToString() == Key)[colum].ToString();
    }



}

[CustomEditor(typeof(ObjectTable))]
public class DataEditor : Editor
{


    public  ObjectTable staticData;
    

    
    public  void RESET(ObjectTable data)
    {
        staticData = data;
        UpdateStats(UpdateMethodOne);
        
    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");
        

        if (GUILayout.Button("Pull Data  One"))
        {
            //UpdateStats(UpdateMethodTwo);
        }
    }

     void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {

        SpreadsheetManager.Read(new GSTU_Search(staticData.AssociatedSheet, staticData.associatedWorksheet), callback);
        
        
    }

     void UpdateMethodOne(GstuSpreadSheet ss)
    {

        staticData.values.Clear();
        
        foreach (GSTU_Cell INDEX in ss.columns[staticData.startINDEX_A1])
        {

            
            if (!staticData.igonoreNames.Contains(INDEX.rowId))
            {
                
                staticData.values.Add(INDEX.rowId);
            }
            

        }
        staticData.INIT();
        foreach (string dataName in staticData.values)
        {

            staticData.UpdateStats(ss.rows[dataName]);
        }

        
    }
    



}
/////////////////
///해시인덱스를 이용한 데이터탐색
///
///
///
///
/// string findid = "Object00001";
/// int I = data.items4.FindIndex(x => x[data.StartINDEX_A1].ToString() == findid);
/// Debug.Log(data.items4[I]["resourceType"]);
/////////////////










