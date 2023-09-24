
using Player;

using UnityEngine;

public class ReJoinScne : MonoBehaviour
{
    [SerializeField]
    ObjectTable waveTable;
    // Start is called before the first frame update
    void Start()
    {
        int index=PlayerPrefs.GetInt("Stage");
        Debug.Log(index + "¿Œµ¶Ω∫");

        string stagename = waveTable.values[index+1].ToString();
        PlayerDataManager.Instacne.Data.fightCharIds = PlayerDataManager.Instacne.Data.havetCharIds[index].id;

        Debug.Log(stagename);
        Debug.Log(waveTable.FindString(stagename, "stageResourceKey"));


        SceneMoveManager.Instance.ImmediatelyChangeScne(waveTable.FindString(stagename, "stageResourceKey"));
    }


}
