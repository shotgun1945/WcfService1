using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.MongoDbService;

namespace WebApplication1.Component
{
    public partial class BasicGrid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BasicGrid_OnDataBinding(object sender, EventArgs e)
        {
            var client = new MongoDbServiceClient();
            
            
            client.SelectAsync("");
            client.Close();
        }
    }
}