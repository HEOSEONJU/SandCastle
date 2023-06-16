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
        List<Transform> target;
        

        [SerializeField]
        CircleCollider2D collider2d;
        public List<Transform> Target { get { return target; } }
        public CircleCollider2D Collider2d
        {
            get { return collider2d; }
        }
        InGameAttack inGameAttack;

        private void OnEnable()
        {
            target = new List<Transform>();
            
            TryGetComponent<InGameAttack>(out inGameAttack);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<IHit>(out IHit temp);


                if (temp is null)
                {
                    return;
                }
                if(!temp.Alive())
                {
                    return;
                }

                Target.Add(collision.transform);
                if (Target.Count >= 2)
                {
                    Target.Sort((a, b) => (a.transform.position - transform.position).magnitude.CompareTo((b.transform.position - transform.position).magnitude));
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<IHit>(out IHit temp);
                if (!(Target is null))
                {
                    Target.Remove(collision.transform);
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