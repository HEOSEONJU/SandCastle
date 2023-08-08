using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainUI
{
    public class ZoomCharInfo : MonoBehaviour
    {
        [SerializeField]
        ObjectTable charTable;

        [SerializeField]
        Image mainImage;
        [SerializeField]
        TextMeshProUGUI nameText;
        [SerializeField]
        Image skillImage;
        [SerializeField]
        TextMeshProUGUI skillText;
        [SerializeField]
        TextMeshProUGUI skillExpText;
        [SerializeField]
        TextMeshProUGUI hpText;
        [SerializeField]
        TextMeshProUGUI atkText;
        [SerializeField]
        TextMeshProUGUI rangeText;


        string charkey;

        public void OpenZoom(string key)
        {
            charkey = key;
            string path = "CharIcon/" + charTable.FindString(key, "imageName");
            Sprite temp = Resources.Load<Sprite>(path);
            mainImage.sprite = temp;
            nameText.text = charTable.FindString(key, "charName");
            skillText.text = charTable.FindString(key, "skillname");
            skillExpText.text = charTable.FindString(key, "skillexp");
            hpText.text = "����� : " + charTable.FindString(key, "maxHP");
            atkText.text = "���ݷ� : "+charTable.FindString(key, "giveDamage");
            rangeText.text ="��Ÿ� : "+ charTable.FindString(key, "range");


        }

        public string ReturnCharKey()
        {
            return charkey;
        }
    }
}