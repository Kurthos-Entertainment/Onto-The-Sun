using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class RoundMenu : MonoBehaviour
{
    public void SelectUpgrade(string upgrade)
    {
        Events.onUpgradeSelected.Invoke(upgrade);
    }
}