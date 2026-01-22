using UnityEngine;
using UnityEngine.UI;

public class DetailsMenu : MonoBehaviour
{
    public GameObject detailsContainer;
    [SerializeField] private Button detailsButton;

    private void Start()
    {
        detailsContainer.SetActive(false);
        detailsButton.gameObject.SetActive(true);
    }

    public void SettingsOpen()
    {
        detailsContainer.SetActive(true);
        detailsButton.gameObject.SetActive(false);
    }

    public void SettingsClosed()
    {
        detailsContainer.SetActive(false);
        detailsButton.gameObject.SetActive(true);
    }
}
