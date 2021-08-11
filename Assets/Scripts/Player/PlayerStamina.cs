using System;
using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{

    public event Action<float> OnStaminaChanged = delegate { };


    [SerializeField] float delayToRecover = 2.0f;
    [SerializeField] float delayWhenDepleted = 4.0f;
    [SerializeField] float recoverySpeed = 0.005f;

    private float maxStamina;
    private float currentStamina;
    private float amountToSpend;

    #region Properties
    public bool IsOutOfStamina => currentStamina < amountToSpend;
    #endregion
    

    private void Awake()
    {
        SetValues();
    }

    private void Start()
    {
        currentStamina = maxStamina;
    }

    public void SpendStamina()
    {
        currentStamina = Mathf.Clamp(currentStamina - amountToSpend, 0.0f, maxStamina);

        UpdateDisplay();

        float timeToWait = currentStamina == 0.0f ? delayWhenDepleted : delayToRecover;

        StopAllCoroutines();
        StartCoroutine(RecoverStamina(timeToWait));
    }

    private IEnumerator RecoverStamina(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        while (currentStamina < maxStamina)
        {
            currentStamina += recoverySpeed;
            UpdateDisplay();

            yield return null;
        }
    }

    private void UpdateDisplay()
    {
        float updatedValue = currentStamina / maxStamina;
        OnStaminaChanged(updatedValue);
    }

    private void SetValues()
    {
        maxStamina = GetComponent<PlayerController>().myStats.GetStamina;
        amountToSpend = GetComponent<PlayerController>().myStats.StaminaCost;
    }

}
