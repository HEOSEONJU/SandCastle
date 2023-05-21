using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheetFunction : MonoBehaviour
{
    static SheetFunction instance;
    

    [SerializeField]
    List<ObjectTable> tables;
    [SerializeField]
    List<DataEditor> dataEditors;
    [SerializeField]
    bool Active;

    [SerializeField]
    string mainSceneName;
    
    int checkCount = 0;
    public static SheetFunction  Instacne
    {
        get { return instance; }
        
    }

    private void Awake()
    {
        if(instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if(Active)
            {
                checkCount = 0;
                ReadSheets();
                
            }

            return;
        }
        
        Destroy(gameObject);
    }
    private void Update()
    {
        
    }

    public void ReadSheets()
    {
        if (dataEditors is null)
        {
            dataEditors = new List<DataEditor>();
            
        }
        dataEditors.Clear();
        foreach (ObjectTable table in tables) 
        {

            dataEditors.Add(new DataEditor());
            dataEditors.Last().RESET(table);
            
        }

        
        
    }
    
    public void Check()
    {
        checkCount++;
        if (checkCount == tables.Count)
        {
            Active = false;

            SceneMoveManager.Instance.ImmediatelyChangeScne(mainSceneName);
        }
    }

}
