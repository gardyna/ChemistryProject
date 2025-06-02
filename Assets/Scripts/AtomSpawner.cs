using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AtomSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform spawnParent; // usually the Canvas
    [SerializeField] private List<AtomTuple> atomPrefabs;     // e.g. H, O, etc.

    public void SpawnAtomAtMouse(string symbol)
    {
        Vector2 spawnPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            spawnParent, 
            Input.mousePosition, 
            null, 
            out spawnPos
        );

        var a = atomPrefabs.FirstOrDefault(t => t.atomSymbol == symbol);
        GameObject atom = Instantiate(a.atomPrefab, spawnParent);
        atom.GetComponent<RectTransform>().anchoredPosition = spawnPos;

        // Optionally set it to be dragged immediately
        AtomDragger dragger = atom.GetComponent<AtomDragger>();
        if (dragger != null)
        {
            dragger.BeginManualDrag();
        }
    }

    [Serializable]
    public class AtomTuple
    {
        [SerializeField] public GameObject atomPrefab;
        [SerializeField] public string atomSymbol;
    }
}
