using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject {
    public partial class GradeCalculator : Form {

        public GradeCalculator()
        {
            InitializeComponent();
        }


        private void onEndCellEdit(object sender, DataGridViewCellEventArgs e) {
            validateEntry(sender, e);
            calculatePercentage(sender, e);
            calculateTotals(sender, e);
        }

        private void calculateTotals(object sender, DataGridViewCellEventArgs e) {
            bool hasScores = false;
            double ptsPoss = 0, score = 0;
            DataGridView data = (DataGridView)sender;
            for (int i = 0; i < data.Rows.Count; i++) {
                //Check to see if there is multiplier
                if (data.Rows[i].Cells[1].Value != null)
                {
                    //Update with the multiplier
                    ptsPoss += Convert.ToDouble(data.Rows[i].Cells[2].Value) * Convert.ToDouble(data.Rows[i].Cells[1].Value);
                    score += Convert.ToDouble(data.Rows[i].Cells[3].Value) * Convert.ToDouble(data.Rows[i].Cells[1].Value);
                }
                else {
                    //Update without the multiplier (multiplier of 1)
                    ptsPoss += Convert.ToDouble(data.Rows[i].Cells[2].Value);
                    score += Convert.ToDouble(data.Rows[i].Cells[3].Value);
                }
            }
            if (ptsPoss != 0 ) { 
                //Update the final scores
                labelPtsPoss.Text = ptsPoss.ToString();
                labelScore.Text = score.ToString();
                labelPercent.Text = (score / ptsPoss * 100).ToString() + "%";
            }
        }

        private void validateEntry(object sender, DataGridViewCellEventArgs e) {
            DataGridView data = (DataGridView)sender;
            //Check if the user is in any other column other than the first
            if (data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) {
                if (e.ColumnIndex != 0)
                {
                    //Check if the user is entering anything other than 0-9 or a "."
                    if (System.Text.RegularExpressions.Regex.IsMatch(data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "[^0-9.]"))
                    {
                        //Delete the cell
                        data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        bool rowIsEmpty = true;
                        //Check to see if the row is empty, if it is delete the row, otherwise don't
                        foreach (DataGridViewCell cell in data.Rows[e.RowIndex].Cells)
                        {
                            if (cell.Value != null)
                            {
                                rowIsEmpty = false;
                                break;
                            }
                        }
                        if (rowIsEmpty)
                        {
                            data.Rows.Remove(data.Rows[e.RowIndex]);
                        }
                        MessageBox.Show("Please enter only numbers.");
                    }
                }
            }     
        }

        private void calculatePercentage(object sender, DataGridViewCellEventArgs e) { 
            DataGridView data = (DataGridView)sender;
            //Check if ptsposs or score isn't empty
            if (data.Rows[e.RowIndex].Cells[2].Value != null && data.Rows[e.RowIndex].Cells[3].Value != null)
            {
                //If the ptsposs is not zero, update the table (this prevents division by 0)
                if (Convert.ToDouble(data.Rows[e.RowIndex].Cells[2].Value) != 0) {
                    data.Rows[e.RowIndex].Cells[4].Value = Convert.ToDouble(data.Rows[e.RowIndex].Cells[3].Value) / Convert.ToDouble(data.Rows[e.RowIndex].Cells[2].Value) * 100;
                }
            }
        }
    }
}
