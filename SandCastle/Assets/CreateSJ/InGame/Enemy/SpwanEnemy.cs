using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Enemy
{ 
public class SpwanEnemy : MonoBehaviour
{
        [SerializeField]
        Enemy_Manager prefab;

        [SerializeField]
        Transform startPoint;


        [SerializeField]
        int count = 30;
        private void Start()
        {
            StartCoroutine(Spwan());
        }
         
        IEnumerator Spwan()
        {

            while (count-->0)
            {
                var a = ObjectPooling.GetObject(prefab.gameObject, this.transform);
                a.transform.position = this.transform.position;
                
                a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                if (!(em is null))
                {
                    em.StartMove(startPoint);
                    em.Reseting(1, 10);
                }
                yield return new WaitForSeconds(1f);
            }
        }

    }
}
