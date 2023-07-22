using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Game {
public class UIManager : StaticInstance<UIManager> {
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private GameObject interactablePopUpContainer;
    [SerializeField] private GameObject gameEndContainer;
    [SerializeField] private TextMeshProUGUI gameEndScore;
    [SerializeField] private GameObject HUDContainer;
    
    public void StartGame() {
        mainMenuContainer.SetActive(false);
        HUDContainer.SetActive(true);
        interactablePopUpContainer.SetActive(true);
        Destroy(interactablePopUpContainer, 5f);
        GameManager.Instance.ChangeState(GameState.Starting);
    }

    public void EndGame() {
        gameEndScore.text = "Dogs sold: " + GameManager.Instance.CurrentScore;
        gameEndContainer.SetActive(true);
        HUDContainer.SetActive(false);
        if (IsInteractablePopUpActive()) interactablePopUpContainer.SetActive(false);
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ToggleInteractableTextActivation(bool status) => interactablePopUpContainer.SetActive(status);
    public bool IsInteractablePopUpActive() => interactablePopUpContainer.activeInHierarchy;

    public void ChangeInteractablePopUpContainerText(string input) {
        TMP_Text strHolder = interactablePopUpContainer.GetComponentInChildren<TMP_Text>(true);
        strHolder.text = input;
    }
}
}