using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * Author: Shawn M. Crawford [sleepy]
 * sleepy3d@gmail.com
 * 2018+
 */
namespace DragonWarriorMonsterZoneEditor
{
    public partial class FormMain : Form
    {
        string path;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            loadDefaultComboBoxesData();
            enableDisableFormControls(false);
        }

        #region buttons
        private void buttonOpenRom_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open file...";
            ofd.Filter = "nes files (*.nes)|*.nes|All files (*.*)|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
                textBoxFilename.Text = path;

                loadRom();
            }
        }

        private void buttonUpdateMonsterZone_Click(object sender, EventArgs e)
        {
            Backend backend = new Backend(path);
            int selectedZoneIndex = comboBoxZones.SelectedIndex;
            int offset = 0x0;
            string monster1 = comboBoxMonster1.SelectedValue.ToString();
            string monster2 = comboBoxMonster2.SelectedValue.ToString();
            string monster3 = comboBoxMonster3.SelectedValue.ToString();
            string monster4 = comboBoxMonster4.SelectedValue.ToString();
            string monster5 = comboBoxMonster5.SelectedValue.ToString();
            string monsterData = monster1 + monster2 + monster3 + monster4 + monster5;

            switch (selectedZoneIndex)
            {
                case 0:
                    offset = 0xF55F;
                    break;
                case 1:
                    offset = 0xF564;
                    break;
                case 2:
                    offset = 0xF569;
                    break;
                case 3:
                    offset = 0xF56E;
                    break;
                case 4:
                    offset = 0xF573;
                    break;
                case 5:
                    offset = 0xF578;
                    break;
                case 6:
                    offset = 0xF57D;
                    break;
                case 7:
                    offset = 0xF582;
                    break;
                case 8:
                    offset = 0xF587;
                    break;
                case 9:
                    offset = 0xF58C;
                    break;
                case 10:
                    offset = 0xF591;
                    break;
                case 11:
                    offset = 0xF596;
                    break;
                case 12:
                    offset = 0xF59B;
                    break;
                case 13:
                    offset = 0xF5A0;
                    break;
                case 14:
                    offset = 0xF5A5;
                    break;
                case 15:
                    offset = 0xF5AA;
                    break;
                case 16:
                    offset = 0xF5AF;
                    break;
                case 17:
                    offset = 0xF5B4;
                    break;
                case 18:
                    offset = 0xF5B9;
                    break;
                case 19:
                    offset = 0xF5BE;
                    break;
                default:
                    offset = 0xF55F;
                    break;
            }

            bool result = backend.setZoneMonsters(offset, monsterData);

            if (result)
            {
                MessageBox.Show("ROM updated!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("ROM update failed.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }

        }
        #endregion

        private void loadRom()
        {
            getROMData();
            enableDisableFormControls(true);
        }

        private void getROMData()
        {
            Backend backend = new Backend(path);
            int offset = 0xF55F; // Zone 0
            getComboBoxIndexes(backend, offset);
        }

        private void getComboBoxIndexes(Backend backend, int offset)
        {
            comboBoxMonster1.SelectedIndex = backend.getComboboxIndexViaOffset(offset);
            comboBoxMonster2.SelectedIndex = backend.getComboboxIndexViaOffset(offset += 0x1);
            comboBoxMonster3.SelectedIndex = backend.getComboboxIndexViaOffset(offset += 0x1);
            comboBoxMonster4.SelectedIndex = backend.getComboboxIndexViaOffset(offset += 0x1);
            comboBoxMonster5.SelectedIndex = backend.getComboboxIndexViaOffset(offset += 0x1);
        }

        #region load combobox defaults
        private void loadDefaultComboBoxesData()
        {
            Backend backend = new Backend(path);

            Dictionary<string, string> zoneData = backend.getDefaultZoneData();

            comboBoxZones.DataSource = new BindingSource(zoneData, null);
            comboBoxZones.DisplayMember = "Value";
            comboBoxZones.ValueMember = "Key";

            Dictionary<string, string> monsterData = backend.getDefaultMonsterData();

            comboBoxMonster1.DataSource = new BindingSource(monsterData, null);
            comboBoxMonster1.DisplayMember = "Value";
            comboBoxMonster1.ValueMember = "Key";

            comboBoxMonster2.DataSource = new BindingSource(monsterData, null);
            comboBoxMonster2.DisplayMember = "Value";
            comboBoxMonster2.ValueMember = "Key";

            comboBoxMonster3.DataSource = new BindingSource(monsterData, null);
            comboBoxMonster3.DisplayMember = "Value";
            comboBoxMonster3.ValueMember = "Key";

            comboBoxMonster4.DataSource = new BindingSource(monsterData, null);
            comboBoxMonster4.DisplayMember = "Value";
            comboBoxMonster4.ValueMember = "Key";

            comboBoxMonster5.DataSource = new BindingSource(monsterData, null);
            comboBoxMonster5.DisplayMember = "Value";
            comboBoxMonster5.ValueMember = "Key";
        }
        #endregion

        private void enableDisableFormControls(bool isEnabled)
        {
            updateMonsterZonesToolStripMenuItem.Enabled = isEnabled;
            buttonUpdateMonsterZone.Enabled = isEnabled;
            groupBox1.Enabled = isEnabled;
            groupBox4.Enabled = isEnabled;
        }

        private void comboBoxZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This triggers on form load so check the path isn't null.
            if (path != null)
            {
                Backend backend = new Backend(path);

                int selectedIndex = comboBoxZones.SelectedIndex;
                int offset = 0x0;

                switch(selectedIndex)
                {
                    case 0:
                        offset = 0xF55F;
                        break;
                    case 1:
                        offset = 0xF564;
                        break;
                    case 2:
                        offset = 0xF569;
                        break;
                    case 3:
                        offset = 0xF56E;
                        break;
                    case 4:
                        offset = 0xF573;
                        break;
                    case 5:
                        offset = 0xF578;
                        break;
                    case 6:
                        offset = 0xF57D;
                        break;
                    case 7:
                        offset = 0xF582;
                        break;
                    case 8:
                        offset = 0xF587;
                        break;
                    case 9:
                        offset = 0xF58C;
                        break;
                    case 10:
                        offset = 0xF591;
                        break;
                    case 11:
                        offset = 0xF596;
                        break;
                    case 12:
                        offset = 0xF59B;
                        break;
                    case 13:
                        offset = 0xF5A0;
                        break;
                    case 14:
                        offset = 0xF5A5;
                        break;
                    case 15:
                        offset = 0xF5AA;
                        break;
                    case 16:
                        offset = 0xF5AF;
                        break;
                    case 17:
                        offset = 0xF5B4;
                        break;
                    case 18:
                        offset = 0xF5B9;
                        break;
                    case 19:
                        offset = 0xF5BE;
                        break;
                    default:
                        offset = 0xF55F;
                        break;
                }

                getComboBoxIndexes(backend, offset);
            }
        }

        #region tool strip menu items
        private void updateMonsterZonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonUpdateMonsterZone_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonOpenRom_Click(sender, e);
        }
        #endregion
    }
}
