using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject
{
    public partial class FormEnterCategory : Form
    {

        public object[] data { get; set; }

        public FormEnterCategory(int remainingPercent)
        {
            InitializeComponent();
            data = new object[2];
            numericUpDownWeight.Maximum = remainingPercent;
        }

        public FormEnterCategory(int remainingPercent, int weight, string name)
        {
            InitializeComponent();
            data = new object[2];
            numericUpDownWeight.Maximum = remainingPercent;
            numericUpDownWeight.Value = weight;
            textBoxCategoryName.Text = name;
        }

        private void buttonResult(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Ok")
            {
                if (textBoxCategoryName.Text == "")
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("You cannot enter an empty category name!");
                }
                else if (numericUpDownWeight.Value == 0)
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("You cannot enter zero for the weight!");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    data[0] = textBoxCategoryName.Text;
                    data[1] = numericUpDownWeight.Value;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void numericUpDownPercent_Enter(object sender, EventArgs e)
        {
            numericUpDownWeight.Select(0, numericUpDownWeight.Text.Length);
        }

        private void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxCategoryName.Text == "")
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("You cannot enter an empty category name!");
                }
                else if (numericUpDownWeight.Value == 0)
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("You cannot enter zero for the weight!");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    data[0] = textBoxCategoryName.Text;
                    data[1] = numericUpDownWeight.Value;
                    this.Close();
                }
            }
        }
    }
}