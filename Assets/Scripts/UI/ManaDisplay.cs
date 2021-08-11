using UnityEngine;

public class ManaDisplay : UISlider
{


    #region Cached references
    private PlayerMana playerMana;
    #endregion


    private void Awake()
    {
        playerMana = GameObject.FindWithTag("Player").GetComponent<PlayerMana>();
    }

    private void OnEnable()
    {
        playerMana.OnManaChanged += SetValue;
    }

    private void OnDisable()
    {
        playerMana.OnManaChanged -= SetValue;
    }

}
