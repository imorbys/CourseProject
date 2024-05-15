using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonData", menuName = "ButtonData")]
public class ButtonData : ScriptableObject
{
    // ���������� �������
    public KeyCode MoveKeyCodeUp;
    public KeyCode MoveKeyCodeDown;
    public KeyCode MoveKeyCodeLeft;
    public KeyCode MoveKeyCodeRight;

    // ���������� ��������
    public KeyCode ShootKeyCodeUp;
    public KeyCode ShootKeyCodeDown;
    public KeyCode ShootKeyCodeLeft;
    public KeyCode ShootKeyCodeRight;
}
