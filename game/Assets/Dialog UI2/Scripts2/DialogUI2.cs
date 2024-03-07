using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Dialogs
{
    public class Dialog2
    {
        public string MainText = "GAME OVER!";
        public string MessageText = "Message goes here";
    }

    public class DialogUI2 : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Text mainTitleUIText;
        [SerializeField] Text messageTextUIText;
        [SerializeField] Button exitUIButton;

        private Dialog2 dialog = new Dialog2();

        public static DialogUI2 Instance;

        void Awake()
        {
            Instance = this;

            exitUIButton.onClick.RemoveAllListeners();
            exitUIButton.onClick.AddListener(Hide); // This line remains the same
            exitUIButton.onClick.AddListener(QuitToHomeScreen); // Add listener for quitting to home screen
        }

        public DialogUI2 SetTitle2(string title)
        {
            dialog.MainText = title;
            return Instance;
        }

        public DialogUI2 SetMessage2(string message)
        {
            dialog.MessageText = message;
            return Instance;
        }

        public void Show()
        {
            mainTitleUIText.text = dialog.MainText;
            messageTextUIText.text = dialog.MessageText;

            canvas.SetActive(true);
        }

        public void Hide()
        {
            canvas.SetActive(false);

            // Optionally reset dialog values
            dialog = new Dialog2();
        }

        // Quit application and return to home screen
        public void QuitToHomeScreen()
        {
            // Add code here to return to home screen
            Debug.Log("Quitting application and returning to home screen...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TopicScene");
        }
    }
}

