using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    public List<Atom> atoms = new();

    public void AddAtom(Atom atom)
    {
        atoms.Add(atom);
        atom.currentMolecule = this;
        atom.transform.SetParent(transform, true);
        atom.GetComponent<AtomDragger>().enabled = false;
        atom.transform.DOPunchScale(atom.transform.localScale * 0.75f, 0.25f).SetEase(Ease.InBounce);
    }

    public void MergeWith(Molecule other)
    {
        foreach (var atom in other.atoms) AddAtom(atom);
        Destroy(other.gameObject);
    }

    public string GetChemicalFormula()
    {
        var counts = new Dictionary<string, int>();

        foreach (var atom in atoms)
        {
            var symbol = atom.Symbol;
            counts.TryAdd(symbol, 0);
            counts[symbol]++;
        }

        // Optional: Order symbols by Hill system (C, then H, then alphabetical)
        var ordered = counts.OrderBy(kv =>
        {
            if (kv.Key == "C") return 0;
            if (kv.Key == "H") return 1;
            if (kv.Key == "Na") return 2;
            if (kv.Key == "Cl") return 3;
            return int.MaxValue;
        }).ThenBy(kv => kv.Key);

        // Build formula
        var formula = "";
        foreach (var kv in ordered)
        {
            formula += kv.Key;
            if (kv.Value > 1)
                formula += kv.Value;
        }

        return formula;
    }
}