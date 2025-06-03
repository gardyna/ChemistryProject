using Unity.VisualScripting;
using UnityEngine;

public class BondingManager : MonoBehaviour
{
    public static BondingManager Instance;

    [SerializeField] private float bondingDistance = 1.5f;
    [SerializeField] private Transform canvasTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var atoms = FindObjectsOfType<Atom>();

        for (var i = 0; i < atoms.Length; i++)
        for (var j = i + 1; j < atoms.Length; j++)
        {
            var a1 = atoms[i];
            var a2 = atoms[j];

            if (Vector3.Distance(a1.transform.position, a2.transform.position) < bondingDistance)
                if (a1.HasFreeElectron() && a2.HasFreeElectron())
                {
                    var e1 = a1.GetFirstFreeElectron();
                    var e2 = a2.GetFirstFreeElectron();

                    if (e1 != null && e2 != null)
                    {
                        // Calculate bonding direction and positions
                        var center = (a1.transform.position + a2.transform.position) / 2f;
                        var direction = (a2.transform.position - a1.transform.position).normalized;
                        var spacing = 10f; // pixel offset

                        var e1Target = center - direction * spacing;
                        var e2Target = center + direction * spacing;

                        // Move electrons visually
                        e1.transform.position = e1Target;
                        e2.transform.position = e2Target;

                        // Mark as bonded so they can't be reused
                        e1.GetComponent<Electron>().isBonded = true;
                        e2.GetComponent<Electron>().isBonded = true;

                        // parent
                        if (a1.currentMolecule == null && a2.currentMolecule == null)
                        {
                            var newMol = new GameObject("Molecule");
                            newMol.transform.SetParent(canvasTransform);
                            var mol = newMol.AddComponent<Molecule>();

                            mol.AddAtom(a1);
                            mol.AddAtom(a2);
                            mol.AddComponent<CanvasGroup>();
                            mol.AddComponent<RectTransform>();
                            newMol.AddComponent<MoleculeDragger>(); // Enable dragging
                        }
                        else if (a1.currentMolecule != null && a2.currentMolecule == null)
                        {
                            a1.currentMolecule.AddAtom(a2);
                        }
                        else if (a1.currentMolecule == null && a2.currentMolecule != null)
                        {
                            a2.currentMolecule.AddAtom(a1);
                        }
                        else if (a1.currentMolecule != a2.currentMolecule)
                        {
                            a1.currentMolecule.MergeWith(a2.currentMolecule);
                        }
                        
                        if (a1.currentMolecule != null)
                        {
                            string formula = a1.currentMolecule.GetChemicalFormula();
                            Debug.Log("New molecule formed: " + formula);
                        }
                    }
                }
        }
    }
}