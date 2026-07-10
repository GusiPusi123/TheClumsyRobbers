using UnityEngine;

public class TiltAnim : MonoBehaviour
{
    public Animator animator; // ссылка на компонент Animator
    public string parameterName = "Tilt"; // имя параметра в Animator
    public KeyCode holdKey = KeyCode.R; // клавиша для зажатия

    void Update()
    {
        if (Input.GetKey(holdKey))
        {
            // при зажатии активировать анимацию
            animator.SetBool(parameterName, true);
        }
        else
        {
            // при отпускании отключить анимацию
            animator.SetBool(parameterName, false);
        }
    }
}
