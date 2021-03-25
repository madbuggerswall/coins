using System.Collections.Generic;

public class PlayNCards : TieredAchievement {
	public PlayNCards(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.cardPlayed.AddListener(check);
	}

	public override void check() {
		int cardsPlayed = stats.getCardsPlayed();
		int index = tiers.IndexOf(cardsPlayed);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}
	public override string getDescription() {
		return "Play " + tiers[tierCompleted] + "cards.";
	}
}