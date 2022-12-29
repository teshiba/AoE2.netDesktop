namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AoE2NetDesktop.CtrlForm;
    using AoE2NetDesktop.LibAoE2Net.JsonFormat;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain
    {
        private readonly List<Label> labelCiv = new();
        private readonly List<Label> labelName = new();
        private readonly List<Label> labelColor = new();
        private readonly List<Label> labelRate = new();
        private readonly List<PictureBox> pictureBox = new();
        private readonly List<Control1v1> control1V1s = new();
        private Dictionary<string, Action<string>> onChangePropertyHandler;

        private void InitEventHandler()
        {
            foreach(Control item in Controls) {
                foreach(Control panelItem in ((Panel)item).Controls) {
                    panelItem.MouseDown += Controls_MouseDown;
                    panelItem.MouseMove += Controls_MouseMove;
                }
            }
        }

        private void InitPlayersCtrlList()
        {
            labelCiv.AddRange(new List<Label> {
                labelCivP1, labelCivP2, labelCivP3, labelCivP4,
                labelCivP5, labelCivP6, labelCivP7, labelCivP8,
            });

            labelName.AddRange(new List<Label> {
                labelNameP1, labelNameP2, labelNameP3, labelNameP4,
                labelNameP5, labelNameP6, labelNameP7, labelNameP8,
            });

            labelColor.AddRange(new List<Label> {
                labelColorP1, labelColorP2, labelColorP3, labelColorP4,
                labelColorP5, labelColorP6, labelColorP7, labelColorP8,
            });

            labelRate.AddRange(new List<Label> {
                labelRateP1, labelRateP2, labelRateP3, labelRateP4,
                labelRateP5, labelRateP6, labelRateP7, labelRateP8,
            });

            pictureBox.AddRange(new List<PictureBox> {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4,
                pictureBox5, pictureBox6, pictureBox7, pictureBox8,
            });
        }

        private void InitOnChangePropertyHandler()
        {
            onChangePropertyHandler = new() {
                { nameof(Settings.Default.ChromaKey), OnChangePropertyChromaKey },
                { nameof(Settings.Default.MainFormIsHideTitle), OnChangePropertyIsHideTitle },
                { nameof(Settings.Default.MainFormIsAlwaysOnTop), OnChangePropertyIsAlwaysOnTop },
                { nameof(Settings.Default.MainFormOpacityPercent), OnChangePropertyOpacity },
                { nameof(Settings.Default.MainFormIsTransparency), OnChangePropertyIsTransparency },
                { nameof(Settings.Default.DrawHighQuality), OnChangePropertyDrawHighQuality },
                { nameof(Settings.Default.IsAutoReloadLastMatch), OnChangePropertyIsAutoReloadLastMatch },
                { nameof(Settings.Default.VisibleGameTime), OnChangePropertyVisibleGameTime },
            };

            Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Init1v1CtrlList()
        {
            control1V1s.Add(new Control1v1() {
                LabelColor = label1v1ColorP1,
                LabelCiv = labelCiv1v1P1,
                LabelName = labelName1v1P1,
                LabelRate = labelRate1v1P1,
                LabelTeamResult = labelTeamResultP1,
                PictureBoxCiv = pictureBoxCiv1v1P1,
                PictureBoxUnit = pictureBoxUnit1v1P1,
                LabelWins = labelWins1v1P1,
                LabelLoses = labelLoses1v1P1,
            });
            control1V1s.Add(new Control1v1() {
                LabelColor = label1v1ColorP2,
                LabelCiv = labelCiv1v1P2,
                LabelName = labelName1v1P2,
                LabelRate = labelRate1v1P2,
                LabelTeamResult = labelTeamResultP2,
                PictureBoxCiv = pictureBoxCiv1v1P2,
                PictureBoxUnit = pictureBoxUnit1v1P2,
                LabelWins = labelWins1v1P2,
                LabelLoses = labelLoses1v1P2,
            });
        }
    }
}
