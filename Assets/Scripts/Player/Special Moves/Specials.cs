using System.Collections.Generic;
using UnityEngine;

public class Specials : MonoBehaviour
{

    private List<Specials> activeSpecials;

    private int currentSpecial = 0;

    public bool isActive;
    protected bool isSelected;


    private void Start()
    {
        activeSpecials = new List<Specials>();
        AddSpecials();
        SelectSpecial();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Change Special"))
        {
            if (currentSpecial >= activeSpecials.Count - 1)
            {
                currentSpecial = 0;
            }
            else
            {
                currentSpecial++;
            }
            SelectSpecial();
        }
    }

    private void SelectSpecial()
    {
        int i = 0;
        foreach (Specials special in activeSpecials)
        {
            if (i == currentSpecial)
            {
                activeSpecials[i].isSelected = true;
            }
            else
            {
                activeSpecials[i].isSelected = false;
            }
            i++;
        }
    }

    private void AddSpecials()
    {
        Component[] mySpecials = GetComponents<Specials>();

        foreach (Specials special in mySpecials)
        {
            if (special.isActive)
            {
                activeSpecials.Add(special);
            }
        }
    }

}
