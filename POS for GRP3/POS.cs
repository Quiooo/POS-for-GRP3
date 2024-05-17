using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_for_GRP3
{
    public partial class POS : Form
    {
        private MySqlConnection connection;

        public POS()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "server=localhost;port=3306;database=merchstore;user=username;password=password;";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                MessageBox.Show("Connection Successful!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message);
            }
        }

        private void POS_Load(object sender, EventArgs e)
        {
            // Connection string for your database
            string connectionString = "server=localhost;port=3306;database=merchstore;user=BERNA@THIS;password=BERNADETH;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // SQL query to retrieve the image data from your table
                string query = "SELECT Color FROM category";

                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();

                    // Execute the command and retrieve the image data
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        byte[] imageData = (byte[])result;

                        // Convert the binary data to an image
                        Image image;
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            image = Image.FromStream(ms);
                        }

                        // Display the image on the PictureBox control
                        pictureBox1.Image = image;
                    }
                    else
                    {
                        MessageBox.Show("No image data found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
