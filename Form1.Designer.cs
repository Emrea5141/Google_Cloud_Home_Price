public partial class MainForm : Form
{
    private System.Windows.Forms.TextBox txtCity;
    private System.Windows.Forms.TextBox txtMinPrice;
    private System.Windows.Forms.TextBox txtMaxPrice;
    private System.Windows.Forms.Button btnFilter;
    private System.Windows.Forms.DataGridView dataGridView1;

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.txtCity = new System.Windows.Forms.TextBox();
        this.txtMinPrice = new System.Windows.Forms.TextBox();
        this.txtMaxPrice = new System.Windows.Forms.TextBox();
        this.btnFilter = new System.Windows.Forms.Button();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();

        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();

        // UI Bileşenlerini burada ayarlayabilirsin

        this.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
    }
}



