using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeriodicCardTable : MonoBehaviour
{
	public Action OnCardUnlocked;
	
	public static PeriodicCardTable Instance;
	public PeriodicTableViews PeriodicTableView;
	public PeriodicTableLoader TableLoader;

	[SerializeField] private RectTransform tableHolder;
	[SerializeField] private RectTransform bottomTableHolder;
	[SerializeField] private GameObject columnPrefab;
	[SerializeField] private GameObject elementEntryPrefab;

	private List<ElementEntry> elementEntries = new List<ElementEntry>();
	private List<ElementEntryData> elementEntryDatas = new List<ElementEntryData>();


	private readonly Dictionary<int, int> TableMap = new Dictionary<int, int>()
	{
		{0, 7},
		{1, 6},
		{2, 4},
		{3, 4},
		{4, 4},
		{5, 4},
		{6, 4},
		{7, 4},
		{8, 4},
		{9, 4},
		{10, 4},
		{11, 4},
		{12, 6},
		{13, 6},
		{14, 6},
		{15, 6},
		{16, 6},
		{17, 7}
	};

	private readonly Dictionary<int, string> EntryMap = new Dictionary<int, string>()
	{
		{0, "Hydrogen"},
		{83, "Helium"},
		{71, "Oxygen"},
		{1, "Lithium"},
		{7, "Beryllium"},
		{2, "Sodium"},
		{8, "Magnesium"},
		{3, "Potassium"},
		{9, "Calcium"},
		{84, "Neon"},
		{53, "Boron"},
		{78, "Chlorine"}
	};



	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		Initialize();
	}

	public void Initialize()
	{
		foreach (var entryMapIndex in TableMap)
		{
			var tableColumn = Instantiate(columnPrefab, tableHolder, true);
			tableColumn.transform.localScale = Vector3.one;

			for (int i = 0; i < entryMapIndex.Value; i++)
			{
				var elementEntry = Instantiate(elementEntryPrefab, tableColumn.transform, true);
				elementEntry.transform.localScale = Vector3.one;
				
				elementEntries.Add(elementEntry.GetComponent<ElementEntry>());
			}
		}
		
		InitializeEntries();
		InitializeBottomTable();
	}

	private void InitializeBottomTable()
	{
		for (int i = 0; i < 28; i++)
		{
			var newEntry = Instantiate(elementEntryPrefab, bottomTableHolder, true);
		}
	}

	public void UnlockEntry(int atomicIndex)
	{
		//Find element in table & flip!
	}
	
	private void InitializeEntries()
	{
		for (int i = 0; i < elementEntries.Count; i++)
		{
			elementEntries[i].SetDebugData(i);
		}
		
		foreach (var kvp in EntryMap)
		{
			elementEntries[kvp.Key].Initialize(TableLoader.GetDataForEntry(kvp.Value));
		}
	}
	
	
	[Serializable]
	public class ElementData
	{
		public int AtomicNumber;
		public string EntryName;
		public string AtomicSymbol;
		public string EntryFact;
		public string EntryGraphic;
	}


	[Serializable]
	public class PeriodicTableViews
	{
		public int Columns = 18;
		public GridLayoutGroup periodicTableParentGroup;
	}
}



