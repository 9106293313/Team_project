using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AltarBrick : MonoBehaviour
{
    public Transform target;  // 玩家的Transform
    public float jumpForce = 5f;  // 跳的力度
    public float upwardsForce = 2f;  // 向上的力度
    public float minJumpInterval = 1f;  // 跳的最小間隔
    public float maxJumpInterval = 3f;  // 跳的最大間隔
    private Animator animator;  // ??控制器
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
            // 等待?机???隔
            yield return new WaitForSeconds(Random.Range(minJumpInterval, maxJumpInterval));

            if (isGrounded)
            {
                // 算朝向玩家的方向向量
                Vector3 direction = (target.position - transform.position).normalized;

                // 跳力度
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
        // 使用射線檢測是否在 Grid 地面上
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
        float startAngle = 0;  // 扇形起始角度
        float endAngle = 180f;  // 扇形?束角度
        float intervalAngle = 60f;  // 每次?射子?的角度?隔

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
