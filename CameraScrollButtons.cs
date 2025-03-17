using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraScrollButtons : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public Button leftButton;
    public Button rightButton;

    public float minX = -5f;
    public float maxX = 35f; // пределы прокрутки

    private bool moveLeft = false;
    private bool moveRight = false;

    void Start()
    {
        AddEventTrigger(leftButton.gameObject, EventTriggerType.PointerDown, () => moveLeft = true);
        AddEventTrigger(leftButton.gameObject, EventTriggerType.PointerUp, () => moveLeft = false);

        AddEventTrigger(rightButton.gameObject, EventTriggerType.PointerDown, () => moveRight = true);
        AddEventTrigger(rightButton.gameObject, EventTriggerType.PointerUp, () => moveRight = false);
    }

    void Update() // Измените положение камеры в зависимости от нажатой кнопки.
    {
        Vector3 newPosition = Camera.main.transform.position;

        if (moveLeft)
            newPosition.x -= scrollSpeed * Time.deltaTime;

        if (moveRight)
            newPosition.x += scrollSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        Camera.main.transform.position = newPosition;
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((data) => action());
        trigger.triggers.Add(entry);
    }
}
