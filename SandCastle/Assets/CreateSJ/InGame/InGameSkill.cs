using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillEnums;
using inGame;

namespace InGame
{
    public class InGameSkill : MonoBehaviour
    {
        [SerializeField]
        ObjectTable skillTable;
        [SerializeField]
        ObjectTable skillObjectTable;
        [SerializeField]
        InGame_Char inGameChar;

        SkillData skillData;
        [SerializeField]
        GameObject skillPrefab;

        [SerializeField]
        Transform poolingParent;
        public void Init()
        {
            
            skillData = new SkillData();
            
            skillData.InitData("Skill00001", skillTable);
            
            
            
            skillPrefab = Resources.Load<GameObject>("Prefab/SkillPrefab/" + skillData.SkillObjectKey);
            


            //ActiveSkill();
        }



        public void ActiveSkill()
        {

            var e = ObjectPooling.GetObject(skillPrefab, poolingParent);//.TryGetComponent<Abstract_Bullet>(out Abstract_Bullet bulletobject);

            e.GetComponent<SkillObject>().Init(skillData);

            /*
            if (bulletobject is null)
            {
                return;
            }
            
            bulletobject.transform.position = attackPoint.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletobject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            bulletobject.DamagePoint = status.GiveDamage;
            bulletobject.Move();
            CanAttack = false;
            StartCoroutine(Delay());
            */
        }





    }

}