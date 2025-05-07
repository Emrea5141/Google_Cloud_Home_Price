using Google.Cloud.BigQuery.V2;
using Google.Apis.Auth.OAuth2;
using System.Data;
using System.Linq;

namespace EmlakBigQueryApp
{
    public partial class Form1 : Form
    {
        private BigQueryClient client;

        private TextBox txtCity;
        private TextBox txtMinPrice;
        private TextBox txtMaxPrice;
        private DataGridView dataGridView1;

        public Form1()
        {
        
            InitializeComponent();

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"homeprice2\bin\Debug\manifest-surfer-452019-f1-7047d79d121c.json");

            client = BigQueryClient.Create("manifest-surfer-452019-f1");
        }

        private void InitializeComponent()
        {
            txtCity = new TextBox();
            txtMinPrice = new TextBox();
            txtMaxPrice = new TextBox();
            dataGridView1 = new DataGridView();
            btnFilter = new Button();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtCity
            // 
            txtCity.Location = new Point(115, 17);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(148, 23);
            txtCity.TabIndex = 0;
            // 
            // txtMinPrice
            // 
            txtMinPrice.Location = new Point(115, 60);
            txtMinPrice.Name = "txtMinPrice";
            txtMinPrice.Size = new Size(148, 23);
            txtMinPrice.TabIndex = 2;
            // 
            // txtMaxPrice
            // 
            txtMaxPrice.Location = new Point(115, 113);
            txtMaxPrice.Name = "txtMaxPrice";
            txtMaxPrice.Size = new Size(148, 23);
            txtMaxPrice.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Location = new Point(331, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(548, 417);
            dataGridView1.TabIndex = 5;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(18, 184);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(245, 29);
            btnFilter.TabIndex = 6;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 24);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 7;
            label1.Text = "City:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 63);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 9;
            label3.Text = "Min Price:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 120);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 10;
            label4.Text = "Max Price:";
            // 
            // Form1
            // 
            ClientSize = new Size(900, 460);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnFilter);
            Controls.Add(txtCity);
            Controls.Add(txtMinPrice);
            Controls.Add(txtMaxPrice);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Emlak Fiyatları BigQuery Uygulaması";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string city = txtCity.Text.Trim();
                string minPriceStr = txtMinPrice.Text.Trim();
                string maxPriceStr = txtMaxPrice.Text.Trim();

                float min = 0;
                float max = float.MaxValue;

                if (!string.IsNullOrEmpty(minPriceStr) && !float.TryParse(minPriceStr, out min))
                {
                    MessageBox.Show("Geçerli bir Min Fiyat girin.");
                    return;
                }

                if (!string.IsNullOrEmpty(maxPriceStr) && !float.TryParse(maxPriceStr, out max))
                {
                    MessageBox.Show("Geçerli bir Max Fiyat girin.");
                    return;
                }

                string query = @"
            SELECT 
               
        `Şehir`, `Fiyat`, `Net_Metrekare`, `Brüt_Metrekare`, `Oda_Sayısı`, 
        `Bulunduğu_Kat`, `Isıtma_Tipi`, `Binanın_Yaşı`, `Eşya_Durumu`,
        `Binanın_Kat_Sayısı`, `Kullanım_Durumu`, `Tapu_Durumu`,
        `Takas`, `Yatırıma_Uygunluk`, `Banyo_Sayısı`
    FROM `manifest-surfer-452019-f1.real_estate.homeprice`
    WHERE LOWER(`Şehir`) = LOWER(@city)
      AND `Fiyat` >= @minPrice
      AND `Fiyat` <= @maxPrice
    LIMIT 100
        ";

                var parameters = new List<BigQueryParameter>
        {
            new BigQueryParameter("city", BigQueryDbType.String, city),
            new BigQueryParameter("minPrice", BigQueryDbType.Float64, min),
            new BigQueryParameter("maxPrice", BigQueryDbType.Float64, max)
        };

                var result = client.ExecuteQuery(query, parameters);

                DataTable table = new DataTable();
                foreach (var field in result.Schema.Fields)
                    table.Columns.Add(field.Name);

                foreach (var row in result)
                {
                    object[] rowData = new object[result.Schema.Fields.Count];
                    for (int i = 0; i < result.Schema.Fields.Count; i++)
                        rowData[i] = row[i]?.ToString();
                    table.Rows.Add(rowData);
                }

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }





        private Button btnFilter;
        private Label label1;
        private Label label3;
        private Label label4;

       
    }
}