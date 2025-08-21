using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTUTransfrom_Translate : MonoBehaviour
{
    /*
        - transform.position (thường dùng để làm các skill như là : tele , ...)
            Đây là tọa độ tuyệt đối của object trong world space.
            Khi gán giá trị cho transform.position, bạn “teleport” (dịch chuyển ngay lập tức) object tới vị trí đó.
            ViDU : transform.position = new Vector3(0, 5, 0);
            ViDu : transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        - transform.translate (để di chuyển 1 vật nào đó theo hướng chứ không phải là mất tức thì)
            Đây là hàm dịch chuyển tương đối, tức là thêm một offset vào vị trí hiện tại.
            Bạn không cần tự cộng thêm vào position, Unity sẽ tự tính.
            ViDu : transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);


        - so sánh nhanh
        | Thuộc tính | `transform.position`              | `transform.Translate()`                                   |
        | ---------- | --------------------------------- | --------------------------------------------------------- |
        | Kiểu       | Gán trực tiếp vị trí tuyệt đối    | Dịch chuyển tương đối từ vị trí hiện tại                  |
        | Di chuyển  | Teleport hoặc cộng tay vào        | Tự động cộng offset                                       |(offsest là : offset thường dùng để nói về một vector hoặc giá trị cộng thêm vào vị trí/rotation/scale gốc)
        | Không gian | Luôn world space                  | Có thể chọn local hoặc world                              |
        | Dùng khi   | Muốn đặt object tại tọa độ cụ thể | Muốn di chuyển theo hướng (thường kết hợp input/rotation) |

     */
    [SerializeField]
    public float speed = 5f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0).normalized;

        // Cách 1: position
        //transform.position += movement * speed * Time.deltaTime;

        // Cách 2: Translate
         transform.Translate(movement * speed * Time.deltaTime, Space.World);
        /*
            transform.Translate(Vector3 translation, Space relativeTo = Space.Self);
                translation: vector bạn muốn dịch chuyển.
                relativeTo: không gian tham chiếu (Space.Self hoặc Space.World).
                    Space.Self (mặc định)
                        Dịch chuyển theo tọa độ local của object (tức là theo hướng object đang quay mặt).
                        Nếu object xoay → hướng di chuyển cũng xoay theo.
                        (VD: nếu player bị ngã lật ngang người thì lúc này ấn sang phải thì nó sẽ bay lên trên chứ không phải sang phải nữa)
                    Space.World
                        Dịch chuyển theo tọa độ toàn cục (global/world space).
                        Không quan tâm object xoay hay không, luôn đi theo trục X, Y, Z của thế giới.
                        (VD: kể cả player bị lật ngang thì mình ấn sang phải nó vẫn sang phải)
         */
    }

}
