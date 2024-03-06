using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Dialogs
{
    public class Dialog
    {
        public string Title = "Are you sure you want to quit this game";
        public string Message = "Remember, all your progress will be lost";
    }

    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private Text titleUIText;
        [SerializeField] private Text messageUIText;
        [SerializeField] private Button closeUIButton;
        [SerializeField] private Button quitUIButton; // New button for quitting to home screen
        private Dialog dialog = new Dialog();

        // Singleton Pattern
        public static DialogUI Instance;

     /*   private void Awake()
        {
            Instance = this;
            // Add close event listener
            if (closeUIButton != null)
            {
                closeUIButton.onClick.RemoveAllListeners();
                closeUIButton.onClick.AddListener(Hide);
            }

            // Add quit event listener
            if (quitUIButton != null)
            {
                quitUIButton.onClick.RemoveAllListeners();
                quitUIButton.onClick.AddListener(QuitToHomeScreen);
            }
        }
*/
        // Set Dialog Title
        public DialogUI SetTitle(string title)
        {
            dialog.Title = title;
            return Instance;
        }

        // Set Dialog Message
        public DialogUI SetMessage(string message)
        {
            dialog.Message = message;
            return Instance;
        }

        // Show dialog
        public void Show()
        {
            titleUIText.text = dialog.Title;
            messageUIText.text = dialog.Message;
            canvas.SetActive(true);
        }

        // Hide dialog
        public void Hide()
        {
        
            Debug.Log("close game");
            canvas.SetActive(false);
            // Reset Dialog
            dialog = new Dialog();
        }

        // Quit application and return to home screen
        public void QuitToHomeScreen()
        {
            // Add code here to return to home screen
            Debug.Log("Quitting application and returning to home screen...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TopicScene");
        }

        // Quit application
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
