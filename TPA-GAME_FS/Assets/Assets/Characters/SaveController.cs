using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    // Start is called before the first frame update

    public float lookRadius = 3f;
    Transform target;
    public GameObject mainCam;
    public GameObject saveCam;
    public GameObject canvas;

    void Start()
    {
        saveCam.SetActive(false);
        canvas.SetActive(false);

        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius && Input.GetKeyDown(KeyCode.E))
        {
            ShowMenu();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void ShowMenu()
    {
        canvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainCam.SetActive(false);
        saveCam.SetActive(true);
    }

    public void pressYes()
    {
        //save
        canvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCam.SetActive(true);
        saveCam.SetActive(false);
    }

    public void pressNo()
    {
        canvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCam.SetActive(true);
        saveCam.SetActive(false);
    }

}
