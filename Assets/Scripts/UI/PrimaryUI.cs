using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrimaryUI : MonoBehaviour
{
    [SerializeField] private GameObject rotationMenu;
    [SerializeField] private TextMeshProUGUI interactButtonText;

    [SerializeField] private TextMeshProUGUI healthTempText;
    [SerializeField] private TextMeshProUGUI turnTempText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthTempText.text = "Health = " + GameManager.Instance.GetHealth();
        turnTempText.text = "Turn " + GameManager.Instance.GetTurnCount();
    }

    public void ToggleInteractMode() // Switches between being able to drag to rotate the cube and click to move the snail
    {
        GameManager.Instance.SetIsMovingSnail(!GameManager.Instance.GetIsMovingSnail());
        if (GameManager.Instance.GetIsMovingSnail())
        {
            interactButtonText.text = "Moving snail";
        }
        else
        {
            interactButtonText.text = "Rotating cube";
        }
    }

    public void ToggleRotationMode() // Toggles the menu for rotating the cube on and off
    {
        rotationMenu.SetActive(true);
    }
}
