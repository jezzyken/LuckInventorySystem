﻿namespace LuckInventorySystem_v2
{
    partial class frmArhivedUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArhivedUsers));
            this.LsvOrder = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LsvUser = new System.Windows.Forms.ListView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LsvOrder
            // 
            this.LsvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvOrder.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LsvOrder.FullRowSelect = true;
            this.LsvOrder.Location = new System.Drawing.Point(0, 31);
            this.LsvOrder.Name = "LsvOrder";
            this.LsvOrder.Size = new System.Drawing.Size(886, 548);
            this.LsvOrder.TabIndex = 18;
            this.LsvOrder.UseCompatibleStateImageBehavior = false;
            this.LsvOrder.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(865, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "x";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 31);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SeaGreen;
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(886, 71);
            this.panel2.TabIndex = 19;
            // 
            // LsvUser
            // 
            this.LsvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvUser.FullRowSelect = true;
            this.LsvUser.Location = new System.Drawing.Point(0, 102);
            this.LsvUser.Name = "LsvUser";
            this.LsvUser.Size = new System.Drawing.Size(886, 477);
            this.LsvUser.TabIndex = 20;
            this.LsvUser.UseCompatibleStateImageBehavior = false;
            this.LsvUser.View = System.Windows.Forms.View.Details;
            this.LsvUser.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LsvUser_ItemSelectionChanged);
            this.LsvUser.SelectedIndexChanged += new System.EventHandler(this.LsvUser_SelectedIndexChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(12, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(113, 43);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Restore";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmArhivedUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 579);
            this.Controls.Add(this.LsvUser);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LsvOrder);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "frmArhivedUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmArhivedUsers";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LsvOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ListView LsvUser;
    }
}