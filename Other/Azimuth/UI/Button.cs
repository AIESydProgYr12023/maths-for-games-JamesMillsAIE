using MathLib;

using Raylib_cs;

namespace Azimuth.UI
{
	public class Button : InteractableWidget
	{
		public delegate void OnClickEvent();

        private readonly float roundedness;
        private readonly string text;
        private readonly int fontSize;
        private readonly float fontSpacing;

        private readonly Font font;
        private readonly Color textColor;
        private readonly Vec2 textSize;

        public class RenderSettings
        {
            public ColorBlock colors = new ColorBlock()
            {
                disabled = new Color(255, 255, 255, 128),
                hovered = new Color(245, 245, 245, 255),
                normal = new Color(255, 255, 255, 255),
                selected = new Color(200, 200, 200, 255)
            };

            public int fontSize;
            public Color textColor;
            public float roundedness = 0.1f;
            public float fontSpacing = 1f;
            public string? fontId = null;

            public RenderSettings(int _fontSize, Color _textColor) : base()
            {
                fontSize = _fontSize;
                textColor = _textColor;
            }
        }

        private OnClickEvent? onClick;

        public Button(Vec2 _position, string _text, RenderSettings _settings)
            : base(_position, Vec2.zero, _settings.colors)
        {
            roundedness = _settings.roundedness;
            text = _text;
            fontSize = _settings.fontSize;
            fontSpacing = _settings.fontSpacing;

            font = string.IsNullOrEmpty(_settings.fontId)
                ? Raylib.GetFontDefault()
                : Assets.Find<Font>(_settings.fontId);
            textColor = _settings.textColor;

            //Raylib function that creates a bounding box around the font based on its size and spacing
            textSize = Raylib.MeasureTextEx(font, text, fontSize, fontSpacing);
            size = textSize;
        }

        public Button(Vec2 _position, Vec2 _buttonSize, string _text, RenderSettings _settings)
            : base(_position, _buttonSize, _settings.colors)
        {
            roundedness = _settings.roundedness;
            text = _text;
            fontSize = _settings.fontSize;
            fontSpacing = _settings.fontSpacing;

            font = string.IsNullOrEmpty(_settings.fontId)
                ? Raylib.GetFontDefault()
                : Assets.Find<Font>(_settings.fontId);
            textColor = _settings.textColor;

            //Raylib function that creates a bounding box around the font based on its size and spacing
            textSize = Raylib.MeasureTextEx(font, text, fontSize, fontSpacing);
            size = _buttonSize;
        }

        public void AddListener(OnClickEvent _event)
        {
            if (onClick == null)
                onClick = _event;
            else
                onClick += _event;
        }

        public void RemoveListener(OnClickEvent _event)
        {
            if (onClick != null)
                onClick -= _event;
        }

        public override void Draw()
        {
            Raylib.DrawRectangleRounded(Bounds, roundedness, 5, ColorFromState());
            Raylib.DrawRectangleRoundedLines(Bounds, roundedness, 5, 2, colors.selected);
            Vec2 p = size - textSize;
            Raylib.DrawTextPro(font, text, new Vec2(position.x + p.x * 0.5f, position.y + p.y * 0.5f), Vec2.zero,
                0f, fontSize, fontSpacing, textColor);
        }

        protected override void OnStateChange(InteractionState _state, InteractionState _oldstate)
        {
            if (_state != InteractionState.Selected && _oldstate == InteractionState.Selected)
            {
                // The button is no longer being clicked, so do the event
                onClick?.Invoke();
            }
        }
	}
}