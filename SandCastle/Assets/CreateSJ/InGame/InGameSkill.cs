
using UnityEngine;
using SkillEnums;

using System.Linq;
using Skill;
namespace InGame
{
    public class InGameSkill : MonoBehaviour
    {
        
        
        [SerializeField]
        InGame_Char inGameChar;
        [SerializeField]
        InGameSkillSensor inGameSkillSensor;

        SkillData skillData;
        [SerializeField]
        SkillObject skillPrefab;

        public Transform target;

        [SerializeField]
        Transform spwanposi;
        [SerializeField]
        Transform poolingParent;
        public void Init(InGameSkillSensor sensor,Transform parent,ObjectTable skilltable,string skillname)
        {
            inGameSkillSensor= sensor;
            poolingParent= parent;
            skillData = new SkillData();
            
            skillData.InitData(skillname, skilltable);
            
            
            
            Resources.Load<GameObject>("Prefab/SkillPrefab/" + skillData.SkillObjectKey).TryGetComponent<SkillObject>(out skillPrefab);
            


            //ActiveSkill();
        }

        public bool SettingTarget()
        {
            if(inGameSkillSensor.GameObjects.Count==0)
            {
                return false;
            }
            switch (skillData.Target)
            {
                case SkillTarget.Near:
                    inGameSkillSensor.GameObjects =inGameSkillSensor.GameObjects.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();
                    
                    target = inGameSkillSensor.GameObjects.First().transform;

                    break;
                case SkillTarget.Random:
                    target = inGameSkillSensor.GameObjects[UnityEngine.Random.Range(0, inGameSkillSensor.GameObjects.Count)].transform;

                    break;
                case SkillTarget.Far:
                    inGameSkillSensor.GameObjects = inGameSkillSensor.GameObjects.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();
                    target = inGameSkillSensor.GameObjects.Last().transform;

                    break;
            }
            return true;
        }


        public void ActiveSkill()
        {
            if(target== null)
            {
                return;
            }

            var e = ObjectPooling.GetObject(skillPrefab.gameObject, poolingParent);//.TryGetComponent<Abstract_Bullet>(out Abstract_Bullet bulletobject);

            e.GetComponent<SkillObject>().Init(skillData);
            
            
            
            switch (skillData.Spwan)
            {
                case SkillSpwan.Player:
                    Debug.Log("스킬생성위치");
                    e.GetComponent<SkillObject>().Active(spwanposi, target);
                    break;
                case SkillSpwan.Target:
                    e.GetComponent<SkillObject>().Active(target, target);
                    break;
                case SkillSpwan.Position:

                    break;
            }
            //e.GetComponent<SkillObject>().Active()
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