using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
namespace bookTest.Code
{
    public class BookOrder : DataGroup
    {
        /// <summary>
        /// Database procedure to save an entry to the database
        /// </summary>
        /// <value></value>
        protected override string SaveProc { get { return "web_CreateOrder"; } }

        /// <summary>
        /// Text description of what this data is
        /// </summary>
        protected override string Description { get { return "book order"; } }

        /// <summary>
        /// A list of books being ordered
        /// </summary>
        private ArrayList books = new ArrayList();

        /// <summary>
        /// A list of books being ordered
        /// </summary>
        public ArrayList Books
        {
            get { return books; }
        }

        /// <summary>
        /// Returns a string of xml containing the book ids in the order
        /// </summary>
        public string BookIds
        {
            get
            {
                string xmlRootName = "Book";               
                StringBuilder xmlString = new StringBuilder();                

                xmlString.AppendFormat("<{0}>", xmlRootName);
                for (int i = 0; i < books.Count; i++)
                {
                    xmlString.AppendFormat("<id>{0}</id>", ((Book)(books[i])).Id);
                }
                xmlString.AppendFormat("</{0}>", xmlRootName);

                return xmlString.ToString();
            }
        }

        /// <summary>
        /// Create a new book order.
        /// </summary>        
        public BookOrder()
        {
            Reset();
        }

        /// <summary>
        /// Reset data for this grouping to the defaults
        /// </summary>
        public override void Reset()
        {
            books.Clear();           
        }

        /// <summary>
        /// Add parameters to the SQL query
        /// </summary>
        public override void BindSaveParams(SqlCommand cmd)
        {
            base.BindSaveParams(cmd);
            cmd.Parameters.Add("@BookIds", SqlDbType.Xml).Value = BookIds;
        }
    }
}
