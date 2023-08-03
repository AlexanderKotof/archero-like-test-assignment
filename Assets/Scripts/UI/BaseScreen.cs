using UnityEngine;

namespace TestAssignment.UI.Screens
{
    public class BaseScreen : MonoBehaviour
    {
        public void ShowHide(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}
