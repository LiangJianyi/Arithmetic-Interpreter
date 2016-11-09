namespace Arithmetic_Interpreter {
	partial class Form1 {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose( bool disposing ) {
			if (disposing && (components != null)) {
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent( ) {
			this.txtInput = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCalculating = new System.Windows.Forms.Button();
			this.lblResult = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtInput.Location = new System.Drawing.Point(56, 32);
			this.txtInput.Margin = new System.Windows.Forms.Padding(6);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(472, 35);
			this.txtInput.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnCalculating);
			this.panel1.Controls.Add(this.txtInput);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(584, 176);
			this.panel1.TabIndex = 1;
			// 
			// btnCalculating
			// 
			this.btnCalculating.Location = new System.Drawing.Point(248, 96);
			this.btnCalculating.Name = "btnCalculating";
			this.btnCalculating.Size = new System.Drawing.Size(80, 48);
			this.btnCalculating.TabIndex = 1;
			this.btnCalculating.Text = "=";
			this.btnCalculating.UseVisualStyleBackColor = true;
			this.btnCalculating.Click += new System.EventHandler(this.btnCalculating_Click);
			// 
			// lblResult
			// 
			this.lblResult.AutoSize = true;
			this.lblResult.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblResult.Location = new System.Drawing.Point(56, 8);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(120, 46);
			this.lblResult.TabIndex = 1;
			this.lblResult.Text = "label1";
			this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.lblResult);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 200);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(584, 77);
			this.panel2.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(584, 277);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(6);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnCalculating;
	}
}

