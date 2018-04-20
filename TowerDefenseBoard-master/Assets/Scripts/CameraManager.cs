using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 60f;
    public float scrollSpeed = 6f;
    public float minY = 10f;
    public float maxY = 80f;

    void Update()
    {
        //Pan
        if (Input.GetKeyDown("c")) doMovement = !doMovement;
        if (!doMovement) return;

        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey("d") )
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey("a") )
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

        }
        //Scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }
}
