using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Transform EquipedParent;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    private EquipedSlot[] eqSlots = new EquipedSlot[5];
    private Vector3 moveDelta;
    private int frameCount = 0;
    public Animator animator;
    public int damage;
    public int Health = 1000;

    private void Start()
    {
        instance = this;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        
        for (int i = 0; i < eqSlots.Length; i++)
        {
            eqSlots[i] = EquipedParent.GetChild(i).GetComponent<EquipedSlot>();
        }
    }

    private void Update()
    {
        if (animator.GetBool("Death") && animator.GetInteger("Frames") < 100)
            animator.SetInteger("Frames", animator.GetInteger("Frames") + 1);
        
        if (animator.GetBool("Death"))
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);
        
        animator.SetFloat("Move", Math.Max(Math.Abs(x), Math.Abs(y)));
        if (animator.GetBool("Attack"))
            frameCount += 1;
        if (frameCount > 40)
        {
            animator.SetBool("Attack", false);
            frameCount = 0;
        }

        if (x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector3 position = new Vector3(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y, 0);
        hit = Physics2D.BoxCast(position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Math.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Math.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("Attack", true);
            Attack();
        }
    }

    private void Attack()
    {
        Vector3 position = new Vector3(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y, 0);
        hit = Physics2D.BoxCast(position, boxCollider.size, 0, new Vector2(1, 0),
            Math.Abs(1 * Time.deltaTime), LayerMask.GetMask("Actor"));
        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.GetDamage();
        }
        
        hit = Physics2D.BoxCast(position, boxCollider.size, 0, new Vector2(-1, 0),
            Math.Abs(-1 * Time.deltaTime), LayerMask.GetMask("Actor"));
        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.GetDamage();
        }
        
        hit = Physics2D.BoxCast(position, boxCollider.size, 0, new Vector2(0, 1),
            Math.Abs(1 * Time.deltaTime), LayerMask.GetMask("Actor"));
        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.GetDamage();
        }
        
        hit = Physics2D.BoxCast(position, boxCollider.size, 0, new Vector2(0, -1),
            Math.Abs(-1 * Time.deltaTime), LayerMask.GetMask("Actor"));
        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.GetDamage();
        }
    }

    public void GetDamage(int dmg)
    {
        int armor = 0;
        int EnemyDamage = 0;
        if (eqSlots[1].SlotItem != null)
            armor += eqSlots[1].SlotItem.Def;
        if (eqSlots[2].SlotItem != null)
            armor += eqSlots[2].SlotItem.Def;
        if (eqSlots[3].SlotItem != null)
            armor += eqSlots[3].SlotItem.Def;

        if (armor >= dmg)
            EnemyDamage = 1;
        else
            EnemyDamage = dmg - armor;

        Debug.Log(armor);
        Health -= EnemyDamage;
        if (Health <= 0)
            animator.SetBool("Death", true);
    }
}
