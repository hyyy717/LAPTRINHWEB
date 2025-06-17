namespace BanS
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            txtPrice = new TextBox();
            txtPublicationYear = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnSearch = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Location = new Point(20, 23);
            dataGridView1.Margin = new Padding(5, 6, 5, 6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1267, 577);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(167, 635);
            txtTitle.Margin = new Padding(5, 6, 5, 6);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(331, 31);
            txtTitle.TabIndex = 1;
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(167, 692);
            txtAuthor.Margin = new Padding(5, 6, 5, 6);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(331, 31);
            txtAuthor.TabIndex = 2;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(167, 750);
            txtPrice.Margin = new Padding(5, 6, 5, 6);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(331, 31);
            txtPrice.TabIndex = 3;
            // 
            // txtPublicationYear
            // 
            txtPublicationYear.Location = new Point(167, 808);
            txtPublicationYear.Margin = new Padding(5, 6, 5, 6);
            txtPublicationYear.Name = "txtPublicationYear";
            txtPublicationYear.Size = new Size(331, 31);
            txtPublicationYear.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(533, 635);
            btnAdd.Margin = new Padding(5, 6, 5, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(125, 44);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(533, 692);
            btnUpdate.Margin = new Padding(5, 6, 5, 6);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(125, 44);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(533, 750);
            btnDelete.Margin = new Padding(5, 6, 5, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(125, 44);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(533, 808);
            btnRefresh.Margin = new Padding(5, 6, 5, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(125, 44);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Tải lại";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(533, 866);
            btnSearch.Margin = new Padding(5, 6, 5, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(125, 44);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 640);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 25);
            label1.TabIndex = 10;
            label1.Text = "Tiêu đề:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 698);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(69, 25);
            label2.TabIndex = 11;
            label2.Text = "Tác giả:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 756);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(41, 25);
            label3.TabIndex = 12;
            label3.Text = "Giá:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 813);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(127, 25);
            label4.TabIndex = 13;
            label4.Text = "Năm xuất bản:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 947); // Tăng chiều cao để chứa nút Tìm kiếm
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSearch);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtPublicationYear);
            Controls.Add(txtPrice);
            Controls.Add(txtAuthor);
            Controls.Add(txtTitle);
            Controls.Add(dataGridView1);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Form1";
            Text = "Quản lý sách";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtPublicationYear;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}