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
    public partial class FormEnterClass : Form
    {

        public string name { get; set; }

        public FormEnterClass()
        {
            InitializeComponent();
        }

        public FormEnterClass(string name)
        {
            InitializeComponent();
            textBoxClassName.Text = name;
        }

        private void buttonResult(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Ok")
            {
                if (textBoxClassName.Text == "")
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("You cannot enter an empty class name!");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    name = textBoxClassName.Text;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxClassName.Text == "")
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("You cannot enter an empty class name!");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    name = textBoxClassName.Text;
                    this.Close();
                }
            }
        }
    }
}
