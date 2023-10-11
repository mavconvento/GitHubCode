
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class MySqlDataConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public MySqlDataConnection()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            ReadConnecntionStringFile();
            // set these values correctly for your database server
            var builder = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = uid,
                Password = password,
                Database = database,
                AllowUserVariables = true
            };

            // open a connection asynchronously
            connection = new MySqlConnection(builder.ConnectionString);
        }

        private void ReadConnecntionStringFile(string type = "")
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + @"\SyncApplication\MySqlConnectionString" + type + ".txt";
                string rootconnectionString = sysDir + @"\MySqlConnectionString" + type + ".txt";

                //check if file is in root directory
                if (File.Exists(rootconnectionString))
                {
                    TextReader tr = new StreamReader(rootconnectionString);
                    using (tr)
                    {
                        server = tr.ReadLine();
                        uid = tr.ReadLine();
                        password = tr.ReadLine();
                        database = tr.ReadLine();
                    }
                }
                else if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        server = tr.ReadLine();
                        uid = tr.ReadLine();
                        password = tr.ReadLine();
                        database = tr.ReadLine();
                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        //Insert statement
        public void Insert(string query, DataTable parameters)
        {
            //string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Clear();

                foreach (DataRow item in parameters.Rows)
                {
                    cmd.Parameters.AddWithValue(item[0].ToString(), item[1].ToString());
                }

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void ExecuteProceduteNoReturn(string procName, DataTable parameters)
        {
            try
            {
                if (this.OpenConnection() == true)
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pigDataSave";
                    cmd.Parameters.Clear();

                    //foreach (DataRow item in parameters.Rows)
                    //{
                    //    cmd.Parameters.Add(new MySqlParameter(item[0].ToString(), item[1].ToString()));
                    //    cmd.Parameters[item[0].ToString()].Direction = System.Data.ParameterDirection.Input;
                    //}
                    DataTable dt = new DataTable();
                    //Execute query
                    dt.Load(cmd.ExecuteReader());

                    //close connection
                    this.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
           
        }

        //Update statement
        public void Update(string query, DataTable parameters)
        {
            //string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Clear();

                foreach (DataRow item in parameters.Rows)
                {
                    cmd.Parameters.AddWithValue(item[0].ToString(), item[1].ToString());
                }

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string query)
        {
            //string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public DataTable Select(string query,DataTable parameters)
        {
            //string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            //List<string>[] list = new List<string>[3];
            //list[0] = new List<string>();
            //list[1] = new List<string>();
            //list[2] = new List<string>();

            DataTable dt = new DataTable();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Clear();

                foreach (DataRow item in parameters.Rows)
                {
                    cmd.Parameters.AddWithValue(item[0].ToString(), item[1].ToString());
                }

                dt.Load(cmd.ExecuteReader());

                this.CloseConnection();

                return dt;
            }
            else
            {
                return dt;
            }
        }

        //Count statement
        public int Count(string query)
        {
            ///string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\MySqlBackup" + year + "-" + month + "-" + day +
            "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                //MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                //MessageBox.Show("Error , unable to Restore!");
            }
        }
    }
}
