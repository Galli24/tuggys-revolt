using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour {

    float targetDistance;

    public float moveSpeed;
    [Range(0f, 1f)]
    public float turnSpeed;

    public float range;
    public float maxDistanceFromTarget;
    public float fov;

    private EnemyHealth eh;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;

    [SerializeField]
    private float damage;

    Transform target;
    PlayerHealth playerHealth;

    [SerializeField]
    private float attackRate = 0.2f;
    private float cooldown = 0f;

    private Animator animator;

	void Start ()
    {
        m_CharacterController = GetComponent<CharacterController>();
        eh = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }
	
	void FixedUpdate ()
    {
        if (eh.getDead())
            return;

        if (cooldown > 0f)
            cooldown -= Time.fixedDeltaTime;

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = target.GetComponent<PlayerHealth>();
            return;
        }

        targetDistance = Vector2.Distance(new Vector2(target.position.x, target.position.z),
            new Vector2(transform.position.x, transform.position.z));

        Vector3 moveDir = Vector3.zero;

        moveDir += Physics.gravity * 30 * Time.fixedDeltaTime;

        if (targetDistance <= range && !Flags.instance.isDead)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0;

            //Fov
            //float angle = Vector3.Angle(direction, transform.forward);
            //if (angle > fov)
            //    return;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), turnSpeed);

            Vector3 desiredMove = transform.forward;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            moveDir.x = desiredMove.x * moveSpeed;
            moveDir.z = desiredMove.z * moveSpeed;

            animator.SetFloat("Move_x", moveDir.x);
            animator.SetFloat("Move_z", moveDir.z);

            if (targetDistance > maxDistanceFromTarget)
                m_CollisionFlags = m_CharacterController.Move(moveDir * Time.fixedDeltaTime);
            else
                Attack();
        }
        else
        {
            animator.SetFloat("Move_x", moveDir.x);
            animator.SetFloat("Move_z", moveDir.z);
        }
	}

    IEnumerator StopAttackAnim()
    {
        yield return new WaitForSeconds(.542f);
        animator.SetBool("Attack", false);
    }

    void Attack()
    {
        if (cooldown > 0f || Flags.instance.isDead)
            return;

        animator.SetBool("Attack", true);
        playerHealth.takeDamage(damage);
        cooldown = attackRate;
        StartCoroutine(StopAttackAnim());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
