namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using AoE2NetDesktop;
    using LibAoE2net;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain : ControllableForm
    {
        private readonly List<Label> labelCiv = new ();
        private readonly List<Label> labelColor = new ();
        private readonly List<Label> labelRate = new ();
        private readonly List<Label> labelName = new ();
        private readonly List<PictureBox> pictureBox = new ();
        private readonly Language language;

        private Point mouseDownPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        /// <param name="language">Display language.</param>
        public FormMain(Language language)
            : base(new CtrlMain())
        {
            InitializeComponent();
            InitEventHandler();
            InitPlayersCtrlList();
            ClearLastMatch();

            // formMain hold the app settings.
            CtrlSettings = new CtrlSettings();
            CtrlSettings.PropertySetting.PropertyChanged += OnChangeProperty;

            SetChromaKey(CtrlSettings.PropertySetting.ChromaKey);
            OnChangeIsHideTitle(CtrlSettings.PropertySetting.IsHideTitle);
            TopMost = CtrlSettings.PropertySetting.IsAlwaysOnTop;
            Opacity = CtrlSettings.PropertySetting.Opacity;
            OnChangeIsTransparency(CtrlSettings.PropertySetting.IsTransparency);
            DrawEx.DrawHighQuality = CtrlSettings.PropertySetting.DrawHighQuality;

            this.language = language;
        }

        /// <summary>
        /// Gets Settings.
        /// </summary>
        public CtrlSettings CtrlSettings { get; private set; }

        private void OnChangeProperty(object sender, PropertyChangedEventArgs e)
        {
            var propertySettings = (PropertySettings)sender;
            switch (e.PropertyName) {
            case "ChromaKey":
                SetChromaKey(propertySettings.ChromaKey);
                break;
            case "IsHideTitle":
                OnChangeIsHideTitle(propertySettings.IsHideTitle);
                break;
            case "IsAlwaysOnTop":
                TopMost = propertySettings.IsAlwaysOnTop;
                break;
            case "Opacity":
                Opacity = propertySettings.Opacity;
                break;
            case "IsTransparency":
                OnChangeIsTransparency(propertySettings.IsTransparency);
                break;
            case "DrawHighQuality":
                DrawEx.DrawHighQuality = propertySettings.DrawHighQuality;
                Refresh();
                break;
            default:
                break;
            }
        }

        private void InitEventHandler()
        {
            foreach (Control item in Controls) {
                item.MouseDown += Controls_MouseDown;
                item.MouseMove += Controls_MouseMove;
            }

            foreach (Control item in panelTeam1.Controls) {
                item.MouseDown += Controls_MouseDown;
                item.MouseMove += Controls_MouseMove;
            }

            foreach (Control item in panelTeam2.Controls) {
                item.MouseDown += Controls_MouseDown;
                item.MouseMove += Controls_MouseMove;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // Async event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void FormMain_Load(object sender, EventArgs e)
        {
            RestoreWindowStatus();
            ResizePanels();

            try {
                _ = await CtrlMain.InitAsync(language);

                // if the app is opened first, need to set user profile.
                if (!await CtrlSettings.ReadProfileAsync()) {
                    OpenSettings();
                }

                _ = await UpdateLastMatch(CtrlSettings.ProfileId);
            } catch (Exception ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            SetChromaKey(CtrlSettings.PropertySetting.ChromaKey);

            Awaiter.Complete();
        }

        private async void UpdateToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            _ = await UpdateLastMatch(CtrlSettings.ProfileId);
            Awaiter.Complete();
        }

        ///////////////////////////////////////////////////////////////////////
        // Event handlers
        ///////////////////////////////////////////////////////////////////////
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CtrlSettings.FormMyHistory?.Close();
            SaveWindowPosition();
            Settings.Default.Save();
        }

        private void LabelName_Paint(object sender, PaintEventArgs e)
        {
            var labelName = (Label)sender;
            var player = (Player)labelName.Tag;

            if (player?.ProfilId == CtrlSettings.ProfileId) {
                labelName.DrawString(e, 20, Color.Black, Color.DarkOrange);
            } else {
                labelName.DrawString(e, 20, Color.Black, Color.MediumSeaGreen);
            }
        }

        private void LabelNameP1_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP2_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP3_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP4_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP5_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP6_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP7_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP8_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelRate_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 22, Color.Black, Color.DeepSkyBlue);
        }

        private void LabelCiv_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 15, Color.Black, Color.YellowGreen);
        }

        private void LabelAveRate_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 18, Color.Black, Color.DarkGoldenrod);
        }

        private void LabelColor_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 23, Color.Black, Color.White, new Point(6, 6));
        }

        private void LabelMap_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 28, Color.Black, Color.DarkKhaki);
        }

        private void LabelGameId_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 14, Color.Black, Color.LightSeaGreen);
        }

        private void LabelServer_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 14, Color.Black, Color.LightSeaGreen);
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            ResizePanels();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                mouseDownPoint = new Point(e.X, e.Y);
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                Left += e.X - mouseDownPoint.X;
                Top += e.Y - mouseDownPoint.Y;
            }
        }

        private void Controls_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                mouseDownPoint = new Point(e.X, e.Y);
            }
        }

        private void Controls_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                Left += e.X - mouseDownPoint.X;
                Top += e.Y - mouseDownPoint.Y;
            }
        }

        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                contextMenuStripMain.Show();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
            case Keys.F5:
                updateToolStripMenuItem.PerformClick();
                break;
            default:
                Size size = GetWindowResizeParams(e);
                Size += size;
                break;
            }

            Awaiter.Complete();

            // local function
            static Size GetWindowResizeParams(KeyEventArgs e)
            {
                var changeSize = 0;

                if (e.Alt) {
                    if (e.Shift) {
                        changeSize = 1;
                    } else {
                        changeSize = 10;
                    }
                }

                var size = e.KeyCode switch {
                    Keys.Right => new Size(changeSize, 0),
                    Keys.Left => new Size(-changeSize, 0),
                    Keys.Up => new Size(0, -changeSize),
                    Keys.Down => new Size(0, changeSize),
                    _ => new Size(0, 0),
                };

                return size;
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void LabelName_DoubleClick(object sender, EventArgs e)
        {
            var labelName = (Label)sender;
            var player = (Player)labelName.Tag;

            var formHistory = CtrlHistory.GenerateFormHistory(player.Name, player.ProfilId);
            formHistory.Show();
        }

        private void ShowMyHistoryHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CtrlSettings.ShowMyHistory();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
