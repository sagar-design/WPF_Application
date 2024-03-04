using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace WPF_Info
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string s = "server=SAGAR-SS;database=demo;integrated security=true";
        SqlConnection con=new SqlConnection(s);
        public MainWindow()
        {
            InitializeComponent();
            Data();
            Autoincrement();
           
        }
        public void Autoincrement()
        {
            int r;
            con.Open();
            SqlCommand cmd = new SqlCommand("select max(id) from employee3", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string d = dr[0].ToString();
                if (d == "")
                {
                    TextBox1.Text = "1";
                }
                else
                {
                    r = Convert.ToInt16(dr[0].ToString());
                    r = r + 1;
                    TextBox1.Text = r.ToString();
                }
                dr.Close();
                con.Close();

            }
        }

        public void Data()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from employee3", con);
            DataSet ds=new DataSet();
            adp.Fill(ds);
            dataGrid1.ItemsSource = ds.Tables[0].DefaultView;
        }
        public void Cleardata()
        {
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Clear();
            ComboBox1.Text = "";
            RadioButton1.IsChecked=false;
            RadioButton2.IsChecked=false;
            DateTimePicker1.Text = "";
        }
        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//insert

            string gender;
            if (RadioButton1.IsChecked == true)
                gender = "Male";
            else if (RadioButton2.IsChecked == true)
                gender = "Female";
            else
                gender = "Other";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText="insert into employee3 values(@id,@name,@gender,@designation,@dateofjoin)";
            cmd.Parameters.AddWithValue("@id", TextBox1.Text);
            cmd.Parameters.AddWithValue("@name", TextBox2.Text);
            cmd.Parameters.AddWithValue("@gender",gender);
            cmd.Parameters.AddWithValue("@designation", ComboBox1.Text);
            cmd.Parameters.AddWithValue("@dateofjoin",DateTimePicker1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Added Record Succesfully");
            
            Data();
            Autoincrement();
            Cleardata();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//search
            SqlDataAdapter adp = new SqlDataAdapter("select * from employee3 where id=" + TextBox3.Text + "", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Employee Not Found");
            }
            else
            {
                TextBox1.Text = dt.Rows[0][0].ToString();
                TextBox2.Text = dt.Rows[0][1].ToString();
                if (dt.Rows[0][2].ToString() == "Male")
                    RadioButton1.IsChecked = true;
                else
                    RadioButton2.IsChecked = true;
                ComboBox1.Text = dt.Rows[0][3].ToString();
                DateTimePicker1.Text = dt.Rows[0][4].ToString();

            }
            }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {//update
            string gender;
            if (RadioButton1.IsChecked == true)
                gender = "Male";
            else if (RadioButton2.IsChecked == true)
                gender = "Female";
            else
                gender = "Other";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update employee3 set name=@p2,gender=@p3,designation=@p4,doj=@p5 where id=@p1";
            cmd.Parameters.AddWithValue("@p1", TextBox1.Text);
            cmd.Parameters.AddWithValue("@p2", TextBox2.Text);
            cmd.Parameters.AddWithValue("@p3", gender);
            cmd.Parameters.AddWithValue("@p4", ComboBox1.Text);
            cmd.Parameters.AddWithValue("@p5", DateTimePicker1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Updated Record Succesfully");
            Cleardata();
            Data();
            Autoincrement();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//delete
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from employee3 where id=@p1";
            cmd.Parameters.AddWithValue("@p1", TextBox1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Autoincrement();
            Data();
            Cleardata();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {//close
            Close();
        }
    }
}
