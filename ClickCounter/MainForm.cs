using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ClickCounter
{
	internal class MainForm : Form
	{
		private IContainer components;

		private Timer timer1;

		private Label lbClicksVal;

		private Label lbLabel;

		private Label lbMaxClickRate;

		private LinkLabel llbWebsite;

		private int clicks;

		private ulong totalClicks;

		private int maxClickRate;

		private bool running;

		private int tmpN;

		private string titleFormat;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				((IDisposable)components).Dispose();
			}
			((Form)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Expected O, but got Unknown
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Expected O, but got Unknown
			((Form)this).set_TransparencyKey(Color.get_DimGray());
			((Control)this).set_BackColor(Color.get_DimGray());
			((Form)this).set_TopMost(true);
			components = (IContainer)new Container();
			timer1 = new Timer(components);
			lbClicksVal = new Label();
			lbLabel = new Label();
			lbMaxClickRate = new Label();
			llbWebsite = new LinkLabel();
			((Control)this).SuspendLayout();
			timer1.set_Enabled(true);
			timer1.set_Interval(1000);
			timer1.add_Tick((EventHandler)timer1_Tick);
			((Control)lbClicksVal).set_Dock((DockStyle)5);
			((Control)lbClicksVal).set_Font(new Font("Microsoft Sans Serif", 48f, (FontStyle)0, (GraphicsUnit)3, (byte)0));
			((Control)lbClicksVal).set_Location(new Point(0, 0));
			((Control)lbClicksVal).set_Name("lbClicksVal");
			((Control)lbClicksVal).set_Size(new Size(105, 67));
			((Control)lbClicksVal).set_TabIndex(0);
			((Control)lbClicksVal).set_Text("0");
			lbClicksVal.set_TextAlign((ContentAlignment)1);
			((Control)lbLabel).set_Dock((DockStyle)1);
			((Control)lbLabel).set_Location(new Point(0, -10));
			((Control)lbLabel).set_Name("lbLabel");
			((Control)lbLabel).set_Size(new Size(105, 28));
			((Control)lbLabel).set_TabIndex(1);
			lbLabel.set_TextAlign((ContentAlignment)2);
			((ContainerControl)this).set_AutoScaleDimensions(new SizeF(6f, 13f));
			((ContainerControl)this).set_AutoScaleMode((AutoScaleMode)1);
			((Form)this).set_ClientSize(new Size(105, 98));
			((Control)this).get_Controls().Add((Control)(object)llbWebsite);
			((Control)this).get_Controls().Add((Control)(object)lbClicksVal);
			((Control)this).get_Controls().Add((Control)(object)lbLabel);
			((Control)this).get_Controls().Add((Control)(object)lbMaxClickRate);
			((Form)this).set_FormBorderStyle((FormBorderStyle)5);
			((Form)this).set_MaximizeBox(false);
			((Form)this).set_MinimizeBox(false);
			((Control)this).set_Name("MainForm");
			((Form)this).add_FormClosed(new FormClosedEventHandler(Form1_FormClosed));
			((Control)this).ResumeLayout(false);
		}

		public void Form1_Load(object sender, EventArgs e)
		{
			ControlExtension.Draggable((Control)(object)lbClicksVal, true);
		}

		public MainForm()
			: this()
		{
			InitializeComponent();
			((Form)this).set_Icon(Icon.ExtractAssociatedIcon(Application.get_ExecutablePath()));
			clicks = 0;
			maxClickRate = 0;
			totalClicks = 0uL;
			running = true;
			new Thread(new ThreadStart(runClickCounter)).Start();
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo("de");
			if (CultureInfo.CurrentCulture.Equals(cultureInfo) || CultureInfo.CurrentCulture.Parent.Equals(cultureInfo))
			{
				((Control)lbLabel).set_Text("");
				titleFormat = "{0}";
			}
			else
			{
				((Control)lbLabel).set_Text("");
				titleFormat = "{0}";
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			totalClicks += (ulong)clicks;
			tmpN = 5000 + clicks * 20;
			if (clicks > maxClickRate)
			{
				maxClickRate = clicks;
			}
			((Control)this).set_Text(string.Format(titleFormat, clicks));
			((Control)lbClicksVal).set_Text(clicks.ToString());
			((Control)lbMaxClickRate).set_Text($"Max.: {maxClickRate}  Total: {totalClicks}");
			clicks = 0;
		}

		private void runClickCounter()
		{
			bool flag = false;
			int num = 0;
			while (running)
			{
				bool flag2 = MouseIsPressed();
				if (flag2 && !flag)
				{
					clicks++;
				}
				flag = flag2;
				if (num++ >= tmpN)
				{
					Thread.Sleep(1);
					num = 0;
				}
			}
		}

		private static bool MouseIsPressed()
		{
			return GetAsyncKeyState((Keys)1) != 0;
		}

		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(Keys vKey);

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			running = false;
		}

		private void llbWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(((Control)llbWebsite).get_Text());
		}
	}
}
