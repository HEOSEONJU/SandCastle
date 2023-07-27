using inGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame
{
    public class PatrolSetting : MonoBehaviour
    {
        
        [SerializeField]
        MasterController playerTransform;
        
        public Transform PlayerTransform
        {
            get { return playerTransform.transform; }
        }


        public int GenCount
        {
            get
            {
                if(patrolPoint==null)
                {
                    return 0;
                }
                return patrolPoint.Count; 
            }
        }

        [SerializeField]
        List<PatrolPoint> patrolPoint;
        public void Init(float defaulthp,float multiply)//����Ʈ����
        {
            patrolPoint = transform.GetComponentsInChildren<PatrolPoint>().ToList();
            
            foreach(var p in patrolPoint)
            {
                p.Init(defaulthp,multiply);
            }
        }


        public PatrolPoint SwpanPoint(int n)//n������Ʈ ��ȯ
        {

            return patrolPoint[n];
        }

        public bool CheckPoint()//��������Ʈ�� �ִ��� Ȯ��
        {
            if(patrolPoint.FindIndex(x=>x.Enable==true)==-1)
            {
                return true;
            }
            return false;
        }
    }
}