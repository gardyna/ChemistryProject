using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCard : MonoBehaviour
{
	[SerializeField] private Image graphic;
	[SerializeField] private Canvas backsideCanvas;
	[SerializeField] private Canvas frontsideCanvas;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI symbolText;
	[SerializeField] private TextMeshProUGUI factText;
	[SerializeField] private TextMeshProUGUI numberText;

	public void InitializeCard(ElementEntryData data)
	{
		nameText.text = data.EntryName;
		symbolText.text = data.AtomicSymbol;
		factText.text = data.EntryFact;
		numberText.text = data.AtomicNumber.ToString();
	}

	private void Update()
	{
		UpdateDrawOrder();
	}

	private void UpdateDrawOrder()
	{
		bool isFrontShowing = transform.localEulerAngles.y is > -89f and < 89f;
		
		frontsideCanvas.sortingOrder = isFrontShowing ? 10 : -10;
		backsideCanvas.sortingOrder = isFrontShowing ? -10 : 10;

		if (transform.localEulerAngles.y > 359f)
		{
			transform.localEulerAngles = Vector3.zero;
		}
	}

	public void Hide() => AchievementManager.Instance.HideCard();
}
