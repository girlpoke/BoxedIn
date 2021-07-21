using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float moveSpeed = .1f;
    float moveTime = 2f;
    float rotateStep = 1;
    Vector3 zoomStep = new Vector3(0, -0.5f, -0.5f);
    public Camera childCamera;

    public Collider mapCollider;
    public GameObject player;
    Vector3 defensePos;

    Vector3 mouseStartPos;
    Vector3 mouseCurrentPos;
    Vector3 spinStartPos;
    Vector3 spinCurrentPos;

    Vector3 newPos;
    Quaternion newRot;
    Vector3 newZoom;


    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
        newRot = transform.rotation;
        newZoom = childCamera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        //if (childCamera.enabled)
        //{
            CameraKeys();
            CameraMouse();
        //}

    }

    public void CameraMouse()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = childCamera.ScreenPointToRay(Input.mousePosition);

            float rayEntry;

            if (plane.Raycast(ray, out rayEntry))
            {
                mouseStartPos = ray.GetPoint(rayEntry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = childCamera.ScreenPointToRay(Input.mousePosition);

            float rayEntry;

            if (plane.Raycast(ray, out rayEntry))
            {
                mouseCurrentPos = ray.GetPoint(rayEntry);

                newPos = transform.parent.position + mouseStartPos - mouseCurrentPos;
            }
        }
        */
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomStep * 20;
        }
        if (Input.GetMouseButtonDown(2))
        {
            spinStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            spinCurrentPos = Input.mousePosition;
            Vector3 difference = spinStartPos - spinCurrentPos;
            spinStartPos = spinCurrentPos;
            //Debug.Log("ROTATE UP: " + Vector3.left * (-difference.y / 100f));
            //newRot *= Quaternion.Euler(Vector3.left * (-difference.y / 100f));
            //Debug.Log("newRot UP: " + newRot);
            //Debug.Log("SPIN: " + Vector3.up * (-difference.x / 5f));
            newRot *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
            //Debug.Log("newRot: " + newRot);
        }
        /*
        if (Input.GetMouseButtonDown(1))
        {
            bool objectFound = false;

            Collider[] objectColliders = Physics.OverlapSphere(transform.parent.position, 0.2f);
            if (objectColliders.Length != 0)
            {

                foreach (var objectCollider in objectColliders)
                {
                    if (objectCollider.gameObject.name.Contains("Sphere"))
                    {
                        Destroy(objectCollider.gameObject);
                        objectFound = true;
                    }
                }
            }

            if (!objectFound)
            {
                defensePos = transform.parent.position;
                defensePos.y = 0.1f;
                //defensePos.x -= 60f;
                Instantiate(defencePrefab, defensePos, Quaternion.identity);
            }
        }*/
        
    }

    public void CameraKeys()
    {
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomStep;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomStep;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            newPos += (transform.forward * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            newPos += (transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            newPos += (transform.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            newPos += (transform.right * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRot *= Quaternion.Euler(Vector3.up * rotateStep);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRot *= Quaternion.Euler(Vector3.up * -rotateStep);
        }

        newPos = new Vector3(Mathf.Clamp(newPos.x, 55, 81), newPos.y, Mathf.Clamp(newPos.z, -2, 31));

        transform.parent.position = Vector3.Lerp(transform.parent.position, newPos, Time.deltaTime * moveTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * moveTime);

        childCamera.transform.localPosition = Vector3.Lerp(childCamera.transform.localPosition, newZoom, Time.deltaTime * moveTime);

    }
}
