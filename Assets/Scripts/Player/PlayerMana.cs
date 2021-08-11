using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{

    public event Action<float> OnManaChanged = delegate { };


    private float maxMana;
    private float currentMana;

    #region Properties
    public bool ManaIsFull => currentMana == maxMana;
    public bool IsOutOfMana(float amount) => currentMana < amount;
    #endregion


    private void Awake()
    {
        SetValues();
    }

    private void Start()
    {
        currentMana = maxMana;
    }

    public void UseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        float updatedValue = currentMana / maxMana;
        OnManaChanged(updatedValue);
    }

    private void SetValues()
    {
        maxMana = GetComponent<PlayerController>().myStats.GetMana;
    }

}
