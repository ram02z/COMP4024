using UnityEngine;
using EasyUI.Dialogs;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Show dialog
        DialogUI.Instance
        .SetTitle ("Notification")
        .SetMessage ("Hello World")
        .Show ();
    }

    
}
