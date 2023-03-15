using Raylib_cs;

namespace MathLib
{
	public class Matrix3Renderer
	{
		public Vec2 screenSize;
		public Vec2 unitSize;

		public readonly List<Action> uiRenderQueue = new();
		private readonly List<Action> renderQueue = new();

		private readonly Dictionary<string, MatrixProps> props = new();
		private readonly Dictionary<string, Mat3> matrices = new();

		private class ExtraInfoActions
		{
			public string text;
			public Action? onScroll;

			public ExtraInfoActions()
			{
				text = "";
			}
		}

		private class MatrixProps
		{
			// ReSharper disable once NotAccessedField.Local
			private string name;
			public bool mouseOver;

			public MatrixProps(string _name)
			{
				name = _name;
			}
		}

		public Matrix3Renderer(Vec2 _screenSize, Vec2 _unitSize)
		{
			this.screenSize = _screenSize;
			this.unitSize = _unitSize;
		}

		public void BeginRender()
		{
			renderQueue.Clear();
			uiRenderQueue.Clear();
			matrices.Clear();
		}

		public void EndRender()
		{
			DrawGrid();

			foreach(Action action in renderQueue)
				action();

			foreach(Action action in uiRenderQueue)
				action();
		}

		public void DrawGrid()
		{
			Vec2 centerPos = screenSize * 0.5f;
			Color color = Color.LIGHTGRAY;

			// Horizontal lines
			int numRows = (int) (screenSize.y / unitSize.y);
			int hNumRows = (numRows / 2) + 1;
			for(int i = -hNumRows; i < hNumRows; i++)
			{
				float thickness = i == 0 ? 2 : 1;
				float yOffset = centerPos.y + (i * unitSize.y);
				Raylib.DrawLineEx(new Vec2(0, yOffset), new Vec2(screenSize.x, yOffset), thickness, color);
			}

			// vertical lines
			int numCols = (int) (screenSize.x / unitSize.x);
			int hNumCols = (numCols / 2) + 1;
			for(int i = -hNumCols; i < hNumCols; i++)
			{
				float thickness = i == 0 ? 2 : 1;
				float xOffset = centerPos.x + (i * unitSize.x);
				Raylib.DrawLineEx(new Vec2(xOffset, 0), new Vec2(xOffset, screenSize.y), thickness, color);
			}
		}

		public void DrawMatrix(string _name, Mat3 _mat, Color _color, Vec2 _textAreaEditPos)
		{
			matrices[_name] = _mat;

			if(props.ContainsKey(_name) == false)
				props.Add(_name, new MatrixProps(_name));

			object boxedMat = _mat;

			renderQueue.Add(() =>
			{
				Mat3 mat3 = (Mat3) boxedMat;

				Vec2 origin = screenSize * 0.5f;

				Vec2 xa = new Vec2(mat3.m1, mat3.m2 * -1) * unitSize.x;
				Vec2 ya = new Vec2(mat3.m4, mat3.m5 * -1) * unitSize.y;
				Vec2 za = new Vec2(mat3.m7 * unitSize.x, mat3.m8 * -1 * unitSize.y);

				float thickness = props[_name].mouseOver ? 3.0f : 2.0f;

                DrawArrow(origin + za, origin + za + xa, thickness, _color);
                DrawArrow(origin + za, origin + za + ya, thickness, _color);
			});

			uiRenderQueue.Add(() => { DrawMatrixEditAreaUI(_name, _textAreaEditPos, _mat, _color); });
		}

		public void DrawPoint(string _name, Vec2 _point, Color _color, float _size)
		{
			renderQueue.Add(() =>
			{
				Vec2 origin = screenSize * 0.5f;

				Vec2 p = new Vec2(
				                  origin.x + (_point.x * unitSize.x),
				                  origin.y + (_point.y * unitSize.y * -1));

				Raylib.DrawCircleV(p, _size, _color);
			});
		}

		private static void DrawArrow(Vec2 _p1, Vec2 _p2, float _thickness, Color _color)
		{
			Vec2 dir = (_p2 - _p1).Normalized * (5 + (_thickness - 1.0f));
			Vec2 pDir = new Vec2(dir.y, -dir.x);

			Raylib.DrawLineEx(_p1, _p2, _thickness, _color);
			Raylib.DrawTriangle(_p2, _p2 - dir + pDir, _p2 - dir - pDir, _color);
		}

		public Mat3 GetMatrix(string _name) => matrices[_name];

