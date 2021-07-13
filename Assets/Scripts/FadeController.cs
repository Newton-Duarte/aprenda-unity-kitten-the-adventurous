using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    [SerializeField] Animator animator;
    int sceneToGo;

    internal void startFade(int sceneIndex)
    {
        sceneToGo = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    internal void OnFadeComplete() => SceneManager.LoadScene(sceneToGo);
}
