using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public event Action<float> OnDataRequested = delegate { };
    public event Action<bool> OnPlayerKilled = delegate { };


    public PlayerStats myStats;

    #region Properties
    public PlayerData PlayerData { get; private set; }
    public bool IsDead => PlayerData.Health == 0;
    #endregion


    private void Awake()
    {
        PlayerData = PlayerPersistence.GetData();
    }

    private void OnEnable()
    {
        ListenToEvents();
    }

    private void Start()
    {
        OnDataRequested(PlayerData.Health);
    }

    private void HealthObserver(float currentHealth)
    {
        PlayerData.Health = currentHealth;
        OnDataRequested(PlayerData.Health);

        if (PlayerData.Health == 0)
        {
            OnPlayerKilled(true);
            EnableColliders(false);
        }
    }

    private void EnableColliders(bool deactivate)
    {
        foreach (Collider2D colliders in GetComponents<Collider2D>())
        {
            colliders.enabled = deactivate;
        }
    }

    private void ListenToEvents()
    {
        GetComponent<PlayerHealth>().OnHealthChanged += HealthObserver;
    }

    private void OnDisable()
    {
        GetComponent<PlayerHealth>().OnHealthChanged -= HealthObserver;
    }

    private void OnDestroy()
    {
        PlayerPersistence.SaveData(PlayerData);
    }

}
