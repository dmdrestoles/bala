using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexPreferencesManager : MonoBehaviour
{
    GameObject codexCanvas, weaponsContainer, peopleContainer, collectiblesContainer;
    GameObject bolo, paltik, revolver, m93;
    GameObject letter1, letter2, letter3, letter4,letter5;
    GameObject ammunition, cedula, cross;

    int isBoloFound,isPaltikFound,isRevolverFound,isM93Found;
    int isLetter1Found, isLetter2Found, isLetter3Found, isLetter4Found, isLetter5Found;
    int isAmmunitionFound, isCedulaFound, isCrossFound;

    // Start is called before the first frame update
    void Start()
    {
        InitializeCodexGO();
        InitializePreferences();
        UpdateCodexWithPreferences();
        // RestartCodexPreferences();
    }

    // Update is called once per frame
    void Update()
    {
         if ( Input.GetKeyDown(KeyCode.F11))
        {
            RestartCodexPreferences();
        }
    }

    void UpdateCodexWithPreferences()
    {
        UpdateCodexItemWithPreferences(bolo, isBoloFound);
        UpdateCodexItemWithPreferences(paltik, isPaltikFound);
        UpdateCodexItemWithPreferences(revolver, isRevolverFound);
        UpdateCodexItemWithPreferences(m93, isM93Found);

        UpdateCodexItemWithPreferences(letter1, isLetter1Found);
        UpdateCodexItemWithPreferences(letter2, isLetter2Found);
        UpdateCodexItemWithPreferences(letter3, isLetter3Found);
        UpdateCodexItemWithPreferences(letter4, isLetter4Found);
        UpdateCodexItemWithPreferences(letter5, isLetter5Found);

        UpdateCodexItemWithPreferences(ammunition, isAmmunitionFound);
        UpdateCodexItemWithPreferences(cross, isCrossFound);
        UpdateCodexItemWithPreferences(cedula, isCedulaFound);
    }
    void UpdateCodexItemWithPreferences(GameObject go, int isFound)
    {
        if (isFound == 1)
        {
            go.SetActive(true);
        }
    }

    void InitializeCodexGO()
    {
        codexCanvas = GameObject.Find("CodexCanvas");
        weaponsContainer = GameObject.Find("WeaponsContainer");
        peopleContainer = GameObject.Find("PeopleContainer");
        collectiblesContainer = GameObject.Find("CollectiblesContainer");

        bolo = GameObject.Find("BoloCodex");
        revolver = GameObject.Find("SW Model 3Codex");
        paltik = GameObject.Find("PaltikCodex");
        m93 = GameObject.Find("Mauser 1893Codex");

        letter1 = GameObject.Find("Letter-1Codex");
        letter2 = GameObject.Find("Letter-2Codex");
        letter3 = GameObject.Find("Letter-3Codex");
        letter4 = GameObject.Find("Letter-4Codex");
        letter5 = GameObject.Find("Letter-5Codex");

        ammunition = GameObject.Find("AmmunitionCodex");
        cedula = GameObject.Find("CedulaCodex");
        cross = GameObject.Find("CrossCodex");

        codexCanvas.SetActive(false);
        weaponsContainer.SetActive(false);
        peopleContainer.SetActive(false);
        collectiblesContainer.SetActive(false);

        bolo.SetActive(false);
        revolver.SetActive(false);
        paltik.SetActive(false);
        m93.SetActive(false);

        letter1.SetActive(false);
        letter2.SetActive(false);
        letter3.SetActive(false);
        letter4.SetActive(false);
        letter5.SetActive(false);

        ammunition.SetActive(false);
        cedula.SetActive(false);
        cross.SetActive(false);
    }

    void InitializePreferences()
    {
        isBoloFound = PlayerPrefs.GetInt("isBoloFound", 0);
        isPaltikFound = PlayerPrefs.GetInt("isPaltikFound", 1);
        isRevolverFound = PlayerPrefs.GetInt("isRevolverFound", 0);
        isM93Found = PlayerPrefs.GetInt("isM93Found", 0);

        isLetter1Found = PlayerPrefs.GetInt("isLetter1Found", 0);
        isLetter2Found = PlayerPrefs.GetInt("isLetter2Found", 0);
        isLetter3Found = PlayerPrefs.GetInt("isLetter3Found", 0);
        isLetter4Found = PlayerPrefs.GetInt("isLetter4Found", 0);
        isLetter5Found = PlayerPrefs.GetInt("isLetter5Found", 0);

        isAmmunitionFound = PlayerPrefs.GetInt("isAmmunitionFound", 0);
        isCedulaFound = PlayerPrefs.GetInt("isCedulaFound", 0);
        isCrossFound = PlayerPrefs.GetInt("isCrossFound", 0);
    }

    void RestartCodexPreferences()
    {
        Debug.Log("Resetting codex preferences");
        PlayerPrefs.SetInt("isBoloFound", 0);
        PlayerPrefs.SetInt("isPaltikFound", 0);
        PlayerPrefs.SetInt("isRevolverFound", 0);
        PlayerPrefs.SetInt("isM93Found", 0);

        PlayerPrefs.SetInt("isLetter1Found", 0);
        PlayerPrefs.SetInt("isLetter2Found", 0);
        PlayerPrefs.SetInt("isLetter3Found", 0);
        PlayerPrefs.SetInt("isLetter4Found", 0);
        PlayerPrefs.SetInt("isLetter5Found", 0);

        PlayerPrefs.SetInt("isAmmunitionFound", 0);
        PlayerPrefs.SetInt("isCedulaFound", 0);
        PlayerPrefs.SetInt("isCrossFound", 0);
    }
}
