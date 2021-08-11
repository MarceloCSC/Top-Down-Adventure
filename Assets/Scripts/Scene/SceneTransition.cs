using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] string sceneToLoad = default;

    [Header("Set Player Position"), Space]
    [SerializeField] SavableVector storedPosition = default;
    [SerializeField] Vector2 newPosition = default;

    [Header("Reset Camera"), Space]
    [SerializeField] SavableVector minBoundaries = default;
    [SerializeField] SavableVector maxBoundaries = default;
    [SerializeField] Vector2 newMinBoundaries = default;
    [SerializeField] Vector2 newMaxBoundaries = default;

    [Header("Transition Effects"), Space]
    [SerializeField] GameObject fadeInPanel = default;
    [SerializeField] GameObject fadeOutPanel = default;
    [SerializeField] float waitToFade = default;


    private void Awake()
    {
        if (fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            storedPosition.currentValue = newPosition;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        if (fadeInPanel != null)
        {
            Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(waitToFade);

        ResetCameraBounds();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void ResetCameraBounds()
    {
        maxBoundaries.currentValue = newMaxBoundaries;
        minBoundaries.currentValue = newMinBoundaries;
    }

}
