using UnityEngine;

public class DeflectProjectiles : Specials
{

    [Header("Mana")]
    [SerializeField] float costPerUse = 2.0f;
    [SerializeField] float continuousUse = 0.01f;
    [Header("Projectile Speed")]
    [SerializeField] float minSpeed = 5.0f;
    [SerializeField] float maxSpeed = 18.0f;
    [SerializeField] float speedMultiplier = 3.0f;
    [Header("Deflect Shield")]
    [SerializeField] float radius = 0.9f;
    [SerializeField] GameObject shield = default;
    [SerializeField] GameObject arrow = default;
    [SerializeField] bool toggleGizmos = true;

    private GameObject lockedObject;
    private Vector2 direction;

    #region Properties
    public bool IsDeflecting { get; private set; }
    #endregion

    #region Cached references
    private PlayerMana myMana;
    #endregion


    private void Awake()
    {
        SetReferences();
    }

    private void Update()
    {
        if (isSelected)
        {
            if (Input.GetButtonDown("Special 1") && !myMana.IsOutOfMana(costPerUse))
            {
                myMana.UseMana(-costPerUse);
                shield.SetActive(true);
            }

            if (IsDeflecting)
            {
                if (Input.GetButton("Special 1") && !myMana.IsOutOfMana(continuousUse))
                {
                    myMana.UseMana(-continuousUse);
                    CalculateDirection();
                }
                else
                {
                    Deflect();
                }
            }
        }
    }

    private void Deflect()
    {
        Rigidbody2D projectile = lockedObject.GetComponent<Rigidbody2D>();

        Vector2 speed = direction * speedMultiplier;
        projectile.velocity = speed.ClampMagnitude(minSpeed, maxSpeed);
        projectile.transform.right = projectile.velocity;

        IsDeflecting = false;
        arrow.SetActive(false);
        Time.timeScale = 1;
    }

    public void SearchProjectiles()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.Center(), radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Projectile>() && !collider.GetComponent<Projectile>().IsFriendly)
            {
                lockedObject = collider.gameObject;
                lockedObject.GetComponent<Projectile>().IsFriendly = true;
                lockedObject.layer = LayerMask.NameToLayer("Player Projectile");

                IsDeflecting = true;
                arrow.SetActive(true);
                arrow.transform.position = lockedObject.transform.position;

                Time.timeScale = 0.1f;
            }
        }
    }

    private void CalculateDirection()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lockedObject.transform.position;
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }

    private void OnDrawGizmos()
    {
        if (toggleGizmos)
        {
            Gizmos.DrawWireSphere(transform.Center(), radius);
        }
    }

    private void SetReferences()
    {
        myMana = GetComponent<PlayerMana>();
    }

}
