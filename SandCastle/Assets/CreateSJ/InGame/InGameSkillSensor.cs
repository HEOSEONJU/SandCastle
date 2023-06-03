using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class InGameSkillSensor : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> gameObjects;
        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
        }
        void Start()
        {
            gameObjects = new List<GameObject>();
        }




        public void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.CompareTag("Enemy"))
            {
                gameObjects.Add(collision.gameObject);
            }

        }
        

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                gameObjects.Remove(collision.gameObject);
            }
        }
    }
}