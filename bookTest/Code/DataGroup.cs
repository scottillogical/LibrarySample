using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace bookTest
{
    public abstract class DataGroup
    {
        /// <summary>
        /// Connection to database
        /// </summary>
        protected string connectionString = ConfigurationManager.ConnectionStrings["SampleConnectionString"].ConnectionString;

        /// <summary>
        /// Text description of what this data is
        /// </summary>
        protected abstract string Description { get; }

        /// <summary>
        /// Database procedure to load the data from the database
        /// </summary>
        protected virtual string LoadProc { get { throw new NotImplementedException("DataGroup LoadProc not implemented"); } }

        /// <summary>
        /// Database procedure to save an entry to the database
        /// </summary>
        protected virtual string SaveProc { get { throw new NotImplementedException("DataGroup SaveProc not implemented"); } }

        /// <summary>
        /// Load data for this grouping from the database
        /// </summary>
        public virtual void Load()
        {
            // Load the default data
            Reset();

            // Load the data from the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Create a loading command
                conn.Open();
                SqlCommand cmd = new SqlCommand(LoadProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                BindLoadParams(cmd);

                // Import the data from the reader to this structure
                SqlDataReader r = cmd.ExecuteReader();
                Import(r);
                r.Close();
            }
        }

        /// <summary>
        /// Add parameters to load the data
        /// </summary>
        public virtual void BindLoadParams(SqlCommand cmd) 
        {
        }

        /// <summary>
        /// Add parameters to save the data
        /// </summary>        
        public virtual void BindSaveParams(SqlCommand cmd) 
        { 
        }

        /// <summary>
        /// Save data for this grouping to the database
        /// </summary>
        public virtual void Save()
        {
            // Save the data to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Create a data save command
                conn.Open();
                SqlCommand cmd = new SqlCommand(SaveProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Load the parameters
                BindSaveParams(cmd);

                // Just execute the query
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Reset data for this grouping to the defaults
        /// </summary>
        public virtual void Reset()
        {
        }

        /// <summary>
        /// Read the return data
        /// </summary>        
        public virtual void Import(SqlDataReader r)
        {
            // Fail if there are no results
            // NOTE: If the derived importer doesn't want to implicitly read a row
            // (perhaps because it's fine receiving 0 rows), it should not call base.Import(r)
            if (!r.Read())
                throw new Exception("Could not load " + Description + "\n");
        }
    }
}
