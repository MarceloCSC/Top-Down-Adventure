
using UnityEngine;

[CreateAssetMenu(fileName = "NewSavableVector", menuName = "Savable Vector")]
public class SavableVector : ScriptableObject, ISerializationCallbackReceiver
{
    
    public Vector2 currentValue;
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        currentValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {

    }
    
}
