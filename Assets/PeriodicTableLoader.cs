using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PeriodicTableLoader : MonoBehaviour
{
	public TextAsset jsonFile; // Assign your JSON file in the Unity Inspector
	public Sprite[] elementSprites; // Assign your sprites in the Unity Inspector
	public List<ElementEntryData> elements;

	private void Awake()
	{
		var wrapper = JsonUtility.FromJson<ElementDataArray>(jsonFile.text);
		List<ElementEntryData> elementList = new List<ElementEntryData>();

		foreach (var data in wrapper.elements)
		{
			// Convert string path to sprite if needed
			data.EntryGraphic = LoadSprite(data.EntryGraphic)?.name;
			elementList.Add(data);
		}

		elements = elementList;
	}


	Sprite LoadSprite(string spritePath)
	{
		foreach (var sprite in elementSprites)
		{
			if ("Sprites/" + sprite.name == spritePath)
				return sprite;
		}
		return null;
	}

	[System.Serializable]
	public class ElementData
	{
		public int AtomicNumber;
		public string EntryName;
		public string AtomicSymbol;
		public string EntryFact;
		public string EntryGraphic;
	}

	[System.Serializable]
	public class ElementDataArray
	{ 
		public ElementEntryData[] elements;
	}

	public ElementEntryData GetDataForEntry(string key)
	{
		return elements.FirstOrDefault(entry => entry.EntryName == key);
	}
}