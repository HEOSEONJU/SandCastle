using InGameResourceEnums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace InGame
{
    public class Water_Mine : Base_Mine
    {








        IEnumerator Ani;

        [SerializeField]
        float delay=0.3f;
        public void OnEnable()
        {
            Ani = SwapImage();
            StartCoroutine(Ani);
        }
        public void OnDisable()
        {
            StopCoroutine(Ani);
        }


        IEnumerator SwapImage()
        {
            while(true) 
            {
                if(sprites==null || sprites.Count==0)
                {
                    yield break;
                }


                Debug.Log(State+gameObject.name);

                switch (State)
                {
                    case ResourceState.Full:
                        mainImage.sprite = sprites[0];
                        Debug.Log("풀1");
                        break;
                    case ResourceState.Half:
                        mainImage.sprite = sprites[2];
                        Debug.Log("하프3");
                        break;
                    case ResourceState.Dead:
                        mainImage.sprite = sprites[4];
                        break;


                }
                yield return new WaitForSeconds(delay);




                switch (State)
                {
                    case ResourceState.Full:
                        mainImage.sprite = sprites[1];
                        Debug.Log("풀2");
                        break;
                    case ResourceState.Half:
                        mainImage.sprite = sprites[3];
                        Debug.Log("하프4");
                        break;
                    case ResourceState.Dead:
                        mainImage.sprite = sprites[5];
                        break;


                }
                yield return new WaitForSeconds(delay);
                
            }
            

        }



        public override void Change_Image()
        {
            return;
        }

    }
}