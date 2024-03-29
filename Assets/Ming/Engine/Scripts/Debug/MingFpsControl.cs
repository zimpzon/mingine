﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ming
{
    [RequireComponent(typeof(RawImage), typeof(TextMeshProUGUI))]
    [AddComponentMenu("Ming/Debug/MingFpsControl")]
    public class MingFpsControl : MingBehaviour
    {
        public KeyCode ToggleKey = KeyCode.I;
        public bool StartEnabled = true;

        const int W = 128;
        const int H = 64;

        int _currentColumn;
        int _textUpdateRate = 10;
        Color32 background_ = new Color32(50, 50, 50, 255);
        RawImage _image;
        TextMeshProUGUI _textFps;
        Texture2D _texture;
        Color32[] _pixels;
        Rect _uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        GameObject _displayRoot;

        private void Awake()
        {
            _displayRoot = transform.GetChild(0).gameObject;
            _displayRoot.SetActive(true);

            _image = GetComponentInChildren<RawImage>();
            _textFps = GetComponentInChildren<TextMeshProUGUI>();

            _pixels = new Color32[W * H];
            for (int i = 0; i < _pixels.Length; ++i)
                _pixels[i] = background_;

            _texture = new Texture2D(W, H, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Repeat,
            };

            _image.texture = _texture;
            _image.uvRect = _uvRect;

            _displayRoot.SetActive(StartEnabled);
        }

        public void Add(float fps)
        {
            if (!_displayRoot.activeSelf)
                return;

            const int Row60Fps = 60;

            Color col = new Color(0.0f, 0.4f, 0.0f, 1.0f);
            float score = Mathf.Clamp(fps / 60, 0.0f, 1.0f);
            float score2 = score * score;
            col.g = 0.4f * score;
            col.r = (1.0f - score2);
            Color32 col32 = col;
            int top = Mathf.Min(H - 1, Mathf.RoundToInt(fps));
            for (int y = 0; y < H; ++y)
            {
                _pixels[y * W + _currentColumn] = y < top ? col32 : background_;
            }

            // Dashed line
            if ((_currentColumn / 5) % 2 == 0)
                _pixels[Row60Fps * W + _currentColumn] = Color.green;

            _texture.SetPixels32(_pixels);
            _texture.Apply();

            _uvRect.x = (1.0f / W) * (_currentColumn + 1);
            _image.uvRect = _uvRect;

            if (++_currentColumn >= W)
                _currentColumn = 0;
        }

        void Update()
        {
            if (Input.GetKeyDown(ToggleKey))
                _displayRoot.SetActive(!_displayRoot.activeSelf);

            if (_displayRoot.activeSelf)
            {
                float fps = 1.0f / Time.deltaTime;
                Add(fps);

                if (Time.frameCount % _textUpdateRate == 0)
                    _textFps.SetText(MingIntToStrLut.GetString(Mathf.RoundToInt(fps)));
            }
        }
    }
}
