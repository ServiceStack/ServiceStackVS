namespace ServiceStackVS.Wizards
{
    partial class NodeJsRequiredForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeJsRequiredForm));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.installedItems = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 48);
            this.label1.TabIndex = 2;
            this.label1.Text = "This template requires NodeJS installed, click install below to download NodeJS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // installedItems
            // 
            this.installedItems.FormattingEnabled = true;
            this.installedItems.Items.AddRange(new object[] {
            "Grunt Installed...",
            "Gulp Installed...",
            "Bower Installed..."});
            this.installedItems.Location = new System.Drawing.Point(17, 60);
            this.installedItems.Name = "installedItems";
            this.installedItems.Size = new System.Drawing.Size(488, 49);
            this.installedItems.TabIndex = 3;
            // 
            // NodeJsRequiredForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 230);
            this.Controls.Add(this.installedItems);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(521, 155);
            this.Name = "NodeJsRequiredForm";
            this.Text = "NodeJS Installation Required";
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox installedItems;
    }
}