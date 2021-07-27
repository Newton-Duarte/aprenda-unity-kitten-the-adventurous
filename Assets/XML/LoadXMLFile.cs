using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LoadXMLFile : MonoBehaviour
{
    [SerializeField] string xmlFile;
    [SerializeField] string currentLanguage = "en";

    public List<string> titleInterface;
    public List<string> winInterface;
    public List<string> shopInterface;
    public List<string> itemDescriptionInterface;
    public List<string> stageName;

    // Start is called before the first frame update
    void Awake()
    {
        loadXMLData();
    }

    internal void loadXMLData()
    {
        if (PlayerPrefs.GetString("language") != "")
        {
            currentLanguage = PlayerPrefs.GetString("language");
        }

        titleInterface.Clear();
        winInterface.Clear();
        shopInterface.Clear();
        itemDescriptionInterface.Clear();
        stageName.Clear();

        TextAsset xmlData = (TextAsset)Resources.Load($"{currentLanguage}/{xmlFile}");

        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.LoadXml(xmlData.text);

        foreach(XmlNode node in xmlDocument["language"].ChildNodes)
        {
            string nodeName = node.Attributes["name"].Value;
            
            foreach(XmlNode n in node["texts"].ChildNodes)
            {
                switch (nodeName)
                {
                    case "title_interface":
                        titleInterface.Add(n.InnerText);
                        break;
                    case "win_interface":
                        winInterface.Add(n.InnerText);
                        break;
                    case "shop_interface":
                        shopInterface.Add(n.InnerText);
                        break;
                    case "item_description_interface":
                        itemDescriptionInterface.Add(n.InnerText);
                        break;
                    case "stage_name":
                        stageName.Add(n.InnerText);
                        break;
                }
            }
        }
    }
}
