using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Pupils
{
    public partial class Form1 : Form
    {
        NguyenADO nguyen = new NguyenADO();
        public Form1()
        {
            InitializeComponent();  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
            cmbFaculty.DataSource = faculties;

        }
        private void LoadDataGrid()
        {
            var N = nguyen.SINHVIENs;
            dgvPupils.DataSource = N.ToList();
        }

        private void dgvPupils_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPupils.SelectedRows.Count > 0)
            {
                // Lấy hàng đầu tiên được chọn
                DataGridViewRow selectedRow = dgvPupils.SelectedRows[0];

                // Kiểm tra để tránh lỗi khi DataGridView chưa có dữ liệu
                if (selectedRow.Cells["MSSV"].Value != null)
                {
                    // Lấy giá trị từ các cột và đổ vào các điều khiển
                    txtID.Text = selectedRow.Cells["MSSV"].Value.ToString();
                    txtName.Text = selectedRow.Cells["HOTEN"].Value.ToString();
                    cmbFaculty.Text = selectedRow.Cells["KHOA"].Value.ToString();

                    // Xử lý giá trị điểm trung bình
                    float averageScore;
                    if (float.TryParse(selectedRow.Cells["DIEMTB"].Value.ToString(), out averageScore))
                    {
                        txtAverageScore.Text = averageScore.ToString("F2"); // Hiển thị với 2 chữ số thập phân
                    }
                    else
                    {
                        txtAverageScore.Text = "0.00"; // Đặt giá trị mặc định nếu không thể parse
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            nguyen.Dispose(); // Giải phóng tài nguyên
        }
        List<string> faculties = new List<string>()
        {
            "CNTT",
            "Khoa Kinh Tế",
            "Khoa Ngoại Ngữ"
        };

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            string id = txtID.Text;
            string name = txtName.Text;
            string faculty = cmbFaculty.SelectedItem.ToString();
            float score;
            if (!float.TryParse(txtAverageScore.Text.Trim(), out score))
            {
                MessageBox.Show("Vui lòng nhập điểm trung bình hợp lệ (số).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           

            // Tạo đối tượng Food mới
            SINHVIEN sv = new SINHVIEN
            {
                MSSV = id,
                HOTEN = name,
                KHOA = faculty,
                DIEMTB = score,
            };

         

            // Thêm món ăn mới vào cơ sở dữ liệu
            try
            {
                nguyen.SINHVIENs.Add(sv);
                nguyen.SaveChanges();
               
            }
            catch (Exception ex) {
                LoadDataGrid();
                MessageBox.Show(ex.Message);
               
            }


            LoadDataGrid();

           

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPupils.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvPupils.SelectedRows[0];

            string mssv = selectedRow.Cells["MSSV"].Value.ToString();

            var me = nguyen.SINHVIENs.First(s => s.MSSV == mssv);
                
            try
            {
                nguyen.SINHVIENs.Remove(me);
                nguyen.SaveChanges();
              
            }
            catch(Exception ex)
            {
                LoadDataGrid();
                MessageBox.Show(ex.Message);
                
            }
            LoadDataGrid();
            txtID.Clear();
            txtName.Clear();
            txtAverageScore.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPupils.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvPupils.SelectedRows[0];
            string mssv = selectedRow.Cells["MSSV"].Value.ToString();
            // Lấy dữ liệu từ các điều khiển
            string name = txtName.Text.Trim();
            string faculty = cmbFaculty.SelectedItem.ToString();
            float score;

            if (!float.TryParse(txtAverageScore.Text.Trim(), out score))
            {
                MessageBox.Show("Vui lòng nhập điểm trung bình hợp lệ (số).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
         
           
            var me = nguyen.SINHVIENs.First(s => s.MSSV == mssv);
            me.MSSV = mssv;
            me.HOTEN = name;
            me.KHOA = faculty;
            me.DIEMTB = score;
            nguyen.SaveChanges();
            LoadDataGrid();
        }
    }
    }

