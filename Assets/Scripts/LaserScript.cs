using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startTransform; // 起點Transform
    public Transform targetTransform; // 目標(終點)Transform

    public float laserDuration = 5f;
    private LineRenderer lineRenderer; 

    public LayerMask collisionLayer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //設置起點
        lineRenderer.SetPosition(0, startTransform.position);

        Destroy(gameObject, laserDuration);
    }

    private void FixedUpdate()
    {
        // 調整雷射的角度和最大距離
        float lerpValue = Vector3.Distance(startTransform.position, targetTransform.position);

        Vector3 endPosition = Vector3.Lerp(startTransform.position, targetTransform.position, lerpValue);
        
        // 設置終點
        lineRenderer.SetPosition(1, endPosition);

        // 檢測射線和物體的碰撞
        RaycastHit2D[] hits = Physics2D.RaycastAll(startTransform.position, endPosition - startTransform.position, lerpValue, collisionLayer);

        Color color =  Color.green;

        Debug.DrawRay(startTransform.position, (endPosition - startTransform.position).normalized * lerpValue, color);

        foreach (RaycastHit2D hit in hits)
        {
            // 處理每個碰撞
            if (hit.collider != null)
            {
                if(hit.collider.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<PlayerInfo>().TakeSpecialDamage2(6,0.25f); //每0.25秒造成6點傷害
                }
            }
        }
    }

}
