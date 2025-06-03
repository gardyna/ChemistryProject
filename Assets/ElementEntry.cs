using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementEntry : MonoBehaviour
{
	public ElementEntryData Data;
	[SerializeField] private TextMeshProUGUI atomicNumberText;
	[SerializeField] private TextMeshProUGUI atomicSymbolText;
	[SerializeField] private TextMeshProUGUI elementNameText;

	public int debugIndex;

	[SerializeField] private Image graphicBackground;
	

	public void SetDebugData(int index) => debugIndex = index;
	
	public void Initialize(ElementEntryData data)
	{
		if (data == null)
		{
			HideEntry();
			return;
		}

		Data = data;
		atomicNumberText.text = data.AtomicNumber.ToString();
		atomicSymbolText.text = data.AtomicSymbol;
		elementNameText.text = data.EntryName;
	}

	public void HideEntry()
	{
		atomicNumberText.text = "";
		atomicSymbolText.text = "";
		elementNameText.text = "";
	}

	public void ShowEntry()
	{
		
	}

	public void ShowInfo()
	{
		Debug.Log($"<color=yellow>Index is: {debugIndex}!</color>");
		if (Data == null) { return; }
		AchievementManager.Instance.UnlockCard(Data.EntryName);
		Debug.Log($"You clicked: {Data.EntryName} with Atomic number: {Data.AtomicNumber}");
	}
}
