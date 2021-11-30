using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{

    public Animator animator;
    private bool isAiming = false;
    private bool isReloading = false;
    // Start is called before the first frame update

    GameObject primary;
    GameObject secondary;
    void Start()
    {
        primary = gameObject.transform.Find("WeaponHolder").Find("Primary").gameObject;
        secondary = gameObject.transform.Find("WeaponHolder").Find("Secondary").gameObject;

        primary.SetActive(true);
        secondary.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check animations per button press.

        if (Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
            Debug.Log("Aiming! " + isAiming);
            animator.SetBool("isAiming", isAiming);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //animator.SetTrigger("Firing");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //StartCoroutine(HotReload());
            //return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("inPrimary", true);
            primary.SetActive(true);
            animator.SetBool("inSecondary", false);
            secondary.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("inPrimary", false);
            primary.SetActive(false);
            animator.SetBool("inSecondary", true);
            secondary.SetActive(true);
        }
    }

    IEnumerator HotReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isReloading", true);
        //startReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        //endReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isReloading", false);
        yield return new WaitForSeconds(0.25f);
        isReloading = false;

    }
}
