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


namespace bookTest.Code
{
    /// <summary>
    /// 
    /// </summary>
    public class Book : DataGroup
    {
        /// <summary>
        /// Database procedure to load the data from the database
        /// </summary>
        protected override string LoadProc { get { return "web_GetBook"; } }

        /// <summary>
        /// Database procedure to save the data from the database
        /// </summary>
        protected override string SaveProc { get { return "web_CreateBook"; } }

        /// <summary>
        /// Text description of what this data is
        /// </summary>
        protected override string Description { get { return "book"; } }

        /// <summary>
        /// A unique identifier for the book
        /// </summary>
        private int? id;

        /// <summary>
        /// A unique identifier for the book
        /// </summary>
        public int? Id
        {
            get { return id; }
        }

        /// <summary>
        /// The title of the book
        /// </summary>
        private string title;

        /// <summary>
        /// The title of the book
        /// </summary>
        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// The author of the book
        /// </summary>
        private string author;

        /// <summary>
        /// The author of the book
        /// </summary>
        public String Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        /// The number of copies in stock
        /// </summary>
        private Int32 copies = 0;

        /// <summary>
        /// The number of copies in stock
        /// </summary>
        public Int32 Copies
        {
            get { return copies; }
            set { copies = value; }
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        public Book(string title, string author, int copies)
        {
            this.title = title;
            this.author = author;
            this.copies = copies;
        }

        
        /// <summary>
        /// Create a new book
        /// </summary>
        public Book(int id, string title, string author, int copies)
        {
            this.title = title;
            this.author = author;
            this.copies = copies;
            this.id = id;
        }
        /// <summary>
        /// Bind the parameters to the SQL statement
        /// </summary>       
        public override void BindSaveParams(SqlCommand cmd)
        {
            base.BindSaveParams(cmd);            
            cmd.Parameters.Add("Title", SqlDbType.VarChar).Value = title;
            cmd.Parameters.Add("Author", SqlDbType.VarChar).Value = author;
            cmd.Parameters.Add("Copies", SqlDbType.Int).Value = copies;
        }

        /// <summary>
        /// Reset data for this grouping to the defaults
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            title = "";
            author = "";
            copies = 0;
            id = null;
        }
    }
}
