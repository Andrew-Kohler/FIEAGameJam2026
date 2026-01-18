using System.Collections;
using UnityEngine;

public class GameCameraMove : MonoBehaviour
{
    public Camera gameCam;
    public Animator camAnim;

    public GameObject menuCanvas;

    public void MoveCamera()
    {
        menuCanvas.SetActive(false);

        camAnim.GetComponent<Animator>();
        camAnim.SetTrigger("moveCam");

        TriggerGameCanvas.triggerCanvas = true;
    }

}
