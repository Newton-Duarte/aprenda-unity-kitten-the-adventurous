using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LoadXMLFile : MonoBehaviour
{
    [SerializeField] string xmlFile;
    [SerializeField] string currentLanguage = "pt-br";

    public List<string> titleInterface;
    public List<string> shopInterface;
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
        shopInterface.Clear();
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
                    case "shop_interface":
                        shopInterface.Add(n.InnerText);
                        break;
                    case "stage_name":
                        stageName.Add(n.InnerText);
                        break;
                }
            }
        }
    }
}
