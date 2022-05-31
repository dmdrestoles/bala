using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{

    public Animator animator;
    private bool isAiming = false;
    private bool isReloading = false;
    // Start is called before the first frame update
    private GameObject primary;
    private GameObject secondary;
    private GameObject melee;
    private bool isPrimary = true;
    public Melee meleeScript;

    void Start()
    {
        primary = gameObject.transform.Find("WeaponHolder").gameObject.transform.Find("Primary").gameObject;
        secondary = gameObject.transform.Find("WeaponHolder").gameObject.transform.Find("Secondary").gameObject;
        melee = gameObject.transform.Find("WeaponHolder").gameObject.transform.Find("Melee").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Check animations per button press.
        if (GameManager.IsInputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                isPrimary = true;
                StartCoroutine(ChangeWeapon(isPrimary));
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                isPrimary = false;
                StartCoroutine(ChangeWeapon(isPrimary));
            }

            else if (Input.GetKeyDown(KeyCode.V))
            {
                animator.SetBool("isAiming", false);
                animator.ResetTrigger("Firing");
                StartCoroutine(Melee(isPrimary));
            }
        }
    }

    IEnumerator Melee(bool isPrimary)
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
}
