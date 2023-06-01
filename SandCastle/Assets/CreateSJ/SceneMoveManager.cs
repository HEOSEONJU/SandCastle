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

    public void ImmediatelyChangeScne(string scenename)//��� ���̵�
    {
        if(SceneManager.GetActiveScene().name!= scenename)

        SceneManager.LoadScene(scenename);

    }
    public void AsyncChangeScne(string scenename)//�񵿱���̵�
    {
        if (SceneManager.GetActiveScene().name != scenename)
            StartCoroutine(SceneLoading(scenename));

    }
    public
    IEnumerator SceneLoading(string scenename)
    {

        Debug.Log("�ε�����");

        yield return null;
        // "AsyncOperation"��� "�񵿱����� ������ ���� �ڷ�ƾ�� ����"
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);
        operation.allowSceneActivation = true;


        while (!operation.isDone)
        {

            yield return null;
        }
        yield break;


    }
}
