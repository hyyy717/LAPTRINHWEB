using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace BanS
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7091/api/Books";
        private List<Book> allBooks = new List<Book>(); // Lưu toàn bộ danh sách sách

        public Form1()
        {
            InitializeComponent();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(Console.Out));
            System.Diagnostics.Trace.AutoFlush = true;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadBooksAsync();
        }

        private async Task LoadBooksAsync()
        {
            try
            {
                System.Diagnostics.Trace.WriteLine("Loading books from API...");
                var response = await _httpClient.GetAsync(ApiBaseUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Trace.WriteLine($"GET Response: {content}");
                allBooks = JsonConvert.DeserializeObject<List<Book>>(content) ?? new List<Book>();
                dataGridView1.DataSource = allBooks;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.WriteLine($"HTTP Error: {ex.Message}");
                MessageBox.Show($"Lỗi kết nối API khi tải danh sách sách: {ex.Message}. Kiểm tra xem API có đang chạy trên {ApiBaseUrl} không.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định khi tải danh sách sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var book = new
            {
                Title = txtTitle.Text.Trim(),
                Author = txtAuthor.Text.Trim(),
                Price = decimal.Parse(txtPrice.Text.Trim(), CultureInfo.InvariantCulture),
                PublicationYear = int.Parse(txtPublicationYear.Text.Trim(), CultureInfo.InvariantCulture)
            };

            try
            {
                System.Diagnostics.Trace.WriteLine($"POST Request: {JsonConvert.SerializeObject(book)}");
                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ApiBaseUrl, content);
                System.Diagnostics.Trace.WriteLine($"POST Status: {response.StatusCode}");
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await LoadBooksAsync();
                    ClearInputs();
                    MessageBox.Show("Thêm sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Trace.WriteLine($"POST Error: {response.StatusCode} - {errorContent}");
                    MessageBox.Show($"Lỗi khi thêm sách: {response.StatusCode} - {errorContent}", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.WriteLine($"HTTP Error: {ex.Message}");
                MessageBox.Show($"Lỗi kết nối API khi thêm sách: {ex.Message}. Kiểm tra xem API có đang chạy không.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu nhập không đúng định dạng. Vui lòng kiểm tra giá (dùng dấu chấm, ví dụ: 10.99) và năm xuất bản.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định khi thêm sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var book = new Book
            {
                Id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value,
                Title = txtTitle.Text.Trim(),
                Author = txtAuthor.Text.Trim(),
                Price = decimal.Parse(txtPrice.Text.Trim(), CultureInfo.InvariantCulture),
                PublicationYear = int.Parse(txtPublicationYear.Text.Trim(), CultureInfo.InvariantCulture)
            };

            try
            {
                System.Diagnostics.Trace.WriteLine($"PUT Request: {JsonConvert.SerializeObject(book)}");
                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{ApiBaseUrl}/{book.Id}", content);
                System.Diagnostics.Trace.WriteLine($"PUT Status: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadBooksAsync();
                    ClearInputs();
                    MessageBox.Show("Cập nhật sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Trace.WriteLine($"PUT Error: {response.StatusCode} - {errorContent}");
                    MessageBox.Show($"Lỗi khi cập nhật sách: {response.StatusCode} - {errorContent}", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.WriteLine($"HTTP Error: {ex.Message}");
                MessageBox.Show($"Lỗi kết nối API khi cập nhật sách: {ex.Message}. Kiểm tra xem API có đang chạy không.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu nhập không đúng định dạng. Vui lòng kiểm tra giá (dùng dấu chấm, ví dụ: 10.99) và năm xuất bản.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định khi cập nhật sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            try
            {
                System.Diagnostics.Trace.WriteLine($"DELETE Request: {ApiBaseUrl}/{id}");
                var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");
                System.Diagnostics.Trace.WriteLine($"DELETE Status: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadBooksAsync();
                    ClearInputs();
                    MessageBox.Show("Xóa sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Trace.WriteLine($"DELETE Error: {response.StatusCode} - {errorContent}");
                    MessageBox.Show($"Lỗi khi xóa sách: {response.StatusCode} - {errorContent}", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.WriteLine($"HTTP Error: {ex.Message}");
                MessageBox.Show($"Lỗi kết nối API khi xóa sách: {ex.Message}. Kiểm tra xem API có đang chạy không.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định khi xóa sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadBooksAsync();
            ClearInputs();
            MessageBox.Show("Tải lại danh sách sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtTitle.Text.Trim(); // Sử dụng txtTitle làm ô tìm kiếm
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập tên sách để tìm kiếm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                System.Diagnostics.Trace.WriteLine($"Searching for books with title containing: {searchTerm}");
                var response = await _httpClient.GetAsync($"{ApiBaseUrl}/search/{Uri.EscapeDataString(searchTerm)}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Trace.WriteLine($"Search Response: {content}");
                var books = JsonConvert.DeserializeObject<List<Book>>(content) ?? new List<Book>();
                dataGridView1.DataSource = books;
                if (!books.Any())
                {
                    MessageBox.Show("Không tìm thấy sách nào với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.WriteLine($"HTTP Error: {ex.Message}");
                MessageBox.Show($"Lỗi kết nối API khi tìm kiếm sách: {ex.Message}. Kiểm tra xem API có đang chạy không.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định khi tìm kiếm sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                txtTitle.Text = row.Cells["Title"].Value?.ToString() ?? "";
                txtAuthor.Text = row.Cells["Author"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                txtPublicationYear.Text = row.Cells["PublicationYear"].Value?.ToString() ?? "";
            }
        }

        private bool ValidateInputs(out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorMessage = "Tiêu đề không được để trống.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                errorMessage = "Tác giả không được để trống.";
                return false;
            }
            if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) || price < 0)
            {
                errorMessage = "Giá phải là một số không âm (dùng dấu chấm, ví dụ: 10.99).";
                return false;
            }
            if (!int.TryParse(txtPublicationYear.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out int year) || year < 0 || year > DateTime.Now.Year)
            {
                errorMessage = $"Năm xuất bản phải là số nguyên từ 0 đến {DateTime.Now.Year}.";
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtPrice.Clear();
            txtPublicationYear.Clear();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int PublicationYear { get; set; }
    }
}