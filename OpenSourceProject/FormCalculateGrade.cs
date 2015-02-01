using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject
{
    public partial class FormCalculateGrade : Form
    {
        public SchoolClass schoolClass { get; set; }
        public FormCalculateGrade(SchoolClass schoolClass)
        {
            InitializeComponent();
            this.schoolClass = schoolClass;
            foreach (Category c in schoolClass.CategoryList)
            {
                comboBoxCategories.Items.Add(c.Name);
            }
        }

        private void onButtonClick(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Calculate")
            {
                if (schoolClass.RemainingWeight == 0)
                {
                    double wantedGrade = (double)numericUpDownGrade.Value;
                    double assignmentPtsPoss = (double)numericUpDownPoints.Value;
                    double currentGrade = schoolClass.Percent;
                    double currentCategoryPercent = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].Percent;
                    double currentCategoryWeight = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].Weight / 100d;
                    double currentCategoryWeightedGrade = currentCategoryPercent * currentCategoryWeight;
                    double wantedCategoryWeightedGrade = currentCategoryWeightedGrade + (wantedGrade - currentGrade);
                    double wantedCategoryGrade = wantedCategoryWeightedGrade / currentCategoryWeight / 100d;
                    double currentPtsPoss = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].PtsPoss;
                    double currentScore = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].Score;
                    double wantedScore = wantedCategoryGrade * (currentPtsPoss + assignmentPtsPoss) - currentScore;
                    MessageBox.Show("You have to get a " + wantedScore + "/" + assignmentPtsPoss + " or a " + Math.Round(wantedScore/assignmentPtsPoss*100,2) + "% to get a " + wantedGrade + "% in this class.", "Results");
                }
                else
                {
                    double wantedGrade = (double)numericUpDownGrade.Value;
                    double assignmentPtsPoss = (double)numericUpDownPoints.Value;
                    double currentGrade = schoolClass.Percent;
                    double currentCategoryPercent = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].Percent;
                    double multiplier = 100 / (100 - schoolClass.RemainingWeight);
                    double currentCategoryWeight = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].Weight / 100d * multiplier;
                    double currentCategoryWeightedGrade = currentCategoryPercent * currentCategoryWeight;
                    double wantedCategoryWeightedGrade = currentCategoryWeightedGrade + (wantedGrade - currentGrade);
                    double wantedCategoryGrade = wantedCategoryWeightedGrade / currentCategoryWeight / 100d;
                    double currentPtsPoss = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].PtsPoss;
                    double currentScore = schoolClass.CategoryList[comboBoxCategories.SelectedIndex].Score;
                    double wantedScore = wantedCategoryGrade * (currentPtsPoss + assignmentPtsPoss) - currentScore;
                    MessageBox.Show("You have to get a " + wantedScore + "/" + assignmentPtsPoss + " or a " + Math.Round(wantedScore / assignmentPtsPoss * 100, 2) + "% to get a " + wantedGrade + "% in this class.", "Results");
                }
            }
            else
            {
                this.Close();
            }
        }

        private void OnEnter(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Text.Length);
        }
    }
}
