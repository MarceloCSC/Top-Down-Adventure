using UnityEngine;
using UnityEngine.UI;

public class HeartsDisplay : MonoBehaviour
{

    [SerializeField] Image[] hearts = default;
    [SerializeField] Sprite fullHeart = default;
    [SerializeField] Sprite halfFullHeart = default;
    [SerializeField] Sprite emptyHeart = default;

    private float heartContainersTotal = 10.0f;

    #region Cached references
    private PlayerController playerController;
    #endregion


    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.OnDataRequested += UpdateHearts;
    }

    private void OnDisable()
    {
        playerController.OnDataRequested -= UpdateHearts;
    }

    public void UpdateHearts(float currentHealth)
    {
        float currentHearts = currentHealth / 2;
        for (int i = 0; i < heartContainersTotal; i++)
        {
            if (!hearts[i].gameObject.activeSelf)
            {
                hearts[i].gameObject.SetActive(true);
            }

            if (i <= currentHearts - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i >= currentHearts)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = halfFullHeart;
            }
        }
    }

}
