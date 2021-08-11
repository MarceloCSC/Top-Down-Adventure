using UnityEngine;

public class IncreaseMana : PickUpable
{

    [SerializeField] float manaPoints = 1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerMana playerMana = other.GetComponent<PlayerMana>();

            if (!playerMana.ManaIsFull)
            {
                playerMana.UseMana(manaPoints);
                gameObject.SetActive(false);
            }

        }
    }

}
