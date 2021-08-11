using UnityEngine;

public class EnemyPooler : ObjectPooler
{

    #region Properties
    public static new EnemyPooler Instance { get; private set; }
    #endregion


    protected override void Awake()
    {
        Instance = this;
        CreatePool();
    }

}
