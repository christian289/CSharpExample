namespace WinformTimerTick
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.BtnWorkStart1 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.BtnWorkStart2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnWorkStart3 = new System.Windows.Forms.Button();
            this.BtnWorkStart4 = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.BtnWorkStart5 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // BtnWorkStart1
            // 
            this.BtnWorkStart1.Location = new System.Drawing.Point(40, 114);
            this.BtnWorkStart1.Name = "BtnWorkStart1";
            this.BtnWorkStart1.Size = new System.Drawing.Size(173, 80);
            this.BtnWorkStart1.TabIndex = 1;
            this.BtnWorkStart1.Text = "무거운 동작 타이머 시작";
            this.BtnWorkStart1.UseVisualStyleBackColor = true;
            this.BtnWorkStart1.Click += new System.EventHandler(this.BtnWorkStart1_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // BtnWorkStart2
            // 
            this.BtnWorkStart2.Location = new System.Drawing.Point(234, 114);
            this.BtnWorkStart2.Name = "BtnWorkStart2";
            this.BtnWorkStart2.Size = new System.Drawing.Size(173, 80);
            this.BtnWorkStart2.TabIndex = 1;
            this.BtnWorkStart2.Text = "무거운 동작 타이머 시작 Enable 추가 ";
            this.BtnWorkStart2.UseVisualStyleBackColor = true;
            this.BtnWorkStart2.Click += new System.EventHandler(this.BtnWorkStart2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "label4";
            // 
            // BtnWorkStart3
            // 
            this.BtnWorkStart3.Location = new System.Drawing.Point(40, 310);
            this.BtnWorkStart3.Name = "BtnWorkStart3";
            this.BtnWorkStart3.Size = new System.Drawing.Size(173, 80);
            this.BtnWorkStart3.TabIndex = 1;
            this.BtnWorkStart3.Text = "가벼운 동작 타이머 시작";
            this.BtnWorkStart3.UseVisualStyleBackColor = true;
            this.BtnWorkStart3.Click += new System.EventHandler(this.BtnWorkStart3_Click);
            // 
            // BtnWorkStart4
            // 
            this.BtnWorkStart4.Location = new System.Drawing.Point(234, 310);
            this.BtnWorkStart4.Name = "BtnWorkStart4";
            this.BtnWorkStart4.Size = new System.Drawing.Size(173, 80);
            this.BtnWorkStart4.TabIndex = 1;
            this.BtnWorkStart4.Text = "가벼운 동작 타이머 시작 Enable 추가 ";
            this.BtnWorkStart4.UseVisualStyleBackColor = true;
            this.BtnWorkStart4.Click += new System.EventHandler(this.BtnWorkStart4_Click);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // BtnWorkStart5
            // 
            this.BtnWorkStart5.Location = new System.Drawing.Point(133, 493);
            this.BtnWorkStart5.Name = "BtnWorkStart5";
            this.BtnWorkStart5.Size = new System.Drawing.Size(173, 80);
            this.BtnWorkStart5.TabIndex = 3;
            this.BtnWorkStart5.Text = "무거운 동작 thread 처리";
            this.BtnWorkStart5.UseVisualStyleBackColor = true;
            this.BtnWorkStart5.Click += new System.EventHandler(this.BtnWorkStart5_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 424);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "label5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 621);
            this.Controls.Add(this.BtnWorkStart5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnWorkStart4);
            this.Controls.Add(this.BtnWorkStart3);
            this.Controls.Add(this.BtnWorkStart2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnWorkStart1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnWorkStart1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnWorkStart2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnWorkStart3;
        private System.Windows.Forms.Button BtnWorkStart4;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Button BtnWorkStart5;
        private System.Windows.Forms.Label label5;
    }
}

