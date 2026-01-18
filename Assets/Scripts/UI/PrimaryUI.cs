using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrimaryUI : MonoBehaviour
{
    [SerializeField] private GameObject rotationMenu;
    [SerializeField] private TextMeshProUGUI interactButtonText;

    [SerializeField] private TextMeshProUGUI healthTempText;
    [SerializeField] private TextMeshProUGUI turnTempText;

    [SerializeField] private GameObject healthContainer;
    [SerializeField] private GameObject helpButton;

    [SerializeField] private GameObject normalRotateText;
    [SerializeField] private GameObject challengeRotateText;
    [SerializeField] private TextMeshProUGUI challengeRotateTextMesh;

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

        if (FindFirstObjectByType<Snail>().isFrosted)
        {
            if (GameManager.Instance.GetHealth() == 3)
            {
                icyShell3.SetActive(true);
            }
            if (GameManager.Instance.GetHealth() == 2)
            {
                icyShell2.SetActive(true);
            }
            if (GameManager.Instance.GetHealth() == 1)
            {
                icyShell1.SetActive(true);
            }
        }
        else
        {
            icyShell1.SetActive(false);
            icyShell2.SetActive(false);
            icyShell3.SetActive(false);
        }
        turnTempText.text = "Turn " + GameManager.Instance.GetTurnCount();

        if (GameManager.Instance.challengeMode)
        {
            normalRotateText.SetActive(false);
            challengeRotateText.SetActive(true);
            challengeRotateTextMesh.text = GameManager.Instance.challengeModeRotations.ToString();
        }
        else
        {
            normalRotateText.SetActive(true);
            challengeRotateText.SetActive(false);
        }
    }

    /*public void ToggleInteractMode() // Switches between being able to drag to rotate the cube and click to move the snail
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
    }*/

    public void ToggleRotationMode() // Toggles the menu for rotating the cube on and off
    {
        if (!GameManager.Instance.GetIsRotatingPiece())
        {
            if (GameManager.Instance.challengeMode) // In challenge mode, you gotta PAY UP!
            {
                if(GameManager.Instance.challengeModeRotations > 0)
                {
                    rotationMenu.SetActive(true);
                    GameManager.Instance.SetRotatingPiece(true);
                    helpButton.SetActive(false);
                    healthContainer.SetActive(false);
                }
            }
            else
            {
                rotationMenu.SetActive(true);
                GameManager.Instance.SetRotatingPiece(true);
                helpButton.SetActive(false);
                healthContainer.SetActive(false);
            }

        }
        else
        {
            rotationMenu.SetActive(false);
            GameManager.Instance.SetRotatingPiece(false);
            helpButton.SetActive(true);
            healthContainer.SetActive(true);
        }


    }
}
