using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {
	Stats stats;
	new Animation animation;

	[SerializeField] Text coinsShot;
	[SerializeField] Text obstaclesHit;
	[SerializeField] Text cardsPlayed;
	[SerializeField] Text collectiblesFound;
	[SerializeField] Text foulsCommitted;

	[SerializeField] Button returnButton;

	void Awake() {
		stats = Stats.loadFromFile();
		animation = GetComponent<Animation>();

		coinsShot.text = stats.getCoinsShot().ToString();
		obstaclesHit.text = stats.getObstaclesHit().ToString();
		cardsPlayed.text = stats.getCardsPlayed().ToString();
		collectiblesFound.text = stats.getCollectiblesFound().ToString();
		foulsCommitted.text = stats.getFoulsCommitted().ToString();

		returnButton.onClick.AddListener(() => {
			animation.Play("hideStatsPanel");
			FindObjectOfType<MainMenuUI>().GetComponent<Animation>().Play("displayMainMenuPanel");
		});
	}
}
