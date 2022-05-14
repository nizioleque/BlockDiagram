namespace FormsPunktowane
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.blok_operacyjny = new System.Windows.Forms.RadioButton();
            this.blok_decyzyjny = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.blockTextBox = new System.Windows.Forms.TextBox();
            this.blok_start = new System.Windows.Forms.RadioButton();
            this.link_button = new System.Windows.Forms.RadioButton();
            this.blok_stop = new System.Windows.Forms.RadioButton();
            this.trash_button = new System.Windows.Forms.RadioButton();
            this.canvasParent = new System.Windows.Forms.Panel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.canvasParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.canvasParent, 0, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.saveButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.loadButton, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            resources.ApplyResources(this.loadButton, "loadButton");
            this.loadButton.Name = "loadButton";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.button6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button7, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.blok_operacyjny, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.blok_decyzyjny, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.blockTextBox, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.blok_start, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.link_button, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.blok_stop, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.trash_button, 2, 1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // blok_operacyjny
            // 
            resources.ApplyResources(this.blok_operacyjny, "blok_operacyjny");
            this.blok_operacyjny.Checked = true;
            this.blok_operacyjny.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.blok_operacyjny.Image = global::FormsPunktowane.Properties.Resources.oper;
            this.blok_operacyjny.Name = "blok_operacyjny";
            this.blok_operacyjny.TabStop = true;
            this.blok_operacyjny.UseVisualStyleBackColor = true;
            // 
            // blok_decyzyjny
            // 
            resources.ApplyResources(this.blok_decyzyjny, "blok_decyzyjny");
            this.blok_decyzyjny.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.blok_decyzyjny.Image = global::FormsPunktowane.Properties.Resources.decis;
            this.blok_decyzyjny.Name = "blok_decyzyjny";
            this.blok_decyzyjny.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.tableLayoutPanel4.SetColumnSpan(this.label1, 3);
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // blockTextBox
            // 
            resources.ApplyResources(this.blockTextBox, "blockTextBox");
            this.tableLayoutPanel4.SetColumnSpan(this.blockTextBox, 3);
            this.blockTextBox.Name = "blockTextBox";
            this.blockTextBox.TextChanged += new System.EventHandler(this.blockTextBox_TextChanged);
            // 
            // blok_start
            // 
            resources.ApplyResources(this.blok_start, "blok_start");
            this.blok_start.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.blok_start.Image = global::FormsPunktowane.Properties.Resources.start;
            this.blok_start.Name = "blok_start";
            this.blok_start.TabStop = true;
            this.blok_start.UseVisualStyleBackColor = true;
            // 
            // link_button
            // 
            resources.ApplyResources(this.link_button, "link_button");
            this.link_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.link_button.Image = global::FormsPunktowane.Properties.Resources.link;
            this.link_button.Name = "link_button";
            this.link_button.TabStop = true;
            this.link_button.UseVisualStyleBackColor = true;
            this.link_button.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // blok_stop
            // 
            resources.ApplyResources(this.blok_stop, "blok_stop");
            this.blok_stop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.blok_stop.Image = global::FormsPunktowane.Properties.Resources.stop;
            this.blok_stop.Name = "blok_stop";
            this.blok_stop.TabStop = true;
            this.blok_stop.UseVisualStyleBackColor = true;
            // 
            // trash_button
            // 
            resources.ApplyResources(this.trash_button, "trash_button");
            this.trash_button.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.trash_button.Image = global::FormsPunktowane.Properties.Resources.trash;
            this.trash_button.Name = "trash_button";
            this.trash_button.TabStop = true;
            this.trash_button.UseVisualStyleBackColor = true;
            // 
            // canvasParent
            // 
            resources.ApplyResources(this.canvasParent, "canvasParent");
            this.canvasParent.Controls.Add(this.canvas);
            this.canvasParent.Name = "canvasParent";
            this.tableLayoutPanel1.SetRowSpan(this.canvasParent, 3);
            // 
            // canvas
            // 
            resources.ApplyResources(this.canvas, "canvas");
            this.canvas.Name = "canvas";
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.canvasParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel canvasParent;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.RadioButton blok_decyzyjny;
        private System.Windows.Forms.RadioButton blok_operacyjny;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton blok_start;
        private System.Windows.Forms.RadioButton link_button;
        private System.Windows.Forms.RadioButton blok_stop;
        private System.Windows.Forms.RadioButton trash_button;
        private System.Windows.Forms.TextBox blockTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
    }
}
