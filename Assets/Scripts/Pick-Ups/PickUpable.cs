using System.Collections;
using UnityEngine;

public class PickUpable : MonoBehaviour
{

    [SerializeField] float timeToDespawn = 2f;


    private void OnEnable()
    {
        StartCoroutine(AutoDespawn());
    }

    private IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(timeToDespawn);

        gameObject.SetActive(false);
    }

}
