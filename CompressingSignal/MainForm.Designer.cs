namespace CompressingSignal
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.bOpen = new System.Windows.Forms.Button();
            this.chartOriginal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCompressing = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bCompessing = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompressing)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bSave);
            this.splitContainer1.Panel2.Controls.Add(this.bCompessing);
            this.splitContainer1.Panel2.Controls.Add(this.bOpen);
            this.splitContainer1.Size = new System.Drawing.Size(989, 507);
            this.splitContainer1.SplitterDistance = 422;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chartOriginal);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartCompressing);
            this.splitContainer2.Size = new System.Drawing.Size(989, 422);
            this.splitContainer2.SplitterDistance = 219;
            this.splitContainer2.TabIndex = 0;
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOpen.Location = new System.Drawing.Point(61, 15);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(107, 54);
            this.bOpen.TabIndex = 0;
            this.bOpen.Text = "Открыть";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // chartOriginal
            // 
            chartArea1.Name = "ChartArea1";
            this.chartOriginal.ChartAreas.Add(chartArea1);
            this.chartOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartOriginal.Legends.Add(legend1);
            this.chartOriginal.Location = new System.Drawing.Point(0, 0);
            this.chartOriginal.Name = "chartOriginal";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartOriginal.Series.Add(series1);
            this.chartOriginal.Size = new System.Drawing.Size(989, 219);
            this.chartOriginal.TabIndex = 0;
            this.chartOriginal.Text = "OriginalSignal";
            // 
            // chartCompressing
            // 
            chartArea2.Name = "ChartArea1";
            this.chartCompressing.ChartAreas.Add(chartArea2);
            this.chartCompressing.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartCompressing.Legends.Add(legend2);
            this.chartCompressing.Location = new System.Drawing.Point(0, 0);
            this.chartCompressing.Name = "chartCompressing";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartCompressing.Series.Add(series2);
            this.chartCompressing.Size = new System.Drawing.Size(989, 199);
            this.chartCompressing.TabIndex = 0;
            this.chartCompressing.Text = "SignalCompressing";
            // 
            // bCompessing
            // 
            this.bCompessing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bCompessing.Location = new System.Drawing.Point(193, 15);
            this.bCompessing.Name = "bCompessing";
            this.bCompessing.Size = new System.Drawing.Size(108, 54);
            this.bCompessing.TabIndex = 1;
            this.bCompessing.Text = "Сжать";
            this.bCompessing.UseVisualStyleBackColor = true;
            this.bCompessing.Click += new System.EventHandler(this.bCompessing_Click);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSave.Location = new System.Drawing.Point(335, 15);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(105, 54);
            this.bSave.TabIndex = 2;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 507);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "CompressingSignal";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompressing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOriginal;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompressing;
        private System.Windows.Forms.Button bCompessing;
        private System.Windows.Forms.Button bSave;
    }
}

