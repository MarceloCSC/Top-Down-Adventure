using UnityEngine;

public class DeflectShield : MonoBehaviour
{

    #region Cached references
    private DeflectProjectiles deflectProjectiles;
    private Animator animator;
    #endregion


    private void Awake()
    {
        SetReferences();
    }

    private void Update()
    {
        if (!animator.IsPlaying(0, "Shield Active")
            && !deflectProjectiles.IsDeflecting)
        {
            gameObject.SetActive(false);
        }
    }

    public void DeflectProjectiles()
    {
        deflectProjectiles.SearchProjectiles();
    }

    private void SetReferences()
    {
        deflectProjectiles = GetComponentInParent<DeflectProjectiles>();
        animator = GetComponent<Animator>();
    }

}
