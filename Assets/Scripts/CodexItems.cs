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
        descImage.GetComponent<Image>().preserveAspect = true;

        if (name == "Weapons")
        {
            ArrayList weaponList = new ArrayList { "BoloCodex", "PaltikCodex", "SumpitCodex", "SW Model 3Codex", "Remington Rolling Block", "Mauser 1893Codex" };
            ArrayList weaponDescription = new ArrayList{
            " The Bolo mainly used as a farming tool is also useful in close quarter combat ",
            " An improvised firearm made using scrap metal and wood; extremely inaccurate ",
            " [insert weapon description for Sumpit] ",
            " A single-action, cartridge-firing revolver. Its top-break cylinder is distinct from most modern revolvers",
            " A breech-loading single-shot rifle used by many colonial armies in the world ",
            " A breech-loading single-shot rifle used by many colonial armies in the world  "
            };

            descContainer.SetActive(true);
            
            descImage.GetComponent<Image>().sprite = image.transform.GetChild(0).GetComponent<Image>().sprite;
            Debug.Log(weaponList.IndexOf(image.name));
            description.GetComponent<TextMeshProUGUI>().text = weaponDescription[weaponList.IndexOf(image.name)].ToString();

            string title = image.name;
            title = title.Remove(title.Length-5 ,5);
            descName.GetComponent<TextMeshProUGUI>().text = title;
            Debug.Log("title:"+title);
        }
        else if (name == "People")
        {
            ArrayList peopleList = new ArrayList { "PeopleTemplate", "Jose Rizal", "Letter-1Codex", "Letter-2Codex", "Letter-3Codex", "Letter-4Codex", "Letter-5Codex", };
            ArrayList peopleDescription = new ArrayList{
            " [insert weapon description for Person] ",
            " José Rizal, in full José Protasio Rizal Mercado y Alonso Realonda, (born June 19, 1861, Calamba, Philippines—died December 30, 1896, Manila), patriot, physician, and man of letters who was an inspiration to the Philippine nationalist movement.",
            " Mahal kong Victoria,\n Maaring hindi na tayo magtagpo, pero iyong tandaan na sa iyo aking puso.",
            " Wala ng bala, wala ng pagkain.",
            " Bilisan niyo ang iyong paggalaw. Dapat tayo mauna bago ang Caviteno ",
            " Alam nila na parating tayo. Nakaistasyon ang mga artilleros sa San Juan.",
            " Wala pang nakakakita sa ating mga hukbo mula sa Norte at saka Sur",
            };

            descContainer.SetActive(true);
            Debug.Log(peopleList.IndexOf(image.name));
            description.GetComponent<TextMeshProUGUI>().text = peopleDescription[peopleList.IndexOf(image.name)].ToString();

            string title = image.name;
            title = title.Remove(title.Length-5 ,5);
            descName.GetComponent<TextMeshProUGUI>().text = title;
            Debug.Log("title:"+title);
        }
        else if (name == "Locations")
        {
            ArrayList locationList = new ArrayList { "LocationTemplate" };
            ArrayList locationDescription = new ArrayList{
            " [insert weapon description for Location] "};
        }
        else if (name == "Collectibles")
        {
            ArrayList locationList = new ArrayList { "LocationTemplate", "AmmunitionCodex", "CedulaCodex", "CrossCodex",  };
            ArrayList locationDescription = new ArrayList{
            " [insert weapon description for Location] ",
            "The evolution of cartridges allowed for more compact handling of the bullet and gunpowder, while the breach-loading, loading the cartridge from behind the barrel, made it easier to load",
            "Historically used as an identification document for Filipinos when treating with the Spanish Colonial Government. Its tearing during the cry of Pugad Lawin signifies the start of the Philippine Revolution",
            "One of the Spanish influences that still resonates today; Catholicism is one of the pillars that helped the Spanish Colonial Government to rule the Philippines.",
            };

            descContainer.SetActive(true);
            descImage.GetComponent<Image>().sprite = image.transform.GetChild(0).GetComponent<Image>().sprite;
            Debug.Log(locationList.IndexOf(image.name));
            description.GetComponent<TextMeshProUGUI>().text = locationDescription[locationList.IndexOf(image.name)].ToString();

            string title = image.name;
            title = title.Remove(title.Length-5 ,5);
            descName.GetComponent<TextMeshProUGUI>().text = title;
            Debug.Log("title:"+title);
        }
    }
}
