using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //相机变量
    public float rotate_speed;
    public float distance;
    public float cam_x;
    public float cam_y;
    public float cam_z;
    public Vector3 cam_pos;

    //获得组件
    private GameObject target;
    private GameObject player;
    private GameObject enemy_manager;
    private PlayerController pc;
    private EnemyManager em;

    void Start()
    {
        rotate_speed = 0.1f;
        distance = 5f;

        player = GameObject.Find("Player");
        enemy_manager = GameObject.Find("EnemyManager");
        pc = player.GetComponent<PlayerController>();
        em = enemy_manager.GetComponent<EnemyManager>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        CheckTarget();
    }

    void LastUpdate()
    {
        
    }

    void CheckTarget()
    {
        if(pc.isTargeting && em.enemy_pool.Count > 0)
        {
            target = em.enemy_pool[0];
            cam_x = (player.transform.position - (target.transform.position - player.transform.position).normalized * distance).x;
            cam_z = (player.transform.position - (target.transform.position - player.transform.position).normalized * distance).z;
            cam_y = player.transform.position.y + distance/2;
            cam_pos = new Vector3(cam_x, cam_y, cam_z);

            this.transform.position = cam_pos;
            this.transform.LookAt(target.transform.position);
            
            //Debug.Log("isTargeting");
        }
        else
        {
            cam_x = (player.transform.position - (player.transform.position - this.transform.position).normalized * distance).x;
            cam_z = (player.transform.position - (player.transform.position - this.transform.position).normalized * distance).z;
            cam_y = player.transform.position.y + distance/4;
            cam_pos = new Vector3(cam_x, cam_y, cam_z);

            this.transform.position = cam_pos;
            this.transform.LookAt(player.transform.position);

            //if(Input.GetKey(KeyCode.))
            //this.transform.LookAt(player.transform.forward);
            //Debug.Log("isNotTargeting");
        }
    }
}
