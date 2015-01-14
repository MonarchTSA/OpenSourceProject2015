using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject
{
    class SchoolClass
    {
        public string name { get; set; }
        public DataGridView data { get; set; }

        public SchoolClass(string name)
        {
            data = new DataGridView();
            this.name = name;
        }

        public char getLetterGrade()
        {
            double percent = score / ptsPoss;
            if (percent == null)
            {
                return ' ';
            }
            else if (percent >= 90d)
            {
                return 'A';
            }
            else if (percent >= 80d)
            {
                return 'B';
            }
            else if (percent >= 70d)
            {
                return 'C';
            }
            else if (percent >= 60d)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }        
        }

        public void calculateTotals(DataGridViewCellEventArgs e, ref Label labelPtsPoss, ref Label labelScore, ref Label labelPercent)
        {
            bool hasScores = false;
            double ptsPoss = 0, score = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i].Cells[3].Value != null)
                {
                    hasScores = true;
                }
                //Check to see if there is multiplier
                if (data.Rows[i].Cells[1].Value != null)
                {
                    //Update with the multiplier
                    ptsPoss += Convert.ToDouble(data.Rows[i].Cells[2].Value) * Convert.ToDouble(data.Rows[i].Cells[1].Value);
                    score += Convert.ToDouble(data.Rows[i].Cells[3].Value) * Convert.ToDouble(data.Rows[i].Cells[1].Value);
                }
                else
                {
                    //Update without the multiplier (multiplier of 1)
                    ptsPoss += Convert.ToDouble(data.Rows[i].Cells[2].Value);
                    score += Convert.ToDouble(data.Rows[i].Cells[3].Value);
                }
            }
            if (ptsPoss != 0 && hasScores)
            {
                //Update the final scores
                labelPtsPoss.Text = ptsPoss.ToString();
                labelScore.Text = score.ToString();
                labelPercent.Text = (score / ptsPoss * 100).ToString() + "%";
            }
        }

        public void validateEntry(DataGridViewCellEventArgs e)
        {
            //Check if the user is in any other column other than the first
            if (data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
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

        public void calculatePercentage(DataGridViewCellEventArgs e)
        {
            //Check if ptsposs or score isn't empty
            if (data.Rows[e.RowIndex].Cells[2].Value != null && data.Rows[e.RowIndex].Cells[3].Value != null)
            {
                //If the ptsposs is not zero, update the table (this prevents division by 0)
                if (Convert.ToDouble(data.Rows[e.RowIndex].Cells[2].Value) != 0)
                {
                    data.Rows[e.RowIndex].Cells[4].Value = Convert.ToDouble(data.Rows[e.RowIndex].Cells[3].Value) / Convert.ToDouble(data.Rows[e.RowIndex].Cells[2].Value) * 100;
                }
            }
        }

        public void onCategoryChange(ComboBox comboBox, EventArgs e)
        {
            if (comboBox.Text == "Create new category")
            {
                MessageBox.Show("changed");

            }
            else
            {
                //load comboBox.Items.indexOf(comboBox.Text)
            }
        }
    }
}
