namespace WinFormsExam.View
{
    partial class StudentsView 
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
            this.StartExamBtn = new System.Windows.Forms.Button();
            this.StudentsList = new System.Windows.Forms.ListView();
            this.IdHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MarkHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // StartExamBtn
            // 
            this.StartExamBtn.Location = new System.Drawing.Point(357, 308);
            this.StartExamBtn.Name = "StartExamBtn";
            this.StartExamBtn.Size = new System.Drawing.Size(75, 23);
            this.StartExamBtn.TabIndex = 1;
            this.StartExamBtn.Text = "Start Exam";
            this.StartExamBtn.UseVisualStyleBackColor = true;
            this.StartExamBtn.Click += new System.EventHandler(this.StartExamBtn_Click);
            // 
            // StudentsList
            // 
            this.StudentsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdHeader,
            this.NameHeader,
            this.MarkHeader});
            this.StudentsList.Location = new System.Drawing.Point(12, 12);
            this.StudentsList.Name = "StudentsList";
            this.StudentsList.Size = new System.Drawing.Size(420, 290);
            this.StudentsList.TabIndex = 2;
            this.StudentsList.UseCompatibleStateImageBehavior = false;
            this.StudentsList.View = System.Windows.Forms.View.Details;
            // 
            // IdHeader
            // 
            this.IdHeader.Text = "ID";
            this.IdHeader.Width = 50;
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Student name";
            this.NameHeader.Width = 290;
            // 
            // MarkHeader
            // 
            this.MarkHeader.Text = "Mark";
            this.MarkHeader.Width = 50;
            // 
            // StudentsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 341);
            this.Controls.Add(this.StudentsList);
            this.Controls.Add(this.StartExamBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StudentsView";
            this.Text = "Exam";
            this.Load += new System.EventHandler(this.StudentsView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartExamBtn;
        private System.Windows.Forms.ListView StudentsList;
        private System.Windows.Forms.ColumnHeader IdHeader;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader MarkHeader;
    }
}