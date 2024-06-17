using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startTransform; // �_�ITransform
    public Transform targetTransform; // �ؼ�(���I)Transform

    public float laserDuration = 5f;
    private LineRenderer lineRenderer; 

    public LayerMask collisionLayer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //�]�m�_�I
        lineRenderer.SetPosition(0, startTransform.position);

        Destroy(gameObject, laserDuration);
    }

    private void FixedUpdate()
    {
        // �վ�p�g�����שM�̤j�Z��
        float lerpValue = Vector3.Distance(startTransform.position, targetTransform.position);

        Vector3 endPosition = Vector3.Lerp(startTransform.position, targetTransform.position, lerpValue);
        
        // �]�m���I
        lineRenderer.SetPosition(1, endPosition);

        // �˴��g�u�M���骺�I��
        RaycastHit2D[] hits = Physics2D.RaycastAll(startTransform.position, endPosition - startTransform.position, lerpValue, collisionLayer);

        Color color =  Color.green;

        Debug.DrawRay(startTransform.position, (endPosition - startTransform.position).normalized * lerpValue, color);

        foreach (RaycastHit2D hit in hits)
        {
            // �B�z�C�ӸI��
            if (hit.collider != null)
            {
                if(hit.collider.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<PlayerInfo>().TakeSpecialDamage2(6,0.25f); //�C0.25��y��6�I�ˮ`
                }
            }
        }
    }

}
