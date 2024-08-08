using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button spinButton;
    public Button claimButton;
    public Button reviveButton;
    public Button loseButton;

    private void OnValidate()
    {
        if (spinButton == null)
        {
            spinButton = FindButtonByName("SpinButton");
        }

        if (claimButton == null)
        {
            claimButton = FindButtonByName("ClaimButton");
        }

        if (reviveButton == null)
        {
            reviveButton = FindButtonByName("ReviveButton");
        }

        if (loseButton == null)
        {
            loseButton = FindButtonByName("LoseButton");
        }
    }

    private Button FindButtonByName(string buttonName)
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            if (button.gameObject.name == buttonName)
            {
                return button;
            }
        }
        return null;
    }

    public void DisableButton(Button button)
    {
        if (button != null)
        {
            button.interactable = false;
        }
    }

    public void EnableButton(Button button)
    {
        if (button != null)
        {
            button.interactable = true;
        }
    }
}
