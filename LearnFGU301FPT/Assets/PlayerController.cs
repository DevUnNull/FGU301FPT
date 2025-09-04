using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//nếu bạn làm mọi thứ trong PlayerController thì nó sẽ thành God Class (vi phạm Single Responsibility Principle).
public class PlayerController : MonoBehaviour
{
    private ShootBullet shootBullet;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float recoilForce = 5f;


    private PlayerStateMachine stateMachine;

    private Weapon weapon;

    private GameObject currentWeapon;
    private void Start()
    {
        shootBullet = new ShootBullet(shootPoint, rb, recoilForce);

        //stateMachine = GetComponent<PlayerStateMachine>(); // phai gan component vao gameobject
        stateMachine = gameObject.AddComponent<PlayerStateMachine>(); // khong can gan component vao gameobject
        stateMachine.ChangeState(new IdleState(stateMachine));

        weapon = gameObject.AddComponent<Weapon>();
        weapon.SetAttackStrategy(new SwordAttack());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            shootBullet.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon.SetAttackStrategy(new SwordAttack());
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon.SetAttackStrategy(new GunAttack());
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon.SetAttackStrategy(new FireAttack());
        }
    }
}
