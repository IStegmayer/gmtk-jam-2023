using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
public class UIManager : StaticInstance<UIManager> {
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private GameObject interactablePopUpContainer;
    [SerializeField] private GameObject gameEndContainer;

    public void StartGame() {
        mainMenuContainer.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Starting);
    }

    public void EndGame() {
        gameEndContainer.SetActive(true);
        interactablePopUpContainer.SetActive(false);
        GameManager.Instance.ChangeState(GameState.End);
    }

    public void RestartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void ToggleInteractableTextActivation(bool status) => interactablePopUpContainer.SetActive(status);
    public bool IsInteractablePopUpActive() => interactablePopUpContainer.activeInHierarchy;
}
}