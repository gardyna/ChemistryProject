using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Electron : MonoBehaviour
{
	[SerializeField] private Image glowGraphic;
    public bool isBonded;

    private Sequence glowSequence;

    private void Start()
    {
	    glowSequence = DOTween.Sequence();
	    glowSequence.Pause();

	    glowSequence.Append(glowGraphic.DOFade(0.75f, 0.5f).SetEase(Ease.InOutExpo).SetLoops(-1, LoopType.Yoyo));
	    glowSequence.Play();
    }

    public void KillAnimation()
    {
	    if (isBonded)
	    {
		    glowSequence?.Kill();
	    }

	    glowGraphic.DOColor(Color.green, 0.15f);
    }
}