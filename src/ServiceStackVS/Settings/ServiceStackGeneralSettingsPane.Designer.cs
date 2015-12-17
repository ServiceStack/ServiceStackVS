namespace ServiceStackVS.Settings
{
    partial class ServiceStackGeneralSettingsPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.optOutStatsChkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // optOutStatsChkBox
            // 
            this.optOutStatsChkBox.AutoSize = true;
            this.optOutStatsChkBox.Location = new System.Drawing.Point(14, 14);
            this.optOutStatsChkBox.Name = "optOutStatsChkBox";
            this.optOutStatsChkBox.Size = new System.Drawing.Size(265, 17);
            this.optOutStatsChkBox.TabIndex = 0;
            this.optOutStatsChkBox.Text = "Opt out of anonymous collection of usage statistics";
            this.optOutStatsChkBox.UseVisualStyleBackColor = true;
            this.optOutStatsChkBox.CheckedChanged += new System.EventHandler(this.optOutStatsChkBox_CheckedChanged);
            // 
            // ServiceStackGeneralSettingsPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optOutStatsChkBox);
            this.Name = "ServiceStackGeneralSettingsPane";
            this.Size = new System.Drawing.Size(300, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox optOutStatsChkBox;
    }
}
