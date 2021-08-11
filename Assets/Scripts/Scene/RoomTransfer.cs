using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransfer : MonoBehaviour
{

    [SerializeField] Vector2 cameraChange = default;
    [SerializeField] Vector3 playerChange = default;
    [Space(20)]
    [SerializeField] bool needText = default;
    [SerializeField] string titleCard = default;
    [SerializeField] float waitTime = 4f;
    [SerializeField] GameObject textUI = default;
    [SerializeField] Text placeholderText = default;

    // Cached references
    private CameraMovement cam;


    private void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minBoundaries += cameraChange;
            cam.maxBoundaries += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(titleCardCoroutine());
            }
        }
    }

    private IEnumerator titleCardCoroutine()
    {
        textUI.SetActive(true);
        placeholderText.text = titleCard;
        yield return new WaitForSeconds(waitTime);
        textUI.SetActive(false);
    }

}
