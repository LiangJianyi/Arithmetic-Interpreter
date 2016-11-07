namespace VisualCalculator2 {
	partial class Calculator {
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
			this.txtDisplay = new System.Windows.Forms.TextBox();
			this.pnlHead = new System.Windows.Forms.Panel();
			this.pnlBody = new System.Windows.Forms.Panel();
			this.btnEqual = new System.Windows.Forms.Button();
			this.btnAddition = new System.Windows.Forms.Button();
			this.btnSubtraction = new System.Windows.Forms.Button();
			this.btnMultiplication = new System.Windows.Forms.Button();
			this.btnDivision = new System.Windows.Forms.Button();
			this.btnZero = new System.Windows.Forms.Button();
			this.btnDot = new System.Windows.Forms.Button();
			this.btnThree = new System.Windows.Forms.Button();
			this.btnTwo = new System.Windows.Forms.Button();
			this.btnOne = new System.Windows.Forms.Button();
			this.btnSix = new System.Windows.Forms.Button();
			this.btnFive = new System.Windows.Forms.Button();
			this.btnFour = new System.Windows.Forms.Button();
			this.btnNine = new System.Windows.Forms.Button();
			this.btnEight = new System.Windows.Forms.Button();
			this.btnRemian = new System.Windows.Forms.Button();
			this.btnReverse = new System.Windows.Forms.Button();
			this.btnClean = new System.Windows.Forms.Button();
			this.btnSeven = new System.Windows.Forms.Button();
			this.pnlHead.SuspendLayout();
			this.pnlBody.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtDisplay
			// 
			this.txtDisplay.BackColor = System.Drawing.Color.White;
			this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDisplay.Enabled = false;
			this.txtDisplay.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtDisplay.Location = new System.Drawing.Point(0, 0);
			this.txtDisplay.Multiline = true;
			this.txtDisplay.Name = "txtDisplay";
			this.txtDisplay.ReadOnly = true;
			this.txtDisplay.Size = new System.Drawing.Size(459, 56);
			this.txtDisplay.TabIndex = 0;
			this.txtDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// pnlHead
			// 
			this.pnlHead.BackColor = System.Drawing.Color.Transparent;
			this.pnlHead.Controls.Add(this.txtDisplay);
			this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHead.Location = new System.Drawing.Point(0, 0);
			this.pnlHead.Name = "pnlHead";
			this.pnlHead.Size = new System.Drawing.Size(459, 56);
			this.pnlHead.TabIndex = 1;
			// 
			// pnlBody
			// 
			this.pnlBody.BackColor = System.Drawing.Color.White;
			this.pnlBody.Controls.Add(this.btnEqual);
			this.pnlBody.Controls.Add(this.btnAddition);
			this.pnlBody.Controls.Add(this.btnSubtraction);
			this.pnlBody.Controls.Add(this.btnMultiplication);
			this.pnlBody.Controls.Add(this.btnDivision);
			this.pnlBody.Controls.Add(this.btnZero);
			this.pnlBody.Controls.Add(this.btnDot);
			this.pnlBody.Controls.Add(this.btnThree);
			this.pnlBody.Controls.Add(this.btnTwo);
			this.pnlBody.Controls.Add(this.btnOne);
			this.pnlBody.Controls.Add(this.btnSix);
			this.pnlBody.Controls.Add(this.btnFive);
			this.pnlBody.Controls.Add(this.btnFour);
			this.pnlBody.Controls.Add(this.btnNine);
			this.pnlBody.Controls.Add(this.btnEight);
			this.pnlBody.Controls.Add(this.btnRemian);
			this.pnlBody.Controls.Add(this.btnReverse);
			this.pnlBody.Controls.Add(this.btnClean);
			this.pnlBody.Controls.Add(this.btnSeven);
			this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBody.Location = new System.Drawing.Point(0, 56);
			this.pnlBody.Name = "pnlBody";
			this.pnlBody.Size = new System.Drawing.Size(459, 404);
			this.pnlBody.TabIndex = 2;
			// 
			// btnEqual
			// 
			this.btnEqual.BackColor = System.Drawing.Color.LightGray;
			this.btnEqual.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnEqual.Location = new System.Drawing.Point(288, 328);
			this.btnEqual.Name = "btnEqual";
			this.btnEqual.Size = new System.Drawing.Size(150, 70);
			this.btnEqual.TabIndex = 0;
			this.btnEqual.Text = "=";
			this.btnEqual.UseVisualStyleBackColor = false;
			this.btnEqual.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEqual_MouseDown);
			this.btnEqual.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEqual_MouseUp);
			// 
			// btnAddition
			// 
			this.btnAddition.BackColor = System.Drawing.Color.LightGray;
			this.btnAddition.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnAddition.Location = new System.Drawing.Point(288, 248);
			this.btnAddition.Name = "btnAddition";
			this.btnAddition.Size = new System.Drawing.Size(150, 70);
			this.btnAddition.TabIndex = 0;
			this.btnAddition.Text = "+";
			this.btnAddition.UseVisualStyleBackColor = false;
			this.btnAddition.Click += new System.EventHandler(this.btnAddition_Click);
			// 
			// btnSubtraction
			// 
			this.btnSubtraction.BackColor = System.Drawing.Color.LightGray;
			this.btnSubtraction.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSubtraction.Location = new System.Drawing.Point(288, 168);
			this.btnSubtraction.Name = "btnSubtraction";
			this.btnSubtraction.Size = new System.Drawing.Size(150, 70);
			this.btnSubtraction.TabIndex = 0;
			this.btnSubtraction.Text = "-";
			this.btnSubtraction.UseVisualStyleBackColor = false;
			this.btnSubtraction.Click += new System.EventHandler(this.btnSubtraction_Click);
			// 
			// btnMultiplication
			// 
			this.btnMultiplication.BackColor = System.Drawing.Color.LightGray;
			this.btnMultiplication.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnMultiplication.Location = new System.Drawing.Point(288, 88);
			this.btnMultiplication.Name = "btnMultiplication";
			this.btnMultiplication.Size = new System.Drawing.Size(150, 70);
			this.btnMultiplication.TabIndex = 0;
			this.btnMultiplication.Text = "X";
			this.btnMultiplication.UseVisualStyleBackColor = false;
			this.btnMultiplication.Click += new System.EventHandler(this.btnMultiplication_Click);
			// 
			// btnDivision
			// 
			this.btnDivision.BackColor = System.Drawing.Color.LightGray;
			this.btnDivision.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnDivision.Location = new System.Drawing.Point(288, 8);
			this.btnDivision.Name = "btnDivision";
			this.btnDivision.Size = new System.Drawing.Size(150, 70);
			this.btnDivision.TabIndex = 0;
			this.btnDivision.Text = "➗";
			this.btnDivision.UseVisualStyleBackColor = false;
			this.btnDivision.Click += new System.EventHandler(this.btnDivision_Click);
			// 
			// btnZero
			// 
			this.btnZero.BackColor = System.Drawing.Color.LightGray;
			this.btnZero.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnZero.Location = new System.Drawing.Point(16, 328);
			this.btnZero.Name = "btnZero";
			this.btnZero.Size = new System.Drawing.Size(150, 70);
			this.btnZero.TabIndex = 0;
			this.btnZero.Text = "0";
			this.btnZero.UseVisualStyleBackColor = false;
			this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
			// 
			// btnDot
			// 
			this.btnDot.BackColor = System.Drawing.Color.LightGray;
			this.btnDot.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnDot.Location = new System.Drawing.Point(176, 328);
			this.btnDot.Name = "btnDot";
			this.btnDot.Size = new System.Drawing.Size(70, 70);
			this.btnDot.TabIndex = 0;
			this.btnDot.Text = ".";
			this.btnDot.UseVisualStyleBackColor = false;
			this.btnDot.Click += new System.EventHandler(this.btnDot_Click);
			// 
			// btnThree
			// 
			this.btnThree.BackColor = System.Drawing.Color.LightGray;
			this.btnThree.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnThree.Location = new System.Drawing.Point(176, 248);
			this.btnThree.Name = "btnThree";
			this.btnThree.Size = new System.Drawing.Size(70, 70);
			this.btnThree.TabIndex = 0;
			this.btnThree.Text = "3";
			this.btnThree.UseVisualStyleBackColor = false;
			this.btnThree.Click += new System.EventHandler(this.btnThree_Click);
			// 
			// btnTwo
			// 
			this.btnTwo.BackColor = System.Drawing.Color.LightGray;
			this.btnTwo.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnTwo.Location = new System.Drawing.Point(96, 248);
			this.btnTwo.Name = "btnTwo";
			this.btnTwo.Size = new System.Drawing.Size(70, 70);
			this.btnTwo.TabIndex = 0;
			this.btnTwo.Text = "2";
			this.btnTwo.UseVisualStyleBackColor = false;
			this.btnTwo.Click += new System.EventHandler(this.btnTwo_Click);
			// 
			// btnOne
			// 
			this.btnOne.BackColor = System.Drawing.Color.LightGray;
			this.btnOne.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOne.Location = new System.Drawing.Point(16, 248);
			this.btnOne.Name = "btnOne";
			this.btnOne.Size = new System.Drawing.Size(70, 70);
			this.btnOne.TabIndex = 0;
			this.btnOne.Text = "1";
			this.btnOne.UseVisualStyleBackColor = false;
			this.btnOne.Click += new System.EventHandler(this.btnOne_Click);
			// 
			// btnSix
			// 
			this.btnSix.BackColor = System.Drawing.Color.LightGray;
			this.btnSix.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSix.Location = new System.Drawing.Point(176, 168);
			this.btnSix.Name = "btnSix";
			this.btnSix.Size = new System.Drawing.Size(70, 70);
			this.btnSix.TabIndex = 0;
			this.btnSix.Text = "6";
			this.btnSix.UseVisualStyleBackColor = false;
			this.btnSix.Click += new System.EventHandler(this.btnSix_Click);
			// 
			// btnFive
			// 
			this.btnFive.BackColor = System.Drawing.Color.LightGray;
			this.btnFive.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnFive.Location = new System.Drawing.Point(96, 168);
			this.btnFive.Name = "btnFive";
			this.btnFive.Size = new System.Drawing.Size(70, 70);
			this.btnFive.TabIndex = 0;
			this.btnFive.Text = "5";
			this.btnFive.UseVisualStyleBackColor = false;
			this.btnFive.Click += new System.EventHandler(this.btnFive_Click);
			// 
			// btnFour
			// 
			this.btnFour.BackColor = System.Drawing.Color.LightGray;
			this.btnFour.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnFour.Location = new System.Drawing.Point(16, 168);
			this.btnFour.Name = "btnFour";
			this.btnFour.Size = new System.Drawing.Size(70, 70);
			this.btnFour.TabIndex = 0;
			this.btnFour.Text = "4";
			this.btnFour.UseVisualStyleBackColor = false;
			this.btnFour.Click += new System.EventHandler(this.btnFour_Click);
			// 
			// btnNine
			// 
			this.btnNine.BackColor = System.Drawing.Color.LightGray;
			this.btnNine.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnNine.Location = new System.Drawing.Point(176, 88);
			this.btnNine.Name = "btnNine";
			this.btnNine.Size = new System.Drawing.Size(70, 70);
			this.btnNine.TabIndex = 0;
			this.btnNine.Text = "9";
			this.btnNine.UseVisualStyleBackColor = false;
			this.btnNine.Click += new System.EventHandler(this.btnNine_Click);
			// 
			// btnEight
			// 
			this.btnEight.BackColor = System.Drawing.Color.LightGray;
			this.btnEight.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnEight.Location = new System.Drawing.Point(96, 88);
			this.btnEight.Name = "btnEight";
			this.btnEight.Size = new System.Drawing.Size(70, 70);
			this.btnEight.TabIndex = 0;
			this.btnEight.Text = "8";
			this.btnEight.UseVisualStyleBackColor = false;
			this.btnEight.Click += new System.EventHandler(this.btnEight_Click);
			// 
			// btnRemian
			// 
			this.btnRemian.BackColor = System.Drawing.Color.LightGray;
			this.btnRemian.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnRemian.Location = new System.Drawing.Point(176, 8);
			this.btnRemian.Name = "btnRemian";
			this.btnRemian.Size = new System.Drawing.Size(70, 70);
			this.btnRemian.TabIndex = 0;
			this.btnRemian.Text = "%";
			this.btnRemian.UseVisualStyleBackColor = false;
			this.btnRemian.Click += new System.EventHandler(this.btnRemian_Click);
			// 
			// btnReverse
			// 
			this.btnReverse.BackColor = System.Drawing.Color.LightGray;
			this.btnReverse.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnReverse.Location = new System.Drawing.Point(96, 8);
			this.btnReverse.Name = "btnReverse";
			this.btnReverse.Size = new System.Drawing.Size(70, 70);
			this.btnReverse.TabIndex = 0;
			this.btnReverse.Text = "+/-";
			this.btnReverse.UseVisualStyleBackColor = false;
			// 
			// btnClean
			// 
			this.btnClean.BackColor = System.Drawing.Color.LightGray;
			this.btnClean.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnClean.Location = new System.Drawing.Point(16, 8);
			this.btnClean.Name = "btnClean";
			this.btnClean.Size = new System.Drawing.Size(70, 70);
			this.btnClean.TabIndex = 0;
			this.btnClean.Text = "C";
			this.btnClean.UseVisualStyleBackColor = false;
			this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
			// 
			// btnSeven
			// 
			this.btnSeven.BackColor = System.Drawing.Color.LightGray;
			this.btnSeven.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSeven.Location = new System.Drawing.Point(16, 88);
			this.btnSeven.Name = "btnSeven";
			this.btnSeven.Size = new System.Drawing.Size(70, 70);
			this.btnSeven.TabIndex = 0;
			this.btnSeven.Text = "7";
			this.btnSeven.UseVisualStyleBackColor = false;
			this.btnSeven.Click += new System.EventHandler(this.btnSeven_Click);
			// 
			// Calculator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(459, 460);
			this.Controls.Add(this.pnlBody);
			this.Controls.Add(this.pnlHead);
			this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "Calculator";
			this.Text = "Calculator";
			this.Load += new System.EventHandler(this.Calculator_Load);
			this.pnlHead.ResumeLayout(false);
			this.pnlHead.PerformLayout();
			this.pnlBody.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtDisplay;
		private System.Windows.Forms.Panel pnlHead;
		private System.Windows.Forms.Panel pnlBody;
		private System.Windows.Forms.Button btnEqual;
		private System.Windows.Forms.Button btnAddition;
		private System.Windows.Forms.Button btnSubtraction;
		private System.Windows.Forms.Button btnMultiplication;
		private System.Windows.Forms.Button btnDivision;
		private System.Windows.Forms.Button btnZero;
		private System.Windows.Forms.Button btnDot;
		private System.Windows.Forms.Button btnThree;
		private System.Windows.Forms.Button btnTwo;
		private System.Windows.Forms.Button btnOne;
		private System.Windows.Forms.Button btnSix;
		private System.Windows.Forms.Button btnFive;
		private System.Windows.Forms.Button btnFour;
		private System.Windows.Forms.Button btnNine;
		private System.Windows.Forms.Button btnEight;
		private System.Windows.Forms.Button btnRemian;
		private System.Windows.Forms.Button btnReverse;
		private System.Windows.Forms.Button btnClean;
		private System.Windows.Forms.Button btnSeven;
	}
}

