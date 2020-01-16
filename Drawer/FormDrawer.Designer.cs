namespace Drawer
{
    partial class FormDrawer
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
            this.Panel = new System.Windows.Forms.Panel();
            Graph = new System.Windows.Forms.PictureBox();
            this.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(Graph)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.AutoScroll = true;
            this.Panel.Controls.Add(Graph);
            this.Panel.Location = new System.Drawing.Point(12, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(819, 465);
            this.Panel.TabIndex = 0;
            // 
            // Graph
            // 
            Graph.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Graph.Location = new System.Drawing.Point(0, 0);
            Graph.Name = "Graph";
            Graph.Size = new System.Drawing.Size(874, 460);
            Graph.TabIndex = 0;
            Graph.TabStop = false;
            // 
            // FormDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(843, 489);
            this.Controls.Add(this.Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormDrawer";
            this.Text = "FormDrawer";
            this.Load += new System.EventHandler(this.FormDrawer_Load);
            this.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(Graph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private static System.Windows.Forms.PictureBox Graph;
    }
}