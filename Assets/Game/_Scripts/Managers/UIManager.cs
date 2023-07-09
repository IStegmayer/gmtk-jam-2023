using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Game {
public class UIManager : StaticInstance<UIManager> {
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private GameObject interactablePopUpContainer;
    [SerializeField] private GameObject gameEndContainer;
    [SerializeField] private GameObject HUDContainer;

    public TMP_Text popUpText;

    public void StartGame() {
        mainMenuContainer.SetActive(false);
        HUDContainer.SetActive(true);
        GameManager.Instance.ChangeState(GameState.Starting);
    }

    public void EndGame() {
        gameEndContainer.SetActive(true);
        HUDContainer.SetActive(false);
        if (IsInteractablePopUpActive()) interactablePopUpContainer.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void ToggleInteractableTextActivation(bool status)
    {
        interactablePopUpContainer.SetActive(status);
    } 
    public bool IsInteractablePopUpActive()
    {
        return interactablePopUpContainer.activeInHierarchy;
    }

    public void changeinteractablePopUpContainerText(string input)
    {
        TMP_Text strholder = interactablePopUpContainer.GetComponentInChildren<TMP_Text>(true);
        // print("->>>"+strholder);

        strholder.text = input;

    }
}
}