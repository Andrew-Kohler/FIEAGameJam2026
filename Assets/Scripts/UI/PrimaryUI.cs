using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrimaryUI : MonoBehaviour
{
    [SerializeField] private GameObject rotationMenu;
    [SerializeField] private TextMeshProUGUI interactButtonText;

    [SerializeField] private TextMeshProUGUI healthTempText;
    [SerializeField] private TextMeshProUGUI turnTempText;

    public GameObject crackedShell1;
    public GameObject crackedShell2;
    public GameObject crackedShell3;

    public GameObject icyShell1;
    public GameObject icyShell2;
    public GameObject icyShell3;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.SetRotatingPiece(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthTempText.text = "Health = " + GameManager.Instance.GetHealth();
        if (GameManager.Instance.GetHealth() == 2 )
        {
            crackedShell3.SetActive(true);
        }
        if (GameManager.Instance.GetHealth() == 1)
        {
            crackedShell2.SetActive(true);
        }
        if (GameManager.Instance.GetHealth() == 0)
        {
            crackedShell1.SetActive(true);
        }
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
        GameManager.Instance.SetRotatingPiece(true);
        this.gameObject.SetActive(false);
    }
}
