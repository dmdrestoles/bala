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
    void Start()
    {
        primary = gameObject.transform.Find("WeaponHolder").gameObject.transform.Find("Primary").gameObject;
        secondary = gameObject.transform.Find("WeaponHolder").gameObject.transform.Find("Secondary").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Check animations per button press.

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("isAiming", false);
            animator.ResetTrigger("Firing");
            StartCoroutine(ChangeWeapon(true));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("isAiming", false);
            animator.ResetTrigger("Firing");
            StartCoroutine(ChangeWeapon(false));
        }
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
