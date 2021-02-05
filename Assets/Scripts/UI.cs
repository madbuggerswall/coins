using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
	[SerializeField] Text scoreLeft;
	[SerializeField] Text scoreRight;
	[SerializeField] Image turnIndicatorL;
	[SerializeField] Image turnIndicatorR;
	[SerializeField] GameObject shotsLeftL;
	[SerializeField] GameObject shotsLeftR;
	[SerializeField] GameObject goalPanel;
	[SerializeField] GameObject faulPanel;
	[SerializeField] GameObject winPanel;
	[SerializeField] Button restart;

	ShotsLeftUI shotsLeftUILeft;
	ShotsLeftUI shotsLeftUIRight;

	void Awake() {
		shotsLeftUILeft = new ShotsLeftUI(shotsLeftL);
		shotsLeftUIRight = new ShotsLeftUI(shotsLeftR);
		restart.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
	}

	public void setScoreL(int score) { scoreLeft.text = score.ToString(); }
	public void setScoreR(int score) { scoreRight.text = score.ToString(); }
	public void setShotsLeftL(int shotsLeft) { shotsLeftUILeft.setShotsLeft(shotsLeft); }
	public void setShotsLeftR(int shotsLeft) { shotsLeftUIRight.setShotsLeft(shotsLeft); }
	public void resetShotsLeftL() { shotsLeftUILeft.resetShotsLeft(); }
	public void resetShotsLeftR() { shotsLeftUIRight.resetShotsLeft(); }
	public void passTurn(bool isLeftsTurn) {
		turnIndicatorL.enabled = isLeftsTurn;
		turnIndicatorR.enabled = !isLeftsTurn;
	}
	public IEnumerator enableWinPanelAfter(bool hasLeftWon, float seconds) {
		string leftWon = "<b>PLAYER L \n WON</b>";
		string rightWon = "<b>PLAYER R \n WON</b>";
		if (hasLeftWon)
			winPanel.GetComponentInChildren<Text>().text = leftWon;
		else
			winPanel.GetComponentInChildren<Text>().text = rightWon;
		yield return new WaitForSeconds(seconds);
		winPanel.SetActive(true);
	}
	public IEnumerator enableGoalPanelFor(float seconds) {
		goalPanel.SetActive(true);
		yield return new WaitForSeconds(seconds);
		goalPanel.SetActive(false);
	}
	public IEnumerator enableFaulPanelFor(float seconds) {
		faulPanel.SetActive(true);
		yield return new WaitForSeconds(seconds);
		faulPanel.SetActive(false);
	}
}

public class ShotsLeftUI {
	Image[] shots;

	public ShotsLeftUI(GameObject shotsLeftPanel) {
		shots = shotsLeftPanel.GetComponentsInChildren<Image>();
	}

	public void setShotsLeft(int shotsLeft) {
		for (int i = 0; i < shots.Length - shotsLeft; i++) {
			shots[i].color = Color.red;
		}
	}

	public void resetShotsLeft() {
		foreach (Image image in shots) {
			image.color = Color.green;
		}
	}
}