namespace TestApp1
{
    partial class TestApp1Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ColorButton = new System.Windows.Forms.Button();
            this.ColorsListBox = new System.Windows.Forms.ListBox();
            this.IgnoredColorsListBox = new System.Windows.Forms.ListBox();
            this.ColorsListBoxLabel = new System.Windows.Forms.Label();
            this.IgnoredColorsListBoxLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ColorButtonLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ColorButton
            // 
            this.ColorButton.Enabled = false;
            this.ColorButton.Location = new System.Drawing.Point(580, 240);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(192, 40);
            this.ColorButton.TabIndex = 1;
            this.ColorButton.Text = "ColorButton";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // ColorsListBox
            // 
            this.ColorsListBox.FormattingEnabled = true;
            this.ColorsListBox.Location = new System.Drawing.Point(580, 30);
            this.ColorsListBox.Name = "ColorsListBox";
            this.ColorsListBox.Size = new System.Drawing.Size(192, 173);
            this.ColorsListBox.TabIndex = 8;
            this.ColorsListBox.Visible = false;
            this.ColorsListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorsList_MouseClick);
            this.ColorsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ColorsList_MouseDoubleClick);
            // 
            // IgnoredColorsListBox
            // 
            this.IgnoredColorsListBox.FormattingEnabled = true;
            this.IgnoredColorsListBox.Location = new System.Drawing.Point(580, 350);
            this.IgnoredColorsListBox.Name = "IgnoredColorsListBox";
            this.IgnoredColorsListBox.Size = new System.Drawing.Size(192, 199);
            this.IgnoredColorsListBox.TabIndex = 9;
            this.IgnoredColorsListBox.Visible = false;
            this.IgnoredColorsListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IgnoredColorsList_MouseClick);
            this.IgnoredColorsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IgnoredColorsList_MouseDoubleClick);
            // 
            // ColorsListBoxLabel
            // 
            this.ColorsListBoxLabel.AutoSize = true;
            this.ColorsListBoxLabel.Location = new System.Drawing.Point(577, 14);
            this.ColorsListBoxLabel.Name = "ColorsListBoxLabel";
            this.ColorsListBoxLabel.Size = new System.Drawing.Size(96, 13);
            this.ColorsListBoxLabel.TabIndex = 10;
            this.ColorsListBoxLabel.Text = "ColorsListBoxLabel";
            this.ColorsListBoxLabel.Visible = false;
            // 
            // IgnoredColorsListBoxLabel
            // 
            this.IgnoredColorsListBoxLabel.AutoSize = true;
            this.IgnoredColorsListBoxLabel.Location = new System.Drawing.Point(577, 334);
            this.IgnoredColorsListBoxLabel.Name = "IgnoredColorsListBoxLabel";
            this.IgnoredColorsListBoxLabel.Size = new System.Drawing.Size(132, 13);
            this.IgnoredColorsListBoxLabel.TabIndex = 11;
            this.IgnoredColorsListBoxLabel.Text = "IgnoredColorsListBoxLabel";
            this.IgnoredColorsListBoxLabel.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BackgroundImage = global::TestApp1.Properties.Resources.kisspng_cursor_pointer_computer_icons_index_finger_hand_responsive_vector_5addb763943a29_6519421715244798436072;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(550, 550);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ColorButtonLabel
            // 
            this.ColorButtonLabel.AutoSize = true;
            this.ColorButtonLabel.Location = new System.Drawing.Point(577, 224);
            this.ColorButtonLabel.Name = "ColorButtonLabel";
            this.ColorButtonLabel.Size = new System.Drawing.Size(88, 13);
            this.ColorButtonLabel.TabIndex = 12;
            this.ColorButtonLabel.Text = "ColorButtonLabel";
            this.ColorButtonLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.ColorButtonLabel);
            this.Controls.Add(this.IgnoredColorsListBoxLabel);
            this.Controls.Add(this.ColorsListBoxLabel);
            this.Controls.Add(this.IgnoredColorsListBox);
            this.Controls.Add(this.ColorsListBox);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Test App 1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.ListBox ColorsListBox;
        private System.Windows.Forms.ListBox IgnoredColorsListBox;
        private System.Windows.Forms.Label ColorsListBoxLabel;
        private System.Windows.Forms.Label IgnoredColorsListBoxLabel;
        private System.Windows.Forms.Label ColorButtonLabel;
    }
}

