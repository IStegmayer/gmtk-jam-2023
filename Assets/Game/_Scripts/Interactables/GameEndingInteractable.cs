namespace Game {
public class GameEndingInteractable : Interactable {
    public override void Interact(Dog dog) {
        UIManager.Instance.EndGame();
    }
}
}