using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchUI : MonoBehaviour {
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
		resetShotsLeftL();
		resetShotsLeftR();
		restart.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
	}

	void setScoreL(int score) { scoreLeft.text = score.ToString(); }
	void setScoreR(int score) { scoreRight.text = score.ToString(); }
	void setShotsLeftL(int shotsLeft) { shotsLeftUILeft.setShotsLeft(shotsLeft); }
	void setShotsLeftR(int shotsLeft) { shotsLeftUIRight.setShotsLeft(shotsLeft); }
	void resetShotsLeftL() { shotsLeftUILeft.resetShotsLeft(); }
	void resetShotsLeftR() { shotsLeftUIRight.resetShotsLeft(); }
	IEnumerator enableWinPanelAfter(bool hasLeftWon, float seconds) {
		string leftWon = "<b>PLAYER L \n WON</b>";
		string rightWon = "<b>PLAYER R \n WON</b>";
		if (hasLeftWon)
			winPanel.GetComponentInChildren<Text>().text = leftWon;
		else
			winPanel.GetComponentInChildren<Text>().text = rightWon;
		yield return new WaitForSeconds(seconds);
		winPanel.SetActive(true);
	}
	IEnumerator enableGoalPanelFor(float seconds) {
		goalPanel.SetActive(true);
		yield return new WaitForSeconds(seconds);
		goalPanel.SetActive(false);
	}
	IEnumerator enableFaulPanelFor(float seconds) {
		faulPanel.SetActive(true);
		yield return new WaitForSeconds(seconds);
		faulPanel.SetActive(false);
	}


	// Interface
	public void passTurn(bool isLeftsTurn) {
		turnIndicatorL.enabled = isLeftsTurn;
		turnIndicatorR.enabled = !isLeftsTurn;
	}
	public void resetActivePlayerShotsUI(bool isPlayerLeftActive) {
		if (isPlayerLeftActive)
			resetShotsLeftL();
		else
			resetShotsLeftR();
	}
	public void setActivePlayerScore(bool isPlayerLeftActive, int score) {
		if (isPlayerLeftActive)
			setScoreL(score);
		else
			setScoreR(score);
	}
	public void setActivePlayerShotsLeft(bool isPlayerLeftActive, int shotsLeft) {
		if (isPlayerLeftActive)
			setShotsLeftL(shotsLeft);
		else
			setShotsLeftR(shotsLeft);
	}
	public void showWinPanel(bool isPlayerLeftActive) {
		StartCoroutine(enableWinPanelAfter(isPlayerLeftActive, 1));
	}
	public void showGoalPanel() { StartCoroutine(enableGoalPanelFor(1)); }
	public void showFaulPanel() { StartCoroutine(enableFaulPanelFor(1)); }
}