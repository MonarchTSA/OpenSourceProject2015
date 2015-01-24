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
    //TODO: Prevent user from entering a negative value in the datagridview
    public partial class FormMain : Form {

        SchoolClass SchoolClass = new SchoolClass("everything");
        List<SchoolClass> Classes = new List<SchoolClass>();

        public FormMain()
        {
            InitializeComponent();
        }

        //This method is fired once the user is done editing a cell
        private void OnEndCellEdit(object sender, DataGridViewCellEventArgs e)
       { 
            //Validate entry
            if (ValidateEntry(e))
            {
                //Add the data on the datagridview to to the current category
                StoreData();

                //Update assigment percentage
                UpdateAssignmentPercentages(e);

                //Update totals
                UpdateTotals();

                //Update grade
                UpdateGrade();
            }
        }

        //This event is fired when the user selects a category from the category selection spinner
        private void OnCategoryChange(object sender, EventArgs e)
        {
            //If the option selected is the first index, a new category is created.
            if (comboBoxCategory.SelectedIndex == 0)
            {
                //Deselect text
                comboBoxCategory.SelectedIndex = -1;
                //Open up category selection form
                FormEnterCategory formEnterCatgory = new FormEnterCategory(100);
                if (formEnterCatgory.ShowDialog() == DialogResult.OK)
                {
                    //Create the category
                    Category category = new Category(formEnterCatgory.data[0].ToString(), Convert.ToDouble(formEnterCatgory.data[1]));

                    //Add it to the categoryList
                    SchoolClass.CategoryList.Add(category);

                    //Add it to the comboBoxCategory
                    comboBoxCategory.Items.Add(category.Name);

                    //Set the currentCategoryIndex to the last index
                    SchoolClass.CurrentCategoryIndex = SchoolClass.CategoryList.Count - 1;
                    if (dataGridView.Enabled == false)
                    {
                        dataGridView.Enabled = true;
                    }

                    //Set the comboBox text to the new currentCategory's name
                    comboBoxCategory.Text = SchoolClass.CurrentCategory.Name;

                    //Load the data
                    LoadData();

                    //Store the data
                    StoreData();
                    
                    //Update the totals
                    UpdateTotals();
                    
                    //Update the grade
                    UpdateGrade();
                
                }
            }

            //Else, set the currentCategoryIndex to the index of the selected option minus one
            else
            {
                SchoolClass.CurrentCategoryIndex = comboBoxCategory.SelectedIndex - 1;
                
                //Load the data
                LoadData();

                //Store the data
                StoreData();

                //Update the totals
                UpdateTotals();

                //Update the grade
                UpdateGrade();
            }




            
        }

        private void LoadData()
        {
            //Update the DataGridView
            dataGridView.Rows.Clear();
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            for (int i = 0; i < SchoolClass.CurrentCategory.AssignmentList.Count; i++)
            {
                //Create a DataGridView row
                DataGridViewRow row = (DataGridViewRow)dataGridView.Rows[0].Clone();
                for (int j = 0; j < 5; j++)
                {
                    //Set the value of each cell equal to 
                    row.Cells[j].Value = SchoolClass.CurrentCategory.AssignmentList[i].getFromIndex(j);
                }
                rows.Add(row);
            }

            //Add all the rows
            for (int i = 0; i < rows.Count; i++)
            {
                dataGridView.Rows.Add(rows[i]);
            }
        }

        //This method take the data from the datagrid view and stores it in schoolclass.currentcategory.assignmentlist
        private void StoreData()
        {
            SchoolClass.CurrentCategory.AssignmentList.Clear();
            //For each row (assignment) in the category
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (!dataGridView.Rows[i].IsNewRow)
                {
                    //Add an assignment to to the current category's assignment list that has the value of the current row
                    SchoolClass.CurrentCategory.AssignmentList.Add(new Assignment());
                    for (int j = 0; j < 5; j++)
                    {
                        try
                        {
                            if (j == 0)
                            {
                                SchoolClass.CurrentCategory.AssignmentList[i].setFromIndex((string)dataGridView.Rows[i].Cells[j].Value, j);
                            }
                            else if (j != 4)
                            {
                                if (dataGridView.Rows[i].Cells[j].Value == null)
                                {
                                    if (j == 1)
                                    {
                                        SchoolClass.CurrentCategory.AssignmentList[i].setFromIndex(1, j);
                                    }
                                    else
                                    {
                                        SchoolClass.CurrentCategory.AssignmentList[i].setFromIndex(-1, j);
                                    }
                                }
                                else
                                {
                                    SchoolClass.CurrentCategory.AssignmentList[i].setFromIndex(Convert.ToDouble(dataGridView.Rows[i].Cells[j].Value), j);
                                }
                            }
                        }
                        catch (NullReferenceException ex) { }
                    }
                }
            }
        }

        //Validates entry, if not valid deletes the cell and shows a message box
        private bool ValidateEntry(DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (e.ColumnIndex != 0)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "[^0-9.]"))
                    {
                        //Delete the cell
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        bool rowIsEmpty = true;
                        //Check to see if the row is empty, if it is delete the row, otherwise don't
                        foreach (DataGridViewCell cell in dataGridView.Rows[e.RowIndex].Cells)
                        {
                            if (cell.Value != null)
                            {
                                rowIsEmpty = false;
                                break;
                            }
                        }
                        if (rowIsEmpty)
                        {
                            dataGridView.Rows.RemoveAt(e.RowIndex);
                        }
                        MessageBox.Show("Please enter only numbers.");
                        return false;
                    }
                }
            }
            return true;
        }

        //Updates the percentage in the datagridview
        private void UpdateAssignmentPercentages(DataGridViewCellEventArgs e)
        {
            if (!double.IsNaN(SchoolClass.CurrentCategory.AssignmentList[e.RowIndex].Percent) &&
                SchoolClass.CurrentCategory.AssignmentList[e.RowIndex].Score != -1)
            {
                dataGridView.Rows[e.RowIndex].Cells[4].Value = SchoolClass.CurrentCategory.AssignmentList[e.RowIndex].Percent;
            }
        }

        //TODO: seperate this into three methods one for ptsPoss, one for score, and one for percent
        //Updates the totals at the bottom
        private void UpdateTotals()
        {
            bool hasScores = false;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    hasScores = true;
                    break;
                }
            }
            if (hasScores)
            {
                labelPtsPoss.Text = SchoolClass.CurrentCategory.PtsPoss.ToString();
                labelScore.Text = SchoolClass.CurrentCategory.Score.ToString();
                labelPercent.Text = SchoolClass.CurrentCategory.Percent.ToString();
            }
            else
            {
                labelPtsPoss.Text = "";
                labelScore.Text = "";
                labelPercent.Text = "";
            }
            groupBoxTotals.Text = SchoolClass.CurrentCategory.Name + " Totals (" + SchoolClass.CurrentCategory.Weight + "%)";
        }

        //This method is fired when a user deletes a row
        private void OnDeleteRow(object sender, DataGridViewRowEventArgs e)
        {
            StoreData();
            UpdateTotals();
            UpdateGrade();
        }

        //This method is fired when the datagridview is clicked
        private void OnClick(object sender, EventArgs e)
        {
            var p = PointToClient(Cursor.Position);
            var c = GetChildAtPoint(p);
            if (c != null && c.Enabled == false)
            {
                MessageBox.Show("No category is created. Please create one first");
            }
        }

        //This method updates the grade groupbox text
        private void UpdateGrade()
        {
            if (!double.IsNaN(SchoolClass.Percent))
            {
                labelLetterGrade.Text = "" + SchoolClass.LetterGrade;
                labelGrade.Text = SchoolClass.Percent + "%";
            }
            else
            {
                labelLetterGrade.Text = "";
                labelGrade.Text = "";
            }
        }
    }
}