namespace OpenSourceProject
{
    partial class FormEnterClass
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelClassName = new System.Windows.Forms.Label();
            this.textBoxClassName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(77, 74);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(52, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonResult);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(19, 74);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(52, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonResult);
            // 
            // labelClassName
            // 
            this.labelClassName.AutoSize = true;
            this.labelClassName.Location = new System.Drawing.Point(23, 32);
            this.labelClassName.Name = "labelClassName";
            this.labelClassName.Size = new System.Drawing.Size(100, 13);
            this.labelClassName.TabIndex = 7;
            this.labelClassName.Text = "Enter a class name:";
            // 
            // textBoxClassName
            // 
            this.textBoxClassName.Location = new System.Drawing.Point(23, 48);
            this.textBoxClassName.Name = "textBoxClassName";
            this.textBoxClassName.Size = new System.Drawing.Size(100, 20);
            this.textBoxClassName.TabIndex = 6;
            this.textBoxClassName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnter);
            // 
            // FormEnterClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(146, 126);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelClassName);
            this.Controls.Add(this.textBoxClassName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEnterClass";
            this.ShowIcon = false;
            this.Text = "Enter a class";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelClassName;
        private System.Windows.Forms.TextBox textBoxClassName;
    }
}