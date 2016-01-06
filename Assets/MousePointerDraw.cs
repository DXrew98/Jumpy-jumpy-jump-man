using UnityEngine;
using System.Collections;

public class MousePointerDraw : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        // get position of entity and target in screen space
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 cursorPos = Input.mousePosition;

        Vector2 aimDirection = cursorPos - screenPos;
        aimDirection.Normalize();

        transform.rotation = Quaternion.Euler(0f,
                                              0f,
        (Mathf.Atan2(aimDirection.y, aimDirection.x) - (Mathf.PI * (6.0f / 12.0f)))  * Mathf.Rad2Deg);
        // sprites should be created facing right
    }
}
