using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{

    [SerializeField] Slider slider = default;


    protected void SetValue(float value)
    {
        slider.value = value;
    }

}
