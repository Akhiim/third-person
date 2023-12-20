using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkwinner : MonoBehaviour
{
    public Camera defultCamera;
    public Camera winnerCamera;
    public bool iswinner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public static checkwinner instance;

    public Transform playerRotation;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        defultCamera.enabled = true;
        winnerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (iswinner)
        {
            defultCamera.enabled = false;
            winnerCamera.enabled = true;
        }
    }

    private void LateUpdate()
    {
        if(target != null && iswinner)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, target.position.z + 2.2f);
            Vector3 smoothPosition = Vector3.Lerp(winnerCamera.transform.position,desiredPosition,smoothSpeed * Time.deltaTime);
            winnerCamera.transform.position = smoothPosition;
            playerRotation.LookAt(new Vector3(playerRotation.position.x, playerRotation.position.y, winnerCamera.transform.position.z));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playercontroller.instance.groundedPlayer)
        {
            iswinner = true;
        }
    }
}
