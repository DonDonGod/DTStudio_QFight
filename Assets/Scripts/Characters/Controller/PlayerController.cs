using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //浮动数值
    public int HP;
    public int SP;
    public float speed;

    //固定数组
    public float walk_speed;
    public float run_speed;
    public float jump_height;
    public float rotate_speed;

    //布尔数组
    public bool isRunning;
    public bool isRolling;
    public bool isInjuring;
    public bool isAttacking;
    public bool isTargeting;

    //获得组件
    private CharacterController cc;
    private Camera cam;
    private GameObject camObject;
    //private GameObject target;
    private GameObject enemy_manager;
    private EnemyManager em;

    //测试
    //private Ray r;

    void Start()
    {
        HP = 100;
        SP = 100;
        walk_speed = 2f;
        run_speed = 3f;
        speed = walk_speed;
        rotate_speed = 1f;

        isRunning = false;
        isRolling = false;
        isInjuring = false;
        isTargeting = false;

        camObject = GameObject.Find("Camera");
        enemy_manager = GameObject.Find("EnemyManager");
        cc = this.GetComponent<CharacterController>();
        cam = camObject.GetComponent<Camera>();
        em = enemy_manager.GetComponent<EnemyManager>();


    }

    void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward.normalized*2, Color.red);
    }

    void FixedUpdate()
    {
        CheckMove();
        CheckRun();
        CheckTarget();
        CheckAttack();

        RecoverSP();
    }

    void CheckMove()
    {
        if(Input.GetKey(KeyCode.W))
        {
            cc.SimpleMove(cam.transform.forward.normalized * speed);
            
            this.transform.rotation = Quaternion.LookRotation(new Vector3(cam.transform.forward.x, 0,cam.transform.forward.z).normalized);
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cc.SimpleMove(-cam.transform.forward.normalized * speed);
            
           
            this.transform.rotation = Quaternion.LookRotation(-new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized);
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            cc.SimpleMove(-cam.transform.right.normalized * speed);
            
            
            this.transform.rotation = Quaternion.LookRotation(-new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            cc.SimpleMove(cam.transform.right.normalized * speed);
            
            
            this.transform.rotation = Quaternion.LookRotation(new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized);
            
        }
    }

    void CheckRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isRunning)
            {
                isRunning = true;
                speed = run_speed;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed = walk_speed;
        }
    }


    void CheckTarget()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTargeting)
            {
                isTargeting = true;
            }
            else
            {
                isTargeting = false;
            }
        }
    }

    void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                SP -= 20;

                Ray r = new Ray(this.transform.position, this.transform.forward);
                RaycastHit hit;
                if(Physics.Raycast(r, out hit, 1f))
                {
                    if(hit.collider.tag == "Enemy")
                    {
                        em.DeleteEnemy(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    void CheckInjuring()
    {

    }

    void RecoverSP()
    {
        if (SP < 0) SP = 0;
        else if (SP < 100) SP += 1;
        else
        {
            SP = 100;
            isAttacking = false;////////////////////////
        }
    }


    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.collider.tag == "Ground")
    //    {
    //        if (isJumping)
    //        {
    //            isJumping = false;
    //        }
    //    }
    //}
}
