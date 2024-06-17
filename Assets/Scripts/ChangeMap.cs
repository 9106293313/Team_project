using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeMap : MonoBehaviour
{
    public bool DoorOpen = false;
    public string targetSceneName;

    GameObject FadeObj;
    public Vector2 TPpos;
    private void Start()
    {
        FadeObj = GameObject.FindWithTag("FadeObj").transform.Find("FadeObj").gameObject;
    }
    private void Update()
    {
        targetSceneName = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().targetSceneName;
        TPpos = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TPpos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(DoorOpen)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(ChangeScene());
            }
        }
    }
    IEnumerator ChangeScene()
    {
        FadeObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        FadeObj.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(0.5f);
        FadeObj.GetComponent<Animator>().SetTrigger("In");
        SceneManager.LoadScene(targetSceneName);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TotalSceneNum += 1;//¸õÂà³õ´º®É+1
        GameObject.FindWithTag("Player").transform.position = new Vector3(TPpos.x, TPpos.y, 0);
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}