using Azimuth;

using MathLib;

using Raylib_cs;

namespace AIE13_MatrixMultiplication
{
	public class MatrixMultiplicationGame : Game
	{
		public class MatrixMultiplyPair
		{
			public string label = "";
			public Mat3 a;
			public Mat3 b;
		}

		private Matrix3Renderer renderer;

		private int currentIndex;
		private List<MatrixMultiplyPair> matrixMultiplies = new();

		private Mat3 matA;
		private Mat3 matB;
		private Mat3 matC;

		public override void Load()
		{
			Vec2 screenSize = new Vec2(Application.Instance!.Window.Width, Application.Instance.Window.Height);
			Vec2 unitSize = new Vec2(24, 24);

			renderer = new Matrix3Renderer(screenSize, unitSize);

			PopulateMatrixMultiplications();
			UpdateSelectedMatrix();
		}

		public override void Update(float _deltaTime)
		{
			if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
				Swap(ref matA, ref matB);

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_UP) && currentIndex > 0)
			{
				currentIndex--;
				UpdateSelectedMatrix();
			}

			if(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN) && currentIndex < matrixMultiplies.Count - 1)
			{
				currentIndex++;
				UpdateSelectedMatrix();
			}

			matC = matA * matB;
		}

		public override void Draw()
		{
			renderer.BeginRender();
			renderer.DrawMatrix("MatA", matA, Color.RED, new Vec2(10, 140));
			renderer.DrawMatrix("MatB", matB, Color.GREEN, new Vec2(140, 10));
			renderer.DrawMatrix("MatC", matC, Color.BLUE, new Vec2(10, 10));
			renderer.EndRender();
			
			matA = renderer.GetMatrix("MatA");
			matB = renderer.GetMatrix("MatB");
			
			Raylib.DrawRectangle(140, 140, 120, 20 + matrixMultiplies.Count * 15, Color.WHITE);
			Raylib.DrawRectangleLines(140, 140, 120, 20 + matrixMultiplies.Count * 15, Color.DARKGRAY);
			Raylib.DrawText("Space to swap A & B", 145, 145, 10, Color.DARKGRAY);

			for(int i = 0; i < matrixMultiplies.Count; i++)
			{
				if(i == currentIndex)
					Raylib.DrawRectangle(142, 160 + i * 15, 116, 13, Color.LIGHTGRAY);

				MatrixMultiplyPair pair = matrixMultiplies[i];
				Raylib.DrawText(pair.label, 145, 162 + i * 15, 10, Color.DARKGRAY);
			}
		}

		public override void Unload() { }

		private void Swap(ref Mat3 _a, ref Mat3 _b)
		{
			Mat3 temp = _a;
			_a = _b;
			_b = temp;
		}

		private void UpdateSelectedMatrix()
		{
			matA = matrixMultiplies[currentIndex % matrixMultiplies.Count].a;
			matB = matrixMultiplies[currentIndex % matrixMultiplies.Count].b;
		}

		private void PopulateMatrixMultiplications()
		{
			matrixMultiplies.Add(new MatrixMultiplyPair
			{
				label = "Identity * Identity",
				a = new Mat3(),
				b = new Mat3()
			});

			matrixMultiplies.Add(new MatrixMultiplyPair
			{
				label = "Translate * Scale",
				a = new Mat3(1, 0, 3,
				             0, 1, 2,
				             0, 0, 1),
				b = new Mat3(2, 0, 0,
				             0, 3, 0,
				             0, 0, 1)
			});

			matrixMultiplies.Add(new MatrixMultiplyPair
			{
				label = "Rotation * Translation",
				a = new Mat3(0.707f, -0.707f, 0,
				             0.707f, 0.707f, 0,
				             0, 0, 1),
				b = new Mat3(1, 0, 3,
				             0, 1, 2,
				             0, 0, 1)
			});

			matrixMultiplies.Add(new MatrixMultiplyPair
			{
				label = "Translation * Rotation",
				a = new Mat3(1, 0, 3,
				             0, 1, 2,
				             0, 0, 1),
				b = new Mat3(0.707f, -0.707f, 0,
				             0.707f, 0.707f, 0,
				             0, 0, 1)
			});

			matrixMultiplies.Add(new MatrixMultiplyPair
			{
				label = "Transform * Rotation",
				a = new Mat3(2, 0, 2,
				             0, 3, 3,
				             0, 0, 1),
				b = new Mat3(0.707f, -0.707f, 0,
				             0.707f, 0.707f, 0,
				             0, 0, 1)
			});
		}
	}
}