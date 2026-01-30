using UnityEngine;
using System;

//ボタンの情報を使いやすい形で取得

public class ButtonInput : MonoBehaviour
{
    int _input=1;
    int _preInput=1;

    bool _isUsedButton = false;//ボタンを使っているか(一度もtrueに書き換えられなかったら使われていないということ)

    public event Action OnInputDown;//ボタンを押した瞬間
    public event Action OnInputUp;//ボタンを離した瞬間
    public event Action<bool> OnInputSwitch;//入力情報が変わった瞬間

    public bool IsInputting { get { return ConvertInputInfoToBool(_input); } }

    public bool IsUsedButton { get { return _isUsedButton; } }

    public void UpdateInputInfo(int input)
    {
        _preInput = _input;
        _input = input;

        //入力情報が変わったかを見る
        if(_input!=_preInput)
        {
            bool current = IsInputting;

            if (IsInputting) OnInputDown?.Invoke();
            else OnInputUp?.Invoke();

            OnInputSwitch?.Invoke(current);
        }

        if (_isUsedButton) return;

        _isUsedButton = true;
    }

    //int型の入力情報をbool型に変換
    //スイッチは1の時は押していない、0の時は押されている状態
    bool ConvertInputInfoToBool(int input)
    {
        return input == 0;
    }
}
