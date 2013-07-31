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
using System.Collections;
using System.Text;

namespace bookTest.Code
{
    public class BookList : DataGroup
    {
        /// <summary>
        /// Database procedure to load the data from the database
        /// </summary>
        protected override string LoadProc { get { return "web_GetBooks"; } }

        /// <summary>
        /// Text description of what this data is
        /// </summary>
        protected override string Description { get { return "list of books"; } }

        /// <summary>
        /// The state of the list of books
        /// </summary>
        public enum States
        {
            All = 0, // All books
            Unordered = 1, // All unordered books
            Ordered = 2 // All ordered books
        }

        /// <summary>
        /// The state of the list of books
        /// </summary>
        private States state = States.All;

        /// <summary>
        /// A list of available books
        /// </summary>
        ArrayList books = new ArrayList();

        /// <summary>
        ///  A list of available books
        /// </summary>
        public ArrayList Books
        {
            get { return books; }
            set { books = value; }
        }

        /// <summary>
        /// Create a list of available books
        /// </summary>
        public BookList()
        {
            Load();
        }

        /// <summary>
        /// Create a list of available books
        /// </summary>
        public BookList(States state)
        {
            this.state = state;
            Load();
        }

        /// <summary>
        /// Import the result of a load command into this data structure
        /// </summary>
        /// <param name="r">Data reader containing the data from the database</param>
        public override void Import(SqlDataReader r)
        {         
            base.Import(r);

            // Load the data
            while (r.Read())
            {
                Book b = new Book(
                     Convert.ToInt32(r["Id"]),
                    r["Title"].ToString(), 
                    r["Author"].ToString(), 
                    Convert.ToInt32(r["Copies"]));
                books.Add(b);
            }
        }

        /// <summary>
        /// Add parameters to SQL Load query
        /// </summary>
        public override void BindLoadParams(SqlCommand cmd)
        {
            base.BindLoadParams(cmd);
            cmd.Parameters.Add("@State", SqlDbType.Int).Value = Convert.ToInt32(state);
        }

        /// <summary>
        /// Reset data for this grouping to the defaults
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            state = States.All;
            books.Clear();
        }
    }
}
