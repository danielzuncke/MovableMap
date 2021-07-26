using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MovableMap
{
	public partial class MapImage : Form
	{
		// For Images
		private PictureBox pbx;
		private Bitmap bmpScreenshot;
		private Graphics gfxScreenshot;

		// For Timer
		private Timer fpsTimer;

		// For Focus
		[DllImport("user32.dll")]
		static extern bool SetForegroundWindow(IntPtr hWnd);
		string targetTitle = "notepad";
		Process returnProcess = null;

		public MapImage()
		{
			InitializeComponent();
			returnProcess = findProcess(targetTitle);
		}

		private Process findProcess(string targetTitle)
		{
			return Process.GetProcesses().FirstOrDefault(pr => pr.MainWindowTitle.ToLower().Contains(targetTitle.ToLower()));
		}

		private void setActive(Process process)
		{
			if (process != null)
			{
				SetForegroundWindow(process.MainWindowHandle);
			}
		}

		private void MapImage_Load(object sender, EventArgs e)
		{
			pbx = new PictureBox
			{
				Name = "mapBox",
				Size = new Size(420, 420),
				Location = new Point(0, 0),
				Visible = true
			};

			this.Controls.Add(pbx);

			InitializeTimer();
		}

		private void DrawMap()
		{
			bmpScreenshot = new Bitmap(420, 420, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			gfxScreenshot = Graphics.FromImage(bmpScreenshot);
			gfxScreenshot.CopyFromScreen(4700, 1020, 0, 0, new Size(420, 420));


			pbx.Image = bmpScreenshot;
		}

		private void InitializeTimer()
		{
			fpsTimer = new Timer();

			fpsTimer.Interval = 1000 / 120;
			fpsTimer.Enabled = true;

			fpsTimer.Tick += new System.EventHandler(fpsTimer_Tick);
		}

		private void fpsTimer_Tick(object sender, System.EventArgs e)
		{
			DrawMap();
		}

		private void StartTimer()
		{
			fpsTimer.Enabled = true;
		}

		private void StopTimer()
		{
			fpsTimer.Enabled = false;
		}

		public void UpdateForm(int xPos, int yPos, bool hide)
		{
			if (xPos < 0 || xPos > 5000)
			{
				xPos = 2000;
			}

			if (yPos < 0 || yPos > 1200)
			{
				yPos = 500;
			}

			this.Location = new Point(xPos, yPos);
			if (hide)
			{
				Opacity = 0;
				StopTimer();
			}
			else
			{
				Opacity = 1;
				StartTimer();
			}
		}

		private void MapImage_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}

		private void MapImage_Activated(object sender, EventArgs e)
		{
			setActive(returnProcess);
		}
	}
}
