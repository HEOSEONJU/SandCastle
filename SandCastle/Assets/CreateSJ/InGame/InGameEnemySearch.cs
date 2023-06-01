using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
namespace InGame
{
    public class InGameEnemySearch : MonoBehaviour
    {
        [SerializeField]
        List<Enemy_Manager> target;
        [SerializeField]
        float searchRange;

        [SerializeField]
        CircleCollider2D collider2d;
        public List<Enemy_Manager> Target { get { return target; } }

        InGameAttack inGameAttack;

        private void OnEnable()
        {
            target = new List<Enemy_Manager>();
            collider2d.radius= searchRange;
            TryGetComponent<InGameAttack>(out inGameAttack);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<Enemy_Manager>(out Enemy_Manager temp);
                if (!(temp is null))
                {
                    Target.Add(temp);
                    if (Target.Count >= 2)
                    {
                        Target.Sort((a, b) => (a.transform.position - transform.position).magnitude.CompareTo((b.transform.position - transform.position).magnitude));
                    }

                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<Enemy_Manager>(out Enemy_Manager temp);
                if (!(Target is null))
                {
                    Target.Remove(temp);
                    if (Target.Count >= 2)
                    {
                        Target.Sort((a, b) => (a.transform.position - transform.position).magnitude.CompareTo((b.transform.position - transform.position).magnitude));
                    }

                }
                if(Target.Count==0)
                {
                    inGameAttack.ResetAngle();
                }
            }
        }

    }
}