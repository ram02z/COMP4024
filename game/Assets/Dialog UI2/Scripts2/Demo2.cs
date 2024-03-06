// Demo2 script
using UnityEngine;
using EasyUI.Dialogs; 

public class Demo2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       DialogUI2.Instance
       .SetTitle2("Notification").SetMessage2("Hello World").Show(); // Changed from SetTitle to SetTitle2 and SetMessage to SetMessage2
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
