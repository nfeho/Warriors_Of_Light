using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public enum AreaTypes {Light, Dark, Window, Trap};
    public AreaTypes areaType;
    public Sprite onSprite;
    public Sprite offSprite;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        switch (areaType) {
            case AreaTypes.Window:
                if (isOn)
                {
                    this.gameObject.tag = "Dark";
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
                    this.isOn = false;
                } else { 
                    this.gameObject.tag = "Light";
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
                    this.isOn = true;
                }
                break;
            case AreaTypes.Dark:
            case AreaTypes.Light:
                gameObject.SetActive(!gameObject.activeSelf);
                break;
            case AreaTypes.Trap:
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    GameObject child = gameObject.transform.GetChild(i).gameObject;
                    child.SetActive(!child.activeInHierarchy);
                }
                break;
        }

    }
}
