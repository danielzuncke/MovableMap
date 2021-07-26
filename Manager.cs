using System;
using System.Windows.Forms;

namespace MovableMap
{
	public partial class Manager : Form
	{
		// Data
		private MapImage mapImage;
		private int posX;
		private int posY;
		private bool hide;

		public Manager(MapImage mapImage)
		{
			InitializeComponent();
			this.mapImage = mapImage;
			Init();
			numericUpDown1.Value = posX;
			numericUpDown2.Value = posY;
			ReloadMap();
		}

		private void Init()
		{
			if (!int.TryParse(Settings.Default.posX, out posX))
			{
				posX = 2890;
			}
			if (!int.TryParse(Settings.Default.posY, out posY))
			{
				posY = 1020;
			}
			if (!bool.TryParse(Settings.Default.Hide.ToString(), out hide))
			{
				hide = false;
			}
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if (int.TryParse(numericUpDown1.Value.ToString(), out posX))
			{
				ReloadMap();
			}
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			if (int.TryParse(numericUpDown2.Value.ToString(), out posY))
			{
				ReloadMap();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			hide = checkBox1.Checked;
			ReloadMap();
		}

		private void ReloadMap()
		{
			mapImage.UpdateForm(posX, posY, hide);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			posX = 2890;
			posY = 1020;
			hide = false;
			ReloadMap();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			Settings.Default.posX = posX.ToString();
			Settings.Default.posY = posY.ToString();
			Settings.Default.Hide = hide;
			Settings.Default.Save();
		}
	}
}
