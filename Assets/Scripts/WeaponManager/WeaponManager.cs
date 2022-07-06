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

    void Start()
    {
        SetActiveAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsInputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                isPrimary = true;
                StartCoroutine(ChangeWeapon(isPrimary));
                SetActiveAnimator();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                isPrimary = false;
                StartCoroutine(ChangeWeapon(isPrimary));
                SetActiveAnimator();
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
        animator.SetBool("inPrimary", isPrimary);
        animator.SetBool("inSecondary", !isPrimary);
        yield return new WaitForSeconds(0.5f);
        secondary.SetActive(!isPrimary);
        primary.SetActive(isPrimary);
    }

    void SetActiveAnimator()
    {
        if (isPrimary)
        {
            animator.runtimeAnimatorController = primary.GetComponent<ActiveWeaponManager>().GetController();
        }
        else{
            animator.runtimeAnimatorController = secondary.GetComponent<ActiveWeaponManager>().GetController();
        }
    }
}
