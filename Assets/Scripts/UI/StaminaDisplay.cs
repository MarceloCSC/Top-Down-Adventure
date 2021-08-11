using UnityEngine;

public class StaminaDisplay : UISlider
{


    #region Cached references
    private PlayerStamina playerStamina;
    #endregion


    private void Awake()
    {
        playerStamina = GameObject.FindWithTag("Player").GetComponent<PlayerStamina>();
    }

    private void OnEnable()
    {
        playerStamina.OnStaminaChanged += SetValue;
    }

    private void OnDisable()
    {
        playerStamina.OnStaminaChanged -= SetValue;
    }

}
