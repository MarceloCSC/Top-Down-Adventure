using UnityEngine;

public class IncreaseHealth : PickUpable
{

    [SerializeField] float healthPoints = 1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (!playerHealth.HealthIsFull)
            {
                playerHealth.ChangeHealth(healthPoints);
                gameObject.SetActive(false);
            }
        }
    }

}
