using System;
using DG.Tweening;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	public static AchievementManager Instance;
	[SerializeField] private AchievementCard achievementCard;
	[SerializeField] private string testKey = "Hydrogen";

	private Sequence achievementSequence;
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}


	public void UnlockCard(string key)
	{
		var cardData = PeriodicCardTable.Instance.TableLoader.GetDataForEntry(key);
		achievementCard.transform.DOScale(Vector3.zero, 0f);

		achievementSequence = DOTween.Sequence();
		achievementSequence.Pause();

		achievementSequence.Append(achievementCard.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBounce));

		achievementSequence.Play();

		achievementCard.InitializeCard(cardData);
	}
	

	public void HideCard()
	{
		achievementSequence?.Kill();

		achievementSequence = DOTween.Sequence();
		achievementSequence.Pause();
		achievementSequence.Append(achievementCard.transform.DOScale(Vector3.zero, 0.35f).SetEase(Ease.OutBack));
		achievementSequence.Play();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			UnlockCard(testKey);
		}
	}
}
