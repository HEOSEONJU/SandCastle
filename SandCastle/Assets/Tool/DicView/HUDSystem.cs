using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AT.HUDSystem
{
    public enum HUD_OBJECT_TYPE
    {
        Damage_Normal,
        Damage_Critical,
    }

    [System.Serializable]
    public class DictionaryOfHUDTypeAndTextMesh : AT.SerializableDictionary.SerializableDictionary<HUD_OBJECT_TYPE, TextMeshPro> { }

    [System.Serializable]
    public class DictionaryOfPoolBase : AT.SerializableDictionary.SerializableDictionary<HUD_OBJECT_TYPE, List<TextMeshPro>> { }

    public class HUDSystem : MonoBehaviour
    {
        /// <summary> HUD System Prefab List </summary>
        public DictionaryOfHUDTypeAndTextMesh prefabList = new DictionaryOfHUDTypeAndTextMesh();

        /// <summary> HUD System Pool Container </summary>
        public DictionaryOfPoolBase pools = new DictionaryOfPoolBase();

        public void Initialize()
        {
            pools.Clear();
            foreach (var obj in prefabList)
            {
                List<TextMeshPro> poolList = new List<TextMeshPro>();
                pools.Add(obj.Key, poolList);
            }
        }

        public TextMeshPro CreateItem(HUD_OBJECT_TYPE type)
        {
            if (prefabList.ContainsKey(type))
            {
                TextMeshPro newTextMesh = Instantiate(prefabList[type]);
                newTextMesh.transform.position = Vector3.zero;
                return newTextMesh;
            }

            Debug.LogErrorFormat("Can not Find Target HUD Object. Type : {0}", type.ToString());
            return null;
        }

        public TextMeshPro GetItem(HUD_OBJECT_TYPE type)
        {
            for (int i = 0; i < pools[type].Count; i++)
            {
                if (!pools[type][i].gameObject.activeSelf)
                    return pools[type][i];
            }

            return CreateItem(type);
        }

    }

}


namespace AT.SerializableDictionary
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        // save the dictionary to lists
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // load dictionary from lists
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }
    }
}
