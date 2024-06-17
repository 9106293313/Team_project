using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float Speed = 10f;
    public int ReverseControlNum = 1;
    public int chargeReverseControlNum = 1;
    public GameObject ArrowPivot;
    float cooldownLimit = 0.2f; //���a�����D�N�o�ɶ�
    float cooldown = 0f;
    float atkTimer = 0f;


    [HideInInspector]public bool charging = false; //�O�_�b����
    float chargeTime = 0f;
    public float AtkChargeTime = 0.6f;

    public Animator animator;
    public ChooseWeapon weaponChoose;

    public GameObject PlayerSprite;
    public GameObject PlayerSpriteRotate;
    public bool isOnGround;
    public float footOffset = 0.375f;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    public GameObject PlayerEffect;
    public GameObject ChargePower;
    public GameObject ChargePower2;
    public GameObject ChargePower3;
    public GameObject ChargePower4;
    public GameObject ChargePower5;
    public AudioSource chargeSound;
    public AudioSource chargeAtkSound;
    bool IsPlayingChargeSound = false;

    private Vector3 MousePosition;
    public float chargeMoveSpeed = 2f;

    public PlayerInfo PlayerInfo;

    public GhostEft ghost;
    float ghostTimer = 0.8f; //���ʹݼv������ɶ�
    float ghostTime = 0f;

    [HideInInspector]public float AtkCoolDown;

    bool CrossBowBool = false;

    //////////////////////////////////�s���ʨt��
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float maxDashDistance = 1f;
    [SerializeField] private float dashGravityScale = 0f;
    bool IsDashing = false; //�O�_�b�i��Ĩ�

    [HideInInspector] public float DashcooldownLimit = 1f; //���a���Ĩ�N�o�ɶ�
    [HideInInspector]public float Dashcooldown = 0f;

    public LineRenderer lineRenderer;

    public AudioSource DashSound;

    public bool playerCanMove=true; //���a��_�ޱ�
    public GameObject JumpParticle;
    public AudioSource JumpSound;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(GameObject.FindWithTag("GameManager").GetComponent<GameManager>().gameIsPaused) //�p�G�C���Ȱ��A�{�����~�����
        {
            return;
        }
        if (GameObject.FindWithTag("CardSelectPanel")!=null) //�p�G�������t��CardSelectPanel��tag������A�{�����~�����
        {
            return;
        }

        if (charging)
        {
            lineRenderer.enabled = true;
            if(CardSystem.HasCard("����") && PlayerInfo.CanShield) //�p�G�����̥d�B�i�H�}�@��
            {
                PlayerInfo.ShieldObj.SetActive(true);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            if (CardSystem.HasCard("����") && PlayerInfo.ShieldObj.activeInHierarchy) 
            {
                if(!PlayerInfo.IsShieldAnimation) //�p�G�����̥d�B�@�ަb���}���𪬺A�ɤ��s�b�A�åB�S���b�����@�ޯ}���ʵe
                {
                    PlayerInfo.ShieldObj.SetActive(false);
                }
            }
        }

        GroundCheck();

        ghostTime+=Time.deltaTime;
        if (ghostTime<ghostTimer)
        {
            ghost.makeGhost=true;
        }
        else
        {
            ghost.makeGhost = false;
        }

        if (cooldown <= cooldownLimit)
        {
            cooldown += Time.deltaTime;
        }
        if (Dashcooldown <= DashcooldownLimit)
        {
            Dashcooldown += Time.deltaTime;
        }

        atkTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(PlayerInfo.curEnergy<=0)
            {
                PlayerInfo.UseNoEnergyText();
            }
            if(atkTimer >= AtkCoolDown)
            {
                Attack();
                atkTimer = 0;
            }
            else
            {
                return;
            }
        }

        if(weaponChoose.weaponNum == 3 && charging) //�⩸�אּ����ɫ��U����~�|����
        {
            if (CrossBowBool == false)
            {
                weaponChoose.BulletLevel(0);
                CrossBowBool = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.Mouse0) && atkTimer >= weaponChoose.currentWeapon.GetComponent<WeaponShoot>().AtkCooldown)
                {
                    CrossBowChargeAtk();
                    atkTimer = 0;
                }
            }
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                CrossBowBool = false;
                WeaponShoot WS = weaponChoose.GetComponent<ChooseWeapon>().currentWeapon.GetComponent<WeaponShoot>();
                WS.AtkCooldown = WS.defaultAtkCooldown; //��weaponShoot�̪��l�u�g���N�o�զ^�w�]��
            }
            
        }

        /*if (charging && weaponChoose.weaponNum==3) //�⩸�אּ����ɷ|�۰ʧ���
        {
            if (atkTimer >= weaponChoose.currentWeapon.GetComponent<WeaponShoot>().AtkCooldown) 
            {
                CrossBowChargeAtk();
                atkTimer = 0;
            }
        }*/


        if (Input.GetKey(KeyCode.Mouse1))
        {
            if(IsDashing==false)
            {
                Charge();
            }
            if(charging)//��������ɭ��O��0
            {
                rb.gravityScale = 0;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (charging)    //�p�G�A����ɩ�}���s
            {
                rb.gravityScale = 1;

                Attack(); //��}���s�]�|����,12/21�W�[

                charging = false;
                ChargeStop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && charging==false) //�ª����ʨt�ΡA�p�G���b��������A�θ��D
        {
            if (PlayerInfo.curStamina > 0 && cooldown > cooldownLimit)
            {
                if(charging!=true)
                {
                    ghost.GetComponent<GhostEft>().ghostDelay = 0.1f;
                    ghostTimer = 0.8f;

                    rb.velocity = Vector2.zero;
                    Jump();
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && charging) //�s���ʨt�ΡA�p�G�b��������A�νĨ�
        {
            if (PlayerInfo.curStamina > 0 && Dashcooldown > DashcooldownLimit)
            {
                ghost.GetComponent<GhostEft>().ghostDelay = 0.005f;
                ghostTimer = 0.3f;

                Dash();
            }
                
        }
        void Dash()
        {
            if(GameObject.FindWithTag("FireSummon"))//�p�G�����l�ꪫ�h���L����
            {
                GameObject.FindWithTag("FireSummon").GetComponent<FireSummon>().Atk();
            }

            Dashcooldown = 0;//���s�Ĩ몺�N�o�ɶ�

            DashSound.Play();

            StartCoroutine(DashTimeCal());


            // �p��Ĩ�Z��
            float dashDistance = Mathf.Lerp(0f, maxDashDistance * 10f, Time.time / dashDuration);

            // �p��Ĩ��V
            Vector2 dashDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            // �p��Ĩ�t��
            Vector2 dashVelocity = dashDirection * dashSpeed * dashDistance;

            // �K�[�Ĩ�t��
            GetComponent<Rigidbody2D>().AddForce(dashVelocity, ForceMode2D.Impulse);

            // �]�m�Ĩ�
            StartCoroutine(EndDash(dashDuration));

            ghostTime = 0f;

            PlayerInfo.Jumping();

            StartCoroutine(SendSpriteRotateSpeed());
        }
        IEnumerator DashTimeCal()
        {
            IsDashing = true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false; //�����ˮ`�˴��I����(���קK����A�bpalyerSpriteRotate�W�s�W�F���ʥΪ��I����)
            yield return new WaitForSeconds(dashDuration);
            IsDashing = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true; //�}�Ҷˮ`�˴��I����
        }



        if (chargeTime>0f)
        {
            ChargePower.GetComponent<Animator>().SetTrigger("Start");
        }
        else
        {
            ChargePower.GetComponent<Animator>().SetTrigger("End");
        }

        if(weaponChoose.weaponNum==1 || weaponChoose.weaponNum ==2)
        {
            if (chargeTime > AtkChargeTime)
            {
                ChargePower2.GetComponent<Animator>().SetTrigger("Start");
            }
            else
            {
                ChargePower2.GetComponent<Animator>().SetTrigger("End");
            }
            if (chargeTime > AtkChargeTime * 2)
            {
                ChargePower3.GetComponent<Animator>().SetTrigger("Start");
            }
            else
            {
                ChargePower3.GetComponent<Animator>().SetTrigger("End");
            }
        }
        
        if(weaponChoose.weaponNum == 1)
        {
            if (chargeTime > AtkChargeTime * 3)
            {
                ChargePower4.GetComponent<Animator>().SetTrigger("Start");
            }
            else
            {
                ChargePower4.GetComponent<Animator>().SetTrigger("End");
            }
            if (chargeTime > AtkChargeTime * 4)
            {
                ChargePower5.GetComponent<Animator>().SetTrigger("Start");
            }
            else
            {
                ChargePower5.GetComponent<Animator>().SetTrigger("End");
            }
        }
        

    }

    void Jump() //�ª����ʨt��
    {
        if(PlayerInfo.curStamina<=0 || cooldown <= cooldownLimit)
        {
            return;
        }
        else
        {
            //�ͦ����D�ɤl�ĪG
            Instantiate(JumpParticle,transform.position,Quaternion.identity);
            JumpSound.Play();

            rb.AddForce(ArrowPivot.transform.up * Speed *ReverseControlNum , ForceMode2D.Impulse);
            cooldown = 0f;

            //PlayerInfo.curStamina--;

            ghostTime = 0f;

            PlayerInfo.Jumping();

            StartCoroutine(SendSpriteRotateSpeed());
        }
    }
    

    IEnumerator EndDash(float duration) //�s���ʨt��
    {
        yield return new WaitForSeconds(duration);

        // ��_���O
        GetComponent<Rigidbody2D>().gravityScale = 1f;

        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        maxDashDistance = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision) //�s���ʨt��
    {
        if (IsDashing == true)
        {
            // �����
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }

    IEnumerator SendSpriteRotateSpeed()
    {
        PlayerSpriteRotate.GetComponent<Animator>().SetFloat("speed", ArrowPivot.transform.up.x);
        yield return new WaitForSeconds(0.5f);
        PlayerSpriteRotate.GetComponent<Animator>().SetFloat("speed",0);
    }
    
    void SetMousePosition()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MousePosition.z = transform.position.z;

    }
    void Charge()
    {
        if (charging == false)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            charging = true;
        }

        PlayerEffect.GetComponent<Animator>().SetBool("Charging", true);
        weaponChoose.currentWeapon.GetComponent<Animator>().SetBool("Charging", true);

        SetMousePosition();
        transform.position = Vector2.MoveTowards(transform.position, MousePosition, chargeMoveSpeed * chargeReverseControlNum * Time.deltaTime);

        chargeTime += Time.deltaTime;

        PlayChargeSound();
    }

    void ChargeStop()
    {
        chargeTime = 0f;

        PlayerEffect.GetComponent<Animator>().SetBool("Charging", false);
        weaponChoose.currentWeapon.GetComponent<Animator>().SetBool("Charging", false);

        StopChargeSound();
    }

    void Attack()
    {
        if (PlayerInfo.curEnergy > 0) //�p�G���a����q�i�H�����A�����e�����ʳt��
        {
            //rb.velocity = Vector2.zero; 
        }
            
        if (charging==false)
        {
            if(PlayerInfo.curEnergy<1)
            {
                return;
            }
            else
            {
                weaponChoose.Attack();
                weaponChoose.BulletLevel(0);
            }
            
        }
        else
        {
            if(weaponChoose.weaponNum==1) //�Z�������}
            {
                if (chargeTime > AtkChargeTime * 5)
                {
                    if (PlayerInfo.curEnergy < 5)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(5);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }
                    
                }
                else if (chargeTime > AtkChargeTime * 4)
                {
                    if (PlayerInfo.curEnergy < 4)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(4);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }
                    
                }
                else if(chargeTime > AtkChargeTime * 3)
                {
                    if (PlayerInfo.curEnergy < 3)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(3);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }
                    
                }
                else if(chargeTime > AtkChargeTime * 2)
                {
                    if (PlayerInfo.curEnergy < 2)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(2);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }
                    
                }
                else if (chargeTime > AtkChargeTime)
                {
                    if (PlayerInfo.curEnergy < 1)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(1);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }
                    
                }
                else if (chargeTime <= AtkChargeTime)
                {
                    return;

                }
            }

            if (weaponChoose.weaponNum == 2) //�Z�����u�}
            {
                if (chargeTime > AtkChargeTime * 3)
                {
                    if (PlayerInfo.curEnergy < 3)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(3);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }

                }
                else if (chargeTime > AtkChargeTime * 2)
                {
                    if (PlayerInfo.curEnergy < 2)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(2);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }

                }
                else if (chargeTime > AtkChargeTime)
                {
                    if (PlayerInfo.curEnergy < 1)
                    {
                        PlayerInfo.UseNoEnergyText();
                    }
                    else
                    {
                        weaponChoose.BulletLevel(1);
                        weaponChoose.Attack();
                        chargeAtkSound.Play();
                        chargeTime = 0f;
                        ChargePower.GetComponent<Animator>().SetTrigger("End");
                    }

                }
                else if (chargeTime <= AtkChargeTime)
                {
                    return;
                }
            }

        }
    }

    void CrossBowChargeAtk()
    {
        if (chargeTime > AtkChargeTime)
        {
            if (PlayerInfo.curEnergy < 1)
            {
                PlayerInfo.UseNoEnergyText();
            }
            else
            {
                weaponChoose.BulletLevel(31);
                weaponChoose.Attack();
            }

        }
        else if (chargeTime <= AtkChargeTime)
        {
            weaponChoose.BulletLevel(0);
            return;

        }
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);

        return hit;
    }
    void GroundCheck()
    {
        RaycastHit2D leftFootCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D righFootCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance, groundLayer);

        if (leftFootCheck || righFootCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (isOnGround)
        {
            PlayerSpriteRotate.GetComponent<Animator>().SetBool("OnGround", true);
        }
        else
        {
            PlayerSpriteRotate.GetComponent<Animator>().SetBool("OnGround", false);
        }
    }

    void PlayChargeSound()
    {
        if(IsPlayingChargeSound==false)
        {
            chargeSound.Play();
            IsPlayingChargeSound=true;
        }
        else
        {
            return;
        }
    }
    void StopChargeSound()
    {
        if (IsPlayingChargeSound == true)
        {
            chargeSound.Stop();
            IsPlayingChargeSound = false;
        }
        else
        {
            return;
        }
    }

    public void FixedBugForResumeGame() //�״_����ɼȰ��A�Ѱ��Ȱ��ɷ|Ĳ�o��bug
    {
        if(charging && !Input.GetKey(KeyCode.Mouse1))
        {
            rb.gravityScale = 1;

            Attack(); 

            charging = false;
            ChargeStop();
        }
    }
}
