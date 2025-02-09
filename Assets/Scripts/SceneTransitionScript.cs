using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour
{
    public SceneTransitionScript instance;

    [SerializeField] Animator anim;


    public IEnumerator sceneTransitons(string scene)
    {
        anim.SetTrigger("transTrigger");

        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene(scene);
    }
}
