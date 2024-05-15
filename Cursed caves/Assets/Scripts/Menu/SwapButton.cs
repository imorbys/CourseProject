using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapButton : MonoBehaviour
{
    [Header("Move")]
    public Text _MovebuttonTextUp;
    public Text _MovebuttonTextDown;
    public Text _MovebuttonTextLeft;
    public Text _MovebuttonTextRight;


    [Header("Shoot")]
    public Text _ShootbuttonTextUp;
    public Text _ShootbuttonTextDown;
    public Text _ShootbuttonTextLeft;
    public Text _ShootbuttonTextRight;


    public ButtonData databut;
    public GameObject warningText;
    private int s = 1;
    public KeyCode keyCode { get; set; }
    private IEnumerator coroutine;
    private string tmpKey;

    private void Start()
    {
        if (_MovebuttonTextLeft != null)
        {
            _MovebuttonTextLeft.text = databut.MoveKeyCodeLeft.ToString();
        }
        if (_MovebuttonTextRight != null)
        {
            _MovebuttonTextRight.text = databut.MoveKeyCodeRight.ToString();
        }
        if (_MovebuttonTextUp != null)
        {
            _MovebuttonTextUp.text = databut.MoveKeyCodeUp.ToString();
        }
        if (_MovebuttonTextDown != null)
        {
            _MovebuttonTextDown.text = databut.MoveKeyCodeDown.ToString();
        }
        if (_ShootbuttonTextLeft != null)
        {
            _ShootbuttonTextLeft.text = databut.ShootKeyCodeLeft.ToString();
        }
        if (_ShootbuttonTextRight != null)
        {
            _ShootbuttonTextRight.text = databut.ShootKeyCodeRight.ToString();
        }
        if (_ShootbuttonTextUp != null)
        {
            _ShootbuttonTextUp.text = databut.ShootKeyCodeUp.ToString();
        }
        if (_ShootbuttonTextDown != null)
        {
            _ShootbuttonTextDown.text = databut.ShootKeyCodeDown.ToString();
        }
    }
    // Движение
    public void ButtonMoveSetKeyLeft()
    {
        StartSettingKey(_MovebuttonTextLeft, databut.MoveKeyCodeLeft);
    }
    public void ButtonMoveSetKeyRight()
    {
        StartSettingKey(_MovebuttonTextRight, databut.MoveKeyCodeRight);
    }
    public void ButtonMoveSetKeyUp()
    {
        StartSettingKey(_MovebuttonTextUp, databut.MoveKeyCodeUp);
    }
    public void ButtonMoveSetKeyDown()
    {
        StartSettingKey(_MovebuttonTextDown, databut.MoveKeyCodeDown);
    }

    // Стрельба
    public void ButtonShootSetKeyLeft()
    {
        StartSettingKey(_ShootbuttonTextLeft, databut.ShootKeyCodeLeft);
    }
    public void ButtonShootSetKeyRight()
    {
        StartSettingKey(_ShootbuttonTextRight, databut.ShootKeyCodeRight);
    }
    public void ButtonShootSetKeyUp()
    {
        StartSettingKey(_ShootbuttonTextUp, databut.ShootKeyCodeUp);
    }
    public void ButtonShootSetKeyDown()
    {
        StartSettingKey(_ShootbuttonTextDown, databut.ShootKeyCodeDown);
    }
    private void StartSettingKey(Text buttonText, KeyCode storedKeyCode)
    {
        tmpKey = buttonText.text;
        buttonText.text = "???";
        coroutine = WaitForKey(buttonText, storedKeyCode);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitForKey(Text buttonText, KeyCode storedKeyCode)
    {
        while (true)
        {
            yield return null;

            foreach (KeyCode n in KeyCode.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(n))
                {
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(storedKeyCode))
                    {
                        buttonText.text = tmpKey;
                        StopCoroutine(coroutine);
                    }
                    else if (!IsKeyCodeUsed(n) && n != KeyCode.Escape) // Изменено условие здесь
                    {
                        keyCode = n;
                        buttonText.text = n.ToString();
                        UpdateStoredKeyCode(buttonText, n);
                        StopCoroutine(coroutine);
                    }
                    else
                    {
                        s = 1;
                        warningText.SetActive(true);
                        yield return StartCoroutine(FadeSaveText());
                    }
                }
            }
        }
    }

    private void UpdateStoredKeyCode(Text buttonText, KeyCode newKeyCode)
    {
        if (buttonText == _MovebuttonTextLeft)
        {
            databut.MoveKeyCodeLeft = newKeyCode;
        }
        else if (buttonText == _MovebuttonTextRight)
        {
            databut.MoveKeyCodeRight = newKeyCode;
        }
        else if (buttonText == _MovebuttonTextUp)
        {
            databut.MoveKeyCodeUp = newKeyCode;
        }
        else if (buttonText == _MovebuttonTextDown)
        {
            databut.MoveKeyCodeDown = newKeyCode;
        }
        else if (buttonText == _ShootbuttonTextLeft)
        {
            databut.ShootKeyCodeLeft = newKeyCode;
        }
        else if (buttonText == _ShootbuttonTextRight)
        {
            databut.ShootKeyCodeRight = newKeyCode;
        }
        else if (buttonText == _ShootbuttonTextUp)
        {
            databut.ShootKeyCodeUp = newKeyCode;
        }
        else if (buttonText == _ShootbuttonTextDown)
        {
            databut.ShootKeyCodeDown = newKeyCode;
        }
    }
    private IEnumerator FadeSaveText()
    {
        float elapsedTime = 0f;
        while (s > 0)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= 1f)
            {
                elapsedTime = 0f;
                s--;

                if (s == 0)
                {
                    warningText.SetActive(false);
                }
            }
            yield return null;
        }
    }
    private bool IsKeyCodeUsed(KeyCode key)
    {
        return key == databut.MoveKeyCodeLeft || key == databut.MoveKeyCodeRight || key == databut.MoveKeyCodeUp || key == databut.MoveKeyCodeDown || key == databut.ShootKeyCodeLeft || key == databut.ShootKeyCodeRight || key == databut.ShootKeyCodeUp || key == databut.ShootKeyCodeDown;
    }
}