		private static string GetMatrixName(Mat3 _mat)
		{
			// compare function for below
			// close enough is good enough :P
			static bool Compare(float _v1, float _v2) => _v1 >= _v2 - 0.0001f && _v1 <= _v2 + 0.0001f;

			// Identity check - all values must match
			if(Compare(_mat.m1, 1) && Compare(_mat.m2, 0) && Compare(_mat.m3, 0) &&
			   Compare(_mat.m4, 0) && Compare(_mat.m5, 1) && Compare(_mat.m6, 0) &&
			   Compare(_mat.m7, 0) && Compare(_mat.m8, 0) && Compare(_mat.m9, 1))
			{
				return "Identity";
			}

			// Scale Matrix
			// m1 and m5 can be anything, the rest have to be same
			// as identity
			if(Compare(_mat.m2, 0) && Compare(_mat.m3, 0) &&
			   Compare(_mat.m4, 0) && Compare(_mat.m6, 0) &&
			   Compare(_mat.m7, 0) && Compare(_mat.m8, 0) && Compare(_mat.m9, 1))
			{
				return "Scale";
			}

			// Translation Matrix
			// m7 and m8 can be anything, the rest have to be same
			// as identity
			if(Compare(_mat.m1, 1) && Compare(_mat.m2, 0) && Compare(_mat.m3, 0) &&
			   Compare(_mat.m4, 0) && Compare(_mat.m5, 1) && Compare(_mat.m6, 0) &&
			   Compare(_mat.m9, 1))
			{
				return "Translation";
			}

			// Rotation Matrix
			// length of x and y axis must be 1
			// x and y axis must be perpendicular (calculated via dot product);
			float xaLen = MathF.Sqrt(_mat.m1 * _mat.m1 + _mat.m2 * _mat.m2);
			float yaLen = MathF.Sqrt(_mat.m4 * _mat.m4 + _mat.m5 * _mat.m5);
			float dot = _mat.m1 * _mat.m4 + _mat.m2 * _mat.m5;
			if(Compare(dot, 0) && Compare(xaLen, 1) && Compare(yaLen, 1) &&
			   Compare(_mat.m3, 0) && Compare(_mat.m6, 0) &&
			   Compare(_mat.m7, 0) && Compare(_mat.m8, 0) && Compare(_mat.m9, 1))
			{
				string deg = (MathF.Atan2(_mat.m2, _mat.m1) * 180.0f / MathF.PI).ToString("0.00");

				return $"Rotation ({deg})";
			}

			// any other configuration...
			// than its just a "transform" matrix
			return "Transform";
		}

		private void ModifyMatrix(string _name, float[] _a, Mat3 _mat)
		{
			_mat.m1 = _a[0];
			_mat.m2 = _a[1];
			_mat.m3 = _a[2];
			_mat.m4 = _a[3];
			_mat.m5 = _a[4];
			_mat.m6 = _a[5];
			_mat.m7 = _a[6];
			_mat.m8 = _a[7];
			_mat.m9 = _a[8];

			matrices[_name] = _mat;
		}

		private static bool IsPointInRect(Vec2 _v, Rectangle _r) => _v.x >= _r.x && _v.x < _r.x + _r.width && _v.y >= _r.y && _v.y < _r.y + _r.height;

