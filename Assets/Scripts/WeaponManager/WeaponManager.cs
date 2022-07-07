using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* This class will manage the states of the weapons whether they equip the primary or secondary weapons.
**/
public class WeaponManager : MonoBehaviour
{
    private bool isAiming = false;
    private bool isReloading = false;
    private bool isPrimary = true;
    
    public Animator animator;
    public GameObject primary;
    public GameObject secondary;
    public GameObject melee;
    public Melee meleeScript;

    public bool isPickup;

    void Start()
    {
        animator.runtimeAnimatorController = primary.GetComponent<ActiveWeaponManager>().GetController();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsInputEnabled)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1) && !isPrimary))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                animator.SetTrigger("Unequip");
                isPrimary = true;
                StartCoroutine(ChangeWeapon(isPrimary));
            }

            else if ((Input.GetKeyDown(KeyCode.Alpha2) && isPrimary))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                animator.SetTrigger("Unequip");
                isPrimary = false;
                StartCoroutine(ChangeWeapon(isPrimary));
            }

            // Refine such that when picking up, check if it's primary or secondary item
            else if (isPickup)
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                animator.SetTrigger("Unequip"); 
                StartCoroutine(ChangeWeapon(isPrimary));
                isPickup = false;
            }

            else if (Input.GetKeyDown(KeyCode.V) && Melee.isBoloAcquired)
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                StartCoroutine(MeleeAttack(isPrimary));
            }
        }
    }

    IEnumerator MeleeAttack(bool isPrimary)
    {
        melee.SetActive(true);
        primary.SetActive(false);
        secondary.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Melee");
        yield return new WaitForSeconds(0.45f);
        meleeScript.CheckForEnemies();
        yield return new WaitForSeconds(0.30f);
        
        secondary.SetActive(!isPrimary);
        primary.SetActive(isPrimary);
        melee.SetActive(false);
    }

    IEnumerator ChangeWeapon(bool isPrimary)
    {
        yield return new WaitForSeconds(0.167f);
        if (isPrimary)
        {
            animator.runtimeAnimatorController = primary.GetComponent<ActiveWeaponManager>().GetController();
        }
        else{
            animator.runtimeAnimatorController = secondary.GetComponent<ActiveWeaponManager>().GetController();
        }
        animator.Rebind();
        animator.Update(0f);
        secondary.SetActive(!isPrimary);
        primary.SetActive(isPrimary);
        animator.ResetTrigger("Unequip");
    }
}
