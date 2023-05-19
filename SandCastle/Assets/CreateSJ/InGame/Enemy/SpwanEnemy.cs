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

        private void Start()
        {
            StartCoroutine(Spwan());
        }

        IEnumerator Spwan()
        {

            while (true)
            {
                var a = ObjectPooling.GetObject(prefab.gameObject, this.transform);
                a.transform.position = this.transform.position;
                a.GetComponent<Enemy_Manager>().StartMove(startPoint);
                
                yield return new WaitForSeconds(3f);
            }
        }

    }
}
