using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;
    public Transform EquipedParent;
    public Animator animator;
    public int Damage;
    private RaycastHit2D hit;
    private BoxCollider2D boxCollider;
    private EquipedSlot[] eqSlots = new EquipedSlot[5];
    private int frameCount = 0;
    private bool isRight = true;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        for (int i = 0; i < eqSlots.Length; i++)
        {
            eqSlots[i] = EquipedParent.GetChild(i).GetComponent<EquipedSlot>();
        }
    }

    private void Update()
    {
        if (PlayerController.instance.transform.position.x <= transform.position.x && isRight)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            isRight = false;
        }
        else if (PlayerController.instance.transform.position.x > transform.position.x && !isRight)
        {
            transform.localScale = Vector3.one;
            isRight = true;
        }
        
        if (animator.GetBool("Death") && animator.GetInteger("Frames") < 100)
            animator.SetInteger("Frames", animator.GetInteger("Frames") + 1);
        
        if (animator.GetBool("Attack"))
            frameCount += 1;
        if (frameCount >= 50)
        {
            animator.SetBool("Attack", false);
            frameCount = 0;
        }
    }

    public void GetDamage()
    {
        if (animator.GetBool("Death"))
            return;
        Health -= PlayerController.instance.damage;
        if (eqSlots[0].SlotItem != null)
            Health -= eqSlots[0].SlotItem.Damage;
        if (Health <= 0)
        {
            animator.SetBool("Death", true);
            return;
        }
        Debug.Log(Health);
        animator.SetBool("Attack", true);
        Attack();
    }

    private void Attack()
    {
        PlayerController.instance.GetDamage(Damage);
    }
}
