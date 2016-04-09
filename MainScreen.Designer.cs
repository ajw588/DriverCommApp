namespace DriverCommApp
{
    /// <summary>
    /// Main GUI Screen Window.
    /// </summary>
    partial class MainScreen
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
            this.LBL_TitleCOMStat = new System.Windows.Forms.Label();
            this.LBL_TitleDBStat = new System.Windows.Forms.Label();
            this.BTN_Stop = new System.Windows.Forms.Button();
            this.LBL_titleloop = new System.Windows.Forms.Label();
            this.BTN_Exit = new System.Windows.Forms.Button();
            this.LBL_valueloop = new System.Windows.Forms.Label();
            this.panelDV = new System.Windows.Forms.Panel();
            this.panelDB = new System.Windows.Forms.Panel();
            this.LBL_COMMStat = new System.Windows.Forms.TextBox();
            this.LBL_DBStat = new System.Windows.Forms.TextBox();
            this.HistoricsTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HistoricsTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LBL_TitleCOMStat
            // 
            this.LBL_TitleCOMStat.AutoSize = true;
            this.LBL_TitleCOMStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_TitleCOMStat.Location = new System.Drawing.Point(263, 6);
            this.LBL_TitleCOMStat.Name = "LBL_TitleCOMStat";
            this.LBL_TitleCOMStat.Size = new System.Drawing.Size(109, 20);
            this.LBL_TitleCOMStat.TabIndex = 0;
            this.LBL_TitleCOMStat.Text = "Drivers Status";
            // 
            // LBL_TitleDBStat
            // 
            this.LBL_TitleDBStat.AutoSize = true;
            this.LBL_TitleDBStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_TitleDBStat.Location = new System.Drawing.Point(12, 6);
            this.LBL_TitleDBStat.Name = "LBL_TitleDBStat";
            this.LBL_TitleDBStat.Size = new System.Drawing.Size(166, 20);
            this.LBL_TitleDBStat.TabIndex = 0;
            this.LBL_TitleDBStat.Text = "RealTime Data Status";
            this.LBL_TitleDBStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_Stop
            // 
            this.BTN_Stop.Location = new System.Drawing.Point(513, 183);
            this.BTN_Stop.Name = "BTN_Stop";
            this.BTN_Stop.Size = new System.Drawing.Size(75, 23);
            this.BTN_Stop.TabIndex = 1;
            this.BTN_Stop.Text = "Stop";
            this.BTN_Stop.UseVisualStyleBackColor = true;
            this.BTN_Stop.Click += new System.EventHandler(this.BTN_Stop_Click);
            // 
            // LBL_titleloop
            // 
            this.LBL_titleloop.AutoSize = true;
            this.LBL_titleloop.Location = new System.Drawing.Point(12, 180);
            this.LBL_titleloop.Name = "LBL_titleloop";
            this.LBL_titleloop.Size = new System.Drawing.Size(60, 26);
            this.LBL_titleloop.TabIndex = 2;
            this.LBL_titleloop.Text = "Avg Loop \r\nTiming (ms)";
            // 
            // BTN_Exit
            // 
            this.BTN_Exit.Location = new System.Drawing.Point(618, 183);
            this.BTN_Exit.Name = "BTN_Exit";
            this.BTN_Exit.Size = new System.Drawing.Size(75, 23);
            this.BTN_Exit.TabIndex = 3;
            this.BTN_Exit.Text = "Exit";
            this.BTN_Exit.UseVisualStyleBackColor = true;
            this.BTN_Exit.Click += new System.EventHandler(this.BTN_Exit_Click);
            // 
            // LBL_valueloop
            // 
            this.LBL_valueloop.AutoSize = true;
            this.LBL_valueloop.Location = new System.Drawing.Point(78, 188);
            this.LBL_valueloop.Name = "LBL_valueloop";
            this.LBL_valueloop.Size = new System.Drawing.Size(20, 13);
            this.LBL_valueloop.TabIndex = 4;
            this.LBL_valueloop.Text = "ms";
            // 
            // panelDV
            // 
            this.panelDV.BackColor = System.Drawing.Color.Lime;
            this.panelDV.Location = new System.Drawing.Point(433, 6);
            this.panelDV.Name = "panelDV";
            this.panelDV.Size = new System.Drawing.Size(26, 22);
            this.panelDV.TabIndex = 7;
            // 
            // panelDB
            // 
            this.panelDB.BackColor = System.Drawing.Color.Red;
            this.panelDB.Location = new System.Drawing.Point(190, 6);
            this.panelDB.Name = "panelDB";
            this.panelDB.Size = new System.Drawing.Size(26, 22);
            this.panelDB.TabIndex = 8;
            // 
            // LBL_COMMStat
            // 
            this.LBL_COMMStat.AcceptsReturn = true;
            this.LBL_COMMStat.Location = new System.Drawing.Point(250, 35);
            this.LBL_COMMStat.Multiline = true;
            this.LBL_COMMStat.Name = "LBL_COMMStat";
            this.LBL_COMMStat.ReadOnly = true;
            this.LBL_COMMStat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LBL_COMMStat.Size = new System.Drawing.Size(240, 135);
            this.LBL_COMMStat.TabIndex = 9;
            // 
            // LBL_DBStat
            // 
            this.LBL_DBStat.AcceptsReturn = true;
            this.LBL_DBStat.Location = new System.Drawing.Point(5, 35);
            this.LBL_DBStat.Multiline = true;
            this.LBL_DBStat.Name = "LBL_DBStat";
            this.LBL_DBStat.ReadOnly = true;
            this.LBL_DBStat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LBL_DBStat.Size = new System.Drawing.Size(240, 135);
            this.LBL_DBStat.TabIndex = 9;
            // 
            // HistoricsTitle
            // 
            this.HistoricsTitle.AutoSize = true;
            this.HistoricsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HistoricsTitle.Location = new System.Drawing.Point(509, 6);
            this.HistoricsTitle.Name = "HistoricsTitle";
            this.HistoricsTitle.Size = new System.Drawing.Size(121, 20);
            this.HistoricsTitle.TabIndex = 0;
            this.HistoricsTitle.Text = "Historics Status";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lime;
            this.panel1.Location = new System.Drawing.Point(678, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(26, 22);
            this.panel1.TabIndex = 7;
            // 
            // HistoricsTxt
            // 
            this.HistoricsTxt.AcceptsReturn = true;
            this.HistoricsTxt.Location = new System.Drawing.Point(495, 35);
            this.HistoricsTxt.Multiline = true;
            this.HistoricsTxt.Name = "HistoricsTxt";
            this.HistoricsTxt.ReadOnly = true;
            this.HistoricsTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HistoricsTxt.Size = new System.Drawing.Size(240, 135);
            this.HistoricsTxt.TabIndex = 9;
            // 
            // MainScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(739, 212);
            this.Controls.Add(this.LBL_DBStat);
            this.Controls.Add(this.HistoricsTxt);
            this.Controls.Add(this.LBL_COMMStat);
            this.Controls.Add(this.panelDB);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelDV);
            this.Controls.Add(this.LBL_TitleDBStat);
            this.Controls.Add(this.LBL_valueloop);
            this.Controls.Add(this.BTN_Exit);
            this.Controls.Add(this.LBL_titleloop);
            this.Controls.Add(this.BTN_Stop);
            this.Controls.Add(this.HistoricsTitle);
            this.Controls.Add(this.LBL_TitleCOMStat);
            this.Name = "MainScreen";
            this.Text = "XWave SCADA -Communication App-";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LBL_TitleCOMStat;
        private System.Windows.Forms.Label LBL_TitleDBStat;
        private System.Windows.Forms.Button BTN_Stop;
        private System.Windows.Forms.Label LBL_titleloop;
        private System.Windows.Forms.Button BTN_Exit;
        private System.Windows.Forms.Label LBL_valueloop;
        private System.Windows.Forms.Panel panelDV;
        private System.Windows.Forms.Panel panelDB;
        private System.Windows.Forms.TextBox LBL_DBStat;
        private System.Windows.Forms.TextBox LBL_COMMStat;
        private System.Windows.Forms.Label HistoricsTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox HistoricsTxt;
    }
}

