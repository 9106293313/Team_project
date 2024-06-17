using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    GameObject FadeObj;
    public string targetSceneName;
    void Start()
    {
        FadeObj = GameObject.FindWithTag("FadeObj").transform.Find("FadeObj").gameObject;
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        FadeObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        FadeObj.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(0.5f);
        FadeObj.GetComponent<Animator>().SetTrigger("In");
        SceneManager.LoadScene(targetSceneName);
    }
}
