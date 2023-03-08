using MathLib;

using Raylib_cs;

namespace Azimuth.UI
{
	public class ImageWidget : Widget
	{
		private Texture2D image;
		
		public ImageWidget(Vec2 _position, Vec2 _size, string _imageId) : base(_position, _size)
		{
			image = Assets.Find<Texture2D>($"Textures/{_imageId}");
		}

		public override void Draw()
		{
			Raylib.DrawTexturePro(image, new Rectangle(0, 0, image.width, image.height), Bounds, Vec2.zero, 0, Color.WHITE);
		}
	}
}