
using UnityEngine;

public class ContextClue : MonoBehaviour
{

    [SerializeField] GameObject sprite = default;


    public void Enable()
    {
        sprite.SetActive(true);
    }

    public void Disable()
    {
        sprite.SetActive(false);
    }

}
