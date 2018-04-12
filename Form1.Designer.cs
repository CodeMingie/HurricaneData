namespace HurDatForms
{
    partial class Form1
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
            this.gridHur = new System.Windows.Forms.DataGridView();
            this.gridHurDetail = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.stateDropdown = new System.Windows.Forms.ComboBox();
            this.minYearText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.recordText = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numText = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridHur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHurDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // gridHur
            // 
            this.gridHur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHur.Location = new System.Drawing.Point(12, 81);
            this.gridHur.Name = "gridHur";
            this.gridHur.Size = new System.Drawing.Size(302, 397);
            this.gridHur.TabIndex = 0;
            this.gridHur.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridHur_CellClick);
            // 
            // gridHurDetail
            // 
            this.gridHurDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHurDetail.Location = new System.Drawing.Point(320, 81);
            this.gridHurDetail.Name = "gridHurDetail";
            this.gridHurDetail.Size = new System.Drawing.Size(434, 397);
            this.gridHurDetail.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(588, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Filter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.filter_Click);
            // 
            // stateDropdown
            // 
            this.stateDropdown.FormattingEnabled = true;
            this.stateDropdown.Location = new System.Drawing.Point(380, 13);
            this.stateDropdown.Name = "stateDropdown";
            this.stateDropdown.Size = new System.Drawing.Size(121, 21);
            this.stateDropdown.TabIndex = 3;
            // 
            // minYearText
            // 
            this.minYearText.Location = new System.Drawing.Point(220, 13);
            this.minYearText.Name = "minYearText";
            this.minYearText.Size = new System.Drawing.Size(100, 20);
            this.minYearText.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Min Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "State";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Record Id";
            // 
            // recordText
            // 
            this.recordText.Location = new System.Drawing.Point(72, 12);
            this.recordText.Name = "recordText";
            this.recordText.Size = new System.Drawing.Size(74, 20);
            this.recordText.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(507, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Load Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.loadData_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "# Records: ";
            // 
            // numText
            // 
            this.numText.AutoSize = true;
            this.numText.Location = new System.Drawing.Point(84, 49);
            this.numText.Name = "numText";
            this.numText.Size = new System.Drawing.Size(13, 13);
            this.numText.TabIndex = 11;
            this.numText.Text = "0";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(669, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 22);
            this.button3.TabIndex = 12;
            this.button3.Text = "Report";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.report_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 490);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.numText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.recordText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minYearText);
            this.Controls.Add(this.stateDropdown);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridHurDetail);
            this.Controls.Add(this.gridHur);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridHur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHurDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridHur;
        private System.Windows.Forms.DataGridView gridHurDetail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox stateDropdown;
        private System.Windows.Forms.TextBox minYearText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox recordText;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label numText;
        private System.Windows.Forms.Button button3;
    }
}

