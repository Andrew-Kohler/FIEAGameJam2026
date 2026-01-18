using UnityEngine;

public class ClipboardAnim : MonoBehaviour
{
    public GameObject clipboard;
    public Animator boardAnim;

    [SerializeField] private GameObject checkOne; // Window
    [SerializeField] private GameObject checkTwo; // Antenna
    [SerializeField] private GameObject checkThree; // Fin

    public bool isOpen;

    private void OnEnable()
    {
        Snail.onItemPickup += CheckOffBoard;
    }
    private void OnDisable()
    {
        Snail.onItemPickup -= CheckOffBoard;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clipboard()
    {
        boardAnim.GetComponent<Animator>();

        if (isOpen == false)
        {
            boardAnim.SetTrigger("isOpen");
            isOpen = true;
        }

        else if (isOpen == true)
        {
            boardAnim.SetTrigger("isClose");
            isOpen = false;
        }
    }

    private void CheckOffBoard(int board)
    {
        Debug.Log("Got thru" + board);
        switch (board)
        {
            case 1: // Ship fin
                checkThree.SetActive(true);
                break;
            case 2: // Window
                checkTwo.SetActive(true);

                break;
            case 3: // Antenna
                checkOne.SetActive(true);
                break;
            default:
                break;
        }
    }
}
