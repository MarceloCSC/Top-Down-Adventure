using System.Collections;
using UnityEngine;

public enum EnemyState
{
    idle,
    chase,
    patrol,
    attack,
    stun,
    dead
}

public class Enemy : MonoBehaviour
{
    public EnemyStats enemyStats;

    [SerializeField] private float chaseRadius = 7f;
    [SerializeField] private float attackRadius = 1.2f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private GameObject deadSprite = default;
    [SerializeField] private float deathDuration = 0.4f;

    #region Properties

    public EnemyState CurrentState { get; private set; }
    public Transform Target { get; private set; }
    public bool IsDead { get; private set; }

    #endregion

    #region Cached references

    private GameObject player;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private LootSpawner lootSpawner;
    private Animator animator;

    #endregion

    private void Awake()
    {
        SetReferences();
    }

    private void OnEnable()
    {
        ComeToLife();
        ListenToEvents();
    }

    private void FixedUpdate()
    {
        if (!IsDead)
        {
            CheckDistance();
            PrepareAttack();
            UpdateAnimation();
        }
    }

    private void CheckDistance()
    {
        if (CurrentState == EnemyState.stun || CurrentState == EnemyState.attack) { return; }
        else if (Vector2.Distance(Target.position, transform.position) <= chaseRadius
                && Vector2.Distance(Target.position, transform.position) > attackRadius)
        {
            ChangeState(EnemyState.chase);
            enemyMovement.Move(Target);
        }
        else if (GetComponent<EnemyPatrol>() != null && GetComponent<EnemyPatrol>().ShouldPatrol)
        {
            ChangeState(EnemyState.patrol);
        }
        else
        {
            ChangeState(EnemyState.idle);
        }
    }

    private void PrepareAttack()
    {
        if (CurrentState == EnemyState.stun) { return; }
        else if (Vector2.Distance(Target.position, transform.position) <= attackRadius
            && Vector2.Distance(Target.position, transform.position) > minDistance)
        {
            enemyMovement.FlipToTarget(Target);
            enemyAttack.ChooseAttack();
        }
    }

    private void UpdateAnimation()
    {
        if (CurrentState == EnemyState.chase || CurrentState == EnemyState.patrol)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
        }
    }

    private void StopAttack(bool playerIsDead)
    {
        CurrentState = EnemyState.stun;
    }

    private void ComeToLife()
    {
        IsDead = false;
        EnableColliders(true);
        FindObjectOfType<EnemyTracker>().Register(this);
        CurrentState = EnemyState.idle;
    }

    public IEnumerator Die()
    {
        IsDead = true;
        EnableColliders(false);

        lootSpawner.DropLoot();
        animator.SetTrigger("getKilled");

        yield return new WaitForSeconds(deathDuration);

        GameObject carcass = Instantiate(deadSprite, transform.position, Quaternion.identity);
        carcass.transform.localScale = transform.localScale;

        gameObject.SetActive(false);
    }

    private void EnableColliders(bool deactivate)
    {
        foreach (Collider2D colliders in GetComponents<Collider2D>())
        {
            colliders.enabled = deactivate;
        }
    }

    private void SetReferences()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        lootSpawner = GetComponent<LootSpawner>();
        player = GameObject.FindWithTag("Player");
        Target = player.transform;
    }

    private void ListenToEvents()
    {
        player.GetComponent<PlayerController>().OnPlayerKilled += StopAttack;
    }

    private void OnDisable()
    {
        FindObjectOfType<EnemyTracker>().Deregister(this);
    }
}