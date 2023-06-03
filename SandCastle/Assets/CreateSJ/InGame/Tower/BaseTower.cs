using Enemy;
using inGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace InGame
{
    public class BaseTower : MonoBehaviour
    {
        [SerializeField]
        TowerSearch towerSearch;
        // Start is called before the first frame update
        [SerializeField]
        GameObject prefab;
        [SerializeField]
        Transform bulletParent;
        [SerializeField]
        Transform target;
        [SerializeField]
        float GiveDamage=5;
        [SerializeField]
        bool CanAttack;
        // Update is called once per frame
        [SerializeField]
        float CoolTime=1f;
        private void Start()
        {
            CanAttack = true;
        }
        void Update()
        {
            Attack();
        }
        public void Attack()
        {
            if(towerSearch.Target.Count>0 && CanAttack)
            {
                if(target== null  || towerSearch.Target.Contains(target.GetComponent<Enemy_Manager>())==false)
                {
                    target = towerSearch.Target[0].transform;
                }

                ObjectPooling.GetObject(prefab, bulletParent).TryGetComponent<Abstract_Bullet>(out Abstract_Bullet bulletobject);
                if (bulletobject is null)
                {
                    return;
                }
                bulletobject.transform.position = transform.position;
                Vector3 dircetion = target.position - this.transform.position;


                

                float angle = Mathf.Atan2(dircetion.y, dircetion.x) * Mathf.Rad2Deg;
                bulletobject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                bulletobject.DamagePoint = GiveDamage;
                bulletobject.Move();
                CanAttack = false;
                StartCoroutine(Delay());
            }
        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(CoolTime);
            CanAttack = true;
        }
    }
}