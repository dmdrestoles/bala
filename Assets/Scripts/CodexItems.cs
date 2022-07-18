using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodexItems : MonoBehaviour
{
    public GameObject image;
    public GameObject descContainer;
    public GameObject descImage;
    public GameObject descName;
    public GameObject description;
    public GameObject descInstructions;

    public void setDescription(string name)
    {
        descInstructions.SetActive(false);

        if (name == "Weapons")
        {
            ArrayList weaponList = new ArrayList { "Bolo", "Paltik", "Sumpit", "Revolver", "Remington Rolling Block", "Spanish M93" };
            ArrayList weaponDescription = new ArrayList{
            " [insert weapon description for Bolo] ",
            " [insert weapon description for Paltik] ",
            " [insert weapon description for Sumpit] ",
            " [insert weapon description for Revolver] ",
            " A breech-loading single-shot rifle used by many colonial armies in the world ",
            " A bolt-action rifle used mainly by the Spanish Army "
            };

            descContainer.SetActive(true);
            descName.GetComponent<TextMeshProUGUI>().text = image.name;
            descImage.GetComponent<Image>().sprite = image.transform.GetChild(0).GetComponent<Image>().sprite;
            Debug.Log(weaponList.IndexOf(image.name));
            description.GetComponent<TextMeshProUGUI>().text = weaponDescription[weaponList.IndexOf(image.name)].ToString();
        }
        else if (name == "People")
        {
            ArrayList peopleList = new ArrayList { "PeopleTemplate", "Jose Rizal" };
            ArrayList peopleDescription = new ArrayList{
            " [insert weapon description for Person] ",
            " José Rizal, in full José Protasio Rizal Mercado y Alonso Realonda, (born June 19, 1861, Calamba, Philippines—died December 30, 1896, Manila), patriot, physician, and man of letters who was an inspiration to the Philippine nationalist movement."
            };

            descContainer.SetActive(true);
            descName.GetComponent<TextMeshProUGUI>().text = image.name;
            Debug.Log(peopleList.IndexOf(image.name));
            description.GetComponent<TextMeshProUGUI>().text = peopleDescription[peopleList.IndexOf(image.name)].ToString();
        }
        else if (name == "Locations")
        {
            ArrayList locationList = new ArrayList { "LocationTemplate" };
            ArrayList locationDescription = new ArrayList{
            " [insert weapon description for Location] "
            };

            descContainer.SetActive(true);
            descName.GetComponent<TextMeshProUGUI>().text = image.name;
            Debug.Log(locationList.IndexOf(image.name));
            description.GetComponent<TextMeshProUGUI>().text = locationDescription[locationList.IndexOf(image.name)].ToString();
        }
    }
}
