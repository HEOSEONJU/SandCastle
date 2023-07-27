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
        public void Init(float defaulthp,float multiply)//게이트생성
        {
            patrolPoint = transform.GetComponentsInChildren<PatrolPoint>().ToList();
            
            foreach(var p in patrolPoint)
            {
                p.Init(defaulthp,multiply);
            }
        }


        public PatrolPoint SwpanPoint(int n)//n번게이트 반환
        {

            return patrolPoint[n];
        }

        public bool CheckPoint()//남은게이트가 있는지 확인
        {
            if(patrolPoint.FindIndex(x=>x.Enable==true)==-1)
            {
                return true;
            }
            return false;
        }
    }
}