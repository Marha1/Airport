namespace Airport.Operator
{
    partial class RegForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegForm));
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            txtName = new TextBox();
            txtSurname = new TextBox();
            txtLastname = new TextBox();
            txtPassportNumber = new TextBox();
            dtpBirthDate = new DateTimePicker();
            nudBaggageWeight = new NumericUpDown();
            btnRegister = new Button();
            txtLength = new TextBox();
            txtWidth = new TextBox();
            txtHeight = new TextBox();
            label1 = new Label();
            label2 = new Label();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBaggageWeight).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            toolStrip1.Location = new Point(0, 423);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(438, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(55, 24);
            toolStripButton1.Text = "Назад";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(20, 60);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Имя";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 2;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(20, 100);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Фамилия";
            txtSurname.Size = new Size(200, 27);
            txtSurname.TabIndex = 3;
            // 
            // txtLastname
            // 
            txtLastname.Location = new Point(20, 140);
            txtLastname.Name = "txtLastname";
            txtLastname.PlaceholderText = "Отчество";
            txtLastname.Size = new Size(200, 27);
            txtLastname.TabIndex = 4;
            // 
            // txtPassportNumber
            // 
            txtPassportNumber.Location = new Point(20, 180);
            txtPassportNumber.Name = "txtPassportNumber";
            txtPassportNumber.PlaceholderText = "Номер паспорта";
            txtPassportNumber.Size = new Size(200, 27);
            txtPassportNumber.TabIndex = 5;
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.Location = new Point(20, 220);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(200, 27);
            dtpBirthDate.TabIndex = 6;
            // 
            // nudBaggageWeight
            // 
            nudBaggageWeight.Location = new Point(20, 260);
            nudBaggageWeight.Name = "nudBaggageWeight";
            nudBaggageWeight.Size = new Size(200, 27);
            nudBaggageWeight.TabIndex = 7;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(20, 300);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(200, 30);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Зарегистрировать";
            btnRegister.Click += btnRegister_Click;
            // 
            // txtLength
            // 
            txtLength.Location = new Point(238, 100);
            txtLength.Name = "txtLength";
            txtLength.PlaceholderText = "Длина (см)";
            txtLength.Size = new Size(200, 27);
            txtLength.TabIndex = 9;
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(238, 140);
            txtWidth.Name = "txtWidth";
            txtWidth.PlaceholderText = "Ширина (см)";
            txtWidth.Size = new Size(200, 27);
            txtWidth.TabIndex = 10;
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(238, 180);
            txtHeight.Name = "txtHeight";
            txtHeight.PlaceholderText = "Высота (см)";
            txtHeight.Size = new Size(200, 27);
            txtHeight.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(238, 77);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 12;
            label1.Text = "Багаж:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(238, 267);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 13;
            label2.Text = "Вес груза";
            // 
            // RegForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(438, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtLength);
            Controls.Add(txtWidth);
            Controls.Add(txtHeight);
            Controls.Add(toolStrip1);
            Controls.Add(txtName);
            Controls.Add(txtSurname);
            Controls.Add(txtLastname);
            Controls.Add(txtPassportNumber);
            Controls.Add(dtpBirthDate);
            Controls.Add(nudBaggageWeight);
            Controls.Add(btnRegister);
            Name = "RegForm";
            Text = "RegForm";
            Load += RegForm_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBaggageWeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private TextBox txtName;
        private TextBox txtSurname;
        private TextBox txtLastname;
        private TextBox txtPassportNumber;
        private DateTimePicker dtpBirthDate;
        private NumericUpDown nudBaggageWeight;
        private Button btnRegister;
        private TextBox txtLength;
        private TextBox txtWidth;
        private TextBox txtHeight;
        private Label label1;
        private Label label2;
    }
}
