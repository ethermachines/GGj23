using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float rayDistance = 3.0f;
    [SerializeField] private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, rayDistance, mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    //play button sound
                    //Debug.Log(interactable.promptMessage);
                    interactable.BaseInteract();
                }
            }
        }
    }
}
