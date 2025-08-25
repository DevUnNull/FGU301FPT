using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// phần cần lưu ý ở đây là lớp PointerEventData đây là lớp sử lý đầu vào và ra của con trỏ chuột 
public class HTU_IPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // Biến để lưu trữ màu gốc của đối tượng UI
    private Color originalColor;

    // Tham chiếu đến thành phần Graphic của đối tượng (ví dụ: Image)
    private Graphic graphic;

    // Biến để lưu trữ vị trí ban đầu của đối tượng UI khi bắt đầu kéo
    private Vector3 originalPosition;

    void Awake()
    {
        // Lấy thành phần Graphic (Image, Text, v.v.)
        graphic = GetComponent<Graphic>();

        // Nếu tìm thấy Graphic, lưu lại màu ban đầu
        if (graphic != null)
        {
            originalColor = graphic.color;
        }

        // Lưu vị trí ban đầu của đối tượng
        originalPosition = transform.position;
    }

    /// <summary>
    /// Được gọi khi con trỏ chuột đi vào phần tử UI.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (graphic != null)
        {
            // Thay đổi màu khi chuột di chuyển vào
            graphic.color = Color.yellow;
            Debug.Log("Mouse entered: " + gameObject.name);
        }
    }

    /// <summary>
    /// Được gọi khi con trỏ chuột rời khỏi phần tử UI.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (graphic != null)
        {
            // Trả lại màu gốc khi chuột rời đi
            graphic.color = originalColor;
            Debug.Log("Mouse exited: " + gameObject.name);
        }
    }

    /// <summary>
    /// Được gọi khi bạn nhấn chuột xuống trên phần tử UI.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // Thay đổi màu tạm thời để báo hiệu đã nhấn
        if (graphic != null)
        {
            graphic.color = Color.red;
            Debug.Log("Mouse down: " + gameObject.name);
        }
    }

    /// <summary>
    /// Được gọi khi bạn nhả chuột khỏi phần tử UI.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // Trả lại màu ban đầu (hoặc màu hover nếu chuột vẫn ở trên đối tượng)
        // Trong trường hợp này, OnPointerExit sẽ lo việc trả màu lại
        if (graphic != null)
        {
            Debug.Log("Mouse up: " + gameObject.name);
        }
    }

    /// <summary>
    /// Được gọi khi bạn nhấn và nhả chuột trên cùng một phần tử UI.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + gameObject.name + " at position " + eventData.position);
    }

    /// <summary>
    /// Được gọi khi bạn kéo phần tử UI.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // Di chuyển đối tượng theo con trỏ chuột
        transform.position = eventData.position;
        Debug.Log("Dragging: " + gameObject.name);
    }
}