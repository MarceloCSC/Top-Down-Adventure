using UnityEngine;

public class Debugger : MonoBehaviour
{

    [SerializeField] bool invinciblePlayer = false;
    [SerializeField] bool invincibleEnemies = false;


    private void Update()
    {
        if (invinciblePlayer)
        {
            FindObjectOfType<PlayerHealth>().DebugHealth = Mathf.Infinity;
        }

        if (invincibleEnemies)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.GetComponent<EnemyDefense>().DebugHealth = Mathf.Infinity;
            }
        }
    }

}
