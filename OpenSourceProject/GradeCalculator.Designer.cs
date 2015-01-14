namespace OpenSourceProject
{
    partial class GradeCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.multiplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptsPoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradeCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.divider2 = new System.Windows.Forms.Label();
            this.divider1 = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelPtsPoss = new System.Windows.Forms.Label();
            this.labelPercent = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.editCategory.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.multiplier,
            this.ptsPoss,
            this.score,
            this.percent});
            this.dataGridView1.Location = new System.Drawing.Point(28, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(542, 150);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.onEndCellEdit);
            // 
            // name
            // 
            this.name.Frozen = true;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // multiplier
            // 
            this.multiplier.Frozen = true;
            this.multiplier.HeaderText = "Multiplier";
            this.multiplier.Name = "multiplier";
            // 
            // ptsPoss
            // 
            this.ptsPoss.Frozen = true;
            this.ptsPoss.HeaderText = "PtsPoss";
            this.ptsPoss.Name = "ptsPoss";
            // 
            // score
            // 
            this.score.Frozen = true;
            this.score.HeaderText = "Score";
            this.score.Name = "score";
            // 
            // percent
            // 
            this.percent.Frozen = true;
            this.percent.HeaderText = "Percent";
            this.percent.Name = "percent";
            this.percent.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.plannerToolStripMenuItem,
            this.gradeCalculatorToolStripMenuItem,
            this.calculatorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(620, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // plannerToolStripMenuItem
            // 
            this.plannerToolStripMenuItem.Name = "plannerToolStripMenuItem";
            this.plannerToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.plannerToolStripMenuItem.Text = "Planner";
            // 
            // gradeCalculatorToolStripMenuItem
            // 
            this.gradeCalculatorToolStripMenuItem.Name = "gradeCalculatorToolStripMenuItem";
            this.gradeCalculatorToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.gradeCalculatorToolStripMenuItem.Text = "Grade Calculator";
            // 
            // calculatorToolStripMenuItem
            // 
            this.calculatorToolStripMenuItem.Name = "calculatorToolStripMenuItem";
            this.calculatorToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.calculatorToolStripMenuItem.Text = "Calculator";
            // 
            // editCategory
            // 
            this.editCategory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.editCategory.Name = "editCategory";
            this.editCategory.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.divider2);
            this.groupBox1.Controls.Add(this.divider1);
            this.groupBox1.Controls.Add(this.labelScore);
            this.groupBox1.Controls.Add(this.labelPtsPoss);
            this.groupBox1.Controls.Add(this.labelPercent);
            this.groupBox1.Location = new System.Drawing.Point(271, 247);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 39);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Category Totals (%)";
            // 
            // divider2
            // 
            this.divider2.AutoSize = true;
            this.divider2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.divider2.Location = new System.Drawing.Point(197, 16);
            this.divider2.Name = "divider2";
            this.divider2.Size = new System.Drawing.Size(2, 15);
            this.divider2.TabIndex = 3;
            // 
            // divider1
            // 
            this.divider1.AutoSize = true;
            this.divider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.divider1.Location = new System.Drawing.Point(101, 16);
            this.divider1.Name = "divider1";
            this.divider1.Size = new System.Drawing.Size(2, 15);
            this.divider1.TabIndex = 4;
            // 
            // labelScore
            // 
            this.labelScore.Location = new System.Drawing.Point(101, 16);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(98, 13);
            this.labelScore.TabIndex = 1;
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPtsPoss
            // 
            this.labelPtsPoss.Location = new System.Drawing.Point(6, 16);
            this.labelPtsPoss.Name = "labelPtsPoss";
            this.labelPtsPoss.Size = new System.Drawing.Size(89, 13);
            this.labelPtsPoss.TabIndex = 0;
            this.labelPtsPoss.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPercent
            // 
            this.labelPercent.Location = new System.Drawing.Point(197, 16);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(96, 13);
            this.labelPercent.TabIndex = 2;
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] {
            "Create new category",
            "your mom"});
            this.comboBoxCategory.Location = new System.Drawing.Point(84, 258);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCategory.TabIndex = 4;
            this.comboBoxCategory.TextChanged += new System.EventHandler(this.onCategoryChange);
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(30, 262);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(49, 13);
            this.labelCategory.TabIndex = 5;
            this.labelCategory.Text = "Category";
            // 
            // GradeCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 298);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GradeCalculator";
            this.Text = "Grade Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.editCategory.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn multiplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptsPoss;
        private System.Windows.Forms.DataGridViewTextBoxColumn score;
        private System.Windows.Forms.DataGridViewTextBoxColumn percent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradeCalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculatorToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip editCategory;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label divider2;
        private System.Windows.Forms.Label divider1;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelPtsPoss;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelCategory;
    }
}

