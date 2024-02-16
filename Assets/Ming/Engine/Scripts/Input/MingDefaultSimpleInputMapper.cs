using UnityEngine;

namespace Ming
{
    public class MingDefaultSimpleInputMapper : IMingSimpleInput<MingSimpleInputType>
    {
        public bool IsActive(MingSimpleInputType inputType)
        {
            return inputType switch
            {
                MingSimpleInputType.Up => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0.5f,
                MingSimpleInputType.Down => Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < -0.5f,
                MingSimpleInputType.Left => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < -0.5f,
                MingSimpleInputType.Right => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0.5f,
                MingSimpleInputType.Jump => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Space),
                MingSimpleInputType.Shoot => Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.LeftShift),
                MingSimpleInputType.Select => Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter),
                MingSimpleInputType.Activate => Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter),
                MingSimpleInputType.Back => Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Backspace),
                _ => false
            };
        }
    }
}
