using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 60f;
    /*
     *  rotationSpeed
     *   - 게임 오브젝트가 1초에 Y축 기준으로 몇 도 회전할지 나타낸다.(초기값 60)
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        /*
         *  Rotate()
         *   - 입력값으로 X, Y, Z축애 대한 회전값을 받고, 현재 회전 상태에서
         *     회전 상태에서 입력된 값만큼 상대적으로 더 회전한다.
         *   - 따라서 transform.Rotate(0f, rotationSpeed, 0f); 는 한 번에 Y축을
         *     기준으로 자신의 게임 오브젝트를 rotationSpeed(60도)만큼 회전한다.
         *     ===> (Updtae()가 실행될 때마다) 매 프레임마다 60도 회전한다.
         *  
         *  현재 작성된 코드로는 1초에 60도가 아닌 한 번(프레임)에 60도 회전한다.
         */
    }
}
