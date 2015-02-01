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
                if (CurrentClass.RemainingWeight == 0)
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
                    FormEnterCategory formEnterCatgory = new FormEnterCategory(CurrentClass.RemainingWeight);
                    if (formEnterCatgory.ShowDialog() == DialogResult.OK)
                    {
                        //Create the category
                        Category category = new Category(formEnterCatgory.data[0].ToString(), Convert.ToInt32(formEnterCatgory.data[1]));
                        CurrentClass.RemainingWeight -= Convert.ToInt32(formEnterCatgory.data[1]);

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

                        editCategoryToolStripMenuItem.Enabled = true;
                        deleteCategoryToolStripMenuItem.Enabled = true;
                        calculateToolStripMenuItem.Enabled = true;
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

                        saveToolStripMenuItem.Enabled = true;
                        saveAsToolStripMenuItem.Enabled = true;

                    }
                    else
                    {
                        editClassToolStripMenuItem.Enabled = true;
                        deleteClassToolStripMenuItem.Enabled = true;
                        addCategoryToolStripMenu.Enabled = true;
                    }
                }
                else if (ClassList.Count != 0)
                {
                    comboBoxClass.Text = CurrentClass.Name;
                }
            }

            //Else, set the currentClassIndex to the index of the selected option minus one
            else
            {
                //Update the currentclassindex current index - 1
                CurrentClassIndex = comboBoxClass.SelectedIndex - 1;

                //Update the groupboxgrade's text to the current class's name
                groupBoxGrade.Text = CurrentClass.Name + " Grade";

                if (CurrentClass.CategoryList.Count != 0)
                {
                    comboBoxCategory.Items.Clear();
                    comboBoxCategory.Items.Add("Create new category");
                    foreach (Category c in CurrentClass.CategoryList)
                    {
                        comboBoxCategory.Items.Add(c.Name);
                    }
                    //Update the combobox text to the current category's text
                    comboBoxCategory.SelectedIndex = CurrentClass.CurrentCategoryIndex + 1;

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
                    dataGridView.Enabled = true;
                }
                else
                {
                    comboBoxCategory.SelectedIndex = -1;
                    comboBoxCategory.Items.Clear();
                    comboBoxCategory.Items.Add("Create new category");
                    labelLetterGrade.Text = "";
                    labelGrade.Text = "";
                    labelPtsPoss.Text = "";
                    labelScore.Text = "";
                    labelPercent.Text = "";
                    groupBoxTotals.Text = "Category Totals (%)";
                    dataGridView.Rows.Clear();
                    dataGridView.Enabled = false;
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
                            try
                            {
                                dataGridView.Rows.Remove(dataGridView.Rows[e.RowIndex]);
                            }
                            catch
                            {
                                MessageBox.Show("Something went wrong. Try again.", "Error");
                            }
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
                ClearTotals();
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
                    MessageBox.Show("No class is created. Please create one first.");
                }
                else if (CurrentClass.CategoryList.Count == 0)
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("No category is created. Please create one first.");
                }
            }
        }

        //This method updates the grade groupbox text
        private void UpdateGrade()
        {
            bool hasBlankCategory = false;
            foreach (Category c in CurrentClass.CategoryList)
            {
                if (double.IsNaN(c.Percent))
                {
                    hasBlankCategory = true;
                    break;
                }
            }
            if (!double.IsNaN(CurrentClass.Percent) && !double.IsInfinity(CurrentClass.Percent))
            {
                labelLetterGrade.Text = "" + CurrentClass.LetterGrade;
                labelGrade.Text = Math.Round(CurrentClass.Percent, 2) + "%";
            }
            else if (!hasBlankCategory)
            {
                ClearGrade();
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
            try
            {
                ClassList = (List<SchoolClass>)formatter.Deserialize(stream);
                stream.Close();
                if (CurrentClass.CategoryList.Count != 0)
                {
                    LoadData();
                    StoreData();
                    UpdateTotals();
                    UpdateGrade();
                    UpdateCategoryList();
                    UpdateClassList();
                    dataGridView.Enabled = true;
                    comboBoxCategory.Enabled = true;
                    groupBoxGrade.Text = CurrentClass.Name + " Grade";
                }
                else
                {
                    groupBoxGrade.Text = CurrentClass.Name + " Grade";
                    UpdateCategoryList();
                    UpdateClassList();
                    comboBoxCategory.Enabled = true;
                }

            }
            catch
            {
                MessageBox.Show("There was an error loading your file. The file might be corrupted.", "Error");
            }
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
                foreach (SchoolClass sc in ClassList)
                {
                    if (sc.CategoryList.Count != 0)
                    {
                        addCategoryToolStripMenu.Enabled = true;
                        editCategoryToolStripMenuItem.Enabled = true;
                        deleteCategoryToolStripMenuItem.Enabled = true;
                        addClassToolStripMenu.Enabled = true;
                        editClassToolStripMenuItem.Enabled = true;
                        deleteClassToolStripMenuItem.Enabled = true;
                        calculateToolStripMenuItem.Enabled = true;
                        saveAsToolStripMenuItem.Enabled = true;
                        saveToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        addClassToolStripMenu.Enabled = true;
                        editClassToolStripMenuItem.Enabled = true;
                        deleteClassToolStripMenuItem.Enabled = true;
                        saveAsToolStripMenuItem.Enabled = true;
                        saveToolStripMenuItem.Enabled = true;
                        addCategoryToolStripMenu.Enabled = true;
                    }
                }
            }
        }

        private void OnEditClass(object sender, EventArgs e)
        {
            FormEnterClass form = new FormEnterClass(CurrentClass.Name);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int index = comboBoxClass.Items.IndexOf(CurrentClass.Name);
                comboBoxClass.Items.Remove(CurrentClass.Name);
                CurrentClass.Name = form.name;
                comboBoxClass.Items.Insert(index, CurrentClass.Name);
                comboBoxClass.SelectedIndex = index;
                groupBoxGrade.Text = CurrentClass.Name + " Grade";
            }
        }

        private void OnDeleteClass(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? This will also delete the categories inside this class.", "Delete class", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (ClassList.Count == 1)
                {
                    ClearTotals();
                    ClearGrade();
                    groupBoxTotals.Text = "Class Grade";
                    groupBoxGrade.Text = "Category Totals (%)";
                    dataGridView.Enabled = false;
                    dataGridView.Rows.Clear();
                    comboBoxCategory.Enabled = false;
                    comboBoxClass.Items.Remove(CurrentClass.Name);
                    comboBoxCategory.Items.Clear();
                    comboBoxCategory.Items.Add("Create new category");
                    ClassList.Remove(CurrentClass);
                    editClassToolStripMenuItem.Enabled = false;
                    deleteClassToolStripMenuItem.Enabled = false;
                    addCategoryToolStripMenu.Enabled = false;
                    editCategoryToolStripMenuItem.Enabled = false;
                    deleteCategoryToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    saveToolStripMenuItem.Enabled = false;
                }
                else
                {
                    comboBoxClass.Items.Remove(CurrentClass.Name);
                    if (CurrentClassIndex != 0)
                    {
                        --CurrentClassIndex;
                    }
                    comboBoxClass.SelectedIndex = CurrentClassIndex + 1;
                    if (CurrentClass.CategoryList.Count != 0)
                    {
                        UpdateTotals();
                        UpdateGrade();
                        LoadData();
                    }
                    else
                    {
                        ClearTotals();
                        ClearGrade();
                    }
                    groupBoxGrade.Text = CurrentClass.Name + " Grade";
                    groupBoxTotals.Text = "Category Totals (%)";
                    ClassList.RemoveAt(CurrentClassIndex + 1);
                    dataGridView.Rows.Clear();
                    dataGridView.Enabled = false;
                    comboBoxCategory.Items.Clear();
                    comboBoxCategory.Items.Add("Create new category");
                    foreach (Category c in CurrentClass.CategoryList)
                    {
                        comboBoxCategory.Items.Add(c.Name);
                    }
                }
            }
        }

        private void OnEditCategory(object sender, EventArgs e)
        {
            FormEnterCategory form = new FormEnterCategory(CurrentClass.RemainingWeight + CurrentClass.CurrentCategory.Weight, CurrentClass.CurrentCategory.Weight, CurrentClass.CurrentCategory.Name);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int index = comboBoxCategory.Items.IndexOf(CurrentClass.CurrentCategory.Name);
                comboBoxCategory.Items.Remove(CurrentClass.CurrentCategory.Name);
                CurrentClass.CurrentCategory.Name = (string)form.data[0];
                CurrentClass.CurrentCategory.Weight = Convert.ToInt32(form.data[1]);
                comboBoxCategory.Items.Insert(index, CurrentClass.CurrentCategory.Name);
                comboBoxCategory.SelectedIndex = index;
                groupBoxTotals.Text = CurrentClass.CurrentCategory.Name + " Totals (" + CurrentClass.CurrentCategory.Weight + "%)";
                UpdateGrade();
            }
        }

        private void OnDeleteCategory(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete category", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //If user is deleting the last category in 
                if (CurrentClass.CategoryList.Count == 1)
                {
                    ClearTotals();
                    ClearGrade();
                    groupBoxTotals.Text = "Category Totals (%)";
                    dataGridView.Enabled = false;
                    dataGridView.Rows.Clear();
                    comboBoxCategory.Items.Remove(CurrentClass.CurrentCategory.Name);
                    CurrentClass.CategoryList.Remove(CurrentClass.CurrentCategory);
                    editCategoryToolStripMenuItem.Enabled = false;
                    deleteCategoryToolStripMenuItem.Enabled = false;
                    calculateToolStripMenuItem.Enabled = false;
                }
                else
                {
                    comboBoxCategory.Items.Remove(CurrentClass.CurrentCategory.Name);
                    CurrentClass.CategoryList.Remove(CurrentClass.CurrentCategory);
                    if (CurrentClass.CurrentCategoryIndex != 0)
                    {
                        --CurrentClass.CurrentCategoryIndex;
                    }
                    comboBoxCategory.SelectedIndex = CurrentClass.CurrentCategoryIndex + 1;
                    LoadData();
                    StoreData();
                    UpdateTotals();
                    UpdateGrade();
                }
            }
        }

        private void OnAddCategory(object sender, EventArgs e)
        {
            comboBoxCategory.SelectedIndex = 0;
            OnCategoryChange(new object(), new EventArgs());
        }

        private void OnAddClass(object sender, EventArgs e)
        {
            comboBoxClass.SelectedIndex = 0;
            OnClassChange(new object(), new EventArgs());
        }

        private void ClearTotals()
        {
            labelPtsPoss.Text = "";
            labelScore.Text = "";
            labelPercent.Text = "";
        }

        private void ClearGrade()
        {
            labelLetterGrade.Text = "";
            labelGrade.Text = "";
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCalculateGrade form = new FormCalculateGrade(CurrentClass);
            form.ShowDialog();
        }
    }
}