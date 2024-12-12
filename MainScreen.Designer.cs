using System;
using System.Drawing;
using System.Windows.Forms;

namespace MadebymeWF
{
    partial class MainScreen
    {
        private Label lblLogo;
        private TextBox txtSearch;
        private Button btnAddAd;
        private FlowLayoutPanel productGrid;
        private Button btnNext;
        private Button btnPrev;

        private void InitializeComponent()
        {
            this.lblLogo = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddAd = new System.Windows.Forms.Button();
            this.productGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(180)))), ((int)(((byte)(71)))));
            this.lblLogo.Location = new System.Drawing.Point(12, 3);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(122, 54);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "МвМ";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(173, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(400, 34);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Пошук";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // btnAddAd
            // 
            this.btnAddAd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(180)))), ((int)(((byte)(71)))));
            this.btnAddAd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddAd.ForeColor = System.Drawing.Color.White;
            this.btnAddAd.Location = new System.Drawing.Point(596, 12);
            this.btnAddAd.Name = "btnAddAd";
            this.btnAddAd.Size = new System.Drawing.Size(180, 40);
            this.btnAddAd.TabIndex = 2;
            this.btnAddAd.Text = "Розмістити оголошення";
            this.btnAddAd.UseVisualStyleBackColor = false;
            this.btnAddAd.Click += new System.EventHandler(this.btnAddAd_Click);
            // 
            // productGrid
            // 
            this.productGrid.AutoScroll = true;
            this.productGrid.Location = new System.Drawing.Point(20, 80);
            this.productGrid.Name = "productGrid";
            this.productGrid.Padding = new System.Windows.Forms.Padding(10);
            this.productGrid.Size = new System.Drawing.Size(700, 400);
            this.productGrid.TabIndex = 3;
            this.productGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.productGrid_Paint);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnNext.Location = new System.Drawing.Point(670, 500);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 30);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "→";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrev.Location = new System.Drawing.Point(20, 500);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(50, 30);
            this.btnPrev.TabIndex = 5;
            this.btnPrev.Text = "←";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // MainScreen
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAddAd);
            this.Controls.Add(this.productGrid);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
