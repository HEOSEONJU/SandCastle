using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMoveManager : MonoBehaviour
{
    static SceneMoveManager instance;

    public static SceneMoveManager Instance
    {
        get { return instance; }
    }
    public void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }




    }

    public void ImmediatelyChangeScne(string scenename)//즉시 씬이동
    {
        if(SceneManager.GetActiveScene().name!= scenename)

        SceneManager.LoadScene(scenename);

    }
    public void AsyncChangeScne(string scenename)//비동기씬이동
    {
        if (SceneManager.GetActiveScene().name != scenename)
            StartCoroutine(SceneLoading(scenename));

    }
    public
    IEnumerator SceneLoading(string scenename)
    {

        Debug.Log("로딩시작");

        yield return null;
        // "AsyncOperation"라는 "비동기적인 연산을 위한 코루틴을 제공"
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);
        operation.allowSceneActivation = true;


        while (!operation.isDone)
        {

            yield return null;
        }
        yield break;


    }
}
