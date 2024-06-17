using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AltarBrick : MonoBehaviour
{
    public Transform target;  // ���a��Transform
    public float jumpForce = 5f;  // �����O��
    public float upwardsForce = 2f;  // �V�W���O��
    public float minJumpInterval = 1f;  // �����̤p���j
    public float maxJumpInterval = 3f;  // �����̤j���j
    private Animator animator;  // ??���
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private int gridLayerMask;

    [SerializeField] GameObject bullet;
    [SerializeField] float rotationStep = 30f;
    [SerializeField] float bulletSpeed = 5f;

    bool JumpAMDelayBool = false;

    private void Start()
    {
        gridLayerMask = LayerMask.GetMask("Grid");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        StartCoroutine(JumpCoroutine());
    }
    private void Update()
    {
        isGrounded = IsOnGround();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && JumpAMDelayBool == false)
        {
            if(isGrounded)
            {
                animator.SetBool("Land", true);
            }
        }
        else
        {
            animator.SetBool("Land", false);
        }
    }

    IEnumerator JumpCoroutine()
    {
        while (true)
        {
            if (isGrounded)
            {
                animator.SetTrigger("End");
            }
            // ����?��???�j
            yield return new WaitForSeconds(Random.Range(minJumpInterval, maxJumpInterval));

            if (isGrounded)
            {
                // ��¦V���a����V�V�q
                Vector3 direction = (target.position - transform.position).normalized;

                // ���O��
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * upwardsForce, ForceMode2D.Impulse);

                StartCoroutine(JumpAMDelay());
                animator.SetTrigger("Jump");
            }
            
        }
    }
    private bool IsOnGround()
    {
        // �ϥήg�u�˴��O�_�b Grid �a���W
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0f, 0f), Vector2.down, 1f, gridLayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0f, 0f), Vector2.down, 1f, gridLayerMask);

        Debug.DrawRay(transform.position + new Vector3(-0.5f, 0f, 0f), Vector2.down * 1f, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.5f, 0f, 0f), Vector2.down * 1f, Color.red);

        if ((hit1.collider != null || hit2.collider != null) && !isGrounded)
        {
            Atk();
        }

        return hit1.collider != null || hit2.collider != null;
    }

    IEnumerator JumpAMDelay()
    {
        JumpAMDelayBool = true;
        yield return new WaitForSeconds(1f);
        JumpAMDelayBool = false;
    }
    void Atk()
    {
        float startAngle = 0;  // ���ΰ_�l����
        float endAngle = 180f;  // ����?������
        float intervalAngle = 60f;  // �C��?�g�l?������?�j

        for (float angle = startAngle; angle <= endAngle; angle += intervalAngle)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * transform.right;

            GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