		// ReSharper disable once InconsistentNaming
		private void DrawMatrixEditAreaUI(string _name, Vec2 _pos, Mat3 _mat, Color _color)
		{
			MatrixProps properties = props[_name];

			Vec2 mousePos = Raylib.GetMousePosition();

			const int TW = 40;
			const int TH = 20;

			// copy the matrix into an array
			// makes it easier for rendering each item via loops later
			float[] arr = new float[] { _mat.m1, _mat.m2, _mat.m3, _mat.m4, _mat.m5, _mat.m6, _mat.m7, _mat.m8, _mat.m9 };

			// List of Extra stuff to displaying in the UI
			List<ExtraInfoActions> extraInfo = GetExtraInfoActions(_name, arr, _mat);

			// Draw Title bg rectangle
			// Draw MatrixComponents rectangle
			// Draw Extra Info Rectangle
			//===============================================================
			Rectangle titleRect = new Rectangle(_pos.x, _pos.y, TW * 3, 15);
			Rectangle matRect = new Rectangle(_pos.x, _pos.y + 15, TW * 3, TH * 3);
			Rectangle extraRect = new Rectangle(matRect.x, matRect.y + matRect.height, matRect.width, extraInfo.Count * 15 + 5);
			Rectangle fullRect = new Rectangle(titleRect.x, titleRect.y, titleRect.width, titleRect.height + matRect.height + extraRect.height);

			// draw box for title
			Raylib.DrawRectangleRec(titleRect, Color.DARKGRAY);
			Raylib.DrawText(_name, (int) titleRect.x + 5, (int) titleRect.y + 2, 10, Color.RAYWHITE);

			// draw the box where our matrix values go
			Color matBgColor = _color;
			matBgColor.a = 16;
			Raylib.DrawRectangleRec(matRect, Color.WHITE);
			Raylib.DrawRectangleRec(matRect, matBgColor);

			// draw box for the extra info
			Raylib.DrawRectangleRec(extraRect, Color.GRAY);
			Raylib.DrawRectangleLinesEx(fullRect, 1, Color.DARKGRAY);
			//===============================================================

			properties.mouseOver = IsPointInRect(mousePos, fullRect);

			// draw the text for each matrix value
			// ==============================================================
			for(int i = 0; i < arr.Length; i++)
			{
				int yId = i % 3;
				int xId = i / 3;
				int xPos = (int) ((xId * TW) + (matRect.x + 6));
				int yPos = (int) ((yId * TH) + (matRect.y + 6));
				Raylib.DrawText(arr[i].ToString("0.000"), xPos, yPos, 10, Color.GRAY);
			}
			// ==============================================================

			// draw the highlight box over the element the mouse is sitting over
			if(IsPointInRect(mousePos, matRect))
			{
				// convert mouse pos to Row/Col index
				// should give value between 0 and 3 because were only working with 3x3 matrix.
				int mpxId = (int) ((mousePos.x - matRect.x) / TW);
				int mpyId = (int) ((mousePos.y - matRect.y) / TH);
				int mpIndex = mpxId * 3 + mpyId;

				// draw colored box over the value that the mouse is over.
				Raylib.DrawRectangle((int) (mpxId * TW + matRect.x),
				                     (int) (mpyId * TH + matRect.y),
				                     TW, TH, new Color((int) _color.r, _color.g, _color.b, 64));

				// Draw the variable name, col and row in the title
				// might be useful for debugging
				string indexText = $"M{mpIndex + 1}:[{mpxId},{mpyId}]";
				int indexTextWidth = Raylib.MeasureText(indexText, 10);
				Raylib.DrawText(indexText, (int) (titleRect.x + titleRect.width - indexTextWidth - 10), (int) (titleRect.y + 3), 10, Color.RAYWHITE);

				// if the mouse wheel has scrolled update the matrix
				if(Raylib.GetMouseWheelMove() != 0.0f)
				{
					arr[mpxId * 3 + mpyId] -= Raylib.GetMouseWheelMove() * 0.1f;
					ModifyMatrix(_name, arr, _mat);
				}
			}

			// loop through the extra info list, draw and handle input
			const int ITEM_HEIGHT = 15;
			for(int i = 0; i < extraInfo.Count; i++)
			{
				Rectangle itemRect = new Rectangle(extraRect.x, extraRect.y + i * ITEM_HEIGHT, extraRect.width, ITEM_HEIGHT);

				// is the mouse within the rect bounds
				if(IsPointInRect(mousePos, itemRect))
				{
					// draw box to show mouse hover
					Raylib.DrawRectangleRec(itemRect, Color.DARKGRAY);

					// call the items onScroll method when mouse wheel moves.
					if(Raylib.GetMouseWheelMove() != 0.0f)
					{
						extraInfo[i].onScroll?.Invoke();
					}
				}

				Raylib.DrawText(extraInfo[i].text, (int) extraRect.x + 10, (int) (itemRect.y + 3), 10, Color.RAYWHITE);
			}
		}

		private List<ExtraInfoActions> GetExtraInfoActions(string _name, float[] _arr, Mat3 _mat)
		{
			List<ExtraInfoActions> extraInfo = new()
			{
				new ExtraInfoActions
				{
					text = GetMatrixName(_mat),
					onScroll = () =>
					{
						string text = GetMatrixName(_mat);
						if(text.StartsWith("Rotation"))
						{
							float scrollVal = Raylib.GetMouseWheelMove();
							RotateVec(scrollVal * 0.01f, ref _arr[0], ref _arr[1]);
							RotateVec(scrollVal * 0.01f, ref _arr[3], ref _arr[4]);
							ModifyMatrix(_name, _arr, _mat);
						}
					}
				},
				new ExtraInfoActions
				{
					text = $"x scale: {new Vec2(_mat.m1, _mat.m2).Magnitude():0.000}",
					onScroll = () =>
					{
						float len = MathF.Sqrt(_arr[0] * _arr[0] + _arr[1] * _arr[1]);
						if(len > 0)
						{
							_arr[0] += _arr[0] / len * Raylib.GetMouseWheelMove() * 0.1f;
							_arr[1] += _arr[1] / len * Raylib.GetMouseWheelMove() * 0.1f;
							ModifyMatrix(_name, _arr, _mat);
						}
					}
				},
				new ExtraInfoActions
				{
					text = $"y scale: {new Vec2(_mat.m4, _mat.m5).Magnitude():0.000}",
					onScroll = () =>
					{
						float len = MathF.Sqrt(_arr[3] * _arr[3] + _arr[4] * _arr[4]);
						if(len > 0)
						{
							_arr[3] += _arr[3] / len * Raylib.GetMouseWheelMove() * 0.1f;
							_arr[4] += _arr[4] / len * Raylib.GetMouseWheelMove() * 0.1f;
							ModifyMatrix(_name, _arr, _mat);
						}
					}
				}
			};

			return extraInfo;
		}

		private static void RotateVec(float _amount, ref float _x, ref float _y)
		{
			float xRotated = _x * MathF.Cos(_amount) - _y * MathF.Sin(_amount);
			float yRotated = _x * MathF.Sin(_amount) + _y * MathF.Cos(_amount);

			_x = xRotated;
			_y = yRotated;
		}
	}
}