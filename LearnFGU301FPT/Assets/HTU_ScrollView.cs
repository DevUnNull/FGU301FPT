using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTU_ScrollView : MonoBehaviour
{
    // ScrollView là thành phần chính dùng để làm ra cuộn ở trên UI () , nó được kết hợp bởi 2 thành phần chính ()
        /*
         * 1, mask : dùng để cho giấu nội dung nằm ngoài khu vực hiển thị ()
         * 2, Scroll Rect : để sử lý logic cuộn 
         */
    // Vậy để mà tạo ra được 1 Scroll View thì mình cần các thành phần sau 
        /*
         * 1, ScrollView(GameObject) : đây là đối tượng cha , chứa các thành phần Image và ScrollRect . Nó định nghĩa vùng hiển thị của ScrollView
         * 2, Viewport(GamObject) : đây là đối tượng con của ScrollView , nó chứa một thnahf phần mask để che đi nhưng phần tử con nằm ngoài  vùng hiển thị của ScrollView
         * 3, Content(GameObject) : là còn của ViewPort , đây là nơi bạn sẽ đặt tất cả các phần tử (các nút , văn bản , hình ảnh...) mà bạn muốn cuộn
         */



}
