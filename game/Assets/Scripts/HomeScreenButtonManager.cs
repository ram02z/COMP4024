using UnityEngine;
using UnityEngine.UI;

public class HomeScreenButtonManager : MonoBehaviour
{
    public Button startGameButton; // Reference to the start game button
    public Button learnButton; // Reference to the learn button
    private Vocabulary _vocabulary; // Reference to the Vocabulary object
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the Vocabulary object in the scene
        _vocabulary = Object.FindObjectOfType<Vocabulary>();
        // Add a listener to the start game button
        startGameButton.onClick.AddListener(StartGame);
        // Add a listener to the learn button
        learnButton.onClick.AddListener(Learn);
    }
    
    // Callback for the start game button
    private void StartGame()
    {
        if (_vocabulary.vocabMap.Count == 0)
        {
            Debug.LogError("Vocabulary map is empty. Cannot start game.");
            return;
        }
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    
    // Callback for the learn button
    private void Learn()
    {
        throw new System.NotImplementedException();
    }
}
