#nullable enable
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Atom : MonoBehaviour
{
    [Header("Atom properties")] [SerializeField]
    private string elementName = null!;

    [SerializeField] private string elementSymbol = null!;
    [SerializeField] [Range(0, 8)] private int maxValenceElectrons;
    [SerializeField] [Range(0, 8)] private int currentValenceElectrons;

    [Header("Electron Configuration")] [SerializeField]
    private List<Transform> electronSlots = null!;

    [SerializeField] private GameObject electronPrefab = null!;

    public Molecule currentMolecule;

    private readonly List<GameObject> activeElectrons = new();
    public string ElementName => elementName;
    public string Symbol => elementSymbol;

    private void Start()
    {
        InitializeElectrons();
    }

    private void InitializeElectrons()
    {
        for (var i = 0; i < currentValenceElectrons && i < electronSlots.Count; i++)
        {
            var dot = Instantiate(electronPrefab, electronSlots[i].position, Quaternion.identity, transform);
            dot.GetComponent<Electron>().isBonded = false;
            activeElectrons.Add(dot);
        }
    }

    public bool HasFreeElectron()
    {
        return activeElectrons.Count > 0;
    }

    public GameObject GetFirstFreeElectron()
    {
        foreach (var electron in activeElectrons)
            if (electron.GetComponent<Electron>()?.isBonded == false)
                return electron;
        return null;
    }


    public void RemoveElectron(GameObject electron)
    {
        if (activeElectrons.Remove(electron))
        {
            //Destroy(electron);
        }
    }


    public void AddBond(GameObject partnerElectron)
    {
        RemoveElectron(partnerElectron);
    }
}