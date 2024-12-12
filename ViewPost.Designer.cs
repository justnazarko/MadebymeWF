using System;
using System.Drawing;
using System.Windows.Forms;

namespace MadebymeWF
{
    partial class ViewPostForm
    {
        private Panel headerPanel;
        private Label lblTitle;
        private PictureBox pbPhoto;
        private Label lblName;
        private Label lblContact;
        private Label lblDescription;
        private Label lblPrice;
        private Button btnBack;

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.lblName = CreateStyledLabel(new Point(360, 80), "Продавець:");
            this.lblContact = CreateStyledLabel(new Point(360, 120), "Контакти:");
            this.lblPrice = CreateStyledLabel(new Point(360, 160), "Ціна:");
            this.lblDescription = CreateStyledLabel(new Point(20, 420), "Опис:", new Size(760, 100));
            this.btnBack = new System.Windows.Forms.Button();

            // headerPanel
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(180)))), ((int)(((byte)(71)))));
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(800, 60);
            this.headerPanel.TabIndex = 0;

            // lblTitle
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Деталі оголошення";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pbPhoto
            this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPhoto.Location = new System.Drawing.Point(20, 80);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(320, 320);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPhoto.TabIndex = 1;
            this.pbPhoto.TabStop = false;

            // lblDescription
            this.lblDescription.AutoSize = false;
            this.lblDescription.TextAlign = ContentAlignment.TopLeft;

            // btnBack
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(180)))), ((int)(((byte)(71)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(600, 360);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(150, 40);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // ViewPostForm
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.pbPhoto);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ViewPostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перегляд оголошення";
            this.headerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
        }

        private Label CreateStyledLabel(Point location, string text, Size? size = null)
        {
            return new Label
            {
                Location = location,
                Size = size ?? new Size(400, 30),
                Text = text,
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };
        }

    }
}
