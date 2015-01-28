using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceProject 
{
    public partial class FormMain : Form 
    {
        //This list stores all the classes
        public List<SchoolClass> ClassList { get; set; }

        //This is the current class index (used in currentClass)
        public int CurrentClassIndex { get; set; }

        //Return ClassList[CurrentClassIndex]
        public SchoolClass CurrentClass
        {
            get
            {
                return ClassList[CurrentClassIndex];
            }
        }

        //This is the current file name.
        public string CurrentFileName { get; set; }

        //Constructor
        public FormMain()
        {
            InitializeComponent();
            ClassList = new List<SchoolClass>();
            CurrentFileName = "";
        }

        //This method is fired once the user is done editing a cell
        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
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
                if (CurrentClass.remainingWeight == 0)
                {
                    comboBoxCategory.Text = CurrentClass.CurrentCategory.Name;
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("Class is full!");
                }
                else
                {
                    //Deselect text
                    comboBoxCategory.SelectedIndex = -1;
                    //Open up category selection form
                    FormEnterCategory formEnterCatgory = new FormEnterCategory(CurrentClass.remainingWeight);
                    if (formEnterCatgory.ShowDialog() == DialogResult.OK)
                    {
                        //Create the category
                        Category category = new Category(formEnterCatgory.data[0].ToString(), Convert.ToInt32(formEnterCatgory.data[1]));
                        CurrentClass.remainingWeight -= Convert.ToInt32(formEnterCatgory.data[1]);

                        //Add it to the categoryList
                        CurrentClass.CategoryList.Add(category);

                        //Add it to the comboBoxCategory
                        comboBoxCategory.Items.Add(category.Name);

                        //Set the currentCategoryIndex to the last index
                        CurrentClass.CurrentCategoryIndex = CurrentClass.CategoryList.Count - 1;
                        if (dataGridView.Enabled == false)
                        {
                            dataGridView.Enabled = true;
                        }

                        //Set the comboBox text to the new currentCategory's name
                        comboBoxCategory.Text = CurrentClass.CurrentCategory.Name;

                        //Load the data
                        LoadData();

                        //Store the data
                        StoreData();

                        //Update the totals
                        UpdateTotals();

                        //Update the grade
                        UpdateGrade();
                    }
                    else if (CurrentClass.CategoryList.Count != 0)
                    {
                        comboBoxCategory.SelectedIndex = comboBoxCategory.Items.IndexOf(CurrentClass.CurrentCategory.Name);
                    }
                }
                
            }

            //Else, set the currentCategoryIndex to the index of the selected option minus one
            else
            {
                CurrentClass.CurrentCategoryIndex = comboBoxCategory.SelectedIndex - 1;
                
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
        
        //This method is upon changing a class
        private void OnClassChange(object sender, EventArgs e)
        {
            //If the option selected is the first index, a new category is created.
            if (comboBoxClass.SelectedIndex == 0)
            {
                //Deselect text
                comboBoxClass.SelectedIndex = -1;
                //Open up category selection form
                FormEnterClass formEnterClass = new FormEnterClass();
                if (formEnterClass.ShowDialog() == DialogResult.OK)
                {
                    //Create the class
                    SchoolClass schoolClass = new SchoolClass(formEnterClass.name);

                    //Add it to the classList
                    ClassList.Add(schoolClass);

                    //Add it to the comboBoxClass
                    comboBoxClass.Items.Add(schoolClass.Name);

                    //Set the currentClassIndex to the last index
                    CurrentClassIndex = ClassList.Count - 1;

                    //Enable the comboBoxCategory if not enabled
                    if (comboBoxCategory.Enabled == false)
                    {
                        comboBoxCategory.Enabled = true;
                    }

                    //Set the combobox text to the newly created class
                    comboBoxClass.Text = CurrentClass.Name;

                    //Update the groupboxgrade's text to the current class's name
                    groupBoxGrade.Text = CurrentClass.Name + " Grade"; 

                    if (ClassList.Count != 1)
                    {
                        //Clear the rows
                        dataGridView.Rows.Clear();

                        //Reset the text
                        groupBoxTotals.Text = "Category Totals (%)";
                        labelLetterGrade.Text = "";
                        labelGrade.Text = "";
                        labelPtsPoss.Text = "";
                        labelScore.Text = "";
                        labelPercent.Text = "";
                        
                        //Update the category list
                        UpdateCategoryList();

                        //Disable the datagriview
                        dataGridView.Enabled = false;
                    }
                }
            }

            //Else, set the currentClassIndex to the index of the selected option minus one
            else
            {
                //Update the currentclassindex to the last index
                CurrentClassIndex = comboBoxClass.SelectedIndex - 1;

                //Update the groupboxgrade's text to the current class's name
                groupBoxGrade.Text = CurrentClass.Name + " Grade"; 

                if (CurrentClass.CategoryList.Count != 0)
                {
                    //Update the combobox text to the current category's text
                    comboBoxCategory.Text = CurrentClass.CurrentCategory.Name;

                    //Load the data
                    LoadData();

                    //Store the data
                    StoreData();

                    //Update the totals
                    UpdateTotals();

                    //Update the grade
                    UpdateGrade();

                    //Update the category list
                    UpdateCategoryList();
                }
             }
        }   

        //This method take the data from the list and puts it into the datagridview
        private void LoadData()
        {
            //Update the DataGridView
            dataGridView.Rows.Clear();
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            for (int i = 0; i < CurrentClass.CurrentCategory.AssignmentList.Count; i++)
            {
                //Create a DataGridView row
                DataGridViewRow row = (DataGridViewRow)dataGridView.Rows[0].Clone();
                for (int j = 0; j < 5; j++)
                {
                    if (j == 1)
                    {
                        row.Cells[j].Value = CurrentClass.CurrentCategory.AssignmentList[i].getFromIndex(j);
                    }
                    //Set the value of each cell equal to 
                    row.Cells[j].Value = CurrentClass.CurrentCategory.AssignmentList[i].getFromIndex(j);
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
            CurrentClass.CurrentCategory.AssignmentList.Clear();
            //For each row (assignment) in the category
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (!dataGridView.Rows[i].IsNewRow)
                {
                    //Add an assignment to to the current category's assignment list that has the value of the current row
                    CurrentClass.CurrentCategory.AssignmentList.Add(new Assignment());
                    for (int j = 0; j < 5; j++)
                    {
                        try
                        {
                            //If the current cell is not in the first column
                            if (j == 0)
                            {
                                CurrentClass.CurrentCategory.AssignmentList[i].setFromIndex((string)dataGridView.Rows[i].Cells[j].Value, j);
                            }
                            else
                            {
                                //If the cell is null
                                if (dataGridView.Rows[i].Cells[j].Value == null)
                                {
                                    if (j == 1)
                                    {
                                        CurrentClass.CurrentCategory.AssignmentList[i].setFromIndex(1, j);
                                    }
                                    else
                                    {
                                        CurrentClass.CurrentCategory.AssignmentList[i].setFromIndex(-1, j);
                                    }
                                }
                                else
                                {
                                    CurrentClass.CurrentCategory.AssignmentList[i].setFromIndex(Convert.ToDouble(dataGridView.Rows[i].Cells[j].Value), j);
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
                        //Store the data
                        StoreData();
                        //Update the assignment pecent
                        UpdateAssignmentPercentages(e);
                        UpdateTotals();
                        UpdateGrade();
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
                            dataGridView.CancelEdit();
                            dataGridView.Rows.Remove(dataGridView.Rows[e.RowIndex]);
                        }
                        SystemSounds.Asterisk.Play();
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
            if (!double.IsNaN(CurrentClass.CurrentCategory.AssignmentList[e.RowIndex].Percent) &&
                CurrentClass.CurrentCategory.AssignmentList[e.RowIndex].Score != -1 &&
                !double.IsInfinity(CurrentClass.CurrentCategory.AssignmentList[e.RowIndex].Percent))
            {
                dataGridView.Rows[e.RowIndex].Cells[4].Value = Math.Round(CurrentClass.CurrentCategory.AssignmentList[e.RowIndex].Percent, 2);
            }
            else
            {
                dataGridView.Rows[e.RowIndex].Cells[4].Value = null;
            }
        }

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
                labelPtsPoss.Text = CurrentClass.CurrentCategory.PtsPoss.ToString();
                labelScore.Text = CurrentClass.CurrentCategory.Score.ToString();
                labelPercent.Text = Math.Round(CurrentClass.CurrentCategory.Percent, 2).ToString();
            }
            else
            {
                labelPtsPoss.Text = "";
                labelScore.Text = "";
                labelPercent.Text = "";
            }
            groupBoxTotals.Text = CurrentClass.CurrentCategory.Name + " Totals (" + CurrentClass.CurrentCategory.Weight + "%)";
        }

        //This method is fired when a user deletes a row
        private void OnDeleteRow(object sender, DataGridViewRowEventArgs e)
        {
            StoreData();
            UpdateTotals();
            UpdateGrade();
        }

        //This method is fired when a user clicks anywhere on the form (shows message box upon clicking disabled components)
        private void OnClick(object sender, EventArgs e)
        {
            var p = PointToClient(Cursor.Position);
            var c = GetChildAtPoint(p);
            if (c != null && c.Enabled == false)
            {
                if (ClassList.Count == 0)
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("No class is created. Please create one first");
                }
                else if (CurrentClass.CategoryList.Count == 0)
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("No category is created. Please create one first");
                }
            }
        }

        //This method updates the grade groupbox text
        private void UpdateGrade()
        {
            if (!double.IsNaN(CurrentClass.Percent) && !double.IsInfinity(CurrentClass.Percent))
            {
                labelLetterGrade.Text = "" + CurrentClass.LetterGrade;
                labelGrade.Text = Math.Round(CurrentClass.Percent, 2) + "%";
            }
            else
            {
                labelLetterGrade.Text = "";
                labelGrade.Text = "";
            }
        }

        //This method is used to update the category list when changing classes
        private void UpdateCategoryList()
        {
            //Clear the combobox
            comboBoxCategory.Items.Clear();
            
            //If there are no categories add the create new category option only
            if (CurrentClass.CategoryList.Count == 0)
            {
                comboBoxCategory.Items.Add("Create new category");
            }
            //Else add add the create new category option and the categories
            else
            {
                comboBoxCategory.Items.Add("Create new category");
                for (int i = 0; i < CurrentClass.CategoryList.Count; i++)
                {
                    //add the name of the current index
                    comboBoxCategory.Items.Add(CurrentClass.CategoryList[i].Name);
                }
            }
            comboBoxCategory.SelectedIndex = comboBoxCategory.Items.IndexOf(CurrentClass.Name);
        }

        //This method is used to update the class list when loading classes
        private void UpdateClassList()
        {
            //Clear the combobox
            comboBoxClass.Items.Clear();

            //If there are no classes add the create new class option only
            if (ClassList.Count == 0)
            {
                comboBoxClass.Items.Add("Create new class");
            }
            //Else add add the create new class option and the classes
            else
            {
                comboBoxClass.Items.Add("Create new class");
                for (int i = 0; i < ClassList.Count; i++)
                {
                    //add the name of the current index
                    comboBoxClass.Items.Add(ClassList[i].Name);
                }
            }
            comboBoxClass.SelectedIndex = CurrentClassIndex + 1;
        }

        //This method is fired when the user clicks on the Save as dialog
        private void OnSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Classes file (*.classes)|*.classes";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                CurrentFileName = sfd.FileName;
                WriteToBinary();
            }
        }
        
        private void OnSave(object sender, EventArgs e)
        {
            if (CurrentFileName == "")
            {
                OnSaveAs(sender, e);
            }
            else
            {
                WriteToBinary();
            }
        }
        //This writes the data to a binary files
        private void WriteToBinary()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrentFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, ClassList);
            stream.Close();
        }

        //This file opens the data from a binary file and laods it
        private void ReadFromBinary()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CurrentFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            ClassList = (List<SchoolClass>)formatter.Deserialize(stream);
            stream.Close();
            LoadData();
            StoreData();
            UpdateTotals();
            UpdateGrade();
            UpdateCategoryList();
            UpdateClassList();
            dataGridView.Enabled = true;
            comboBoxCategory.Enabled = true;
        }

        //This event is fired when the open dialog is selected
        private void OnFileOpen(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Classes file (*.classes)|*.classes";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CurrentFileName = ofd.FileName;
                ReadFromBinary();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}